
using JSIL;
using JSIL.Meta;
namespace System
{
    public struct Double
    {
        public const double Epsilon = 4.9406564584124650e-324;
        public const double MaxValue = 1.7976931348623157e308;
        public const double MinValue = -1.7976931348623157e308;
        public const double NaN = 0.0d / 0.0d;
        public const double NegativeInfinity = -1.0d / 0.0d;
        public const double PositiveInfinity = 1.0d / 0.0d;
        
        [JSReplacement("$value === Number.POSITIVE_INFINITY")]
        public extern static bool IsPositiveInfinity(double value);

        [JSReplacement("$value == Number.NEGATIVE_INFINITY")]
        public extern static bool IsNegativeInfinity(double value);

        [JSReplacement("isNaN($value)")]
        public extern static bool IsNaN(double value);

        public static double Parse(string shortRep, Globalization.NumberFormatInfo nfi)
        {
            throw new NotImplementedException();
        }
    }
}
