using System;
using System.Collections.Generic;
using Nito.Comparers.Util;
using System.ComponentModel;

namespace Nito.Comparers.Fast
{
    /// <summary>
    /// 
    /// </summary>
    public static class FastComparerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TComparer2"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="specialNullHandling"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IFullComparer<T> CompareBy<T, TKey, TComparer2>(this TComparer2 source, Func<T, TKey> selector, bool specialNullHandling = false, bool descending = false)  where TComparer2 : IComparer<T>, IEqualityComparer<T>
        {
            return source.CompareBy<T,TComparer2>((TComparer2)FastComparerBuilder.For<TKey>().Default().SelectFrom(selector, specialNullHandling), descending);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="specialNullHandling"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IFullComparer<T> CompareBy<T, TKey>(this IFullComparer<T> source, Func<T, TKey> selector, bool specialNullHandling = false, bool descending = false)
        {
            return CompareBy<T, TKey, IFullComparer<T>>(source, selector, specialNullHandling, descending);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IFullComparer<T> MakeReverse<T>(this IFullComparer<T> source)
        {
            return new FullReverseComparer<T, IFullComparer<T>>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparer"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IFullComparer<T> MakeReverse<T, TComparer>(this TComparer source) where TComparer : IComparer<T>, IEqualityComparer<T>
        {
            return new FullReverseComparer<T, TComparer>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparer"></typeparam>
        /// <param name="source"></param>
        /// <param name="thenBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IFullComparer<T> CompareBy<T, TComparer>(this TComparer source, TComparer thenBy, bool descending = false) where TComparer : IComparer<T>, IEqualityComparer<T>
        {
            return new FullCompoundComparer<T, TComparer>(source, (descending ? (TComparer)thenBy.Reverse() : thenBy));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="thenBy"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IFullComparer<T> CompareBy<T>(this IFullComparer<T> source, IFullComparer<T> thenBy, bool descending = false)
        {
            return CompareBy<T, IFullComparer<T>>(source, thenBy, descending);
        }
        /// <summary>
        /// Returns a comparer that uses a key comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TComparer"></typeparam>
        /// <typeparam name="TComparer2"></typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector"></param>
        /// <param name="keyComparer"></param>
        /// <param name="specialNullHandling"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IFullComparer<T> CompareBy<T, TKey, TComparer, TComparer2>(this TComparer2 source, Func<T, TKey> selector, TComparer keyComparer, bool specialNullHandling = false, bool descending = false) where TComparer : IComparer<TKey>, IEqualityComparer<TKey> where TComparer2 : IComparer<T>, IEqualityComparer<T>
        {
            return source.ThenBy(keyComparer.SelectFrom(selector, specialNullHandling), descending);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TComparer"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IFullComparer<IEnumerable<T>> MakeSequence<T, TComparer>(this TComparer source) where TComparer : IComparer<T>, IEqualityComparer<T>
        {
            return new FullSequenceComparer<T, TComparer>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IFullComparer<IEnumerable<T>> MakeSequence<T>(this IFullComparer<T> source)
        {
            return new FullSequenceComparer<T, IFullComparer<T>>(source);
        }
    }
}
namespace Nito.Comparers
{
    /// <summary>
    /// Provides extension methods for comparers.
    /// </summary>
    public static class ComparerExtensions
    {
        /// <summary>
        /// Returns a comparer that reverses the evaluation of the specified source comparer.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A comparer that reverses the evaluation of the specified source comparer.</returns>
        public static IFullComparer<T> Reverse<T>(this IComparer<T> source)
        {
            return new ReverseComparer<T, IComparer<T>>(source);
        }


        /// <summary>
        /// Returns a comparer that uses another comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="thenBy">The comparer that is used if <paramref name="source"/> determines the objects are equal. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="descending">A value indicating whether the sorting is done in descending order. If <c>false</c> (the default), then the sort is in ascending order.</param>
        /// <returns>A comparer that uses another comparer if the source comparer determines the objects are equal.</returns>
        public static IFullComparer<T> ThenBy<T>(this IComparer<T> source, IComparer<T> thenBy, bool descending = false)
        {
            return new CompoundComparer<T, IComparer<T>>(source, descending ? thenBy.Reverse() : thenBy);
        }


        /// <summary>
        /// Returns a comparer that uses a key comparer if the source comparer determines the objects are equal.
        /// </summary>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <typeparam name="TKey">The type of key objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="keyComparer">The key comparer. Defaults to <c>null</c>. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="specialNullHandling">A value indicating whether <c>null</c> values are passed to <paramref name="selector"/>. If <c>false</c>, then <c>null</c> values are considered less than any non-<c>null</c> values and are not passed to <paramref name="selector"/>.</param>
        /// <param name="descending">A value indicating whether the sorting is done in descending order. If <c>false</c> (the default), then the sort is in ascending order.</param>
        /// <returns>A comparer that uses a key comparer if the source comparer determines the objects are equal.</returns>
        public static IFullComparer<T> ThenBy<T, TKey>(this IComparer<T> source, Func<T, TKey> selector, IComparer<TKey> keyComparer = null, bool specialNullHandling = false, bool descending = false)
        {
            return source.ThenBy(keyComparer.SelectFrom(selector, specialNullHandling), descending);
        }



        /// <summary>
        /// Returns a comparer that will perform a lexicographical ordering on a sequence of items.
        /// </summary>
        /// <typeparam name="T">The type of sequence elements being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <returns>A comparer that will perform a lexicographical ordering on a sequence of items.</returns>
        public static IFullComparer<IEnumerable<T>> Sequence<T>(this IComparer<T> source)
        {
            return new SequenceComparer<T, IComparer<T>>(source);
        }


    }
}
