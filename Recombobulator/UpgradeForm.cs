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
    public partial class UpgradeForm : Form
    {
        public string FilePath
        {
            get { return fileNameTextBox.Text; }
            set { fileNameTextBox.Text = value; }
        }
        public ushort StartingTextureIndex = 0;

        Repository _repository = null;

        public UpgradeForm()
        {
            InitializeComponent();
        }

        private void TextureStartTextBox_Leave(object sender, EventArgs e)
        {
            NumericUpDown numeric = ((NumericUpDown)sender);
            TextBox text = (TextBox)numeric.Controls[1];
            if (text.Text == "")
            {
                text.Text = numeric.Value.ToString();
            }
        }

        public void Initialize(Repository repository, string fileName)
        {
            _repository = repository;

            textureSetCombo.Items.Add(fileName);
            textureSetCombo.SelectedIndex = 0;

            ushort index = (ushort)_repository.Textures.Count;

            textureTextBox0.Text = (index + 0).ToString();
            textureTextBox1.Text = (index + 1).ToString();
            textureTextBox2.Text = (index + 2).ToString();
            textureTextBox3.Text = (index + 3).ToString();
            textureTextBox4.Text = (index + 4).ToString();
            textureTextBox5.Text = (index + 5).ToString();
            textureTextBox6.Text = (index + 6).ToString();
            textureTextBox7.Text = (index + 7).ToString();

            StartingTextureIndex = index;
        }
    }
}
