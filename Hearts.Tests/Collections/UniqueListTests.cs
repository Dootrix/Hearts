using Hearts.Collections;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Collections
{
    [TestFixture]
    public class UniqueListTests
    {
        [TestFixture]
        public class Add
        {
            [Test]
            public void ThreeUniqueItemsReturnsCountThree()
            {
                var uniqueList = new UniqueList<Card>();
                uniqueList.Add(Cards.AceOfClubs);
                uniqueList.Add(Cards.KingOfClubs);
                uniqueList.Add(Cards.JackOfClubs);

                Assert.AreEqual(3, uniqueList.Count);
            }

            [Test]
            public void DuplicatePassedReturnsCountOne()
            {
                var uniqueList = new UniqueList<Card>();
                uniqueList.Add(Cards.AceOfClubs);
                uniqueList.Add(Cards.AceOfClubs);

                Assert.AreEqual(1, uniqueList.Count);
            }
        }
    }
}
