using Braille.Runtime.TranslatorServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Braille
{
    internal static class BrailleHelper
    {
        [JsAssemblyStatic]
        public static IEqualityComparer<T> GetDefaultEqualityComparer<T>()
        {
            return EqualityComparer<T>.Default;
        }

        [JsAssemblyStatic]
        public static IComparer<T> GetDefaultComparer<T>()
        {
            return Comparer<T>.Default;
        }
    }
}
