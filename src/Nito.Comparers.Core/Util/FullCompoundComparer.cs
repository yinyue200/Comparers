using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Comparers.Util
{
    class FullCompoundComparer<T,TComparer> : BaseCompoundComparer<T, TComparer> where TComparer : IComparer<T>, IEqualityComparer<T>
    {
        public FullCompoundComparer(TComparer source, TComparer secondSource)
            : base(source, secondSource)
        {

        }
        protected override int DoGetHashCode(T obj)
        {
            unchecked
            {
                var ret = (int)2166136261;
                ret += _source.GetHashCode();
                ret *= 16777619;
                ret += _secondSource.GetHashCode();
                ret *= 16777619;
                return ret;
            }
        }
        protected override bool DoEquals(T x, T y)
        {
            return _source.Equals(x, y) && _secondSource.Equals(x, y);
        }
    }
    class CompoundComparer<T> : BaseCompoundComparer<T, IComparer<T>>
    {
        protected readonly IComparer<T> _hashcodeComparer1;
        protected readonly IComparer<T> _hashcodeComparer2;
        public CompoundComparer(IComparer<T> source, IComparer<T> secondSource)
            : base(source, secondSource)
        {
            //
        }
        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to return a hash code. This object may be <c>null</c>.</param>
        /// <returns>A hash code for the specified object.</returns>
        protected override int DoGetHashCode(T obj)
        {
            unchecked
            {
                var ret = (int)2166136261;
                ret += ComparerHelpers.GetHashCodeFromComparer(_hashcodeComparer1, obj);
                ret *= 16777619;
                ret += ComparerHelpers.GetHashCodeFromComparer(_hashcodeComparer2, obj);
                ret *= 16777619;
                return ret;
            }
        }
    }
}
