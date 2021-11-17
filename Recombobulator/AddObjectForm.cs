using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	partial class AddObjectForm : AddFileForm
	{
		public override string RelativePath { get { return pathTextBox.Text; } }

		public AddObjectForm()
		{
			InitializeComponent();
			continueButton.Select();
		}

		public void Initialize(Repository repository, string fileName)
		{
			FileName = fileName;
			_repository = repository;

			string textureSetName = fileName;

			pathTextBox.Text = _repository.MakeObjectFilePath(fileName);
			FullPath = _repository.MakeObjectFilePath(fileName, true);

			SR1Repository.Object existingObject = repository.Objects.Objects.Find(x => x.ObjectName == fileName);
			if (existingObject != null && existingObject.TextureSet != "")
			{
				textureSetName = existingObject.TextureSet;
			}

			TexSet currentTextureSet = null;
			foreach (TexSet textureSet in _repository.TextureSets.TexSets)
			{
				textureSetCombo.Items.Add(textureSet.Name);

				if (textureSet.Name == textureSetName)
				{
					currentTextureSet = textureSet;
				}
			}

			if (currentTextureSet != null)
			{
				textureSetCombo.SelectedIndex = currentTextureSet.Index;
			}
			else
			{
				textureSetCombo.Items.Add(fileName);
				textureSetCombo.SelectedIndex = _repository.TextureSets.Count;
			}
		}

		private void textureSetCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = ((ComboBox)sender).SelectedIndex;
			TextureSet = selectedIndex;
		}

		private void textureList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (e.IsSelected)
			{
				e.Item.Selected = false;
			}
		}

		private void renameButton_Click(object sender, EventArgs e)
		{
			RenameForm renameForm = new RenameForm();
			renameForm.NewName = "";
			renameForm.Text = "Rename";
			renameForm.Message = "Please select a new name.";
			if (renameForm.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}

			while (true)
			{
				string filePath = _repository.MakeObjectFilePath(renameForm.NewName);

				uint fileHash = Repository.GetSR1HashName(filePath);
				if (_repository.Assets.Assets.Find(x => x.FileHash == fileHash) != null)
				{
					renameForm.NewName = "";
					renameForm.Text = "Existing File";
					renameForm.Message = "File already exists!\r\nPlease select a new name.";
					if (renameForm.ShowDialog() == DialogResult.Cancel)
					{
						return;
					}
				}
				else
				{
					break;
				}
			}

			string fileName = renameForm.NewName;

			pathTextBox.Text = _repository.MakeObjectFilePath(fileName);
			FullPath = _repository.MakeObjectFilePath(fileName, true);

			string initialTextureSet = textureSetCombo.Items[_repository.TextureSets.Count].ToString();
			SR1Repository.TexSet existingObject = _repository.TextureSets.Find(x => x.Name == initialTextureSet);
			if (existingObject == null)
			{
				bool newSetSelected = (textureSetCombo.SelectedIndex == _repository.TextureSets.Count);
				textureSetCombo.Items.RemoveAt(_repository.TextureSets.Count);
				textureSetCombo.Items.Add(fileName);
				if (newSetSelected)
				{
					textureSetCombo.SelectedIndex = _repository.TextureSets.Count;
				}
			}

			FileName = fileName;
		}
	}
}
