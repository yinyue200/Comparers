﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nito.Comparers;
using Xunit;
using Nito.Comparers.Fast;

namespace UnitTests.Fast
{
    public class Compare_ThenByUnitTests
    {
        private sealed class Person : ComparableBase<Person>
        {
            static Person()
            {
                DefaultComparer = FastComparerBuilder.For<Person>().CompareBy(p => p.LastName);
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        private static readonly Person AbeAbrams = new Person { FirstName = "Abe", LastName = "Abrams" };
        private static readonly Person JackAbrams = new Person { FirstName = "Jack", LastName = "Abrams" };
        private static readonly Person WilliamAbrams = new Person { FirstName = "William", LastName = "Abrams" };
        private static readonly Person CaseyJohnson = new Person { FirstName = "Casey", LastName = "Johnson" };

        [Fact]
        public void SubstitutesCompareDefaultForComparerDefault()
        {
            IComparer<Person> thenByComparer = ComparerBuilder.For<Person>().OrderBy(p => p.FirstName);
            var comparer = Comparer<Person>.Default.ThenBy(thenByComparer);
            Assert.Equal(FastComparerBuilder.For<Person>().Default().ThenBy(thenByComparer).ToString(), comparer.ToString());

            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(comparer);
            Assert.Equal(new[] { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson }, list);
        }

        [Fact]
        public void SubstitutesCompareDefaultForNull()
        {
            IComparer<Person> thenByComparer = ComparerBuilder.For<Person>().OrderBy(p => p.FirstName);
            IComparer<Person> source = null;
            var comparer = source.ThenBy(thenByComparer);
            Assert.Equal(FastComparerBuilder.For<Person>().Default().ThenBy(thenByComparer).ToString(), comparer.ToString());

            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(comparer);
            Assert.Equal(new[] { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson }, list);
        }

        [Fact]
        public void ThenByUsesComparer()
        {
            IComparer<Person> thenByComparer = FastComparerBuilder.For<Person>().CompareBy(p => p.FirstName);
            var list = new List<Person> { AbeAbrams, WilliamAbrams, CaseyJohnson, JackAbrams };
            list.Sort(FastComparerBuilder.For<Person>().Default().ThenBy(thenByComparer));
            Assert.Equal(new[] { AbeAbrams, JackAbrams, WilliamAbrams, CaseyJohnson }, list);
        }

        [Fact]
        public void ThenByIsAppliedAsTieBreaker()
        {
            IComparer<Person> thenByComparer = FastComparerBuilder.For<Person>().CompareBy(p => p.FirstName);
            IComparer<Person> defaultComparer = FastComparerBuilder.For<Person>().Default();
            IComparer<Person> fullComparer = defaultComparer.ThenBy(thenByComparer);
            Assert.True(defaultComparer.Compare(AbeAbrams, WilliamAbrams) == 0);
            Assert.True(thenByComparer.Compare(AbeAbrams, WilliamAbrams) < 0);
            Assert.True(fullComparer.Compare(AbeAbrams, WilliamAbrams) < 0);
        }

        [Fact]
        public void ThenByIsOnlyAppliedAsTieBreaker()
        {
            IComparer<Person> thenByComparer = new FailComparer<Person>();
            var comparer = FastComparerBuilder.For<Person>().Default().ThenBy(thenByComparer);
            Assert.True(comparer.Compare(AbeAbrams, CaseyJohnson) < 0);
            Assert.True(comparer.Compare(CaseyJohnson, AbeAbrams) > 0);
        }

        [Fact]
        public void ToString_DumpsComparer()
        {
            Assert.Equal("Compound(Default(Compound(Null, Select<String>(Default(String: IComparable<T>)))), Select<String>(Default(String: IComparable<T>)))", ComparerBuilder.For<Person>().Default().CompareBy(p => p.LastName).ToString());
        }

        // The delegate overloads are tested by Compare_._Key.
    }
}
