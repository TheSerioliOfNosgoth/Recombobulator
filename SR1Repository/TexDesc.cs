﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
	public class TexDesc
	{
		public string FilePath { get; set; } = "";
		public int TextureIndex { get; set; } = 0;

		public bool IsNew { get; set; } = false;
	}
}
