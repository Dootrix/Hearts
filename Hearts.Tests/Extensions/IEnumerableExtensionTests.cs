using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearts.Extensions;

namespace Hearts.Tests.Extensions
{
    [TestFixture]
    public class IEnumerableExtensionTests
    {
        [TestFixture]
        public class PermuteTests
        {
            [Test]
            public void WhenOneItemExpectOnePermutation()
            {
                Assert.AreEqual(1, new[] { 7 }.Permute().Count());
            }

            [Test]
            public void WhenOneItemExpectOnlyPermutationIsTheItem()
            {
                Assert.AreEqual(7, new[] { 7 }.Permute().Single().Single());
            }

            [Test]
            public void WhenThreeItemsExpectSixPermutations()
            {
                Assert.AreEqual(6, new[] { 7, 5, 4 }.Permute().Count());                
            }

            [Test]
            public void WhenThreeItemsExpectAllPermutationsMatch()
            {
                var expectedCombinations = new[]
                {
                    new[] { 7, 5, 4 },
                    new[] { 7, 4, 5 },
                    new[] { 5, 7, 4 },
                    new[] { 5, 4, 7 },
                    new[] { 4, 7, 5 },
                    new[] { 4, 5, 7 },
                };

                int i = 0;
                int j = 0;
                int totalElements = 0;

                foreach (var combination in new[] { 7, 5, 4 }.Permute())
                {
                    j = 0;

                    foreach (var item in combination.ToArray())
                    {
                        Assert.AreEqual(expectedCombinations[i][j], item);
                        totalElements++;
                        j++;
                    }

                    i++;
                }

                Assert.AreEqual(18, totalElements);
            }
        }
    }
}
