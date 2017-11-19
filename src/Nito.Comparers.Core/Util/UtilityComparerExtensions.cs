﻿using System;
using System.Collections.Generic;

namespace Nito.Comparers.Util
{
    /// <summary>
    /// Provides extension methods for comparers.
    /// </summary>
    internal static class UtilityComparerExtensions
    {
        /// <summary>
        /// Returns a comparer that works by comparing the results of the specified key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of key objects being compared.</typeparam>
        /// <typeparam name="T">The type of objects being compared.</typeparam>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="specialNullHandling">A value indicating whether <c>null</c> values are passed to <paramref name="selector"/>. If <c>false</c>, then <c>null</c> values are considered less than any non-<c>null</c> values and are not passed to <paramref name="selector"/>.</param>
        /// <returns>A comparer that works by comparing the results of the specified key selector.</returns>
        public static IFullComparer<T> SelectFrom<T, TSource>(this IComparer<TSource> source, Func<T, TSource> selector, bool specialNullHandling = false)
        {
            return new SelectComparer<T, TSource, IComparer<TSource>>(selector, source, specialNullHandling);
        }
        public static IFullComparer<T> SelectFrom<T, TSource, TComparer>(this TComparer source, Func<T, TSource> selector, bool specialNullHandling = false) where TComparer:IComparer<TSource>,IEqualityComparer<TSource>
        {
            return new FullSelectComparer<T, TSource, TComparer>(selector, source, specialNullHandling);
        }
    }
}
