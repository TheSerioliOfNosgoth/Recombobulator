using System.Windows.Forms;

namespace Recombobulator
{
	public partial class CreateProjectForm : Form
	{
		public enum ProjectType
		{
			Empty,
			Metadata,
			Assets,
		}

		public ProjectType CreateProjectType = ProjectType.Empty;

		public CreateProjectForm()
		{
			InitializeComponent();

			string[] projectType =
			{
				"Empty Project",
				"Import Metadata",
				"Import Assets",
			};

			typeComboBox.Items.AddRange(projectType);
			typeComboBox.SelectedIndex = 0;
		}

		private void CreateProjectForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			CreateProjectType = (ProjectType)typeComboBox.SelectedIndex;
		}
	}
}
