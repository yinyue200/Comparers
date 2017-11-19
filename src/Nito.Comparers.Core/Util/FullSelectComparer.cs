using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Comparers.Util
{
    internal class FullSelectComparer<T, TSource, TComparer> : SelectComparer<T, TSource, TComparer> where TComparer : IComparer<TSource>,IEqualityComparer<TSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectComparer{T, TSource, TComparer}"/> class.
        /// </summary>
        /// <param name="selector">The key selector. May not be <c>null</c>.</param>
        /// <param name="source">The source comparer. If this is <c>null</c>, the default comparer is used.</param>
        /// <param name="specialNullHandling">A value indicating whether <c>null</c> values are passed to <paramref name="selector"/>. If <c>false</c>, then <c>null</c> values are considered less than any non-<c>null</c> values and are not passed to <paramref name="selector"/>.</param>
        public FullSelectComparer(Func<T, TSource> selector, TComparer source, bool specialNullHandling)
            : base(selector,source, specialNullHandling)
        {

        }
        protected override bool DoEquals(T x, T y)
        {
            return _source.Equals(_selector(x), _selector(y));
        }
    }
}
