using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class TexSetList
    {
        List<TexSet> _list = new List<TexSet>();
        public TexSet this[int index]
        {
            get { return _list[index]; }
        }
        public int Count { get { return _list.Count; } }
        public List<TexSet> TexSets { get { return _list; } set { _list = value; } }

        public void Add(TexSet texSet)
        {
            _list.Add(texSet);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public TexSet Find(Predicate<TexSet> match)
        {
            return _list.Find(match);
        }

        public List<TexSet> FindAll(Predicate<TexSet> match)
        {
            return _list.FindAll(match);
        }
    }
}
