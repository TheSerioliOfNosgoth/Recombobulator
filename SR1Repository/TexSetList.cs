using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class TexSetList : ListBase<TexSet>
	{
		public List<TexSet> TexSets { get { return _list; } set { _list = value; } }
	}
}
