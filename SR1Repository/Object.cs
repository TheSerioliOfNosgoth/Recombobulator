using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class Object
	{
		public string ObjectName { get; set; } = "";
		public short NumModels { get; set; } = 0;
		public short NumAnimations { get; set; } = 0;
		public short NumSections { get; set; } = 0;
		public bool IsNew { get; set; } = false;
		public string TextureSet { get; set; } = "";
	}
}
