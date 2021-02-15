using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class AssetDescList
    {
        List<AssetDesc> _list = new List<AssetDesc>();
        public AssetDesc this[int index]
        {
            get { return _list[index]; }
        }
        public int Count { get { return _list.Count; } }
        public List<AssetDesc> Assets { get { return _list; } set { _list = value; } }

        public void Add(AssetDesc asset)
        {
            _list.Add(asset);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public AssetDesc Find(Predicate<AssetDesc> match)
        {
            return _list.Find(match);
        }

        public List<AssetDesc> FindAll(Predicate<AssetDesc> match)
        {
            return _list.FindAll(match);
        }
    }
}
