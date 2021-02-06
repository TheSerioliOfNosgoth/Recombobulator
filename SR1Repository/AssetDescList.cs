using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class AssetDescList
    {
        readonly List<AssetDesc> _assetDescList = new List<AssetDesc>();
        public int Count { get { return _assetDescList.Count; } }
        public AssetDesc[] Assets
        { 
            get { return _assetDescList.ToArray(); }
            set { _assetDescList.Clear(); _assetDescList.AddRange(value); }
        }

        public void Add(AssetDesc asset)
        {
            _assetDescList.Add(asset);
        }

        public void Clear()
        {
            _assetDescList.Clear();
        }

        public List<AssetDesc> FindAll(Predicate<AssetDesc> match)
        {
            return _assetDescList.FindAll(match);
        }
    }
}
