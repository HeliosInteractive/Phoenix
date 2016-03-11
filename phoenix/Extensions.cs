namespace phoenix
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;

    /// <summary>
    /// Some helper methods and extensions used in Phoenix
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// CamelCase to under_score string conversion
        /// </summary>
        /// <param name="input">CamelCase string</param>
        /// <returns>under_score string</returns>
        public static string ToUnderScore(this string input)
        {
            return string
                .Concat(
                input.Select((x, i) => i > 0 && char.IsUpper(x) ?
                    "_" + x.ToString()
                    : x.ToString()))
                .ToLower();
        }

        /// <summary>
        /// under_score to CamelCase string conversion
        /// </summary>
        /// <param name="input">under_score string</param>
        /// <returns>CamelCase string</returns>
        public static string ToCamelCase(this string input)
        {
            return CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(input)
                .Replace("_", string.Empty);
        }

        /// <summary>
        /// Enum to be used with AsPath, in order to distinguish between
        /// a directory string or a file name string
        /// </summary>
        public enum PathType
        {
            FilePath, /// <summary>Directory + file name</summary>
            FileName, /// <summary>only file name</summary>
        }

        /// <summary>
        /// Cleans a string as if it was a path
        /// </summary>
        /// <param name="input">dirty path string</param>
        /// <returns>clean path string</returns>
        public static string AsPath(this string input, PathType type)
        {
            if (type == PathType.FilePath)
                foreach (var c in Path.GetInvalidPathChars())
                    input = input.Replace(c.ToString(), string.Empty);

            if (type == PathType.FileName)
                foreach (var c in Path.GetInvalidFileNameChars())
                    input = input.Replace(c.ToString(), string.Empty);

            return input.Trim();
        }

        /// <summary>
        /// Turns a Windows absolute path to Cygwin path.
        /// No-op if path is already a Cygwin path
        /// </summary>
        /// <param name="path">Windows Path</param>
        /// <returns>Converted Cygwin path</returns>
        public static string AsCygwinPath(this string path)
        {
            path = path.AsPath(PathType.FilePath);

            if (String.IsNullOrWhiteSpace(path) || path.StartsWith("/cygdrive/"))
                return path;

            if (Path.IsPathRooted(path))
            {
                path = path.Replace("\\", "/");
                path = path.Replace(":", string.Empty);
                path = string.Format("/cygdrive/{0}", path);
            }

            return path.Trim();
        }

        /// <summary>
        /// A basic type cast checking. Checks to see if value is safely
        /// cast-able to conversionType
        /// </summary>
        /// <param name="conversionType">Target type</param>
        /// <param name="value">source object</param>
        /// <returns></returns>
        public static bool CanBeCastedFrom(this Type conversionType, object value)
        {
            if (conversionType == null || value == null || (value as IConvertible) == null)
                return false;

            return true;
        }
    }
}
