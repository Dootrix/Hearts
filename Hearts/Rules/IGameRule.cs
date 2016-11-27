using Hearts.Model;
using System.Collections.Generic;

namespace Hearts.Rules
{
    public interface IGameRule
    {
        IEnumerable<Card> FilterCards(IEnumerable<Card> cards, Round round);

        bool Applies(Round round);
    }
}
