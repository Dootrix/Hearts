using Hearts.Model;

namespace Hearts.Extensions
{
    public static class CardExtensions
    {
        public static string ToAbbreviation(this Card card)
        {
            return card.Kind.ToAbbreviation();
        }
    }
}