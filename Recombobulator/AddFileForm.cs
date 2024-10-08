﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	class AddFileForm : Form
	{
		public string FullPath { get; protected set; } = "";
		public virtual string RelativePath { get { return ""; } }

		public int TextureSet { get; protected set; } = 0;

		public bool ImportTextures { get; protected set; } = false;

		public bool PromptTextures { get; protected set; } = false;

		public string FileName { get; protected set; } = "";

		protected Repository _repository = null;
	}
}
