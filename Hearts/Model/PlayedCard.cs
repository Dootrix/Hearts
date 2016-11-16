namespace Hearts.Model
{
    public class PlayedCard
    {
        public PlayedCard(Player player, Card card)
        {
            this.Player = player;
            this.Card = card;
        }

        public Player Player { get; private set; }

        public Card Card { get; private set; }
    }
}