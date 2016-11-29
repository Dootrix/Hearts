using System;
using System.Collections.Generic;
using System.Linq;
using Hearts.Collections;
using NUnit.Framework;

namespace Hearts.Tests.Collections
{
    [TestFixture]
    public class SelectiveListTests
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void ListPassedToPredicateIsSameListDifferentInstance()
            {
                ICollection<int> predicateList = null;
                Func<ICollection<int>, int, bool> truePredicate = (list, item) =>
                {
                    predicateList = list;
                    return true;
                };

                var selectiveList = new SelectiveList<int>(truePredicate);
                selectiveList.Add(5);
                selectiveList.Add(6);

                Assert.False(selectiveList == predicateList);
                CollectionAssert.AreEqual(selectiveList, predicateList);
            }
        }

        [TestFixture]
        public class Add
        {
            [Test]
            public void FalsePredicateNeverAdds()
            {
                Func<ICollection<int>, int, bool> falsePredicate = (list, item) => false;

                var selectiveList = new SelectiveList<int>(falsePredicate);
                selectiveList.Add(5);

                Assert.IsEmpty(selectiveList);
            }

            [Test]
            public void TruePredicateAdds()
            {
                Func<ICollection<int>, int, bool> truePredicate = (list, item) => true;

                var selectiveList = new SelectiveList<int>(truePredicate);
                selectiveList.Add(5);

                Assert.AreEqual(5, selectiveList.FirstOrDefault());
                Assert.AreEqual(1, selectiveList.Count);
            }
        }
    }
}
