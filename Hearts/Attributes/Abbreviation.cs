using System;
using Hearts.Extensions;

namespace Hearts.Attributes
{
    public static class Abbreviation
    {
        public static string Get<T>(this T value)
        {
            var type = typeof(T);
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AbbreviationAttribute),
                false);

            return ((AbbreviationAttribute)attributes[0]).Value;
        }

        public static T GetAbbreviatedValue<T>(this char abbreviation, T defaultValue)
            where T : struct, IConvertible
        {
            return abbreviation.ToString().GetAbbreviatedValue<T>(defaultValue);
        }

        public static T GetAbbreviatedValue<T>(this string abbreviation, T defaultValue)
            where T : struct, IConvertible
        {
            var returnValue = defaultValue;

            string[] names = Enum.GetNames(typeof(T));

            foreach (var name in names)
            {
                var value = name.ToEnum<T>(defaultValue);
                if (Get(value) == abbreviation)
                {
                    returnValue = value;
                }
            }

            return returnValue;
        }
    }
}
