using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class Portal
	{
		public int SignalID { get; set; } = 0;
		public string DestUnitName { get; set; } = "";
		public int DestUnitID { get; set; } = 0;
		public int DestSignalID { get; set; } = 0;
		public string OldDestUnitName { get; set; } = "";
		public uint OldDestVersion { get; set; } = 0;
	}
}
