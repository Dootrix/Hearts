
using Hearts.Attributes;
using Hearts.Model;

namespace Hearts.Extensions
{
    public static class SuitExtensions
    {
        public static string ToAbbreviation(this Suit suit)
        {
            return Abbreviation.Get(suit);
        }
    }
}