using Hearts.Deal;
using Hearts.Factories;
using Hearts.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearts
{
    public class Game
    {
        public GameTable GameTable;
        public Dealer Dealer;
        public Player[] Players;

        public Game()
        {
            this.Init();
        }

        private void Init()
        {
            this.GameTable = new GameTable();
            this.Dealer = new Dealer(new StandardDeckFactory(), new EvenHandDealAlgorithm());
            this.Players = new List<Player> { new Player(), new Player(), new Player(), new Player() }.ToArray();
            this.Dealer.DealStartingHands(this.Players);
        }
    }
}