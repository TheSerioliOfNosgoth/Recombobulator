using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;

namespace Recombobulator.ParticlePanels
{
	public partial class EditLightningsPanel : EditPanelBase
	{
		SR1_StructureSeries<GenericLightningParams> _lightningsList = null;
		SR1_StructureSeries<GenericLightningParams> _lightningsListBackup = null;
		GenericLightningParams _copiedLightningParams = new GenericLightningParams();

		public EditLightningsPanel()
		{
			InitializeComponent();
		}

		public void Open(object lightningsList, object lightningsListBackup = null)
		{
			_lightningsList = (SR1_StructureSeries<GenericLightningParams>)lightningsList;
			_lightningsListBackup = (SR1_StructureSeries<GenericLightningParams>)lightningsListBackup;

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.Items.Clear();
			for (int i = 0; i < _lightningsList.Count; i++)
			{
				selectionComboBox.Items.Add("lightning-" + i.ToString());
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

		private void glowColor_TextChanged(object sender, EventArgs e)
		{
			int glow_color = 0;

			foreach (Control control in fieldsPanel.Controls)
			{
				if (control.Name == "glow_color")
				{
					int.TryParse(control.Text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out glow_color);
					glow_color |= unchecked((int)0xFF000000);
				}
			}

			glowColorBox.BackColor = Color.FromArgb(glow_color);
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
			glowColorBox.Visible = false;

			fieldsPanel.Controls.Clear();
			fieldsPanel.RowStyles.Clear();

			if (newIndex < 0)
			{
				return;
			}

			addButton.Enabled = true;
			copyButton.Enabled = true;
			pasteButton.Enabled = true;

			if (_lightningsListBackup != null)
			{
				if (newIndex < _lightningsListBackup.Count)
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
			glowColorBox.Visible = true;

			List<SR1_Structure> membersRead = _lightningsList[newIndex].MembersRead;
			fieldsPanel.RowCount = membersRead.Count;

			int row = 0;
			int color = 0;
			int glow_color = 0;
			foreach (SR1_Structure structure in membersRead)
			{
				TextBox valueTextBox = AddField(structure, "", ref row);

				if (structure.Name == "color")
				{
					color = ((SR1_Primative<int>)structure).Value;
					valueTextBox.TextChanged += color_TextChanged;
				}

				if (structure.Name == "glow_color")
				{
					glow_color = ((SR1_Primative<int>)structure).Value;
					valueTextBox.TextChanged += glowColor_TextChanged;
				}
			}

			color |= unchecked((int)0xFF000000);
			glow_color |= unchecked((int)0xFF000000);

			colorBox.BackColor = Color.FromArgb(color);
			glowColorBox.BackColor = Color.FromArgb(glow_color);

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			_lightningsList.Add(new GenericLightningParams());
			selectionComboBox.SelectedIndex = _lightningsList.Count - 1;
		}

		private void insertButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0)
			{
				return;
			}

			currentIndex = Math.Min(currentIndex, _lightningsList.Count);

			_lightningsList.InsertAt(currentIndex, new GenericLightningParams());
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

			_lightningsList.RemoveAt(currentIndex);
			selectionComboBox.SelectedIndex = -1;

			if (selectionComboBox.Items.Count > 0)
			{
				currentIndex = Math.Min(currentIndex, _lightningsList.Count - 1);
				selectionComboBox.SelectedIndex = currentIndex;
			}
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _lightningsList.Count)
			{
				return;
			}

			GenericLightningParams.Copy(_copiedLightningParams, (GenericLightningParams)_lightningsList[currentIndex]);
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _lightningsList.Count)
			{
				return;
			}

			GenericLightningParams.Copy((GenericLightningParams)_lightningsList[currentIndex], _copiedLightningParams);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (_lightningsListBackup == null ||
				currentIndex < 0 ||
				currentIndex >= _lightningsList.Count ||
				currentIndex >= _lightningsListBackup.Count)
			{
				return;
			}

			GenericLightningParams.Copy(
				(GenericLightningParams)_lightningsList[currentIndex],
				(GenericLightningParams)_lightningsListBackup[currentIndex]
			);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}
	}
}
