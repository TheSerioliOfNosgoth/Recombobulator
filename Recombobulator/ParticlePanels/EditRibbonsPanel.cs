using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;

namespace Recombobulator.ParticlePanels
{
    public partial class EditRibbonsPanel : EditPanelBase
    {
        SR1_StructureSeries<GenericRibbonParams> _ribbonsList = null;
        SR1_StructureSeries<GenericRibbonParams> _ribbonsListBackup = null;
        GenericRibbonParams _copiedRibbonParams = new GenericRibbonParams();

        public EditRibbonsPanel()
        {
            InitializeComponent();
        }

        public void Open(object ribbonsList, object ribbonsListBackup = null)
        {
            _ribbonsList = (SR1_StructureSeries<GenericRibbonParams>)ribbonsList;
            _ribbonsListBackup = (SR1_StructureSeries<GenericRibbonParams>)ribbonsListBackup;

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.Items.Clear();
            for (int i = 0; i < _ribbonsList.Count; i++)
            {
                selectionComboBox.Items.Add("ribbon-" + i.ToString());
            }

            selectionComboBox.SelectedIndex = 0;
        }

        protected override bool SetValue(TextBox textBox, SR1_PrimativeBase primitive)
        {
            bool result = base.SetValue(textBox, primitive);
            if (!result)
            {
				return result;
			}

			if (TrySetColor("startColor", textBox, startColorBox))
			{
				return result;
			}

			if (TrySetColor("endColor", textBox, endColorBox))
			{
				return result;
			}

			return result;
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
            _structures.Clear();

            if (newIndex < 0)
            {
                return;
            }

            addButton.Enabled = true;
            copyButton.Enabled = true;
            pasteButton.Enabled = true;

            if (_ribbonsListBackup != null)
            {
                if (newIndex < _ribbonsListBackup.Count)
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

            List<SR1_Structure> membersRead = _ribbonsList[newIndex].MembersRead;
            fieldsPanel.RowCount = membersRead.Count;

            int row = 0;
            foreach (SR1_Structure structure in membersRead)
            {
                TextBox valueTextBox = AddField(structure, ref row);

                if (structure.Name == "startColor" ||
                    structure.Name == "endColor")
                {
                    SetValue(valueTextBox, structure as SR1_PrimativeBase);
                }
            }

            fieldsPanel.Visible = true;
            fieldsPanel.Enabled = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            _ribbonsList.Add(new GenericRibbonParams());
            selectionComboBox.SelectedIndex = _ribbonsList.Count - 1;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0)
            {
                return;
            }

            currentIndex = Math.Min(currentIndex, _ribbonsList.Count);

            _ribbonsList.InsertAt(currentIndex, new GenericRibbonParams());
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

            _ribbonsList.RemoveAt(currentIndex);
            selectionComboBox.SelectedIndex = -1;

            if (selectionComboBox.Items.Count > 0)
            {
                currentIndex = Math.Min(currentIndex, _ribbonsList.Count - 1);
                selectionComboBox.SelectedIndex = currentIndex;
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _ribbonsList.Count)
            {
                return;
            }

            GenericRibbonParams.Copy(_copiedRibbonParams, (GenericRibbonParams)_ribbonsList[currentIndex]);
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _ribbonsList.Count)
            {
                return;
            }

            GenericRibbonParams.Copy((GenericRibbonParams)_ribbonsList[currentIndex], _copiedRibbonParams);

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (_ribbonsListBackup == null ||
                currentIndex < 0 ||
                currentIndex >= _ribbonsList.Count ||
                currentIndex >= _ribbonsListBackup.Count)
            {
                return;
            }

            GenericRibbonParams.Copy(
                (GenericRibbonParams)_ribbonsList[currentIndex],
                (GenericRibbonParams)_ribbonsListBackup[currentIndex]
            );

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }
    }
}
