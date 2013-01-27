//
//
//

using System;
using System.Text;
using System.Collections;

namespace Mono.Tools.LocaleBuilder {

        public class Entry {

		// maps strings to indexes
		static Hashtable hash;
		static ArrayList string_order;
		// idx 0 is reserved to indicate null
		static int curpos = 1;

		// serialize the strings in Hashtable.
		public static string GetStrings () {
			Console.WriteLine ("Total string data size: {0}", curpos);
			if (curpos > UInt16.MaxValue)
				throw new Exception ("need to increase idx size in culture-info.h");
			StringBuilder ret = new StringBuilder ();
			// the null entry
#if !JSIL
			ret.Append ("\"\\0\"\n");
#else
            ret.Append("\"\",\n");
#endif
			foreach (string s in string_order) {
				ret.Append ("\t\"");
				ret.Append (s);
#if !JSIL
				ret.Append ("\\0\"\n");
#else
                ret.Append("\",\n");
#endif
			}

			return ret.ToString ();
		}
#if JSIL
        public static string GetStringLengths() {
            if (curpos > UInt16.MaxValue)
                throw new Exception("need to increase idx size in culture-info.h");
            StringBuilder ret = new StringBuilder();
            // the null entry
            ret.Append("\t0,\n");
            foreach (string s in string_order)
            {
                ret.Append("\t");
                ret.Append(s.Length);
                ret.Append(",\n");
            }
            return ret.ToString();
        }
#endif
		static Entry () {
			hash = new Hashtable ();
			string_order = new ArrayList ();
		}
		static int AddString (string s, int size) {
			object o = hash [s];
			if (o == null) {
				int ret;
				string_order.Add (s);
#if !JSIL
				ret = curpos;
#else
                ret = string_order.Count;
#endif
				hash [s] = ret;
				curpos += size + 1; // null terminator
				return ret;
			} else {
				return (int)o;
			}
		}

                internal static String EncodeStringIdx (string str)
                {
                        if (str == null)
                                return "0";

                        StringBuilder ret = new StringBuilder ();
                        byte [] ba = new UTF8Encoding ().GetBytes (str);
                        bool in_hex = false;
                        foreach (byte b in ba) {
                                if (b > 127 || (in_hex && is_hex (b))) {
#if !JSIL
                                        ret.AppendFormat ("\\x{0:x}", b);
#else
                                        ret.AppendFormat("\\u{0:x4}", b);
#endif
                                        in_hex = true;
                                } else {
                                        if (b == '\\')
                                                ret.Append ('\\');
                                        ret.Append ((char) b);
                                        in_hex = false;
                                }
                        }
			int res = AddString (ret.ToString (), ba.Length);
                        return res.ToString ();
                }

                private static bool is_hex (int e)
		{
			return (e >= '0' && e <= '9') || (e >= 'A' && e <= 'F') || (e >= 'a' && e <= 'f');
		}
        }
}




