
namespace System
{
    public struct Int64
    {
        public const long MaxValue = 0x7fffffffffffffff;
        public const long MinValue = -9223372036854775808;

        public string ToString(string p)
        {
            return string.Format(string.Format("{{0:{0}}}", p), this);
        }
    }
}
