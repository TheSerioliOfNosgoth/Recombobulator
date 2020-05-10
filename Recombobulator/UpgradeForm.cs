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
    }
}
