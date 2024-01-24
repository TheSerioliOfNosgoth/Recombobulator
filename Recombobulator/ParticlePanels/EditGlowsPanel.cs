using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Recombobulator.ParticlePanels
{
    public partial class EditGlowsPanel : EditPanelBase
    {
        SR1_StructureSeries<GenericGlowParams> _glowsList = null;
        SR1_StructureSeries<GenericGlowParams> _glowsListBackup = null;
        GenericGlowParams _copiedGlowParams = new GenericGlowParams();

        public EditGlowsPanel()
        {
            InitializeComponent();
        }

        public void Open(object glowsList, object glowsListBackup = null)
        {
            _glowsList = (SR1_StructureSeries<GenericGlowParams>)glowsList;
            _glowsListBackup = (SR1_StructureSeries<GenericGlowParams>)glowsListBackup;

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.Items.Clear();
            for (int i = 0; i < _glowsList.Count; i++)
            {
                selectionComboBox.Items.Add("glow-" + i.ToString());
            }

            selectionComboBox.SelectedIndex = 0;
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

			fieldsPanel.Controls.Clear();
			fieldsPanel.RowStyles.Clear();

			if (newIndex < 0)
			{
				return;
			}

			addButton.Enabled = true;
			copyButton.Enabled = true;
			pasteButton.Enabled = true;

			if (_glowsListBackup != null)
			{
				if (newIndex < _glowsListBackup.Count)
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

			List<SR1_Structure> membersRead = _glowsList[newIndex].MembersRead;
			fieldsPanel.RowCount = membersRead.Count;

			int row = 0;
			foreach (SR1_Structure structure in membersRead)
			{
				TextBox valueTextBox = AddField(structure, "", ref row);
			}

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}

		private void addButton_Click(object sender, EventArgs e)
        {
            _glowsList.Add(new GenericGlowParams());
            selectionComboBox.SelectedIndex = _glowsList.Count - 1;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0)
            {
                return;
            }

            currentIndex = Math.Min(currentIndex, _glowsList.Count);

            _glowsList.InsertAt(currentIndex, new GenericGlowParams());
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

            _glowsList.RemoveAt(currentIndex);
            selectionComboBox.SelectedIndex = -1;

            if (selectionComboBox.Items.Count > 0)
            {
                currentIndex = Math.Min(currentIndex, _glowsList.Count - 1);
                selectionComboBox.SelectedIndex = currentIndex;
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _glowsList.Count)
            {
                return;
            }

            GenericGlowParams.Copy(_copiedGlowParams, (GenericGlowParams)_glowsList[currentIndex]);
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _glowsList.Count)
            {
                return;
            }

            GenericGlowParams.Copy((GenericGlowParams)_glowsList[currentIndex], _copiedGlowParams);

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (_glowsListBackup == null ||
                currentIndex < 0 ||
                currentIndex >= _glowsList.Count ||
                currentIndex >= _glowsListBackup.Count)
            {
                return;
            }

            GenericGlowParams.Copy(
                (GenericGlowParams)_glowsList[currentIndex],
                (GenericGlowParams)_glowsListBackup[currentIndex]
            );

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }
    }
}
