namespace Hearts.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string self, int? defaultValue = null)
        {
            int result = default(int);
            if (int.TryParse(self, out result))
            {
                return result;
            }

            return defaultValue;
        }
    }
}
