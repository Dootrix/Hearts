using Hearts.Attributes;
using Hearts.Model;

namespace Hearts.Extensions
{
    public static class KindExtensions
    {
        public static string ToAbbreviation(this Kind rank)
        {
            return Abbreviation.Get(rank);
        }
    }
}