using System;
using System.Collections.Generic;

namespace SR1Repository
{
	public class IntroList : ListBase<Intro>
	{
		public List<Intro> Intros { get { return _list; } set { _list = value; } }
	}
}
