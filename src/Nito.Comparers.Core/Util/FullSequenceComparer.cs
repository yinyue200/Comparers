using System;
using System.Collections.Generic;
using System.Text;

namespace Nito.Comparers.Util
{
    class FullSequenceComparer<T, TComparer> : SequenceComparer<T,TComparer> where TComparer : IComparer<T>,IEqualityComparer<T>
    {
        public FullSequenceComparer(TComparer source)
            : base(source)
        {
        }
        protected override bool DoEquals(IEnumerable<T> x, IEnumerable<T> y)
        {
            using (var xIter = x.GetEnumerator())
            using (var yIter = y.GetEnumerator())
            {
                while (true)
                {
                    if (!xIter.MoveNext())
                    {
                        if (!yIter.MoveNext())
                            return true;
                        return false;
                    }

                    if (!yIter.MoveNext())
                        return false;

                    var ret = _source.Equals(xIter.Current, yIter.Current);
                    if (ret !=true)
                        return ret;
                }
            }
        }

    }
}
