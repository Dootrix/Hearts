namespace Hearts
{
    public static class CardExtensions
    {
        public static string ToAbbreviation(this Card card)
        {
            return card.Kind.ToAbbreviation();
        }
    }
}