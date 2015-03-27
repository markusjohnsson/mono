//
// System.Buffer.cs
//
// Authors:
//   Paolo Molaro (lupus@ximian.com)
//   Dan Lewis (dihlewis@yahoo.co.uk)
//
// (C) 2001 Ximian, Inc.  http://www.ximian.com
//

//
// Copyright (C) 2004 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using Braille.Runtime.TranslatorServices;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System {
	[ComVisible (true)]
	public static class Buffer {

		public static int ByteLength (Array array)
		{
			// note: the other methods in this class also use ByteLength to test for
			// null and non-primitive arguments as a side-effect.

			if (array == null)
				throw new ArgumentNullException ("array");

			int length = ByteLengthInternal (array);
			if (length < 0)
				throw new ArgumentException (Locale.GetText ("Object must be an array of primitives."));

#if BRAILLE
            BrailleCheckType(array);
#endif

			return length;
		}

		public static byte GetByte (Array array, int index)
        {
#if BRAILLE
            BrailleCheckType(array);
#endif
			if (index < 0 || index >= ByteLength (array))
				throw new ArgumentOutOfRangeException ("index", Locale.GetText(
					"Value must be non-negative and less than the size of the collection."));

			return GetByteInternal (array, index);
		}

		public static void SetByte (Array array, int index, byte value)
		{
#if BRAILLE
            BrailleCheckType(array);
#endif
			if (index < 0 || index >= ByteLength (array))
				throw new ArgumentOutOfRangeException ("index", Locale.GetText(
					"Value must be non-negative and less than the size of the collection."));

			SetByteInternal (array, index, value);
		}

		public static void BlockCopy (Array src, int srcOffset, Array dst, int dstOffset, int count)
		{
			if (src == null)
				throw new ArgumentNullException ("src");

			if (dst == null)
				throw new ArgumentNullException ("dst");

#if BRAILLE
            BrailleCheckType(src);
            BrailleCheckType(dst);
#endif

			if (srcOffset < 0)
				throw new ArgumentOutOfRangeException ("srcOffset", Locale.GetText(
					"Non-negative number required."));

			if (dstOffset < 0)
				throw new ArgumentOutOfRangeException ("dstOffset", Locale.GetText (
					"Non-negative number required."));

			if (count < 0)
				throw new ArgumentOutOfRangeException ("count", Locale.GetText (
					"Non-negative number required."));


			// We do the checks in unmanaged code for performance reasons
			bool res = BlockCopyInternal (src, srcOffset, dst, dstOffset, count);
			if (!res) {
				// watch for integer overflow
				if ((srcOffset > ByteLength (src) - count) || (dstOffset > ByteLength (dst) - count))
					throw new ArgumentException (Locale.GetText (
						"Offset and length were out of bounds for the array or count is greater than " + 
						"the number of elements from index to the end of the source collection."));
			}
		}

#if BRAILLE
        
        private static void BrailleCheckType(Array array)
        {
            if (array == null)
                throw new ArgumentNullException();

            if (array is byte[]) return;
            if (array is int[]) return;
            if (array is uint[]) return;
            if (array is sbyte[]) return;
            if (array is char[]) return;
            if (array is short[]) return;
            if (array is ushort[]) return;
            
            throw new ArgumentException();
        }

        [JsImport(
            @"function(array) { 
                return array.jsarr.buffer.byteLength; 
            }")]
        private extern static int ByteLengthInternal(Array array);

        [JsImport("function(array, index){ return (new Uint8Array(array.jsarr.buffer))[index]; }")]
        private extern static byte GetByteInternal(Array array, int index);

        [JsImport("function(array, index, value){ return (new Uint8Array(array.jsarr.buffer))[index] = value; }")]
        private extern static void SetByteInternal(Array array, int index, int value);

        [JsImport(
            @"function(src, src_offset, dest, dest_offset, count) {
                var srcArr = src instanceof Uint8Array ? src : new Uint8Array(src.jsarr.buffer);
                var destArr = dest instanceof Uint8Array ? dest : new Uint8Array(dest.jsarr.buffer);

                if (count > srcArr.length - src_offset || count > destArr.length - dest_offset)
                    throw new (asm1['System.ArgumentException']())();

                for (var i=src_offset, j=dest_offset, c=0; c < count; c++, i++, j++) {
                    destArr[j] = srcArr[i];
                } 
            }")]
        internal extern static bool BlockCopyInternal(Array src, int src_offset, Array dest, int dest_offset, int count);

#else
		// private
		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		private extern static int ByteLengthInternal (Array array);

		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		private extern static byte GetByteInternal (Array array, int index);

		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		private extern static void SetByteInternal (Array array, int index, int value);

		[MethodImplAttribute (MethodImplOptions.InternalCall)]
		internal extern static bool BlockCopyInternal (Array src, int src_offset, Array dest, int dest_offset, int count);
#endif
	}
}
