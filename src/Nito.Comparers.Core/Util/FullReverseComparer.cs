using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Comparers.Util
{
    class FullReverseComparer<T, TComparer> : ReverseComparer<T, TComparer> where TComparer : IComparer<T>,IEqualityComparer<T>
    {
        public FullReverseComparer(TComparer source):base(source){

        }
        protected override bool DoEquals(T x, T y)
        {
            return _source.Equals(y, x);
        }
    }
}
