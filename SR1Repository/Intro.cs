﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class Intro
	{
		public class Vector
		{
			public short X { get; set; }
			public short Y { get; set; }
			public short Z { get; set; }
		}

		public string ObjectName { get; set; } = "";
		public string UnitName { get; set; } = "";
		public int StreamUnitID { get; set; } = 0;
		public int IntroUniqueID { get; set; } = 0;

		public Vector Position { get; set; } = new Vector();
		public Vector Rotation { get; set; } = new Vector();
	}
}
