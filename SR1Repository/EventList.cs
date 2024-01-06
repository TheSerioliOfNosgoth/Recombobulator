using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class EventList : ListBase<Event>
	{
		public List<Event> Events { get { return _list; } set { _list = value; } }
	}
}
