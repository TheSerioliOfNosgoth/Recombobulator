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

namespace Recombobulator
{
    public partial class UpgradeForm : Form
    {
        public ushort StartingTextureIndex
        {
            get { return (ushort)textureStartTextBox.Value; }
            set { textureStartTextBox.Value = value; }
        }
        public string FileName
        {
            get { return fileNameTextBox.Text; }
            set { fileNameTextBox.Text = value; }
        }

        public UpgradeForm()
        {
            InitializeComponent();
            textureStartTextBox.Controls[0].Visible = false;
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

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            try
            {
                string directory = Path.GetDirectoryName(fileNameTextBox.Text);
                if (Directory.Exists(directory))
                {
                    dialog.InitialDirectory = directory;
                }
            }
            catch
            {
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileNameTextBox.Text = dialog.FileName;
            }
        }

        public void SetTextureStartingIndex(ushort index)
        {
            textureStartTextBox.Value = index;

            textureTextBox0.Text = (index + 0).ToString();
            textureTextBox1.Text = (index + 1).ToString();
            textureTextBox2.Text = (index + 2).ToString();
            textureTextBox3.Text = (index + 3).ToString();
            textureTextBox4.Text = (index + 4).ToString();
            textureTextBox5.Text = (index + 5).ToString();
            textureTextBox6.Text = (index + 6).ToString();
            textureTextBox7.Text = (index + 7).ToString();
        }
    }
}
