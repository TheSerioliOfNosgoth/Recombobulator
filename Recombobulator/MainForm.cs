using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
    public partial class MainForm : Form
    {
        SR1_File _file = new SR1_File();
        bool _fileLoaded = false;

        Repository _repository = null;
        ProgressWindow _progressWindow = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
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

                    testExportToolStripMenuItem.Enabled = true;
                    addToProjectToolStripMenuItem.Enabled = true;

                    treeListView.Nodes.AddRange(_file.CreateChunkNodes());

                    string textureIDs = "\r\n\r\nTexture IDs:\r\n\r\n";
                    foreach (ushort textureID in _file._TextureIDs)
                    {
                        textureIDs += "Texture-" + textureID.ToString("00000") + "\r\n";
                    }

                    scripts.Text = _file.GetScripts();
                    summary.Text = _file.GetErrors() + textureIDs;

                    _fileLoaded = true;
                    if (_repository != null && _fileLoaded == true)
                    {
                        addToProjectToolStripMenuItem.Enabled = true;
                    }
                }
                catch (Exception exception)
                {

                }

                treeListView.EndUpdate();

                displayModeTabs.SelectedTab = fileDataTab;
            }
        }

        private void TestExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dialog.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(_file._FilePath));
                    _file.Export(dialog.FileName);
                }
                catch (Exception exception)
                {

                }
            }
        }

        private void AddToProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpgradeForm upgradeDialog = new UpgradeForm();
            string rawFileName = Path.GetFileNameWithoutExtension(_file._FilePath);
            upgradeDialog.Initialize(_repository, rawFileName); 
            upgradeDialog.FilePath = _file._FilePath;
            upgradeDialog.FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Path.GetFileName(_file._FilePath));
            if (upgradeDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _file.Export(upgradeDialog.FilePath, SR1_File.Version.Retail_PC, upgradeDialog.StartingTextureIndex);
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

        private void BeginUnpacking()
        {
            if (_progressWindow != null)
            {
                _progressWindow.Dispose();
            }
            _progressWindow = new ProgressWindow();
            _progressWindow.Title = "Unpacking Repository...";
            _progressWindow.SetMessage("");
            //_progressWindow.Icon = this.Icon;
            _progressWindow.Owner = this;
            _progressWindow.TopLevel = true;
            _progressWindow.ShowInTaskbar = false;
            this.Enabled = false;
            _progressWindow.Show();
        }

        private void EndUnpacking()
        {
            if (_repository != null && _fileLoaded == true)
            {
                addToProjectToolStripMenuItem.Enabled = true;
            }

            Enabled = true;
            _progressWindow.Hide();
            _progressWindow.Dispose();
        }

        private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;

            string recentFolder = Properties.Settings.Default.RecentFolder;
            if (recentFolder != null && System.IO.Directory.Exists(recentFolder))
            {
                folderDialog.SelectedPath = recentFolder;
            }

            DialogResult dialogResult = folderDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            Properties.Settings.Default.RecentFolder = folderDialog.SelectedPath;
            Properties.Settings.Default.Save();

            Repository repository = new Repository(folderDialog.SelectedPath);

            Invoke(new MethodInvoker(BeginUnpacking));

            Thread loadingThread = new Thread((() =>
            {
                repository.UnpackRepository();

                _repository = repository;
                Invoke(new MethodInvoker(EndUnpacking));
            }));

            loadingThread.Name = "LoadingThread";
            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            //loadingThread.Join();

            Thread progressThread = new Thread((() =>
            {
                do
                {
                    // Do I really want to be locking the whole thing?
                    lock (repository)
                    {
                        _progressWindow.SetMessage(repository.RecentMessage);
                        if (repository.FilesToRead > 0)
                        {
                            _progressWindow.SetProgress((100 * repository.FilesRead) / repository.FilesToRead);
                        }
                        else
                        {
                            _progressWindow.SetProgress(0);
                        }
                    }
                    Thread.Sleep(20);
                }
                while (loadingThread.IsAlive);
            }));

            progressThread.Name = "ProgressThread";
            progressThread.SetApartmentState(ApartmentState.STA);
            progressThread.Start();
        }

        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;

            string recentFolder = Properties.Settings.Default.RecentFolder;
            if (recentFolder != null && System.IO.Directory.Exists(recentFolder))
            {
                folderDialog.SelectedPath = recentFolder;
            }

            DialogResult dialogResult = folderDialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            Properties.Settings.Default.RecentFolder = folderDialog.SelectedPath;
            Properties.Settings.Default.Save();

            Repository repository = new Repository(folderDialog.SelectedPath);
            repository.LoadRepository();
            _repository = repository;

            if (_repository != null && _fileLoaded == true)
            {
                addToProjectToolStripMenuItem.Enabled = true;
            }
        }
    }
}
