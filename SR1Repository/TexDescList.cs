using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class TexDescList : ListBase<TexDesc>
	{
		public List<TexDesc> Textures { get { return _list; } set { _list = value; } }
	}
}
