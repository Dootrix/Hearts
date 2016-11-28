using System;

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

        public static T ToEnum<T>(this string input, T defaultValue)
            where T : struct, IConvertible
        {
            if (input == null)
            {
                return defaultValue;
            }

            T result;
            if (Enum.TryParse(input, true, out result))
            {
                return result;
            }

            return defaultValue;
        }
    }
}
