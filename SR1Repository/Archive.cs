using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class Archive
	{
		public int ArchiveIndex { get; set; } = 0;

		public string ArchiveName { get; set; } = "";

		// TODO - Add list of objects in the archive.

		public int TextureStart { get; set; } = 0;
		public int TextureCount { get; set; } = 0;
	}
}
