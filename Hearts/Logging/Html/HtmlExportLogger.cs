using Hearts.Attributes;
using Hearts.Extensions;
using Hearts.Model;
using Hearts.Scoring;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hearts.Logging
{
    public class HtmlExportLogger : ILogger
    {
        public ILogDisplayOptions Options = new DefaultLogOptions();
        private StringBuilder buffer = new StringBuilder();

        public void BeginLogging()
        {
            this.buffer.Clear();
        }

        public void StopLogging()
        {
            string result = this.buffer.ToString();
            this.buffer.Clear();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "Hearts", "Last Simulation.html");
            File.WriteAllText(path, result);
        }

        public void StartingHands(IEnumerable<CardHand> hands)
        {
            if (!Options.DisplayStartingHands) return;

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append(@"Starting Hands:");
            this.buffer.Append(this.CloseH2Tag());

            foreach (var hand in hands)
            {
                this.Hand(hand, hand.Owner.Name);
            }

            this.buffer.Append(this.NewLine());
        }

        public void TotalSimulationTime(long elapsedMilliseconds)
        {
            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(string.Format("Total simulation time: {0}s.", elapsedMilliseconds / 1000.0));
            this.buffer.Append(this.ClosePTag());
            this.buffer.Append(this.NewLine());
        }

        public void PassDirection(Pass pass)
        {
            if (!Options.DisplayPass) return;

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Pass direction: " + Abbreviation.Get(pass));
            this.buffer.Append(this.CloseH2Tag());
            this.buffer.Append(this.NewLine());
        }

        public void HandsAfterPass(IEnumerable<CardHand> hands)
        {
            if (!Options.DisplayHandsAfterPass) return;

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Post-Pass Hands:");
            this.buffer.Append(this.CloseH2Tag());
            this.buffer.Append(this.NewLine());

            foreach (var hand in hands)
            {
                this.Hand(hand, hand.Owner.Name);
            }

            this.buffer.Append(this.NewLine());
        }

        public void Pass(Player player, IEnumerable<Card> cards)
        {
            if (!Options.DisplayPass) return;

            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(this.OpenSpanBlue());
            this.buffer.Append(this.HtmlSpace() + player.Name.PadLeft(Options.NamePad) + this.HtmlSpace()); // TODO: Spacing won't work in HTML, use table?
            this.buffer.Append(this.CloseSpan());
            this.buffer.Append(" pass ");

            foreach (var card in cards)
            {
                this.Card(card);
                this.buffer.Append(this.HtmlSpace());
            }

            this.buffer.Append(this.ClosePTag());
            this.buffer.Append(this.NewLine());
        }

        public void TrickSummary(PlayedTrick trick)
        {
            if (!Options.DisplayTrickSummary) return;

            foreach (var playedCard in trick.Cards)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(this.HtmlSpace() + playedCard.Key.Name.PadLeft(Options.NamePad) + this.HtmlSpace()); // TODO: Spacing won't work in HTML, use table?
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" play ");
                this.Card(playedCard.Value);

                if (trick.Winner == playedCard.Key)
                {
                    this.buffer.Append(this.OpenSpanBlue());
                    this.buffer.Append(" Win");
                    int trickScore = trick.Cards.Select(i => i.Value).Score();

                    if (trickScore > 0)
                    {
                        this.buffer.Append(string.Format(" {0}pts", trickScore));
                    }

                    this.buffer.Append(this.CloseSpan());
                }

                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());
        }

        public void IllegalPass(Player player, IEnumerable<Card> cards)
        {
            if (!Options.DisplayExceptions) return;

            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(this.OpenSpanBlue());
            this.buffer.Append(player.Name);
            this.buffer.Append(" made an ILLEGAL PASS! (");
            this.buffer.Append(string.Join(", ", cards.Select(i => i.ToString())));
            this.buffer.Append(") - ");
            this.buffer.Append(player.DebuggerDisplay);
            this.buffer.Append(this.CloseSpan());
            this.buffer.Append(this.ClosePTag());
        }

        public void IllegalPlay(Player player, Card card)
        {
            if (!Options.DisplayExceptions) return;

            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(this.OpenSpanBlue());
            this.buffer.Append(player.Name);
            this.buffer.Append(" played an ILLEGAL CARD! (");
            this.buffer.Append(card);
            this.buffer.Append(") - ");
            this.buffer.Append(player.DebuggerDisplay);
            this.buffer.Append(this.CloseSpan());
            this.buffer.Append(this.ClosePTag());
        }

        public void OutOfCardsException()
        {
            if (!Options.DisplayExceptions) return;

            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(this.OpenSpanBlue());
            this.buffer.Append("*** OUT OF CARDS! ***");
            this.buffer.Append(this.CloseSpan());
            this.buffer.Append(this.ClosePTag());
        }

        public void PointsForRound(RoundResult roundResult)
        {
            if (!Options.DisplayPointsForRound) return;

            var scores = roundResult.Scores;

            foreach (var score in scores)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(this.HtmlSpace() + score.Key.Name.PadLeft(Options.NamePad) + this.HtmlSpace());
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" : ");
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(score.Value.ToString().PadLeft(3));
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" pts");
                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());
        }

        public void LogFinalWinner(GameResult gameResult)
        {
            if (!Options.DisplayLogFinalWinner) return;

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.HorizontalRule());
            this.buffer.Append(this.NewLine());

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Moonshots:");
            this.buffer.Append(this.CloseH2Tag());

            foreach (var player in gameResult.Moonshots)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1}", player.Key.Name, player.Value));
                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Rounds: W/L/P");
            this.buffer.Append(this.CloseH2Tag());

            foreach (var player in gameResult.RoundWins)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1}/{2}/{3}", player.Key.Name, player.Value, gameResult.RoundLosses[player.Key], gameResult.RoundsPlayed));
                this.buffer.Append(this.ClosePTag());
            }
            
            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Scores");
            this.buffer.Append(this.CloseH2Tag());

            foreach (var score in gameResult.Scores)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(this.HtmlSpace() + score.Key.Name.PadLeft(Options.NamePad) + this.HtmlSpace());
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" : ");
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(score.Value.ToString().PadLeft(3));
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" pts ");

                if (score.Value == gameResult.Scores.Min(i => i.Value))
                {
                    this.buffer.Append(this.OpenSpanBlue());
                    this.buffer.Append("Wins");
                    this.buffer.Append(this.CloseSpan());
                }

                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.HorizontalRule());
            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.NewLine());
        }
        
        public void LogSimulationSummary(SimulationResult result)
        {
            if (!Options.DisplaySimulationSummary || !result.GameResults.Any()) return;

            var players = result.GameResults.First().Scores.Select(i => i.Key);

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.HorizontalRule());
            this.buffer.Append(this.NewLine());

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Simulation Stats");
            this.buffer.Append(this.CloseH2Tag());

            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(string.Format("Based on {0} game{1}", result.GameResults.Count, result.GameResults.Count > 1 ? "s" : string.Empty));
            this.buffer.Append(this.ClosePTag());

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Execution Times: Pass/Play");
            this.buffer.Append(this.CloseH2Tag()); 

            foreach (var player in players)
            {
                int moonshots = result.GameResults.SelectMany(i => i.Moonshots).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var averagePassTiming = result.TimerService.GetAveragePassTiming(player);
                var averagePlayTiming = result.TimerService.GetAveragePlayTiming(player);

                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1:0}ms / {2:0}ms", player.Name, averagePassTiming, averagePlayTiming));
                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());
            
            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Moonshots:");
            this.buffer.Append(this.CloseH2Tag());
            
            foreach (var player in players)
            {
                int moonshots = result.GameResults.SelectMany(i => i.Moonshots).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var botPlayer = result.MoonshotAttempts.SingleOrDefault(i => i.Bot.Player == player);
                int attempts = botPlayer != null ? botPlayer.MoonshotAttempts : 0;

                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1} / {2}", player.Name, moonshots, attempts));
                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Rounds: W/L/P");
            this.buffer.Append(this.CloseH2Tag());
            
            foreach (var player in players)
            {
                var playerName = player.Name;
                var roundsWon = result.GameResults.SelectMany(i => i.RoundWins).Where(i => i.Key == player).Select(i => i.Value).Sum();
                var roundsLost = result.GameResults.SelectMany(i => i.RoundLosses).Where(i => i.Key == player).Select(i => i.Value).Sum();
                int roundsPlayed = result.GameResults.Select(i => i.RoundsPlayed).Sum();

                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1}/{2}/{3}", playerName, roundsWon, roundsLost, roundsPlayed));
                this.buffer.Append(this.ClosePTag());                
            }

            this.buffer.Append(this.NewLine());

            this.buffer.Append(this.OpenH2Tag());
            this.buffer.Append("Games: W/L/P");
            this.buffer.Append(this.CloseH2Tag());            

            var totalWon = new Dictionary<Player, int>();
            int gamesPlayed = result.GameResults.Count();

            foreach (var player in players)
            {
                var playerName = player.Name;
                int gamesWon = result.GameResults.SelectMany(i => i.Winners).Where(i => i == player).Count();
                int gameLost = result.GameResults.SelectMany(i => i.Losers).Where(i => i == player).Count();
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("{0} : {1}/{2}/{3}", playerName, gamesWon, gameLost, gamesPlayed));
                this.buffer.Append(this.ClosePTag());
                totalWon.Add(player, gamesWon);
            }

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.HorizontalRule());
            this.buffer.Append(this.NewLine());

            foreach (var player in players)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(this.HtmlSpace() + player.Name.PadLeft(Options.NamePad) + this.HtmlSpace());
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(" : ");
                this.buffer.Append(this.OpenSpanBlue());
                float percent = (float)totalWon[player] / gamesPlayed;
                this.buffer.Append(percent.ToString("0.00% ").PadLeft(8));
                

                if (totalWon[player] == totalWon.Max(i => i.Value))
                {
                    this.buffer.Append(" Best");
                }

                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(this.ClosePTag());
            }

            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.HorizontalRule());
            this.buffer.Append(this.NewLine());
            this.buffer.Append(this.NewLine());
        }

        public void LogAgentMoveNote(string note)
        {
            if (Options.DisplayAgentMoveNotes)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(note);
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(this.ClosePTag());
            }
        }
        public void LogAgentSummaryNote(string note)
        {
            if (Options.DisplayAgentSummaryNotes)
            {
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(this.OpenSpanBlue());
                this.buffer.Append(note);
                this.buffer.Append(this.CloseSpan());
                this.buffer.Append(this.ClosePTag());
            }
        }

        public void LogRandomSeed(int randomSeed)
        {
            if (Options.DisplayRandomSeed)
            {
                this.buffer.Append(this.NewLine());
                this.buffer.Append(this.OpenPTag());
                this.buffer.Append(string.Format("Simulation random seed: {0}", randomSeed));
                this.buffer.Append(this.ClosePTag());
                this.buffer.Append(this.NewLine());
            }
        }

        public void Card(Card card)
        {
            if (card.Suit == Suit.Clubs || card.Suit == Suit.Spades)
            {
                this.buffer.Append(this.OpenSpanBlack());
            }
            else
            {
                this.buffer.Append(this.OpenSpanRed());
            }

            this.buffer.Append(card);
            this.buffer.Append(this.CloseSpan());
        }

        private void Hand(IEnumerable<Card> cards, string name)
        {
            int padToLength = 38;
            this.buffer.Append(this.OpenPTag());
            this.buffer.Append(this.OpenSpanBlue());
            this.buffer.Append(this.HtmlSpace() + name.PadLeft(Logging.Log.Options.NamePad) + this.HtmlSpace());
            this.buffer.Append(this.CloseSpan());
            this.buffer.Append(this.ClosePTag());

            foreach (var suit in new List<Suit> { Suit.Hearts, Suit.Spades, Suit.Diamonds, Suit.Clubs })
            {
                var cardsOfSuit = cards.OfSuit(suit);

                foreach (var card in cardsOfSuit.Ascending())
                {
                    Card(card);
                    this.buffer.Append(this.HtmlSpace());
                }

                this.buffer.Append(new string(' ', padToLength - cardsOfSuit.Count() * 3));
            }

            NewLine();
        }

        #region Helpers
        private string OpenSpanBlue()
        {
            return @"<span class=""color:blue;"">";
        }

        private string OpenPTag()
        {
            return @"<p>";
        }

        private string OpenH2Tag()
        {
            return @"<h2>";
        }

        private string OpenSpanRed()
        {
            return @"<span class=""color:red;"">";
        }

        private string OpenSpanBlack()
        {
            return @"<span class=""color:black;"">";
        }

        private string ClosePTag()
        {
            return @"</p>";
        }

        private string CloseH2Tag()
        {
            return @"</h2>";
        }

        private string CloseSpan()
        {
            return @"</span>";
        }

        private string HtmlSpace()
        {
            return @"&nbsp;";
        }

        private string NewLine()
        {
            return @"<br>";
        }

        private string HorizontalRule()
        {
            return "<hr>";
        }
        #endregion /Helpers
    }
}
