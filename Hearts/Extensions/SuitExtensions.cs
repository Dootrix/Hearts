
using Hearts.Model;

namespace Hearts.Extensions
{
    public static class SuitExtensions
    {
        public static string ToAbbreviation(this Suit suit)
        {
            var type = typeof(Suit);
            var memInfo = type.GetMember(suit.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(AbbreviationAttribute), false);

            return ((AbbreviationAttribute)attributes[0]).Value;
        }
    }
}