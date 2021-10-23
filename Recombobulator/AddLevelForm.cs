using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	partial class AddLevelForm : AddFileForm
	{
		public override string RelativePath { get { return pathTextBox.Text; } }

		public bool RemovePortals { get { return removePortalsCheckBox.Checked; } }

		public bool RemoveSignals { get { return removeSignalsCheckBox.Checked; } }

		public bool RemoveEvents { get { return removeEventsCheckBox.Checked; } }

		public bool RemoveVMOs { get { return removeVMOsCheckBox.Checked; } }

		SR1Structures.StreamUnitPortalList _portalList = null;

		public AddLevelForm()
		{
			InitializeComponent();
			continueButton.Select();
		}

		public void Initialize(Repository repository, string fileName, SR1_File file)
		{
			_fileName = fileName;
			_repository = repository;

			string textureSetName = fileName;

			pathTextBox.Text = _repository.MakeLevelFilePath(fileName);
			FullPath = _repository.MakeLevelFilePath(fileName, true);

			Level existingLevel = repository.Levels.Levels.Find(x => x.UnitName == fileName);
			if (existingLevel != null && existingLevel.TextureSet != "")
			{
				textureSetName = existingLevel.TextureSet;
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

			foreach (string objectName in file._ObjectNames)
			{
				string lowerCase = objectName.ToLower();
				if (_repository.Objects.Find(x => x.ObjectName == lowerCase) == null)
				{
					requiredObjectList.Items.Add(lowerCase);
				}
			}

			SR1Structures.Level level = (SR1Structures.Level)file._Structures[0];
			SR1Structures.Terrain terrain = (SR1Structures.Terrain)file._Structures[level.terrain.Offset];
			_portalList = (SR1Structures.StreamUnitPortalList)file._Structures[terrain.StreamUnits.Offset];

			this.portalList.Items.Clear();
			foreach (SR1Structures.StreamUnitPortal portal in _portalList.portals.List)
			{
				this.portalList.Items.Add(portal.tolevelname);
				this.portalList.SetItemChecked(this.portalList.Items.Count - 1, true);
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

			pathTextBox.Text = _repository.MakeLevelFilePath(fileName);
			FullPath = _repository.MakeLevelFilePath(fileName, true);

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

			_fileName = fileName;
		}

		private void removePortalsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			portalList.Enabled = !removePortalsCheckBox.Checked;
		}

		private void portalList_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			_portalList.portals[e.Index].OmitFromMigration = e.NewValue != CheckState.Checked;
		}
	}
}
