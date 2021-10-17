using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class Portal
	{
		public string UnitName { get; set; } = "";
		public int SignalID { get; set; } = 0;
		public int StreamUnitID { get; set; } = 0;
		public string SourceUnitName { get; set; } = "";
		public uint SourceVersion { get; set; } = 0;
	}
}
