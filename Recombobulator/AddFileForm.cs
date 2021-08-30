using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
    public partial class AddFileForm : Form
    {
        public string FullPath { get; private set; } = "";
        public string RelativePath { get { return pathTextBox.Text; } }

        public int TextureSet { get; private set; } = 0;

        public bool RemovePortals { get { return removePortalsCheckBox.Checked; } }

        public bool RemoveSignals { get { return removeSignalsCheckBox.Checked; } }

        public bool RemoveEvents { get { return removeEventsCheckBox.Checked; } }

        public bool RemoveVMOs { get { return removeVMOsCheckBox.Checked; } }

        Repository _repository = null;

        public AddFileForm()
        {
            InitializeComponent();
        }

        public void Initialize(Repository repository, string fileName, bool isLevel, List<string> requiredObjects)
        {
            _repository = repository;

            string textureSetName = fileName;

            if (isLevel)
            {
                pathTextBox.Text = _repository.MakeLevelFilePath(fileName);
                FullPath = _repository.MakeLevelFilePath(fileName, true);

                Level existingLevel = repository.Levels.Levels.Find(x => x.UnitName == fileName);
                if (existingLevel != null && existingLevel.TextureSet != "")
                {
                    textureSetName = existingLevel.TextureSet;
                }
            }
            else
            {
                pathTextBox.Text = _repository.MakeObjectFilePath(fileName);
                FullPath = _repository.MakeObjectFilePath(fileName, true);

                SR1Repository.Object existingObject = repository.Objects.Objects.Find(x => x.ObjectName == fileName);
                if (existingObject != null && existingObject.TextureSet != "")
                {
                    textureSetName = existingObject.TextureSet;
                }
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
    }
}
