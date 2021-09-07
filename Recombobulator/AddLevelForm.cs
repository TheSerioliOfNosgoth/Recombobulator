﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
    public partial class AddLevelForm : AddFileForm
    {
        public override string RelativePath { get { return pathTextBox.Text; } }

        public bool RemovePortals { get { return removePortalsCheckBox.Checked; } }

        public bool RemoveSignals { get { return removeSignalsCheckBox.Checked; } }

        public bool RemoveEvents { get { return removeEventsCheckBox.Checked; } }

        public bool RemoveVMOs { get { return removeVMOsCheckBox.Checked; } }

        public AddLevelForm()
        {
            InitializeComponent();
            continueButton.Select();
        }

        public void Initialize(Repository repository, string fileName, List<string> requiredObjects)
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

            foreach (string objectName in requiredObjects)
            {
                string lowerCase = objectName.ToLower();
                if (_repository.Objects.Find(x => x.ObjectName == lowerCase) == null)
                {
                    requiredObjectList.Items.Add(lowerCase);
                }
            }
        }

        private void textureSetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            if (selectedIndex < _repository.TextureSets.Count)
            {
                TexSet textureSet = _repository.TextureSets.TexSets[selectedIndex];
                textureList.Items.Clear();
                foreach (ushort textureID in textureSet.TextureIDs)
                {
                    textureList.Items.Add(_repository.MakeTextureFilePath(textureID));
                }
            }
            else
            {
                ushort textureIndex = (ushort)_repository.Textures.Count;
                for (int t = 0; t < 8; t++)
                {
                    textureList.Items.Add(_repository.MakeTextureFilePath(textureIndex + t));
                }
            }

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
    }
}
