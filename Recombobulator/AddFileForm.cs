using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public void Initialize(Repository repository, string fileName, bool isLevel)
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
        }

        private void textureSetCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((ComboBox)sender).SelectedIndex;
            if (selectedIndex < _repository.TextureSets.Count)
            {
                TexSet textureSet = _repository.TextureSets.TexSets[selectedIndex];
                textureTextBox0.Text = (textureSet.TextureIDs[0]).ToString();
                textureTextBox1.Text = (textureSet.TextureIDs[1]).ToString();
                textureTextBox2.Text = (textureSet.TextureIDs[2]).ToString();
                textureTextBox3.Text = (textureSet.TextureIDs[3]).ToString();
                textureTextBox4.Text = (textureSet.TextureIDs[4]).ToString();
                textureTextBox5.Text = (textureSet.TextureIDs[5]).ToString();
                textureTextBox6.Text = (textureSet.TextureIDs[6]).ToString();
                textureTextBox7.Text = (textureSet.TextureIDs[7]).ToString();
            }
            else
            {
                ushort textureIndex = (ushort)_repository.Textures.Count;
                textureTextBox0.Text = textureIndex++.ToString();
                textureTextBox1.Text = textureIndex++.ToString();
                textureTextBox2.Text = textureIndex++.ToString();
                textureTextBox3.Text = textureIndex++.ToString();
                textureTextBox4.Text = textureIndex++.ToString();
                textureTextBox5.Text = textureIndex++.ToString();
                textureTextBox6.Text = textureIndex++.ToString();
                textureTextBox7.Text = textureIndex++.ToString();
            }

            TextureSet = selectedIndex;
        }
    }
}
