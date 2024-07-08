using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;

namespace Recombobulator.ParticlePanels
{
    public partial class EditBlastRingsPanel : EditPanelBase
    {
        SR1_StructureSeries<GenericBlastRingParams> _blastRingsList = null;
        SR1_StructureSeries<GenericBlastRingParams> _blastRingsListBackup = null;
        GenericBlastRingParams _copiedBlastRingParams = new GenericBlastRingParams();

        public EditBlastRingsPanel()
        {
            InitializeComponent();
        }

        public void Open(object blastRingsList, object blastRingsListBackup = null)
        {
            _blastRingsList = (SR1_StructureSeries<GenericBlastRingParams>)blastRingsList;
            _blastRingsListBackup = (SR1_StructureSeries<GenericBlastRingParams>)blastRingsListBackup;

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.Items.Clear();
            for (int i = 0; i < _blastRingsList.Count; i++)
            {
                selectionComboBox.Items.Add("blastRing-" + i.ToString());
            }

            selectionComboBox.SelectedIndex = 0;
        }

        private void startColor_TextChanged(object sender, EventArgs e)
        {
            int startColor = 0;

            foreach (Control control in fieldsPanel.Controls)
            {
                if (control.Name == "startColor")
				{
					int.TryParse(control.Text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out startColor);
					startColor |= unchecked((int)0xFF000000);
				}
            }

            startColorBox.BackColor = Color.FromArgb(startColor);
        }

        private void endColor_TextChanged(object sender, EventArgs e)
		{
			int endColor = 0;

			foreach (Control control in fieldsPanel.Controls)
			{
				if (control.Name == "endColor")
				{
                    int.TryParse(control.Text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out endColor);
                    endColor |= unchecked((int)0xFF000000);
				}
			}

			endColorBox.BackColor = Color.FromArgb(endColor);
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

			startColorBox.Visible = false;
			endColorBox.Visible = false;

			fieldsPanel.Controls.Clear();
			fieldsPanel.RowStyles.Clear();

			if (newIndex < 0)
			{
				return;
			}

			addButton.Enabled = true;
			copyButton.Enabled = true;
			pasteButton.Enabled = true;

			if (_blastRingsListBackup != null)
			{
				if (newIndex < _blastRingsListBackup.Count)
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

			startColorBox.Visible = true;
			endColorBox.Visible = true;

			List<SR1_Structure> membersRead = _blastRingsList[newIndex].MembersRead;
			fieldsPanel.RowCount = membersRead.Count;

			int row = 0;
			int startColor = 0;
			int endColor = 0;
			foreach (SR1_Structure structure in membersRead)
			{
				TextBox valueTextBox = AddField(structure, ref row);

				if (structure.Name == "startColor")
				{
					startColor = ((SR1_Primative<int>)structure).Value;
					valueTextBox.TextChanged += startColor_TextChanged;
				}

				if (structure.Name == "endColor")
				{
					endColor = ((SR1_Primative<int>)structure).Value;
					valueTextBox.TextChanged += endColor_TextChanged;
				}
			}

			startColor |= unchecked((int)0xFF000000);
			endColor |= unchecked((int)0xFF000000);

			startColorBox.BackColor = Color.FromArgb(startColor);
			endColorBox.BackColor = Color.FromArgb(endColor);

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}

		private void addButton_Click(object sender, EventArgs e)
        {
            _blastRingsList.Add(new GenericBlastRingParams());
            selectionComboBox.SelectedIndex = _blastRingsList.Count - 1;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0)
            {
                return;
            }

            currentIndex = Math.Min(currentIndex, _blastRingsList.Count);

            _blastRingsList.InsertAt(currentIndex, new GenericBlastRingParams());
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

            _blastRingsList.RemoveAt(currentIndex);
            selectionComboBox.SelectedIndex = -1;

            if (selectionComboBox.Items.Count > 0)
            {
                currentIndex = Math.Min(currentIndex, _blastRingsList.Count - 1);
                selectionComboBox.SelectedIndex = currentIndex;
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _blastRingsList.Count)
            {
                return;
            }

            GenericBlastRingParams.Copy(_copiedBlastRingParams, (GenericBlastRingParams)_blastRingsList[currentIndex]);
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _blastRingsList.Count)
            {
                return;
            }

            GenericBlastRingParams.Copy((GenericBlastRingParams)_blastRingsList[currentIndex], _copiedBlastRingParams);

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (_blastRingsListBackup == null ||
                currentIndex < 0 ||
                currentIndex >= _blastRingsList.Count ||
                currentIndex >= _blastRingsListBackup.Count)
            {
                return;
            }

            GenericBlastRingParams.Copy(
                (GenericBlastRingParams)_blastRingsList[currentIndex],
                (GenericBlastRingParams)_blastRingsListBackup[currentIndex]
            );

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }
    }
}
