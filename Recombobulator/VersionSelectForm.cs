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
	public partial class VersionSelectForm : Form
	{
		public SR1_File.Version Version = SR1_File.Version.Detect;

		public VersionSelectForm()
		{
			InitializeComponent();

			string[] versions =
			{
				"Detect",
				"Proto",
				"Jan23",
				"Feb04",
				"Feb16",
				"Apr14",
				"May12",
				"Jun01",
				"Jun10",
				"Jun18",
				"Jul14",
				"Retail PC",
			};

			versionComboBox.Items.AddRange(versions);
			versionComboBox.SelectedIndex = 0;
		}

		private void VersionSelectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			int index = versionComboBox.SelectedIndex;
			if (index > 0 && index < (int)SR1_File.Version.Next)
			{
				Version = (SR1_File.Version)index;
			}
			else
			{
				Version = SR1_File.Version.Detect;
			}
		}
	}
}
