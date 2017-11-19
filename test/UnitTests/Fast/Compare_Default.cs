﻿using System.Collections.Generic;
using System.Linq;
using Nito.Comparers;
using Xunit;
using System;
using Nito.Comparers.Fast;

namespace UnitTests.Fast
{
    public class Compare_DefaultUnitTests
    {
        [Fact]
        public void IsEquivalentToComparerDefault()
        {
            var list1 = new[] { 3, 5, 4, 2, 6 }.ToList();
            var list2 = new[] { 3, 5, 4, 2, 6 }.ToList();
            list1.Sort();
            list2.Sort(FastComparerBuilder.For<int>().Default());
            Assert.Equal(list1, list2);
        }

        [Fact]
        public void UsesSequenceComparerForSequences()
        {
            var three = new[] { 3 };
            var four = new[] { 4 };
            var five = new[] { 5 };
            var list1 = new[] { three, five, four }.ToList();
            var list2 = new[] { three, five, four }.ToList();
            var comparer1 = FastComparerBuilder.For<int>().Default().MakeSequence();
            var comparer2 = FastComparerBuilder.For<int[]>().Default();
            list1.Sort(comparer1);
            list2.Sort(comparer2);
            Assert.Equal(list1, list2);
        }

        [Fact]
        public void NullIsLessThanValue()
        {
            var list = new int?[] { 3, null, 4, 2, 6 }.ToList();
            list.Sort(FastComparerBuilder.For<int?>().Default());
            Assert.Equal(list, new int?[] { null, 2, 3, 4, 6 });
        }

        [Fact]
        public void NullSequenceIsLessThanValuesAndEmptySequence()
        {
            var none = new int[0];
            var five = new[] { 5 };
            var list = new[] { five, none, null }.ToList();
            list.Sort(FastComparerBuilder.For<int[]>().Default());
            Assert.Equal(list, new[] { null, none, five });
        }

        [Fact]
        public void NullIsEqualToNull()
        {
            var comparer = FastComparerBuilder.For<int?>().Default();
            Assert.True(comparer.Compare(null, null) == 0);
            Assert.True(comparer.Equals(null, null));
            Assert.Equal(comparer.GetHashCode((object)null), comparer.GetHashCode((object)null));
            Assert.Equal(comparer.GetHashCode((int?)null), comparer.GetHashCode((int?)null));
        }

        [Fact]
        public void NullSequenceIsEqualToNullSequence()
        {
            var comparer = FastComparerBuilder.For<int[]>().Default();
            Assert.True(comparer.Compare(null, null) == 0);
            Assert.True(comparer.Equals(null, null));
            Assert.Equal(comparer.GetHashCode((object)null), comparer.GetHashCode((object)null));
            Assert.Equal(comparer.GetHashCode((int[])null), comparer.GetHashCode((int[])null));
        }

        [Fact]
        public void ImplementsGetHashCode()
        {
            var comparer = FastComparerBuilder.For<int?>().Default();
            var bclDefault = EqualityComparer<int?>.Default;
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode(13));
            Assert.Equal(bclDefault.GetHashCode(7) == bclDefault.GetHashCode(13), comparer.GetHashCode(7) == comparer.GetHashCode(13));
        }

        [Fact]
        public void ImplementsGetHashCode_NonGeneric()
        {
            var comparer = FastComparerBuilder.For<int?>().Default() as System.Collections.IEqualityComparer;
            var bclDefault = EqualityComparer<int?>.Default;
            Assert.Equal(comparer.GetHashCode(13), comparer.GetHashCode(13));
            Assert.Equal(bclDefault.GetHashCode(7) == bclDefault.GetHashCode(13), comparer.GetHashCode(7) == comparer.GetHashCode(13));
        }

        [Fact]
        public void ImplementsGetHashCodeForNull()
        {
            var comparer = FastComparerBuilder.For<int?>().Default();
            Assert.Equal(comparer.GetHashCode((object)null), comparer.GetHashCode((object)null));
            Assert.Equal(comparer.GetHashCode((int?)null), comparer.GetHashCode((int?)null));
        }

        [Fact]
        public void UsesSequenceComparerForEnumerables()
        {
            var three = new[] { 3 };
            var four = new[] { 4 };
            var five = new[] { 5 };
            var list1 = new[] { three, five, four }.ToList();
            var list2 = new[] { three, five, four }.ToList();
            var comparer1 = FastComparerBuilder.For<int>().Default().MakeSequence();
            var comparer2 = FastComparerBuilder.For<IEnumerable<int>>().Default();
            list1.Sort(comparer1);
            list2.Sort(comparer2);
            Assert.Equal(list1, list2);
        }

        [Fact]
        public void DefaultForString_IsDefaultComparer()
        {
            // Ensure string default comparer is not a sequence comparer over chars.
            Assert.NotEqual(FastComparerBuilder.For<char>().Default().MakeSequence().ToString(), FastComparerBuilder.For<string>().Default().ToString());
        }

        [Fact]
        public void ToString_DumpsComparer()
        {
            Assert.Equal("Default(Int32: IComparable<T>)", FastComparerBuilder.For<int>().Default().ToString());
        }

        [Fact]
        public void ToString_DumpsComparer_NonGeneric()
        {
            Assert.Equal("Default(NonGenericComparable: IComparable)", FastComparerBuilder.For<NonGenericComparable>().Default().ToString());
        }

        private sealed class NonGenericComparable : IComparable
        {
            public int CompareTo(object obj)
            {
                throw new Exception();
            }
        }
    }
}
