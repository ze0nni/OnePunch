using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Random
{
    public class Shuffled<T> : IEnumerable<T>
    {
        readonly IEnumerable<T> origin;

        public Shuffled(IEnumerable<T> origin)
        {
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var source = origin.ToArray();
            var size = source.Length;
            if (0== size) {
                yield break;
            }

            for (var i = 0; i < size - 1; i++) {
                var n = UnityEngine.Random.Range(i, size);
                if (n != i) {
                    var tmp = source[n];
                    source[n] = source[i];
                    source[i] = tmp;
                }
                yield return source[i];
            }

            yield return source[size - 1];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
