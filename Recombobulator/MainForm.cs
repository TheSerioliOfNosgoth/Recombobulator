using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SR1Repository;
using SR1File = CDC.SR1File;
using SR1PSXTextureFile = BenLincoln.TheLostWorlds.CDTextures.SoulReaverPSXCRMTextureFile;
using TPages = BenLincoln.TheLostWorlds.CDTextures.PSXTextureDictionary;

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

		private void OpenDataFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VersionSelectForm versionSelectForm = new VersionSelectForm();
			if (versionSelectForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			OpenFileDialog dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Filter =
					"Soul Reaver Files|*.pcm;*.drm|" +
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
					SR1_File.ImportFlags importFlags = SR1_File.ImportFlags.None;
					importFlags |= SR1_File.ImportFlags.LogErrors;
					importFlags |= SR1_File.ImportFlags.LogScripts;
					SR1_File.Version version = versionSelectForm.Version;
					_file.Import(dialog.FileName, importFlags, version);

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

					addDataFileToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
					testExportToolStripMenuItem.Enabled = _fileLoaded;
				}
				catch (Exception ex)
				{

				}

				pcmFileTreeListView.EndUpdate();

				displayModeTabs.SelectedTab = pcmFileDataTab;
			}
		}

		private void TestExportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VersionSelectForm versionSelectForm = new VersionSelectForm();
			if (versionSelectForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			SaveFileDialog dialog = new SaveFileDialog()
			{
				Filter = "Soul Reaver Files|*.pcm;*.drm",
				DefaultExt = "pcm",
				FilterIndex = 1,
				FileName = Path.GetFileName(_file._FilePath)
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					SR1_File.MigrateFlags migrateFlags = SR1_File.MigrateFlags.None;
					SR1_File.Version version = versionSelectForm.Version;
					_file.Export(dialog.FileName, version, migrateFlags, new SR1_File.Overrides());
				}
				catch (Exception ex)
				{

				}
			}
		}

		private void AddDataFileToProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = Path.GetFileNameWithoutExtension(_file._FilePath);

			while (true)
			{
				string filePath = _file._IsLevel ?
					_repository.MakeLevelFilePath(fileName) :
					_repository.MakeObjectFilePath(fileName);

				uint fileHash = Repository.GetSR1HashName(filePath);
				if (_repository.Assets.Assets.Find(x => x.FileHash == fileHash) != null)
				{
					RenameForm renameForm = new RenameForm();
					renameForm.Text = "Existing File";
					renameForm.Message = "File already exists!\r\nPlease select a new name.";
					if (renameForm.ShowDialog() == DialogResult.Cancel)
					{
						return;
					}

					fileName = renameForm.NewName;
				}
				else
				{
					break;
				}
			}

			AddFileForm addFileDialog;
			if (_file._IsLevel)
			{
				AddLevelForm addLevelDialog = new AddLevelForm();
				addLevelDialog.Initialize(_repository, fileName, _file);
				addFileDialog = addLevelDialog;
			}
			else
			{
				AddObjectForm addObjectDialog = new AddObjectForm();
				addObjectDialog.Initialize(_repository, fileName);
				addFileDialog = addObjectDialog;
			}

			if (addFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					fileName = addFileDialog.FileName;

					TexSet textureSet = _repository.TextureSets.TexSets.Find(x => x.Index == addFileDialog.TextureSet);
					if (textureSet == null)
					{
						if (addFileDialog.ImportTextures)
						{
							textureSet = ImportTextureSet(fileName, _file._FilePath);
						}
						else if (addFileDialog.PromptTextures)
						{
							textureSet = CreateTextureSet(fileName, _file._FilePath);
						}
					}

					SR1_File.MigrateFlags migrateFlags = SR1_File.MigrateFlags.None;

					SR1_File.Overrides overrides = new SR1_File.Overrides();
					overrides.NewName = fileName;

					if (textureSet != null)
					{
						for (int t = 0; t < textureSet.TextureIDs.Length; t++)
						{
							ushort tPage = _repository.Textures[textureSet.TextureIDs[t]].TPage;
							if (overrides.NewTextureIDs.ContainsKey(tPage))
							{
								overrides.NewTextureIDs.Add(tPage, textureSet.TextureIDs[t]);
							}
						}
					}

					object newObject = null;
					string category = null;
					if (_file._IsLevel)
					{
						AddLevelForm addLevelDialog = (AddLevelForm)addFileDialog;

						if (addLevelDialog.RemovePortals)
						{
							migrateFlags |= SR1_File.MigrateFlags.RemovePortals;
						}

						if (addLevelDialog.RemoveSignals)
						{
							migrateFlags |= SR1_File.MigrateFlags.RemoveSignals;
						}

						if (addLevelDialog.RemoveEvents)
						{
							migrateFlags |= SR1_File.MigrateFlags.RemoveEvents;
						}

						if (addLevelDialog.RemoveAnimatedTextures)
						{
							migrateFlags |= SR1_File.MigrateFlags.RemoveAnimatedTextures;
						}

						SR1Structures.Level level = (SR1Structures.Level)_file._Structures[0];
						uint sourceVersion = level.versionNumber.Value;

						overrides.OldStreamUnitID = level.streamUnitID.Value;
						overrides.NewStreamUnitID = 0;
						_repository.FindAvailableStreamUnitID(ref overrides.NewStreamUnitID);

						int[] newIntroIDs = new int[_file._IntroIDs.Count];
                        _repository.FindAvailableIntroIDs(ref newIntroIDs);
						for (int i = 0; i < newIntroIDs.Length; i++)
						{
                            overrides.NewIntroIDs.Add(_file._IntroIDs[i], newIntroIDs[i]);
						}

						//overrides.NewObjectNames.Add("priests", "witch");
						_file.Export(addFileDialog.FullPath, SR1_File.Version.Retail_PC, migrateFlags, overrides);

						string sourceUnitName = level.Name;
						int sourceUnitID = level.streamUnitID.Value;
						newObject = _repository.AddNewLevel(fileName, sourceUnitName, sourceUnitID, sourceVersion, textureSet.Name);
						category = "Levels";
					}
					else
					{
						_file.Export(addFileDialog.FullPath, SR1_File.Version.Retail_PC, migrateFlags, overrides);
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
				catch (Exception ex)
				{

				}
			}

			addFileDialog.Dispose();
		}

		private void DetailedExportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VersionSelectForm versionSelectForm = new VersionSelectForm();
			if (versionSelectForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select a folder to test.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
				Properties.Settings.Default.Save();

				DoBulkTesting(
					dialog.SelectedPath,
					versionSelectForm.Version,
					SR1_File.TestFlags.ListAllFiles |
					SR1_File.TestFlags.IgnoreDuplicates | SR1_File.TestFlags.ListObjectTypes/* |
					SR1_File.TestFlags.ListRelocModules | SR1_File.TestFlags.ListObjectTypes*/
				);
			}
		}

		private void BriefExportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VersionSelectForm versionSelectForm = new VersionSelectForm();
			if (versionSelectForm.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select a folder to test.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
				Properties.Settings.Default.Save();

				DoBulkTesting(
					dialog.SelectedPath,
					versionSelectForm.Version,
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

			addDataFileToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
			addTextureFileToProjectToolStripMenuItem.Enabled = (_repository != null);
			compileProjectToolStripMenuItem.Enabled = (_repository != null);
			scriptedImportsToolStripMenuItem.Enabled = (_repository != null);
			editPortalToolStripMenuItem.Enabled = false;

			_progressWindow.Show();
		}

		private void EndUnpacking()
		{
			addDataFileToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
			addTextureFileToProjectToolStripMenuItem.Enabled = (_repository != null);
			compileProjectToolStripMenuItem.Enabled = (_repository != null);
			scriptedImportsToolStripMenuItem.Enabled = (_repository != null);
			editPortalToolStripMenuItem.Enabled = false;

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

				TreeNode texSetTreeNode = projectTreeView.Nodes.Add("TextureSets", "TextureSets");
				foreach (TexSet texSet in _repository.TextureSets.TexSets)
				{
					TreeNode node = new TreeNode();
					node.Text = texSet.Name;
					node.Tag = texSet;
					texSetTreeNode.Nodes.Add(node);
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

		private void BeginImportTextures()
		{
			if (_progressWindow != null)
			{
				_progressWindow.Dispose();
			}
			_progressWindow = new ProgressWindow();
			_progressWindow.Title = "Importing Textures...";
			_progressWindow.SetMessage("");
			//_progressWindow.Icon = this.Icon;
			_progressWindow.Owner = this;
			_progressWindow.TopLevel = true;
			_progressWindow.ShowInTaskbar = false;
			this.Enabled = false;
			_progressWindow.Show();
		}

		private void EndImportTextures()
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

		private void DoBulkTesting(string folderName, SR1_File.Version testVersion, SR1_File.TestFlags flags)
		{
			testResults.Clear();

			Invoke(new MethodInvoker(BeginBulkTesting));

			int filesRead = 0;
			int filesToRead = 0;
			string recentMessage = "";

			Thread loadingThread = new Thread((() =>
			{
				string testResults = "";
				string[] exportTestResults = SR1_File.TestFolder(folderName, testVersion, flags, ref filesRead, ref filesToRead, ref recentMessage);
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

		private void BeginScriptedImport()
		{
			if (_progressWindow != null)
			{
				_progressWindow.Dispose();
			}
			_progressWindow = new ProgressWindow();
			_progressWindow.Title = "Importing...";
			_progressWindow.SetMessage("");
			//_progressWindow.Icon = this.Icon;
			_progressWindow.Owner = this;
			_progressWindow.TopLevel = true;
			_progressWindow.ShowInTaskbar = false;
			this.Enabled = false;
			_progressWindow.Show();
		}

		private void EndScriptedImport()
		{
			projectTreeView.Nodes.Clear();

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

				TreeNode texSetTreeNode = projectTreeView.Nodes.Add("TextureSets", "TextureSets");
				foreach (TexSet texSet in _repository.TextureSets.TexSets)
				{
					TreeNode node = new TreeNode();
					node.Text = texSet.Name;
					node.Tag = texSet;
					texSetTreeNode.Nodes.Add(node);
				}

				projectTreeView.Sort();
				displayModeTabs.SelectedTab = projectTab;
			}

			Enabled = true;
			_progressWindow.Hide();
			_progressWindow.Dispose();
		}

		private void NewProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateProjectForm createProjectDialog = new CreateProjectForm();
			if (createProjectDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			folderDialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && System.IO.Directory.Exists(recentFolder))
			{
				folderDialog.SelectedPath = recentFolder;
			}

			if (folderDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = folderDialog.SelectedPath;
			Properties.Settings.Default.Save();

			Repository repository = new Repository(folderDialog.SelectedPath);

			Invoke(new MethodInvoker(BeginUnpacking));

			Thread loadingThread = new Thread((() =>
			{
				switch (createProjectDialog.CreateProjectType)
				{
					case CreateProjectForm.ProjectType.Metadata:
						repository.UnpackRepository(true);
						break;
					case CreateProjectForm.ProjectType.Assets:
						repository.UnpackRepository(false);
						break;
					default:
						repository.CreateRepository();
						break;
				}

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

			addDataFileToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
			addTextureFileToProjectToolStripMenuItem.Enabled = (_repository != null);
			compileProjectToolStripMenuItem.Enabled = (_repository != null);
			scriptedImportsToolStripMenuItem.Enabled = (_repository != null);
			editPortalToolStripMenuItem.Enabled = false;

			Repository repository = new Repository(folderDialog.SelectedPath);
			if (repository != null && repository.LoadRepository())
			{
				_repository = repository;

				addDataFileToProjectToolStripMenuItem.Enabled = (_fileLoaded && _repository != null);
				addTextureFileToProjectToolStripMenuItem.Enabled = (_repository != null);
				compileProjectToolStripMenuItem.Enabled = (_repository != null);
				scriptedImportsToolStripMenuItem.Enabled = (_repository != null);
				editPortalToolStripMenuItem.Enabled = false;

				projectTreeView.Nodes.Clear();

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

				TreeNode texSetTreeNode = projectTreeView.Nodes.Add("TextureSets", "TextureSets");
				foreach (TexSet texSet in _repository.TextureSets.TexSets)
				{
					TreeNode node = new TreeNode();
					node.Text = texSet.Name;
					node.Tag = texSet;
					texSetTreeNode.Nodes.Add(node);
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

		private void CompileProjectToolStripMenuItem_Click(object sender, EventArgs e)
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
			editPortalToolStripMenuItem.Enabled = false;

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
				editPortalToolStripMenuItem.Enabled = true;

				Level level = (Level)e.Node.Tag;
				string text = "Unit Name: " + level.UnitName + "\r\n";
				text += "Unit ID: " + level.UnitID.ToString() + "\r\n";
				text += "Source Unit Name: " + level.SourceUnitName + "\r\n";
				text += "Source Unit ID: " + level.SourceUnitID.ToString() + "\r\n";
				text += "Source Version: 0x" + level.SourceVersion.ToString("X8") + "\r\n";
				if (level.TextureSet != null && level.TextureSet != "")
				{
					text += "Texture Set (imported): " + level.TextureSet + "\r\n";
				}

				text += "Portals:\r\n";
				foreach (Portal portal in level.Portals.Portals)
				{
					string destUnitName = "missing";
					if (portal.DestUnitName != null && portal.DestUnitName != "")
					{
						destUnitName = portal.DestUnitName;
					}

					int signalID = portal.SignalID;

					text += "\torigin { " + level.UnitName + ", " + signalID + " }, destination { " + destUnitName + ", " + portal.DestSignalID + " }, ";
					text += "old destination { " + portal.OldDestUnitName + ", " + portal.OldDestSignalID + ", 0x" + portal.OldDestVersion.ToString("X8") + " }\r\n";
				}

				text += "Intros:\r\n";
				foreach (Intro intro in _repository.Intros.Intros)
				{
					if (intro.StreamUnitID == level.UnitID)
					{
						float rX = (intro.Rotation.X * 360) / 4096f;
						float rY = (intro.Rotation.Y * 360) / 4096f;
						float rZ = (intro.Rotation.Z * 360) / 4096f;

						text += "\t" + intro.ObjectName + " " + intro.IntroUniqueID + " ";
						text += "{ position {" + intro.Position.X + ", " + intro.Position.Y + ", " + intro.Position.Z + " }";
						text += ", rotation {" + rX + ", " + rY + ", " + rZ + " } }\r\n";
					}
				}

				text += "Events:\r\n";
				foreach (Event srEvent in _repository.Events.Events)
				{
					if (srEvent.StreamUnitID == level.UnitID)
					{
						text += "\tevent " + srEvent.EventNumber.ToString() + "\r\n\t{\r\n";
						text += "\t\tinstances\r\n\t\t{\r\n";

						foreach (EventInstance instance in srEvent.Instances.Instances)
						{
							Intro intro = _repository.Intros.Find(x => x.IntroUniqueID == instance.IntroID);

							string objectName = "missing " + instance.IntroID;
							string unitName = "missing";

							if (intro != null)
							{
								objectName = intro.ObjectName + " " + intro.IntroUniqueID;
								unitName = intro.UnitName;
							}

							text += "\t\t\t" + objectName;
							text += " { ";
							text += "unitName { " + unitName + " }, ";
							text += "unitID { " + instance.UnitID.ToString() + " }, ";
							text += "offset { 0x" + instance.EventInstanceOffset.ToString("X8") + " }";
							text += " }\r\n";
						}

						text += "\t\t}\r\n";
						text += "\t}\r\n";
					}
				}

				projectTextBox.Text = text;
			}
			else if (e.Node.Parent.Text == "Objects")
			{
				SR1Repository.Object obj = (SR1Repository.Object)e.Node.Tag;
				string text = "Object Name: " + obj.ObjectName + "\r\n";
				text += "Models: " + obj.NumModels + "\r\n";
				text += "Animations: " + obj.NumAnimations + "\r\n";
				text += "Sections (Bones): " + obj.NumSections + "\r\n";
				if (obj.TextureSet != null && obj.TextureSet != "")
				{
					text += "Texture Set (imported): " + obj.TextureSet + "\r\n";
				}

				projectTextBox.Text = text;
			}
			else if (e.Node.Parent.Text == "TextureSets")
			{
				TexSet texSet = (TexSet)e.Node.Tag;
				string text = "Texture Set Name: " + texSet.Name + "\r\n";
				text += "Texture IDs: " + texSet.Name + "\r\n";
				for (int i = 0; i < texSet.TextureIDs.Length; i++)
				{
					text += "\ttexture" + i.ToString() + ": " + texSet.TextureIDs[i] + "\r\n";
				}

				projectTextBox.Text = text;
			}
		}
		private void projectTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (_repository == null)
			{
				return;
			}

			if (e.Button == MouseButtons.Right)
			{
				TreeNode treeNode = e.Node;
				projectTreeView.SelectedNode = treeNode;

				if (treeNode != null && treeNode.Parent != null && treeNode.Parent.Text == "Levels")
				{
					projectContextMenu.Show(projectTreeView, e.Location);
				}
			}
		}

		private void EditPortalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_repository == null)
			{
				return;
			}

			TreeNode treeNode = projectTreeView.SelectedNode;
			if (treeNode == null || treeNode.Parent == null || treeNode.Parent.Text != "Levels")
			{
				return;
			}

			EditPortalForm editPortalForm = new EditPortalForm();
			editPortalForm.Initialize(_repository, treeNode.Text);

			if (editPortalForm.ShowDialog() == DialogResult.OK)
			{
				_repository.EditPortal(editPortalForm.FromUnit, editPortalForm.ToUnit, editPortalForm.FromSignal, editPortalForm.ToSignal);
				_repository.SaveRepository();
			}

			editPortalForm.Dispose();
		}

		class ImportScript
		{
			public readonly List<ImportFile> ImportFiles = new List<ImportFile>();
			public readonly List<ReplacePortal> ReplacePortals = new List<ReplacePortal>();
		}

		class ImportFile
		{
			public enum Flags : int
			{
				None = 0,
				RemoveEvents = 1,
				RemoveSignals = 2,
				RemovePortals = 4,
				RemoveVertexMorphs = 8,
				RemoveAnimatedTextures = 16,
				ForceWaterTranslucent = 32,
				Default = RemoveEvents | RemoveAnimatedTextures | ForceWaterTranslucent,
			}

			public string importName = null;
			public string exportName = null;
			public string textureSet = null;
			public bool isLevel = false;
			public Flags flags = Flags.Default;
			public string[] removePortals = null;
			public ReplaceObject[] replaceObjects = null;
		};

		struct ReplaceObject
		{
			public string oldObject;
			public string newObject;
		}

		struct ReplacePortal
		{
			public string fromSignal;
			public string toSignal;
		}

		private void DoScriptedImport(string folderName, ImportScript importScript)
		{
			Invoke(new MethodInvoker(BeginScriptedImport));

			int filesRead = 0;
			int filesToRead = 0;
			string recentMessage = "";

			Thread importThread = new Thread((() =>
			{
				ImportFromScript(folderName, importScript, ref filesRead, ref filesToRead, ref recentMessage);

				Invoke(new MethodInvoker(EndScriptedImport));
			}));

			importThread.Name = "ScriptedImportThread";
			importThread.SetApartmentState(ApartmentState.STA);
			importThread.Start();
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
				while (importThread.IsAlive);
			}));

			progressThread.Name = "ProgressThread";
			progressThread.SetApartmentState(ApartmentState.STA);
			progressThread.Start();
		}

		private void ImportFromScript(string folderName, ImportScript importScript, ref int filesRead, ref int filesToRead, ref string recentMessage)
		{
			Interlocked.Exchange(ref filesRead, 0);
			Interlocked.Exchange(ref filesToRead, 0);

			filesToRead = importScript.ImportFiles.Count;

			foreach (ImportFile importFile in importScript.ImportFiles)
			{
				string importFolderName = importFile.importName.TrimEnd("0123456789".ToCharArray());
				string importParentFolder = folderName;
				string importPath = importFile.isLevel ?
					Path.Combine(importParentFolder, "kain2\\area", importFolderName, "bin", importFile.importName + ".drm") :
					Path.Combine(importParentFolder, "kain2\\object", importFolderName, importFile.importName + ".drm");
				string relativeImportPath = importFile.isLevel ?
					Path.Combine("kain2\\area", importFolderName, "bin", importFile.importName + ".drm") :
					Path.Combine("kain2\\object", importFolderName, importFile.importName + ".drm");

				Console.WriteLine("Importing file: " + relativeImportPath);

				Interlocked.Exchange(ref recentMessage, (string)relativeImportPath.Clone());

				if (!File.Exists(importPath))
				{
					Interlocked.Increment(ref filesRead);
					continue;
				}

				SR1_File file = new SR1_File();
				file.Import(importPath);

				string exportName = (importFile.exportName != null) ? importFile.exportName : importFile.importName;
				string exportPath = importFile.isLevel ?
					_repository.MakeLevelFilePath(exportName, true) :
					_repository.MakeObjectFilePath(exportName, true);
				string relativeExportPath = importFile.isLevel ?
					_repository.MakeLevelFilePath(exportName) :
					_repository.MakeObjectFilePath(exportName);

				uint fileHash = Repository.GetSR1HashName(exportPath);
				if (_repository.Assets.Assets.Find(x => x.FileHash == fileHash) != null)
				{
					Interlocked.Increment(ref filesRead);
					continue;
				}

				TexSet textureSet = null;

				if (importFile.textureSet != null)
				{
					textureSet = _repository.TextureSets.TexSets.Find(x => x.Name == importFile.textureSet);
				}

				if (textureSet == null)
				{
					textureSet = ImportTextureSet(exportName, importPath);
				}

				SR1_File.MigrateFlags migrateFlags = SR1_File.MigrateFlags.None;

				SR1_File.Overrides overrides = new SR1_File.Overrides();
				overrides.NewName = exportName;

				if (textureSet.TextureIDs != null)
				{
					for (int t = 0; t < textureSet.TextureIDs.Length; t++)
					{
						overrides.NewTextureIDs.Add(_repository.Textures[textureSet.TextureIDs[t]].TPage, textureSet.TextureIDs[t]);
					}
				}

				object newObject = null;

				if (importFile.isLevel)
				{
					if ((importFile.flags & ImportFile.Flags.RemoveEvents) != 0)
					{
						migrateFlags |= SR1_File.MigrateFlags.RemoveEvents;
					}

					if ((importFile.flags & ImportFile.Flags.RemoveVertexMorphs) != 0)
					{
						migrateFlags |= SR1_File.MigrateFlags.RemoveVertexMorphs;
					}

					if ((importFile.flags & ImportFile.Flags.RemoveAnimatedTextures) != 0)
					{
						migrateFlags |= SR1_File.MigrateFlags.RemoveAnimatedTextures;
					}

					if ((importFile.flags & ImportFile.Flags.ForceWaterTranslucent) != 0)
					{
						migrateFlags |= SR1_File.MigrateFlags.ForceWaterTranslucent;
					}

                    SR1Structures.Level level = (SR1Structures.Level)file._Structures[0];
                    uint sourceVersion = level.versionNumber.Value;

                    overrides.OldStreamUnitID = level.streamUnitID.Value;
                    overrides.NewStreamUnitID = 0;
                    _repository.FindAvailableStreamUnitID(ref overrides.NewStreamUnitID);

                    int[] newIntroIDs = new int[file._IntroIDs.Count];
                    _repository.FindAvailableIntroIDs(ref newIntroIDs);
                    for (int i = 0; i < newIntroIDs.Length; i++)
                    {
                        overrides.NewIntroIDs.Add(file._IntroIDs[i], newIntroIDs[i]);
                    }

                    //overrides.NewObjectNames.Add("priests", "witch");

                    if (importFile.replaceObjects != null)
					{
						foreach (ReplaceObject replaceObject in importFile.replaceObjects)
						{
							overrides.NewObjectNames.Add(replaceObject.oldObject, replaceObject.newObject);
						}
					}

					RemovePortals(file, importFile);
					file.Export(exportPath, SR1_File.Version.Retail_PC, migrateFlags, overrides);

					string sourceUnitName = level.Name;
					int sourceUnitID = level.streamUnitID.Value;
					newObject = _repository.AddNewLevel(exportName, sourceUnitName, sourceUnitID, sourceVersion, textureSet.Name);
				}
				else
				{
					file.Export(exportPath, SR1_File.Version.Retail_PC, migrateFlags, overrides);
					newObject = _repository.AddNewObject(exportName, textureSet.Name);
				}

				_repository.AddNewAsset(relativeExportPath);

				Interlocked.Increment(ref filesRead);
			}

			foreach (ReplacePortal replacePortal in importScript.ReplacePortals)
			{
				string[] fromPortal = replacePortal.fromSignal.Split(',');
				string[] toPortal = replacePortal.toSignal.Split(',');
				string fromUnit = fromPortal[0];
				string toUnit = toPortal[0];
				int.TryParse(fromPortal[1], out int fromSignal);
				int.TryParse(toPortal[1], out int toSignal);
				_repository.EditPortal(fromUnit, toUnit, fromSignal, toSignal);
			}

			_repository.SaveRepository();
		}

		private void RemovePortals(SR1_File file, ImportFile importFile)
		{
			if (importFile.removePortals != null && importFile.removePortals.Length > 0)
			{
				SR1Structures.Level level = (SR1Structures.Level)file._Structures[0];
				SR1Structures.Terrain terrain =
					(SR1Structures.Terrain)file._Structures[level.terrain.Offset];
				SR1Structures.SR1_StructureSeries<SR1Structures.MultiSignal> multiSignals =
					(SR1Structures.SR1_StructureSeries<SR1Structures.MultiSignal>)file._Structures[level.SignalListStart.Offset];
				SR1Structures.StreamUnitPortalList portalList =
					(SR1Structures.StreamUnitPortalList)file._Structures[terrain.StreamUnits.Offset];

				foreach (SR1Structures.StreamUnitPortal portal in portalList.portals)
				{
					if (!Array.Exists(importFile.removePortals, x => x == portal.tolevelname.ToString()))
					{
						continue;
					}

					portal.OmitFromMigration = true;

					foreach (SR1Structures.MultiSignal mSignal in multiSignals)
					{
						if (mSignal.signalNum.Value == portal.MSignalID.Value)
						{
							mSignal.OmitFromMigration = true;

							if (mSignal.numSignals.Value > 0)
							{
								((SR1Structures.Signal)mSignal.signalList[0]).OmitFromMigration = true;
							}

							break;
						}
					}
				}
			}
		}

		private TexSet ImportTextureSet(string textureSetName, string filePath)
		{
			TexSet oldTextureSet;
			do
			{
				oldTextureSet = _repository.TextureSets.TexSets.Find(x => x.Name == textureSetName);
				if (oldTextureSet != null)
				{
					_repository.TextureSets.TexSets.Remove(oldTextureSet);
				}
			}
			while (oldTextureSet != null);

			TexSet textureSet = new TexSet();
			textureSet.Name = textureSetName;
			textureSet.Index = _repository.TextureSets.Count;
			string textureFileName = Path.ChangeExtension(filePath, "crm");

			if (!File.Exists(filePath) || !File.Exists(textureFileName))
			{
				textureSet.TextureIDs = new ushort[0];
			}
			else
			{
				try
				{
					CDC.ExportOptions options = new CDC.ExportOptions();
					SR1File sr1File = new SR1File(filePath, CDC.Platform.PSX, options);
					TPages tPages = sr1File.TPages;

					SR1PSXTextureFile textureFile = new SR1PSXTextureFile(textureFileName);
					textureFile.BuildTexturesFromPolygonData(tPages, false, true, options);

					TexDesc[] textures = new TexDesc[textureFile.TextureCount];
					textureSet.TextureIDs = new ushort[textureFile.TextureCount];

					ushort textureIndex = (ushort)_repository.Textures.Count;
					for (int t = 0; t < textures.Length; t++)
					{
						Bitmap bitmap;
						if (t >= textureFile.TextureCount)
						{
							textureSet.TextureIDs[t] = 0;
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
							textures[t].TPage = tPages[t].tPage;

							_repository.Textures.Add(textures[t]);

							textureSet.TextureIDs[t] = (ushort)newTextureIndex;
						}
					}
				}
				catch (Exception ex)
				{

				}
			}

			TreeNode[] nodes = projectTreeView.Nodes.Find("TextureSets", false);
			if (nodes.Length > 0 && nodes[0] != null)
			{
				TreeNode node = new TreeNode();
				node.Text = textureSet.Name;
				node.Tag = textureSet;
				nodes[0].Nodes.Add(node);
				projectTreeView.Sort();
			}

			_repository.TextureSets.Add(textureSet);

			return textureSet;
		}

		private TexSet CreateTextureSet(string textureSetName, string filePath)
		{
			TexSet oldTextureSet;
			do
			{
				oldTextureSet = _repository.TextureSets.TexSets.Find(x => x.Name == textureSetName);
				if (oldTextureSet != null)
				{
					_repository.TextureSets.TexSets.Remove(oldTextureSet);
				}
			}
			while (oldTextureSet != null);

			TexSet textureSet = new TexSet();
			textureSet.Name = textureSetName;
			textureSet.Index = _repository.TextureSets.Count;
			string textureFileName = Path.ChangeExtension(filePath, "crm");

			if (!File.Exists(filePath) || !File.Exists(textureFileName))
			{
				textureSet.TextureIDs = new ushort[0];
			}
			else
			{
				try
				{
					CDC.ExportOptions options = new CDC.ExportOptions();
					SR1File sr1File = new SR1File(filePath, CDC.Platform.PSX, options);
					TPages tPages = sr1File.TPages;

					SR1PSXTextureFile textureFile = new SR1PSXTextureFile(textureFileName);
					textureFile.BuildTexturesFromPolygonData(tPages, false, true, options);

					textureSet.TextureIDs = new ushort[textureFile.TextureCount];
                    for (int t = 0; t < textureFile.TextureCount; t++)
                    {
						textureSet.TextureIDs[t] = ushort.MaxValue;
                    }

                    TextureSetForm textureSetForm = new TextureSetForm();
					textureSetForm.Initialize(_repository, textureSet);
					textureSetForm.ShowDialog();
				}
				catch (Exception ex)
				{

				}
			}

			TreeNode[] nodes = projectTreeView.Nodes.Find("TextureSets", false);
			if (nodes.Length > 0 && nodes[0] != null)
			{
				TreeNode node = new TreeNode();
				node.Text = textureSet.Name;
				node.Tag = textureSet;
				nodes[0].Nodes.Add(node);
				projectTreeView.Sort();
			}

			_repository.TextureSets.Add(textureSet);

			return textureSet;
		}

		private void ImportUndercityFeb04ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "city9", exportName = "city17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city10", exportName = "city18", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city11", exportName = "city22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city16", isLevel = true/*, removePortals = new string[] { "undrct1,90" }*/ });

			ReplaceObject replaceUndblk = new ReplaceObject { oldObject = "undblk", newObject = "pshblk" };
			ReplaceObject replaceDumbub = new ReplaceObject { oldObject = "dumbub", newObject = "pshblk" };
			ReplaceObject[] replaceUCObjects = new ReplaceObject[] { replaceUndblk, replaceDumbub };

			importFiles.Add(new ImportFile { importName = "undrct1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct3", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct5", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct8", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct12", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct20", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct21", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct23", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lantrn", isLevel = false });
			importFiles.Add(new ImportFile { importName = "bwall", isLevel = false });
			importFiles.Add(new ImportFile { importName = "swall", isLevel = false });
			//importFiles.Add(new ImportFile { importName = "undblk", isLevel = false }); // Can't convert. Replace with pshblk.
			//importFiles.Add(new ImportFile { importName = "dumbub", isLevel = false }); // Can't convert. Replace with pshblk.

			List<ReplacePortal> replacePortals = importScript.ReplacePortals;
			replacePortals.Add(new ReplacePortal { fromSignal = "city8,2", toSignal = "city17,1" });

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportUndercityFeb16ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "city9", exportName = "city17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city10", exportName = "city18", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city11", exportName = "city22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city16", isLevel = true });

			ReplaceObject replaceUndblk = new ReplaceObject { oldObject = "undblk", newObject = "pshblk" };
			ReplaceObject replaceDumbub = new ReplaceObject { oldObject = "dumbub", newObject = "pshblk" };
			// ReplaceObject replacePriests = new ReplaceObject { oldObject = "priests", newObject = "vlgra" };
			ReplaceObject[] replaceUCObjects = new ReplaceObject[] { replaceUndblk, replaceDumbub/*, replacePriests*/ };

			importFiles.Add(new ImportFile { importName = "undrct1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct3", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct5", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct8", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct12", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct17", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct20", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct21", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct23", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lantrn", isLevel = false });
			importFiles.Add(new ImportFile { importName = "priests", isLevel = false });
			//importFiles.Add(new ImportFile { importName = "bwall", isLevel = false }); // Not needed in Feb 16.
			//importFiles.Add(new ImportFile { importName = "swall", isLevel = false }); // Not needed in Feb 16.
			//importFiles.Add(new ImportFile { importName = "undblk", isLevel = false }); // Can't convert. Replace with pshblk.
			//importFiles.Add(new ImportFile { importName = "dumbub", isLevel = false }); // Can't convert. Replace with pshblk.

			List<ReplacePortal> replacePortals = importScript.ReplacePortals;
			replacePortals.Add(new ReplacePortal { fromSignal = "city8,2", toSignal = "city17,1" });
			replacePortals.Add(new ReplacePortal { fromSignal = "city12,1", toSignal = "city22,2" });
			replacePortals.Add(new ReplacePortal { fromSignal = "city12,98", toSignal = "city16,97" });

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportSmokestackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "mrlock1", exportName = "mrlock15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock7", exportName = "mrlock16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock2", exportName = "mrlock17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock3", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock8", exportName = "mrlock18", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair5", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair6", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair7", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair17", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair19", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair20", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair21", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair23", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair3", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair29", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair28", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair24", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair25", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair26", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair13", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair14", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair27", isLevel = true });

			// 04-02-1999 only.
			//importFiles.Add(new ImportFile { importName = "lair8", isLevel = true });
			//importFiles.Add(new ImportFile { importName = "lair18", isLevel = true });
			//importFiles.Add(new ImportFile { importName = "lair22", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair32", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair33", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair34", isLevel = true, removePortals = new string[] { "lair35,56" } });
			//importFiles.Add(new ImportFile { importName = "lair35", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair31", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair30", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair2", isLevel = true, /*removePortals = new string[] { "lair32,50" }*/ });

			importFiles.Add(new ImportFile { importName = "mrlock4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock5", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock6", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock13", isLevel = true });

			importFiles.Add(new ImportFile { importName = "hitme", isLevel = false }); // Check this one. Looks glitchy. No texture.
			importFiles.Add(new ImportFile { importName = "ispirit", isLevel = false }); // Also glitchy.
			importFiles.Add(new ImportFile { importName = "trifrca", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcb", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcc", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcd", isLevel = false });
			importFiles.Add(new ImportFile { importName = "blade", isLevel = false });
			importFiles.Add(new ImportFile { importName = "bring", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoora", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoorb", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoore", isLevel = false });
			importFiles.Add(new ImportFile { importName = "dndoor", isLevel = false });
			importFiles.Add(new ImportFile { importName = "lrdial", isLevel = false });
			importFiles.Add(new ImportFile { importName = "steam", isLevel = false });
			importFiles.Add(new ImportFile { importName = "lairdr", isLevel = false });
			importFiles.Add(new ImportFile { importName = "moregg", isLevel = false });

			List<ReplacePortal> replacePortals = importScript.ReplacePortals;
			replacePortals.Add(new ReplacePortal { fromSignal = "hubb3,2", toSignal = "mrlock15,1" });

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportRetreatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "retreat1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "retreat2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "retreat3", isLevel = true });

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportOraclesCaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "nightb5", exportName = "nightb12", isLevel = true, removePortals = new string[] { "nightb7,41" } });
			//importFiles.Add(new ImportFile { importName = "nightb7", isLevel = true });
			importFiles.Add(new ImportFile { importName = "adda1", isLevel = true, /*removePortals = new string[] { "nightb5,50" }*/ });
			importFiles.Add(new ImportFile { importName = "adda2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "oracle2", exportName = "oracle25", isLevel = true });

			// Retail
			// oracle2,5 - oracle23,6
			// oracle2,3 - oracle3,4
			// oracle2,53 - mrlock14,99
			// oracle2,24 - oracle4,1

			// Feb 16
			// oracle2,5 - oracle1,6
			// oracle2,3 - oracle3,4
			// oracle2,53 - adda1,52

			List<ReplacePortal> replacePortals = importScript.ReplacePortals;
			replacePortals.Add(new ReplacePortal { fromSignal = "oracle23,6", toSignal = "oracle25,5" });
			replacePortals.Add(new ReplacePortal { fromSignal = "oracle3,4", toSignal = "oracle25,3" });
			replacePortals.Add(new ReplacePortal { fromSignal = "nightb4,81", toSignal = "nightb12,80" });

			replacePortals.Add(new ReplacePortal { fromSignal = "adda1,51", toSignal = "nightb12,50" });
			replacePortals.Add(new ReplacePortal { fromSignal = "adda1,52", toSignal = "oracle25,53" });
            replacePortals.Add(new ReplacePortal { fromSignal = "adda1,56", toSignal = "adda2,55" });

            DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportAllCutAreasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;
			List<ReplacePortal> replacePortals = importScript.ReplacePortals;

			#region Undercity

			importFiles.Add(new ImportFile { importName = "city9", exportName = "city17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city10", exportName = "city18", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city11", exportName = "city22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "city16", isLevel = true/*, removePortals = new string[] { "undrct1,90" }*/ });

			ReplaceObject replaceUndblk = new ReplaceObject { oldObject = "undblk", newObject = "pshblk" };
			ReplaceObject replaceDumbub = new ReplaceObject { oldObject = "dumbub", newObject = "pshblk" };
			//ReplaceObject replacePriests = new ReplaceObject { oldObject = "priests", newObject = "vlgra" };
			ReplaceObject[] replaceUCObjects = new ReplaceObject[] { replaceUndblk, replaceDumbub/*, replacePriests*/ };

			importFiles.Add(new ImportFile { importName = "undrct1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct3", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct5", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct8", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct12", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct17", isLevel = true, replaceObjects = replaceUCObjects });
			importFiles.Add(new ImportFile { importName = "undrct20", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct21", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct22", isLevel = true });
			importFiles.Add(new ImportFile { importName = "undrct23", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lantrn", isLevel = false });
			importFiles.Add(new ImportFile { importName = "priests", isLevel = false });
			//importFiles.Add(new ImportFile { importName = "bwall", isLevel = false }); // Not needed in Feb 16.
			//importFiles.Add(new ImportFile { importName = "swall", isLevel = false }); // Not needed in Feb 16.
			//importFiles.Add(new ImportFile { importName = "undblk", isLevel = false }); // Can't convert. Replace with pshblk.
			//importFiles.Add(new ImportFile { importName = "dumbub", isLevel = false }); // Can't convert. Replace with pshblk.

			replacePortals.Add(new ReplacePortal { fromSignal = "city8,2", toSignal = "city17,1" });
			replacePortals.Add(new ReplacePortal { fromSignal = "city12,1", toSignal = "city22,2" });
			replacePortals.Add(new ReplacePortal { fromSignal = "city12,98", toSignal = "city16,97" });

			#endregion

			#region Smokestack

			importFiles.Add(new ImportFile { importName = "mrlock1", exportName = "mrlock15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock7", exportName = "mrlock16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock2", exportName = "mrlock17", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock3", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock8", exportName = "mrlock18", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair5", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair6", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair7", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair15", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair16", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair17", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair19", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair20", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair21", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair23", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair3", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair29", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair28", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair24", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair25", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair26", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair13", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair14", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair27", isLevel = true });

			// 04-02-1999 only.
			//importFiles.Add(new ImportFile { importName = "lair8", isLevel = true });
			//importFiles.Add(new ImportFile { importName = "lair18", isLevel = true });
			//importFiles.Add(new ImportFile { importName = "lair22", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair32", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair33", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair34", isLevel = true, removePortals = new string[] { "lair35,56" } });
			//importFiles.Add(new ImportFile { importName = "lair35", isLevel = true });

			importFiles.Add(new ImportFile { importName = "lair9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair31", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair30", isLevel = true });
			importFiles.Add(new ImportFile { importName = "lair2", isLevel = true, /*removePortals = new string[] { "lair32,50" }*/ });

			importFiles.Add(new ImportFile { importName = "mrlock4", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock5", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock6", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock9", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock10", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock11", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock12", isLevel = true });
			importFiles.Add(new ImportFile { importName = "mrlock13", isLevel = true });

			importFiles.Add(new ImportFile { importName = "hitme", isLevel = false }); // Check this one. Looks glitchy. No texture.
			importFiles.Add(new ImportFile { importName = "ispirit", isLevel = false }); // Also glitchy.
			importFiles.Add(new ImportFile { importName = "trifrca", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcb", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcc", isLevel = false });
			importFiles.Add(new ImportFile { importName = "trifrcd", isLevel = false });
			importFiles.Add(new ImportFile { importName = "blade", isLevel = false });
			importFiles.Add(new ImportFile { importName = "bring", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoora", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoorb", isLevel = false });
			importFiles.Add(new ImportFile { importName = "ldoore", isLevel = false });
			importFiles.Add(new ImportFile { importName = "dndoor", isLevel = false });
			importFiles.Add(new ImportFile { importName = "lrdial", isLevel = false });
			importFiles.Add(new ImportFile { importName = "steam", isLevel = false });
			importFiles.Add(new ImportFile { importName = "lairdr", isLevel = false });
			importFiles.Add(new ImportFile { importName = "moregg", isLevel = false });

			replacePortals.Add(new ReplacePortal { fromSignal = "hubb3,2", toSignal = "mrlock15,1" });

			#endregion

			#region Oracles's Cave

			importFiles.Add(new ImportFile { importName = "nightb5", exportName = "nightb12", isLevel = true, removePortals = new string[] { "nightb7,41" } });
			//importFiles.Add(new ImportFile { importName = "nightb7", isLevel = true });
			importFiles.Add(new ImportFile { importName = "adda1", isLevel = true, /*removePortals = new string[] { "nightb5,50" }*/ });
			importFiles.Add(new ImportFile { importName = "adda2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "oracle2", exportName = "oracle25", isLevel = true });

			// Retail
			// oracle2,5 - oracle23,6
			// oracle2,3 - oracle3,4
			// oracle2,53 - mrlock14,99
			// oracle2,24 - oracle4,1

			// Feb 16
			// oracle2,5 - oracle1,6
			// oracle2,3 - oracle3,4
			// oracle2,53 - adda1,52

			replacePortals.Add(new ReplacePortal { fromSignal = "oracle23,6", toSignal = "oracle25,5" });
			replacePortals.Add(new ReplacePortal { fromSignal = "oracle3,4", toSignal = "oracle25,3" });
			replacePortals.Add(new ReplacePortal { fromSignal = "nightb4,81", toSignal = "nightb12,80" });

			replacePortals.Add(new ReplacePortal { fromSignal = "adda1,51", toSignal = "nightb12,50" });
			replacePortals.Add(new ReplacePortal { fromSignal = "adda1,52", toSignal = "oracle25,53" });
            replacePortals.Add(new ReplacePortal { fromSignal = "adda1,56", toSignal = "adda2,55" });

            #endregion

            #region Mountain Retreat

            importFiles.Add(new ImportFile { importName = "retreat1", isLevel = true });
			importFiles.Add(new ImportFile { importName = "retreat2", isLevel = true });
			importFiles.Add(new ImportFile { importName = "retreat3", isLevel = true });

			#endregion

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void ImportMovieRoomsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// LOAD_GetBigFileFileIndex for Error no. 357.

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select root folder.";
			dialog.ShowNewFolderButton = false;

			string recentFolder = Properties.Settings.Default.RecentFolder;
			if (recentFolder != null && Directory.Exists(recentFolder))
			{
				dialog.SelectedPath = recentFolder;
			}

			if (dialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			Properties.Settings.Default.RecentFolder = dialog.SelectedPath;
			Properties.Settings.Default.Save();

			ImportScript importScript = new ImportScript();

			List<ImportFile> importFiles = importScript.ImportFiles;

			importFiles.Add(new ImportFile { importName = "movie1", isLevel = true, flags = ImportFile.Flags.ForceWaterTranslucent });
			importFiles.Add(new ImportFile { importName = "movie2", isLevel = true, flags = ImportFile.Flags.RemoveAnimatedTextures });
			importFiles.Add(new ImportFile { importName = "movie3", isLevel = true, flags = ImportFile.Flags.RemoveAnimatedTextures });
			importFiles.Add(new ImportFile { importName = "movie4", isLevel = true, flags = ImportFile.Flags.RemoveAnimatedTextures });
			importFiles.Add(new ImportFile { importName = "movie5", isLevel = true, flags = ImportFile.Flags.RemoveAnimatedTextures });
			importFiles.Add(new ImportFile { importName = "movie6", isLevel = true, flags = ImportFile.Flags.RemoveAnimatedTextures });
			importFiles.Add(new ImportFile { importName = "prthstr", isLevel = false });

			DoScriptedImport(dialog.SelectedPath, importScript);
		}

		private void OpenParticlesPCFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Filter =
					"Soul Reaver Files|*.pcm;*.drm|" +
					"All Files (*.*)|*.*",
				DefaultExt = "pcm",
				FilterIndex = 1
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					//string filePath = _repository.MakeObjectFilePath("particle", true);
					particlesPanel.Open(dialog.FileName, dialog.FileName);
				}
				catch (Exception ex)
				{

				}

				displayModeTabs.SelectedTab = particlesTab;
				saveParticlesFileToolStripMenuItem.Enabled = true;
			}
		}

		private void OpenParticlesPSXFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				CheckFileExists = true,
				CheckPathExists = true,
				Filter =
					"Soul Reaver Files|*.pcm;*.drm|" +
					"All Files (*.*)|*.*",
				DefaultExt = "pcm",
				FilterIndex = 1
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					//string filePath = _repository.MakeObjectFilePath("particle", true);
					particlesPanel.Open(dialog.FileName, dialog.FileName);
				}
				catch (Exception ex)
				{

				}

				displayModeTabs.SelectedTab = particlesTab;
				saveParticlesFileToolStripMenuItem.Enabled = true;
			}
		}

		private void SaveParticlesFileToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog
			{
				Filter = "Soul Reaver Files|*.pcm;*.drm",
				DefaultExt = "pcm",
				FilterIndex = 1
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					particlesPanel.Save(dialog.FileName);
				}
				catch (Exception ex)
				{

				}
			}
		}

		private void addTextureFileToProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_repository != null)
			{
				OpenFileDialog dialog = new OpenFileDialog
				{
					CheckFileExists = true,
					CheckPathExists = true,
					Filter =
						"Texture Archive (*.big)|*.big|" +
						"PNG (*.png)|*.png|" +
						"Bitmap (*.bmp)|*.bmp",
					DefaultExt = "big",
					FilterIndex = 1
				};

				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

				if (dialog.FilterIndex == 1)
				{
					Repository repository = _repository;

					Invoke(new MethodInvoker(BeginImportTextures));

					Thread loadingThread = new Thread((() =>
					{
						repository.AddAdditionalTextures(dialog.FileName);
						_repository = repository;

						Invoke(new MethodInvoker(EndImportTextures));
					}));

					loadingThread.Name = "ImportTexturesThread";
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

					//_repository.AddAdditionalTextures(dialog.FileName);
					return;
				}

				Bitmap bitmap = new Bitmap(dialog.FileName);

				if (bitmap.Width == 256 && bitmap.Height == 256)
				{
					int newTextureIndex = _repository.Textures.Count;

					string textureName = _repository.MakeTextureFilePath(newTextureIndex, true);
					bitmap.Save(textureName);

					TexDesc texture = new TexDesc();
					texture.TextureIndex = newTextureIndex;
					texture.FilePath = _repository.MakeTextureFilePath(newTextureIndex);
					texture.IsNew = true;

					_repository.Textures.Add(texture);
				}
				else
				{
					MessageBox.Show("Image size must be 256x256 pixels.", "Invalid Size");
				}
			}
		}
	}
}
