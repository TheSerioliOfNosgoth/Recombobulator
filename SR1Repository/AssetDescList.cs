﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class AssetDescList
    {
        List<AssetDesc> _assetDescList = new List<AssetDesc>();
        public int Count { get { return _assetDescList.Count; } }
        public List<AssetDesc> Assets { get { return _assetDescList; } set { _assetDescList = value; } }

        public void Add(AssetDesc asset)
        {
            _assetDescList.Add(asset);
        }

        public void Clear()
        {
            _assetDescList.Clear();
        }

        public AssetDesc Find(Predicate<AssetDesc> match)
        {
            return _assetDescList.Find(match);
        }

        public List<AssetDesc> FindAll(Predicate<AssetDesc> match)
        {
            return _assetDescList.FindAll(match);
        }
    }
}
