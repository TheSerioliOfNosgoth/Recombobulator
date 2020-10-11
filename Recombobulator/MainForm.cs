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
    public partial class MainForm : Form
    {
        SR1_File _file = new SR1_File();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                treeListView.BeginUpdate();
                treeListView.Nodes.Clear();
                summary.Text = "";

                try
                {
                    _file.Import(dialog.FileName, SR1_File.ImportFlags.LogErrors | SR1_File.ImportFlags.LogScripts);

                    exportToolStripMenuItem.Enabled = true;
                    exportUpgradedToolStripMenuItem.Enabled = true;

                    treeListView.Nodes.AddRange(_file.CreateChunkNodes());

                    string textureIDs = "\r\n\r\nTexture IDs:\r\n\r\n";
                    foreach (ushort textureID in _file._TextureIDs)
                    {
                        textureIDs += "Texture-" + textureID.ToString("00000") + "\r\n";
                    }

                    scripts.Text = _file.GetScripts();
                    summary.Text = _file.GetErrors() + textureIDs;
                }
                catch (Exception exception)
                {

                }

                treeListView.EndUpdate();

                displayModeTabs.SelectedTab = fileDataTab;
            }
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dialog.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(_file._FileName));
                    _file.Export(dialog.FileName);
                }
                catch (Exception exception)
                {

                }
            }
        }

        private void ExportUpgradedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpgradeForm upgradeDialog = new UpgradeForm();
            upgradeDialog.StartingTextureIndex = 1287;
            //upgradeDialog.FileName = _file._FileName;
            upgradeDialog.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(_file._FileName));
            if (upgradeDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _file.Export(upgradeDialog.FileName, SR1_File.Version.Retail_PC, upgradeDialog.StartingTextureIndex);
                }
                catch (Exception exception)
                {

                }
            }

            upgradeDialog.Dispose();
        }

        private void DetailedExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select a folder to test.";
            dialog.ShowNewFolderButton = false;

            string recentFolder = Properties.Settings.Default.RecentFolder;
            if (recentFolder != null && System.IO.Directory.Exists(recentFolder))
            {
                dialog.SelectedPath = recentFolder;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                testResults.Clear();
                string[] exportTestResults = SR1_File.TestFolder(dialog.SelectedPath, true);
                foreach (string result in exportTestResults)
                {
                    testResults.Text += result + "\r\n";
                }
                displayModeTabs.SelectedTab = testResultsTab;
            }

            Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void BriefExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select a folder to test.";
            dialog.ShowNewFolderButton = false;

            string recentFolder = Properties.Settings.Default.RecentFolder;
            if (recentFolder != null && System.IO.Directory.Exists(recentFolder))
            {
                dialog.SelectedPath = recentFolder;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                testResults.Clear();
                string[] exportTestResults = SR1_File.TestFolder(dialog.SelectedPath, false);
                foreach (string result in exportTestResults)
                {
                    testResults.Text += result + "\r\n";
                }
                displayModeTabs.SelectedTab = testResultsTab;
            }

            Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
            Properties.Settings.Default.Save();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
