using System.Text.RegularExpressions;

namespace Workshop.Helpers
{
    public static class StringExtensions
    {
        public static string ToUrlFriendly(this string source)
        {
            var result = source;

            result = Regex.Replace(result, @"[\uFE30-\uFFA0\x20-\x2f\x3a-\x40\x5b-\x60\x7b-\x7e¡@]", "-");
            result = Regex.Replace(result, @"-{2,}", "-");
            result = Regex.Replace(result, @"^-*|-*$", "");

            return result;
        }
    }
}