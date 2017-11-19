using System.Collections.Generic;
using System.Linq;
using Nito.Comparers;
using Xunit;
using System;
using Nito.Comparers.Fast;

#pragma warning disable CS0162

namespace UnitTests.Fast
{
    public class Compare_BuilderUnitTests
    {
        [Fact]
        public void ForT_ReturnsBuilderForT()
        {
            var result = FastComparerBuilder.For<int>();
            Assert.IsType<FastComparerBuilder<int>>(result);
        }

        [Fact]
        public void ForFuncT_ReturnsBuilderForT_WithoutInvokingFunc()
        {
            var result = FastComparerBuilder.For(() =>
            {
                throw new Exception();
                return 3;
            });
            Assert.IsType<FastComparerBuilder<int>>(result);
        }

        [Fact]
        public void ForElementsOfT_ReturnsBuilderForT_WithoutEnumeratingSequence()
        {
            var result = FastComparerBuilder.ForElementsOf(ThrowEnumerable());
            Assert.IsType<FastComparerBuilder<int>>(result);
        }

        [Fact]
        public void ForElementsOfFuncT_ReturnsBuilderForT_WithoutInvokingFunc()
        {
            var result = FastComparerBuilder.ForElementsOf(() =>
            {
                throw new Exception();
                return ThrowEnumerable();
            });
            Assert.IsType<FastComparerBuilder<int>>(result);
        }

        private static IEnumerable<int> ThrowEnumerable()
        {
            throw new Exception();
            yield return 13;
        }
    }
}
