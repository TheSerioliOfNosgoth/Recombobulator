using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	public partial class EditPortalForm : Form
	{
		public EditPortalForm()
		{
			InitializeComponent();
		}

		public void Initialize(Repository repository, string fileName)
		{
			fromUnitTextBox.Text = fileName;
		}
	}
}
