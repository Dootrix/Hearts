using System.Collections.Generic;
using Hearts.Model;

namespace Hearts.AI
{
    public interface IRequiresNoPassNotification
    {
        void OnNoPass(GameState gameState);
    }
}