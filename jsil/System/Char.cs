
using JSIL.Meta;
namespace System
{
    public struct Char
    {
        [JSReplacement(@"(""0123456789"".indexOf($source[$index]) !== -1)")]
        public extern static bool IsDigit(string source, int index);

        [JSReplacement(@"("" \n\r\t"".indexOf($source[$index]) !== -1)")]
        public extern static bool IsWhiteSpace(string source, int index);

        [JSReplacement(@"(""0123456789"".indexOf($c) !== -1)")]
        public extern static bool IsDigit(char c);

        [JSReplacement(@"(""abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ"".indexOf($p) !== -1)")]
        public extern static bool IsLetter(char p);
        [JSReplacement(@"("" \n\r\t"".indexOf($p) !== -1)")]
        public static extern bool IsWhiteSpace(char p);

        internal static char ToLowerInvariant(char p)
        {
            throw new NotImplementedException();
        }

        internal static bool IsLower(char hexDigit)
        {
            return 'a' <= hexDigit && hexDigit <= 'z';
        }

        internal static char ToUpperInvariant(char c)
        {
            if ('a' <= c && c <= 'z')
                return (char) (c + 'a' - 'A');

            return c;
        }
    }
}
