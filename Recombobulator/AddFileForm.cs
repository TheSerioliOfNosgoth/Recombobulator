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

        public int FileID { get; set; } = 0;

        public int TextureSet { get; private set; } = 0;

        Repository _repository = null;

        public AddFileForm()
        {
            InitializeComponent();

            string a = null;
            string b = null;
            System.Reflection.ConstructorInfo cinfo = typeof(TreeList.TreeListColumn).GetConstructor(new Type[] { typeof(string), typeof(string) });
            object o = new System.ComponentModel.Design.Serialization.InstanceDescriptor(cinfo, new object[] { a, b }, false);
        }

        public void Initialize(Repository repository, string fileName, bool isLevel, List<string> requiredObjects)
        {
            _repository = repository;

            if (isLevel)
            {
                pathTextBox.Text = _repository.MakeAreaFilePath(fileName);
                FullPath = _repository.MakeAreaFilePath(fileName, true);

                // Could find an unused one or even validate overwrites of existing levels.
                FileID = _repository.Levels.NextAvailableID;
            }
            else
            {
                pathTextBox.Text = _repository.MakeObjectFilePath(fileName);
                FullPath = _repository.MakeObjectFilePath(fileName, true);
            }

            TexSet currentTextureSet = null;
            foreach (TexSet textureSet in _repository.TextureSets.TexSets)
            {
                textureSetCombo.Items.Add(textureSet.Name);

                if (textureSet.Name == fileName)
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
