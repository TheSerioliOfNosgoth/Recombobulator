using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class AssetDescList : ListBase<AssetDesc>
	{
		public List<AssetDesc> Assets { get { return _list; } set { _list = value; } }
	}
}
