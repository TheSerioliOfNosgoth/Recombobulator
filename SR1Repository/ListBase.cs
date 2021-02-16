using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class ListBase<T>
    {
        protected List<T> _list = new List<T>();
        public T this[int index]
        {
            get { return _list[index]; }
        }
        public int Count { get { return _list.Count; } }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Add(IEnumerable<T> items)
        {
            _list.AddRange(items);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public T Find(Predicate<T> match)
        {
            return _list.Find(match);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            return _list.FindAll(match);
        }
    }
}
