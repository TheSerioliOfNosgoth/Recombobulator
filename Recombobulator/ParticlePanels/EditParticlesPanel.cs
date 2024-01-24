using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System;

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

        private void startColor_TextChanged(object sender, EventArgs e)
        {
            byte startRed = 0;
            byte startGreen = 0;
            byte startBlue = 0;

            foreach (Control control in fieldsPanel.Controls)
            {
                if (control.Name == "startColor_red")
                {
                    byte.TryParse(control.Text, out startRed);
                }

                if (control.Name == "startColor_green")
                {
                    byte.TryParse(control.Text, out startGreen);
                }

                if (control.Name == "startColor_blue")
                {
                    byte.TryParse(control.Text, out startBlue);
                }
            }

            startColorBox.BackColor = Color.FromArgb(startRed, startGreen, startBlue);
        }

        private void endColor_TextChanged(object sender, EventArgs e)
        {
            byte endRed = 0;
            byte endGreen = 0;
            byte endBlue = 0;

            foreach (Control control in fieldsPanel.Controls)
            {
                if (control.Name == "endColor_red")
                {
                    byte.TryParse(control.Text, out endRed);
                }

                if (control.Name == "endColor_green")
                {
                    byte.TryParse(control.Text, out endGreen);
                }

                if (control.Name == "endColor_blue")
                {
                    byte.TryParse(control.Text, out endBlue);
                }
            }

            endColorBox.BackColor = Color.FromArgb(endRed, endGreen, endBlue);
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
			int startRed = 0;
			int startGreen = 0;
			int startBlue = 0;
			int endRed = 0;
			int endGreen = 0;
			int endBlue = 0;
			foreach (SR1_Structure structure in membersRead)
			{
				TextBox valueTextBox = AddField(structure, "", ref row);

				if (structure.Name == "startColor_red")
				{
					startRed = ((SR1_Primative<byte>)structure).Value;
					valueTextBox.TextChanged += startColor_TextChanged;
				}

				if (structure.Name == "startColor_green")
				{
					startGreen = ((SR1_Primative<byte>)structure).Value;
					valueTextBox.TextChanged += startColor_TextChanged;
				}

				if (structure.Name == "startColor_blue")
				{
					startBlue = ((SR1_Primative<byte>)structure).Value;
					valueTextBox.TextChanged += startColor_TextChanged;
				}

				if (structure.Name == "endColor_red")
				{
					endRed = ((SR1_Primative<byte>)structure).Value;
					valueTextBox.TextChanged += endColor_TextChanged;
				}

				if (structure.Name == "endColor_green")
				{
					endGreen = ((SR1_Primative<byte>)structure).Value;
				}

				if (structure.Name == "endColor_blue")
				{
					endBlue = ((SR1_Primative<byte>)structure).Value;
					valueTextBox.TextChanged += endColor_TextChanged;
				}
			}

			startColorBox.BackColor = Color.FromArgb(startRed, startGreen, startBlue);
			endColorBox.BackColor = Color.FromArgb(endRed, endGreen, endBlue);

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
