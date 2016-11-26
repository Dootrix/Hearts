using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearts.Randomisation
{
    public interface IControlledRandom
    {
        int Next(int minValue, int maxValue);

        void ResetSeedToValue(int value);

        void ResetSeedToTime();

        void ResetRandomWithCurrentSeed();

        int GetSeed();
    }
}
