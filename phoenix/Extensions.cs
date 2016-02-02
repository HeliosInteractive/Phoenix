namespace phoenix
{
    using System.Linq;
    using System.Globalization;

    static class Extensions
    {
        public static string ToUnderScore(this string input)
        {
            return string
                .Concat(
                input.Select((x, i) => i > 0 && char.IsUpper(x) ?
                    "_" + x.ToString()
                    : x.ToString()))
                .ToLower();
        }
        public static string ToCamelCase(this string input)
        {
            return CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(input)
                .Replace("_", string.Empty);
        }
    }
}
