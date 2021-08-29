using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recombobulator
{
    public partial class ExistingFileForm : Form
    {
        public string FileName { get { return fileNameTextBox.Text; } }

        public ExistingFileForm()
        {
            InitializeComponent();
        }
    }
}
