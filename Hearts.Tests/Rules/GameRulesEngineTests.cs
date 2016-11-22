using Hearts.Extensions;
using Hearts.Model;
using Hearts.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Tests.Rules
{
    [TestFixture]
    public class GameRulesEngineTests
    {
        [Test]
        public void ExpectOnlyTwoOfClubsPlayableInitially()
        {
            var engine = new GameRulesEngine();

            var round = new Round(numberOfPlayers: 4, roundNumber: 1);

            var playableCards = engine.GetPlayableCards(Cards.Deck, round).ToDebugString();

            Assert.AreEqual("2♣", playableCards);
        }
    }
}
