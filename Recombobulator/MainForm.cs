using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SR1Repository;
using SRFile = CDC.Objects.SRFile;
using SR1File = CDC.Objects.SR1File;
using SRModel = CDC.Objects.Models.SRModel;
using SR1PSTextureFile = BenLincoln.TheLostWorlds.CDTextures.SoulReaverPlaystationTextureFile;

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

                    string textureIDs = "Textures:\r\n\r\n";
                    foreach (ushort textureID in _file._TextureIDs)
                    {
                        textureIDs += "texture-" + textureID.ToString("00000") + "\r\n";
                    }
                    textureIDs += "\r\n";

                    string objectNames = "";
                    if (_file._IsLevel)
                    {
                        objectNames = "Objects:\r\n\r\n";
                        foreach (string objectName in _file._ObjectNames)
                        {
                            objectNames += objectName + "\r\n";
                        }
                        objectNames += "\r\n";
                    }

                    string errors = _file.GetErrors();
                    if (errors == null)
                    {
                        errors = "";
                    }
                    else if (errors != "")
                    {
                        errors += "\r\n";
                    }

                    string heading = "File: " + _file._FilePath + "\r\n\r\n";

                    pcmFileSummary.Text = heading + errors + textureIDs + objectNames;
                    scripts.Text = _file.GetScripts();

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
            addFileDialog.Initialize(_repository, fileName, _file._IsLevel, _file._ObjectNames);
            if (addFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    #region Textures
                    TexSet textureSet = _repository.TextureSets.TexSets.Find(x => x.Index == addFileDialog.TextureSet);
                    if (textureSet == null)
                    {
                        CDC.Objects.ExportOptions options = new CDC.Objects.ExportOptions();
                        SRFile srFile = new SR1File(_file._FilePath, options);
                        string textureFileName = Path.ChangeExtension(_file._FilePath, "crm");
                        try
                        {
                            SR1PSTextureFile textureFile = new SR1PSTextureFile(textureFileName);

                            uint polygonCountAllModels = 0;
                            foreach (SRModel srModel in srFile.Models)
                            {
                                polygonCountAllModels += srModel.PolygonCount;
                            }

                            SR1PSTextureFile.SoulReaverPlaystationPolygonTextureData[] polygons =
                                new SR1PSTextureFile.SoulReaverPlaystationPolygonTextureData[polygonCountAllModels];

                            int polygonNum = 0;
                            foreach (SRModel srModel in srFile.Models)
                            {
                                foreach (CDC.Polygon polygon in srModel.Polygons)
                                {
                                    polygons[polygonNum].paletteColumn = polygon.paletteColumn;
                                    polygons[polygonNum].paletteRow = polygon.paletteRow;
                                    polygons[polygonNum].u = new int[3];
                                    polygons[polygonNum].v = new int[3];
                                    polygons[polygonNum].materialColour = polygon.colour;

                                    polygons[polygonNum].u[0] = (int)(srModel.Geometry.UVs[polygon.v1.UVID].u * 255);
                                    polygons[polygonNum].v[0] = (int)(srModel.Geometry.UVs[polygon.v1.UVID].v * 255);
                                    polygons[polygonNum].u[1] = (int)(srModel.Geometry.UVs[polygon.v2.UVID].u * 255);
                                    polygons[polygonNum].v[1] = (int)(srModel.Geometry.UVs[polygon.v2.UVID].v * 255);
                                    polygons[polygonNum].u[2] = (int)(srModel.Geometry.UVs[polygon.v3.UVID].u * 255);
                                    polygons[polygonNum].v[2] = (int)(srModel.Geometry.UVs[polygon.v3.UVID].v * 255);

                                    polygons[polygonNum].textureID = polygon.material.textureID;
                                    polygons[polygonNum].CLUT = polygon.material.clutValue;

                                    polygons[polygonNum].textureUsed = polygon.material.textureUsed;
                                    polygons[polygonNum].visible = polygon.material.visible;

                                    polygonNum++;
                                }
                            }

                            textureFile.BuildTexturesFromPolygonData(polygons, false, true, options);

                            textureSet = new TexSet();
                            textureSet.Name = fileName;
                            textureSet.Index = _repository.TextureSets.Count;

                            TexDesc[] textures = new TexDesc[8];

                            ushort textureIndex = (ushort)_repository.Textures.Count;
                            for (int t = 0; t < textures.Length; t++)
                            {
                                Bitmap bitmap;
                                if (t >= textureFile.TextureCount)
                                {
                                    textureSet.TextureIDs[t] = textureIndex;
                                }
                                else
                                {
                                    bitmap = textureFile.GetTextureAsBitmap(t);

                                    int newTextureIndex = textureIndex + t;

                                    string textureName = _repository.MakeTextureFilePath(newTextureIndex, true);
                                    bitmap.Save(textureName);

                                    textures[t] = new TexDesc();
                                    textures[t].TextureIndex = textureIndex + t;
                                    textures[t].FilePath = _repository.MakeTextureFilePath(newTextureIndex);
                                    textures[t].IsNew = true;

                                    _repository.Textures.Add(textures[t]);

                                    textureSet.TextureIDs[t] = (ushort)newTextureIndex;
                                }
                            }

                            _repository.TextureSets.Add(textureSet);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    #endregion

                    int newStreamUnitID = 0;
                    _repository.FindAvailableStreamUnitID(ref newStreamUnitID);

                    int numIntros = _file._IntroIDs.Count;
                    int[] newIntroIDs = new int[numIntros];
                    _repository.FindAvailableIntroIDs(ref newIntroIDs);

                    _file.Export(addFileDialog.FullPath, SR1_File.Version.Retail_PC, textureSet.TextureIDs, newStreamUnitID, newIntroIDs);

                    object newObject = null;
                    string category = null;
                    if (_file._IsLevel)
                    {
                        newObject = _repository.AddNewLevel(fileName, addFileDialog.FileID, textureSet.Name);
                        category = "Levels";
                    }
                    else
                    {
                        newObject = _repository.AddNewObject(fileName, textureSet.Name);
                        category = "Objects";
                    }

                    if (newObject != null && category != null)
                    {
                        TreeNode[] nodes = projectTreeView.Nodes.Find(category, false);
                        if (nodes.Length > 0 && nodes[0] != null)
                        {
                            TreeNode node = new TreeNode();
                            node.Text = fileName;
                            node.Tag = newObject;
                            nodes[0].Nodes.Add(node);
                            projectTreeView.Sort();
                        }
                    }

                    _repository.AddNewAsset(addFileDialog.RelativePath);
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
                Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
                Properties.Settings.Default.Save();

                DoBulkTesting(
                    dialog.SelectedPath,
                        SR1_File.TestFlags.ListAllFiles |
                        SR1_File.TestFlags.IgnoreDuplicates | SR1_File.TestFlags.ListObjectTypes/* |
                        SR1_File.TestFlags.ListRelocModules | SR1_File.TestFlags.ListObjectTypes*/
                );
            }
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
                Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
                Properties.Settings.Default.Save();

                DoBulkTesting(
                    dialog.SelectedPath,
                        SR1_File.TestFlags.IgnoreDuplicates | SR1_File.TestFlags.ListObjectTypes/* |
                        SR1_File.TestFlags.ListRelocModules | SR1_File.TestFlags.ListObjectTypes*/
                );
            }
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
                TreeNode levelsNode = projectTreeView.Nodes.Add("Levels", "Levels");
                foreach (Level level in _repository.Levels.Levels)
                {
                    TreeNode node = new TreeNode();
                    node.Text = level.UnitName;
                    node.Tag = level;
                    levelsNode.Nodes.Add(node);
                }

                TreeNode objectsNode = projectTreeView.Nodes.Add("Objects", "Objects");
                foreach (SR1Repository.Object obj in _repository.Objects.Objects)
                {
                    TreeNode node = new TreeNode();
                    node.Text = obj.ObjectName;
                    node.Tag = obj;
                    objectsNode.Nodes.Add(node);
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

        private void BeginBulkTesting()
        {
            if (_progressWindow != null)
            {
                _progressWindow.Dispose();
            }
            _progressWindow = new ProgressWindow();
            _progressWindow.Title = "Testing...";
            _progressWindow.SetMessage("");
            //_progressWindow.Icon = this.Icon;
            _progressWindow.Owner = this;
            _progressWindow.TopLevel = true;
            _progressWindow.ShowInTaskbar = false;
            this.Enabled = false;
            _progressWindow.Show();
        }

        private delegate void StringParamInvoker(string results);

        private void EndBulkTesting(string results)
        {
            testResults.Text = results;
            displayModeTabs.SelectedTab = testResultsTab;

            Enabled = true;
            _progressWindow.Hide();
            _progressWindow.Dispose();
        }

        private void DoBulkTesting(string folderName, SR1_File.TestFlags flags)
        {
            testResults.Clear();

            Invoke(new MethodInvoker(BeginBulkTesting));

            int filesRead = 0;
            int filesToRead = 0;
            string recentMessage = "";

            Thread loadingThread = new Thread((() =>
            {
                string testResults = "";
                string[] exportTestResults = SR1_File.TestFolder(folderName, flags, ref filesRead, ref filesToRead, ref recentMessage);
                foreach (string result in exportTestResults)
                {
                    testResults += result + "\r\n";
                }

                Invoke(new StringParamInvoker(EndBulkTesting), testResults);
            }));

            loadingThread.Name = "BulkTestingThread";
            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.Start();
            //loadingThread.Join();

            Thread progressThread = new Thread((() =>
            {
                do
                {
                    lock (recentMessage)
                    {
                        _progressWindow.SetMessage(recentMessage);
                    }

                    if (filesToRead > 0)
                    {
                        _progressWindow.SetProgress((100 * filesRead) / filesToRead);
                    }
                    else
                    {
                        _progressWindow.SetProgress(0);
                    }
                    Thread.Sleep(20);
                }
                while (loadingThread.IsAlive);
            }));

            progressThread.Name = "ProgressThread";
            progressThread.SetApartmentState(ApartmentState.STA);
            progressThread.Start();
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

            if (repository != null && repository.LoadRepository())
            {
                _repository = repository;

                addToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
                compileProjectToolStripMenuItem.Enabled = (_repository != null);

                TreeNode levelsNode = projectTreeView.Nodes.Add("Levels", "Levels");
                foreach (Level level in _repository.Levels.Levels)
                {
                    TreeNode node = new TreeNode();
                    node.Text = level.UnitName;
                    node.Tag = level;
                    levelsNode.Nodes.Add(node);
                }

                TreeNode objectsNode = projectTreeView.Nodes.Add("Objects", "Objects");
                foreach (SR1Repository.Object obj in _repository.Objects.Objects)
                {
                    TreeNode node = new TreeNode();
                    node.Text = obj.ObjectName;
                    node.Tag = obj;
                    objectsNode.Nodes.Add(node);
                }

                projectTreeView.Sort();
                displayModeTabs.SelectedTab = projectTab;
            }
            else
            {
                MessageBox.Show(
                    "Unable to load repository at \"" + folderDialog.SelectedPath + "\"", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
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

            if (e.Node.Parent == null)
            {
                projectTextBox.Text = "";
            }
            else if (e.Node.Parent.Text == "Levels")
            {
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
            else if (e.Node.Parent.Text == "Objects")
            {
                SR1Repository.Object obj = (SR1Repository.Object)e.Node.Tag;
                string text = "Object Name: " + obj.ObjectName + "\r\n";
                text += "Models: " + obj.NumModels + "\r\n";
                text += "Animations: " + obj.NumAnimations +"\r\n";
                text += "Sections (Bones): " + obj.NumSections + "\r\n";
                if (obj.TextureSet != null && obj.TextureSet != "")
                {
                    text += "Texture Set (imported): " + obj.TextureSet + "\r\n";
                }

                projectTextBox.Text = text;
            }
        }
    }
}
