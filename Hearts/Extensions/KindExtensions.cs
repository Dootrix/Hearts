using System.Reflection;

namespace Hearts
{
    public static class KindExtensions
    {
        public static string ToAbbreviation(this Kind rank)
        {
            var type = typeof(Kind);
            var memInfo = type.GetMember(rank.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AbbreviationAttribute),
                false);

            var x = attributes[0];


            return ((AbbreviationAttribute)attributes[0]).Value;
        }
    }
}