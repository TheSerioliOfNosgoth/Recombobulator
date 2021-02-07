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
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter =
                    "Soul Reaver PCM Files|*.pcm|" +
                    "All Files (*.*)|*.*",
                DefaultExt = "pcm",
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pcmFileTreeListView.BeginUpdate();
                pcmFileTreeListView.Nodes.Clear();
                pcmFileSummary.Text = "";

                try
                {
                    _file.Import(dialog.FileName, SR1_File.ImportFlags.LogErrors | SR1_File.ImportFlags.LogScripts);

                    pcmFileTreeListView.Nodes.AddRange(_file.CreateChunkNodes());

                    string textureIDs = "\r\n\r\nTexture IDs:\r\n\r\n";
                    foreach (ushort textureID in _file._TextureIDs)
                    {
                        textureIDs += "Texture-" + textureID.ToString("00000") + "\r\n";
                    }

                    scripts.Text = _file.GetScripts();
                    pcmFileSummary.Text = _file.GetErrors() + textureIDs;

                    _fileLoaded = true;

                    testExportToolStripMenuItem.Enabled = _fileLoaded;
                    addToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
                }
                catch (Exception exception)
                {

                }

                pcmFileTreeListView.EndUpdate();

                displayModeTabs.SelectedTab = pcmFileDataTab;
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
            string fileName = Path.GetFileNameWithoutExtension(_file._FilePath);

            AddFileForm addFileDialog = new AddFileForm();
            addFileDialog.Initialize(_repository, fileName, _file._IsLevel);
            if (addFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    #region Textures
                    bool isNewTextureSet = false;
                    TexSet textureSet = _repository.TextureSets.TexSets.Find(x => x.Index == addFileDialog.TextureSet);
                    TexDesc[] textures = new TexDesc[8];
                    if (textureSet == null)
                    {
                        isNewTextureSet = true;

                        textureSet = new TexSet();
                        textureSet.Name = fileName;
                        textureSet.Index = _repository.TextureSets.Count;

                        // For the moment, a new texture set contains all new textures.
                        ushort textureIndex = (ushort)_repository.Textures.Count;
                        for (int t = 0; t < textures.Length; t++)
                        {
                            textureSet.TextureIDs[t] = textureIndex;

                            textures[t] = new TexDesc();
                            textures[t].TextureIndex = textureIndex;
                            textures[t].FilePath = _repository.MakeTextureFilePath(textureIndex);
                            textures[t].IsNew = true;

                            textureIndex++;
                        }
                    }

                    if (isNewTextureSet)
                    {
                        _repository.Textures.Add(textures);
                        _repository.TextureSets.Add(textureSet);
                    }
                    #endregion

                    uint fileLength = _file.Export(addFileDialog.FullPath, SR1_File.Version.Retail_PC, textureSet.TextureIDs);

                    if (_file._IsLevel && _repository.Levels.Levels.Find(x => x.UnitName == fileName) == null)
                    {
                        Level level = new Level();
                        level.UnitName = fileName;
                        level.StreamUnitID = addFileDialog.FileID;
                        level.IsNew = true;
                        level.TextureSet = textureSet.Name;

                        _repository.Levels.Add(level);
                        _repository.Levels.NextAvailableID++;

                        TreeNode node = new TreeNode();
                        node.Text = level.UnitName;
                        node.Tag = level;
                        projectTreeView.Nodes.Add(node);
                        projectTreeView.Sort();
                    }

                    string relativePath = addFileDialog.RelativePath;
                    string extension = Path.GetExtension(relativePath);
                    int endOfName = relativePath.Length - extension.Length;
                    uint fileHash = Repository.GetSR1HashName(relativePath);
                    if (_repository.Assets.Assets.Find(x => x.FileHash == fileHash) == null)
                    {
                        AssetDesc asset = new AssetDesc();

                        asset.FilePath = relativePath;
                        asset.FileHash = fileHash;
                        asset.FileLength = fileLength;
                        asset.Code.code0 = char.ToUpperInvariant(relativePath[endOfName - 4]);
                        asset.Code.code1 = char.ToUpperInvariant(relativePath[endOfName - 3]);
                        asset.Code.code2 = char.ToUpperInvariant(relativePath[endOfName - 2]);
                        asset.Code.code3 = char.ToUpperInvariant(relativePath[endOfName - 1]);
                        asset.FileIndex = _repository.Assets.Count;
                        asset.IsNew = true;

                        _repository.Assets.Add(asset);
                    }

                    _repository.SaveRepository();
                }
                catch (Exception exception)
                {

                }
            }

            addFileDialog.Dispose();
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
            addToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
            compileProjectToolStripMenuItem.Enabled = (_repository != null);

            if (_repository != null)
            {
                foreach (Level level in _repository.Levels.Levels)
                {
                    TreeNode node = new TreeNode();
                    node.Text = level.UnitName;
                    node.Tag = level;
                    projectTreeView.Nodes.Add(node);
                }

                projectTreeView.Sort();
                displayModeTabs.SelectedTab = projectTab;
            }

            Enabled = true;
            _progressWindow.Hide();
            _progressWindow.Dispose();
        }

        private void BeginPacking()
        {
            if (_progressWindow != null)
            {
                _progressWindow.Dispose();
            }
            _progressWindow = new ProgressWindow();
            _progressWindow.Title = "Packing Repository...";
            _progressWindow.SetMessage("");
            //_progressWindow.Icon = this.Icon;
            _progressWindow.Owner = this;
            _progressWindow.TopLevel = true;
            _progressWindow.ShowInTaskbar = false;
            this.Enabled = false;
            _progressWindow.Show();
        }

        private void EndPacking()
        {
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

            loadingThread.Name = "UnpackingThread";
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

            addToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
            compileProjectToolStripMenuItem.Enabled = (_repository != null);

            if (_repository != null)
            {
                foreach (Level level in _repository.Levels.Levels)
                {
                    TreeNode node = new TreeNode();
                    node.Text = level.UnitName;
                    node.Tag = level;
                    projectTreeView.Nodes.Add(node);
                }

                projectTreeView.Sort();
                displayModeTabs.SelectedTab = projectTab;
            }
        }

        private void compileProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repository repository = _repository;

            Invoke(new MethodInvoker(BeginPacking));

            Thread loadingThread = new Thread((() =>
            {
                repository.PackRepository();

                _repository = repository;
                Invoke(new MethodInvoker(EndPacking));
            }));

            loadingThread.Name = "PackingThread";
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

        private void projectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_repository == null)
            {
                return;
            }

            Level level = (Level)e.Node.Tag;
            string text = "Unit Name: " + level.UnitName + "\r\n";
            text += "Unit ID: " + level.StreamUnitID.ToString() + "\r\n";
            if (level.TextureSet != null && level.TextureSet != "")
            {
                text += "Texture Set (imported): " + level.TextureSet + "\r\n";
            }
            text += "Intros:\r\n";

            foreach (Intro intro in _repository.Intros.Intros)
            {
                if (intro.StreamUnitID == level.StreamUnitID)
                {
                    float rX = (intro.Rotation.X * 360) / 4096f;
                    float rY = (intro.Rotation.Y * 360) / 4096f;
                    float rZ = (intro.Rotation.Z * 360) / 4096f;

                    text += "\t" + intro.ObjectName + " " + intro.IntroUniqueID;
                    text += ", position {" + intro.Position.X + ", " + intro.Position.Y + ", " + intro.Position.Z + " }";
                    text += ", rotation {" + rX + ", " + rY + ", " + rZ + " }\r\n";
                }
            }

            projectTextBox.Text = text;
        }
    }
}
