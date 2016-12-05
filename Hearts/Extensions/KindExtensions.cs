using Hearts.Attributes;
using Hearts.Model;

namespace Hearts.Extensions
{
    public static class KindExtensions
    {
        public static string ToAbbreviation(this Kind rank)
        {
            return Abbreviation.Get(rank);
        }

        public static Kind? Increment(this Kind rank)
        {
            if (rank == Kind.Ace)
            {
                return null;
            }

            return rank + 1;
        }

        public static Kind? Increment(this Kind rank, int amount)
        {
            Kind? result = rank;
            for (int i = 0; i < amount; i++)
            {
                if (!result.HasValue)
                {
                    return null;
                }

                result = result.Value.Increment();
            }

            return result;
        }

        public static Kind? Decrement(this Kind rank)
        {
            if (rank == Kind.Two)
            {
                return null;
            }

            return rank - 1;
        }

        public static Kind? Decrement(this Kind rank, int amount)
        {
            Kind? result = rank;
            for (int i = 0; i < amount; i++)
            {
                if (!result.HasValue)
                {
                    return null;
                }

                result = result.Value.Decrement();
            }

            return result;
        }
    }
}