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
