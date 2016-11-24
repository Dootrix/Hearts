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
    }
}
