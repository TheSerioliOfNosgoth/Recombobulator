using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class ObjectList
    {
        List<Object> _list = new List<Object>();
        public Object this[int index]
        {
            get { return _list[index]; }
        }
        public int Count { get { return _list.Count; } }
        public List<Object> Objects { get { return _list; } set { _list = value; } }

        public void Add(Object obj)
        {
            _list.Add(obj);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public Object Find(Predicate<Object> match)
        {
            return _list.Find(match);
        }

        public List<Object> FindAll(Predicate<Object> match)
        {
            return _list.FindAll(match);
        }
    }
}
