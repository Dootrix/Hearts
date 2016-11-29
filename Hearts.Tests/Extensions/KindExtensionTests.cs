using Hearts.Extensions;
using Hearts.Model;
using NUnit.Framework;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class KindExtensionTests
    {
        [TestFixture]
        public class ToAbbreviation
        {
            [Test]
            public void TwoReturnsTwoDigit()
            {
                var abbr = Kind.Two.ToAbbreviation();

                Assert.AreEqual("2", abbr);
            }

            [Test]
            public void JackReturnsJCharacter()
            {
                var abbr = Kind.Jack.ToAbbreviation();

                Assert.AreEqual("J", abbr);
            }
        }

        [TestFixture]
        public class Increment
        {
            [Test]
            public void KingIncrementsToAce()
            {
                var result = Kind.King.Increment();
                Assert.AreEqual(Kind.Ace, result);
            }

            [Test]
            public void AceIncrementsToNull()
            {
                var result = Kind.Ace.Increment();
                Assert.AreEqual(null, result);
            }
        }

        [TestFixture]
        public class Decrement
        {
            [Test]
            public void KingDecrementsToQueen()
            {
                var result = Kind.King.Decrement();
                Assert.AreEqual(Kind.Queen, result);
            }

            [Test]
            public void TwoDecrementsToNull()
            {
                var result = Kind.Two.Decrement();
                Assert.AreEqual(null, result);
            }
        }
    }
}
