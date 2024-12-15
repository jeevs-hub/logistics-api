namespace logistics_api.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            if (source == null || toCheck == null)
            {
                return false;
            }

            return source.Contains(toCheck, StringComparison.OrdinalIgnoreCase);
        }
    }
}
