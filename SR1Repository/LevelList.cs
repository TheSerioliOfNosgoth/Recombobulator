using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class LevelList : ListBase<Level>
	{
		public int NextAvailableID { get; set; }
		public List<Level> Levels { get { return _list; } set { _list = value; } }
	}
}
