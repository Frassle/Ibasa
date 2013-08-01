using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Collections
{
    public class Bimap<TLeft, TRight> : IEnumerable<Tuple<TLeft, TRight>>
    {
        Multimap<TLeft, TRight> leftMap;
        Multimap<TRight, TLeft> rightMap;

        public Bimap()
        {
            leftMap = new Multimap<TLeft, TRight>();
            rightMap = new Multimap<TRight, TLeft>();
        }

        public void AddLeft(TLeft key, TRight value)
        {
            leftMap.Add(key, value);
            rightMap.Add(value, key);
        }

        public void AddRight(TRight key, TLeft value)
        {
            rightMap.Add(key, value);
            leftMap.Add(value, key);
        }

        public IEnumerable<TRight> GetLeft(TLeft left)
        {
            return leftMap[left];
        }

        public IEnumerable<TLeft> GetRight(TRight right)
        {
            return rightMap[right];
        }

        public IEnumerator<Tuple<TLeft, TRight>> GetEnumerator()
        {
            foreach (var pair in leftMap)
            {
                foreach (var item in pair)
                {
                    yield return Tuple.Create(pair.Key, item);
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
