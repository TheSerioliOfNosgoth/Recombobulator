using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class TexDescList
    {
        List<TexDesc> _list = new List<TexDesc>();
        public TexDesc this[int index]
        {
            get { return _list[index]; }
        }
        public int Count { get { return _list.Count; } }
        public List<TexDesc> Textures { get { return _list; } set { _list = value; } }

        public void Add(TexDesc texture)
        {
            _list.Add(texture);
        }

        public void Add(IEnumerable<TexDesc> textures)
        {
            _list.AddRange(textures);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public TexDesc Find(Predicate<TexDesc> match)
        {
            return _list.Find(match);
        }

        public List<TexDesc> FindAll(Predicate<TexDesc> match)
        {
            return _list.FindAll(match);
        }
    }
}
