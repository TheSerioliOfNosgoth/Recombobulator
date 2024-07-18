using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;
using System.Globalization;
using System.Linq;

namespace Recombobulator.ParticlePanels
{
    public partial class EditParticlesPanel : EditPanelBase
    {
        SR1_StructureSeries<GenericParticleParams> _particlesList = null;
        SR1_StructureSeries<GenericParticleParams> _particlesListBackup = null;
        GenericParticleParams _copiedParticleParams = new GenericParticleParams();

        public EditParticlesPanel()
        {
            InitializeComponent();
        }

        public void Open(object particlesList, object particlesListBackup = null)
        {
            _particlesList = (SR1_StructureSeries<GenericParticleParams>)particlesList;
            _particlesListBackup = (SR1_StructureSeries<GenericParticleParams>)particlesListBackup;

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.Items.Clear();
            for (int i = 0; i < _particlesList.Count; i++)
            {
                selectionComboBox.Items.Add("particle-" + i.ToString());
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

			bool isStartColor = textBox.Name.StartsWith("startColor_");
            bool isEndColor = textBox.Name.StartsWith("endColor_");

            if (isStartColor || isEndColor)
            {
                byte red = 0;
                byte green = 0;
                byte blue = 0;

                foreach (Control control in fieldsPanel.Controls)
                {
                    if ((isStartColor && control.Name.StartsWith("startColor_")) ||
                        (isEndColor && control.Name.StartsWith("endColor_")))
                    {
                        if (control.Name.EndsWith("_red"))
                        {
                            byte.TryParse(control.Text, out red);
                        }
                        else if (control.Name.EndsWith("_green"))
                        {
                            byte.TryParse(control.Text, out green);
                        }
                        else if (control.Name.EndsWith("_blue"))
                        {
                            byte.TryParse(control.Text, out blue);
                        }
                    }
                }

                if (isStartColor)
                {
                    startColorBox.BackColor = Color.FromArgb(red, green, blue);
                }
                else if (isEndColor)
                {
                    endColorBox.BackColor = Color.FromArgb(red, green, blue);
                }
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

            if (_particlesListBackup != null)
            {
                if (newIndex < _particlesListBackup.Count)
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

            List<SR1_Structure> membersRead = _particlesList[newIndex].MembersRead;
            fieldsPanel.RowCount = membersRead.Count;

            int row = 0;
            foreach (SR1_Structure structure in membersRead)
            {
                TextBox valueTextBox = AddField(structure, ref row);

                if (structure.Name.StartsWith("startColor_") ||
                    structure.Name.StartsWith("endColor_"))
                {
                    SetValue(valueTextBox, structure as SR1_PrimativeBase);
                }
            }

            fieldsPanel.Visible = true;
            fieldsPanel.Enabled = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            _particlesList.Add(new GenericParticleParams());
            selectionComboBox.SelectedIndex = _particlesList.Count - 1;
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0)
            {
                return;
            }

            currentIndex = Math.Min(currentIndex, _particlesList.Count);

            _particlesList.InsertAt(currentIndex, new GenericParticleParams());
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

            _particlesList.RemoveAt(currentIndex);
            selectionComboBox.SelectedIndex = -1;

            if (selectionComboBox.Items.Count > 0)
            {
                currentIndex = Math.Min(currentIndex, _particlesList.Count - 1);
                selectionComboBox.SelectedIndex = currentIndex;
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _particlesList.Count)
            {
                return;
            }

            GenericParticleParams.Copy(_copiedParticleParams, (GenericParticleParams)_particlesList[currentIndex]);
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (currentIndex < 0 ||
                currentIndex >= _particlesList.Count)
            {
                return;
            }

            GenericParticleParams.Copy((GenericParticleParams)_particlesList[currentIndex], _copiedParticleParams);

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            int currentIndex = selectionComboBox.SelectedIndex;

            if (_particlesListBackup == null ||
                currentIndex < 0 ||
                currentIndex >= _particlesList.Count ||
                currentIndex >= _particlesListBackup.Count)
            {
                return;
            }

            GenericParticleParams.Copy(
                (GenericParticleParams)_particlesList[currentIndex],
                (GenericParticleParams)_particlesListBackup[currentIndex]
            );

            selectionComboBox.SelectedIndex = -1;
            selectionComboBox.SelectedIndex = currentIndex;
        }
    }
}
