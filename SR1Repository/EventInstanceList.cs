using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class EventInstanceList : ListBase<EventInstance>
	{
		public List<EventInstance> Instances { get { return _list; } set { _list = value; } }
	}
}
