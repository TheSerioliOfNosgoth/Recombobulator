using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	public partial class EditPortalForm : Form
	{
		Repository _repository = null;
		List<Level> _levels = null;

		public string FromUnit { get { return fromUnitComboBox.Text; } }
		public string ToUnit { get { return toUnitComboBox.Text; } }
		public int FromSignal { get { Int32.TryParse(fromSignalComboBox.Text, out int result); return result; } }
		public int ToSignal { get { Int32.TryParse(toSignalComboBox.Text, out int result); return result; } }

		public EditPortalForm()
		{
			InitializeComponent();
		}

		public void Initialize(Repository repository, string fileName)
		{
			_repository = repository;

			string path = _repository.MakeLevelFilePath(fileName);
			string fullPath = _repository.MakeLevelFilePath(fileName, true);

			_levels = new List<Level>();

			foreach (Level level in _repository.Levels.Levels)
			{
				fromUnitComboBox.Items.Add(level.UnitName);
				toUnitComboBox.Items.Add(level.UnitName);
				_levels.Add(level);
			}

			_levels.Sort((a, b) => a.UnitName.CompareTo(b.UnitName));

			int fromLevelIndex = _levels.FindIndex(x => x.UnitName == fileName);
			fromUnitComboBox.SelectedIndex = fromLevelIndex;
			fromUnitComboBox.Enabled = false;

			toUnitComboBox.SelectedIndex = 0;
		}

		private void fromUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = ((ComboBox)sender).SelectedIndex;
			Level level = _levels[selectedIndex];

			fromSignalComboBox.Items.Clear();
			foreach (Portal portal in level.Portals.Portals)
			{
				fromSignalComboBox.Items.Add(portal.SignalID.ToString());
			}

			fromSignalComboBox.SelectedIndex = 0;
		}

		private void toUnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = ((ComboBox)sender).SelectedIndex;
			Level level = _levels[selectedIndex];

			toSignalComboBox.Items.Clear();
			foreach (Portal portal in level.Portals.Portals)
			{
				toSignalComboBox.Items.Add(portal.SignalID.ToString());
			}

			toSignalComboBox.SelectedIndex = 0;
		}
	}
}
