using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class ArchiveList : ListBase<Archive>
	{
		public List<Archive> Archives { get { return _list; } set { _list = value; } }
	}
}
