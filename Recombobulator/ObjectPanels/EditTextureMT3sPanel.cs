using Recombobulator.SR1Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Recombobulator.ParticlePanels
{
	public partial class EditTextureMT3sPanel : EditPanelBase
	{
		SR1_StructureSeries<TextureMT3> _texturesList = null;
		SR1_StructureSeries<TextureMT3> _texturesListBackup = null;
		TextureMT3 _copiedTexture = new TextureMT3();

		public EditTextureMT3sPanel()
		{
			InitializeComponent();
		}

		public void Open(object texturesList, object texturesListBackup = null)
		{
			_texturesList = (SR1_StructureSeries<TextureMT3>)texturesList;
			_texturesListBackup = (SR1_StructureSeries<TextureMT3>)texturesListBackup;

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.Items.Clear();
			for (int i = 0; i < _texturesList.Count; i++)
			{
				selectionComboBox.Items.Add("texture-" + i.ToString());
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

			if (_texturesListBackup != null)
			{
				if (newIndex < _texturesListBackup.Count)
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

			List<SR1_Structure> membersRead = _texturesList[newIndex].MembersRead;
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
			_texturesList.Add(new TextureMT3());
			selectionComboBox.SelectedIndex = _texturesList.Count - 1;
		}

		private void insertButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0)
			{
				return;
			}

			currentIndex = Math.Min(currentIndex, _texturesList.Count);

			_texturesList.InsertAt(currentIndex, new TextureMT3());
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

			_texturesList.RemoveAt(currentIndex);
			selectionComboBox.SelectedIndex = -1;

			if (selectionComboBox.Items.Count > 0)
			{
				currentIndex = Math.Min(currentIndex, _texturesList.Count - 1);
				selectionComboBox.SelectedIndex = currentIndex;
			}
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _texturesList.Count)
			{
				return;
			}

			TextureMT3.Copy(_copiedTexture, (TextureMT3)_texturesList[currentIndex]);
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _texturesList.Count)
			{
				return;
			}

			TextureMT3.Copy((TextureMT3)_texturesList[currentIndex], _copiedTexture);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (_texturesListBackup == null ||
				currentIndex < 0 ||
				currentIndex >= _texturesList.Count ||
				currentIndex >= _texturesListBackup.Count)
			{
				return;
			}

			TextureMT3.Copy(
				(TextureMT3)_texturesList[currentIndex],
				(TextureMT3)_texturesListBackup[currentIndex]
			);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}
	}
}
