using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;

namespace Recombobulator.ParticlePanels
{
	public partial class EditFlashesPanel : EditPanelBase
	{
		SR1_StructureSeries<GenericFlashParams> _flashesList = null;
		SR1_StructureSeries<GenericFlashParams> _flashesListBackup = null;
		GenericFlashParams _copiedFlashParams = new GenericFlashParams();

		public EditFlashesPanel()
		{
			InitializeComponent();
		}

		public void Open(object lightningsList, object lightningsListBackup = null)
		{
			_flashesList = (SR1_StructureSeries<GenericFlashParams>)lightningsList;
			_flashesListBackup = (SR1_StructureSeries<GenericFlashParams>)lightningsListBackup;

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.Items.Clear();
			for (int i = 0; i < _flashesList.Count; i++)
			{
				selectionComboBox.Items.Add("flash-" + i.ToString());
			}

			selectionComboBox.SelectedIndex = 0;
		}

		private void color_TextChanged(object sender, EventArgs e)
		{
			int color = 0;

			foreach (Control control in fieldsPanel.Controls)
			{
				if (control.Name == "color")
				{
					int.TryParse(control.Text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out color);
					color |= unchecked((int)0xFF000000);
				}
			}

			colorBox.BackColor = Color.FromArgb(color);
		}

		private void selectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int newIndex = selectionComboBox.SelectedIndex;

			insertButton.Enabled = false;
			removeButton.Enabled = false;
			resetButton.Enabled = false;
			addButton.Enabled = false;
			copyButton.Enabled = false;
			pasteButton.Enabled = false;

			colorBox.Visible = false;

			fieldsPanel.Controls.Clear();
			fieldsPanel.RowStyles.Clear();
			_structures.Clear();

			if (newIndex < 0)
			{
				return;
			}

			addButton.Enabled = true;
			copyButton.Enabled = true;
			pasteButton.Enabled = true;

			if (_flashesListBackup != null)
			{
				if (newIndex < _flashesListBackup.Count)
				{
					insertButton.Enabled = false;
					removeButton.Enabled = false;
					resetButton.Enabled = true;
				}
				else
				{
					insertButton.Enabled = true;
					removeButton.Enabled = true;
					resetButton.Enabled = false;
				}
			}
			else
			{
				insertButton.Enabled = true;
				removeButton.Enabled = true;
				resetButton.Enabled = false;
			}

			colorBox.Visible = true;

			List<SR1_Structure> membersRead = _flashesList[newIndex].MembersRead;
			fieldsPanel.RowCount = membersRead.Count;

			int row = 0;
			int color = 0;
			foreach (SR1_Structure structure in membersRead)
			{
				TextBox valueTextBox = AddField(structure, ref row);

				if (structure.Name == "color")
				{
					color = ((SR1_Primative<int>)structure).Value;
					valueTextBox.TextChanged += color_TextChanged;
				}
			}

			color |= unchecked((int)0xFF000000);

			colorBox.BackColor = Color.FromArgb(color);

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			_flashesList.Add(new GenericFlashParams());
			selectionComboBox.SelectedIndex = _flashesList.Count - 1;
		}

		private void insertButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0)
			{
				return;
			}

			currentIndex = Math.Min(currentIndex, _flashesList.Count);

			_flashesList.InsertAt(currentIndex, new GenericFlashParams());
			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 || currentIndex >= selectionComboBox.Items.Count)
			{
				return;
			}

			_flashesList.RemoveAt(currentIndex);
			selectionComboBox.SelectedIndex = -1;

			if (selectionComboBox.Items.Count > 0)
			{
				currentIndex = Math.Min(currentIndex, _flashesList.Count - 1);
				selectionComboBox.SelectedIndex = currentIndex;
			}
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _flashesList.Count)
			{
				return;
			}

			GenericFlashParams.Copy(_copiedFlashParams, (GenericFlashParams)_flashesList[currentIndex]);
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _flashesList.Count)
			{
				return;
			}

			GenericFlashParams.Copy((GenericFlashParams)_flashesList[currentIndex], _copiedFlashParams);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (_flashesListBackup == null ||
				currentIndex < 0 ||
				currentIndex >= _flashesList.Count ||
				currentIndex >= _flashesListBackup.Count)
			{
				return;
			}

			GenericFlashParams.Copy(
				(GenericFlashParams)_flashesList[currentIndex],
				(GenericFlashParams)_flashesListBackup[currentIndex]
			);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}
	}
}
