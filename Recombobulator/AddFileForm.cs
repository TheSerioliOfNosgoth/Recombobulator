using System;
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

        protected string _fileName = null;

        protected Repository _repository = null;
    }
}
