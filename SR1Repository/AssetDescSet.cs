using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    class AssetDescSet
    {
        readonly List<AssetDesc> _AssetDescList = new List<AssetDesc>();
        public int Count { get { return _AssetDescList.Count; } }
        public AssetDesc[] Assets
        { 
            get { return _AssetDescList.ToArray(); }
            set { _AssetDescList.Clear(); _AssetDescList.AddRange(value); }
        }

        public void Add(AssetDesc asset)
        {
            _AssetDescList.Add(asset);
        }

        public void Clear()
        {
            _AssetDescList.Clear();
        }

        public List<AssetDesc> FindAll(Predicate<AssetDesc> match)
        {
            return _AssetDescList.FindAll(match);
        }
    }
}
