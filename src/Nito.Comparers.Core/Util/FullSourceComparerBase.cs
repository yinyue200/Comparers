using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Comparers.Util
{
    internal abstract class FullSourceComparerBase<T, TSource, TComparer> : SourceComparerBase<T, TSource,TComparer> where TComparer: IComparer<TSource>,IEqualityComparer<TSource>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FullSourceComparerBase{T, TSource, TComparer}"/> class.
        /// </summary>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="specialNullHandling">A value indicating whether <c>null</c> values are passed to <see cref="EqualityComparerBase{T}.DoGetHashCode"/> and <see cref="ComparerBase{T}.DoCompare"/>. If <c>false</c>, then <c>null</c> values are considered less than any non-<c>null</c> values and are not passed to <see cref="EqualityComparerBase{T}.DoGetHashCode"/> nor <see cref="ComparerBase{T}.DoCompare"/>.</param>
        protected FullSourceComparerBase(TComparer source, bool specialNullHandling)
            : base(source,specialNullHandling)
        {

        }
    }
}
