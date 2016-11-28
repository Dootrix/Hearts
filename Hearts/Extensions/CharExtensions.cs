using System;

namespace Hearts.Extensions
{
    public static class CharExtensions
    {
        public static int? ToNullableInt(this char self, int? defaultValue = null)
        {
            int result = default(int);
            if (int.TryParse(self.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }

        public static T ToEnum<T>(this char input, T defaultValue)
            where T : struct, IConvertible
        {
            T result;
            if (Enum.TryParse(input.ToString(), true, out result))
            {
                return result;
            }

            return defaultValue;
        }
    }
}
