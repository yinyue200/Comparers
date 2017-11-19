﻿using System.Collections.Generic;

namespace Nito.Comparers.Util
{
    /// <summary>
    /// A comparer that uses another comparer if the source comparer determines the objects are equal.
    /// </summary>
    /// <typeparam name="T">The type of objects being compared.</typeparam>
    /// <typeparam name="TComparer"></typeparam>
    internal class BaseCompoundComparer<T, TComparer> : SourceComparerBase<T, T, TComparer> where TComparer:IComparer<T>
    {
        /// <summary>
        /// The second comparer.
        /// </summary>
        protected readonly TComparer _secondSource;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompoundComparer{T}"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="secondSource">The second comparer. If this is <c>null</c>, the default comparer is used.</param>
        public BaseCompoundComparer(TComparer source, TComparer secondSource)
            : base(source, true)
        {
            _secondSource = secondSource;
        }



        /// <summary>
        /// Compares two objects and returns a value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first object to compare. This object may be <c>null</c>.</param>
        /// <param name="y">The second object to compare. This object may be <c>null</c>.</param>
        /// <returns>A value less than 0 if <paramref name="x"/> is less than <paramref name="y"/>, 0 if <paramref name="x"/> is equal to <paramref name="y"/>, or greater than 0 if <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        protected override int DoCompare(T x, T y)
        {
            var ret = _source.Compare(x, y);
            if (ret != 0)
                return ret;
            return _secondSource.Compare(x, y);
        }

        /// <summary>
        /// Returns a short, human-readable description of the comparer. This is intended for debugging and not for other purposes.
        /// </summary>
        public override string ToString()
        {
            return "Compound(" + _source + ", " + _secondSource + ")";
        }
    }
}
