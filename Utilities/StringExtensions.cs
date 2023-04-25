namespace AppMVC.Utilities
{
    public static class StringExtensions
    {
        public static string ToLowerTrim(this string input)
        {
            return input.ToLower().Trim();
        }
    }
}
