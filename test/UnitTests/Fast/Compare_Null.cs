using Nito.Comparers;
using Nito.Comparers.Fast;
using Xunit;

namespace UnitTests.Fast
{
    public class Compare_NullUnitTests
    {
        [Fact]
        public void ComparesUnequalElementsAsEqual()
        {
            var comparer = FastComparerBuilder.For<int>().Null();
            Assert.Equal(0, comparer.Compare(13, 17));
            Assert.True(comparer.Equals(19, 21));
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode(17));
        }

        [Fact]
        public void ComparesNullElementsAsEqualToValueElements()
        {
            var comparer = FastComparerBuilder.For<int?>().Null();
            Assert.Equal(0, comparer.Compare(null, 17));
            Assert.Equal(0, comparer.Compare(13, null));
            Assert.True(comparer.Equals(null, 21));
            Assert.True(comparer.Equals(19, null));
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode((object)null));
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode((int?)null));
        }

        [Fact]
        public void ComparesEqualElementsAsEqual()
        {
            var comparer = FastComparerBuilder.For<int>().Null();
            Assert.Equal(0, comparer.Compare(13, 13));
            Assert.True(comparer.Equals(13, 13));
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode(13));
        }

        [Fact]
        public void ComparesNullElementsAsEqual()
        {
            var comparer = FastComparerBuilder.For<int?>().Null();
            Assert.Equal(0, comparer.Compare(null, null));
            Assert.True(comparer.Equals(null, null));
            Assert.Equal(comparer.GetHashCode((object)null), comparer.GetHashCode((object)null));
            Assert.Equal(comparer.GetHashCode((int?)null), comparer.GetHashCode((int?)null));
        }

        [Fact]
        public void ToString_DumpsComparer()
        {
            Assert.Equal("Null", ComparerBuilder.For<int>().Null().ToString());
        }
    }
}
