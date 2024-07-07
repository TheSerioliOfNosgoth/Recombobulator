using Recombobulator.SR1Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreeList;
using static Recombobulator.SR1_File;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Recombobulator.ParticlePanels
{
	public partial class MainObjectPanel : UserControl
	{
		SR1_File _file = new SR1_File();
		SR1_File _fileBackup = new SR1_File();
		bool _isFileLoaded = false;
		bool _hasBackup = false;

		public MainObjectPanel()
		{
			InitializeComponent();

			categoriesTreeView.Nodes.Add(CreateNode("TextureMT3s", editTextureMT3sPanel));
			categoriesTreeView.Nodes.Add(CreateNode("GlyphTuneData", editGlyphTuneDataPanel));

			categoriesTreeView.SelectedNode = categoriesTreeView.Nodes[0];
		}

		protected TreeNode CreateNode(string name, UserControl control)
		{
			TreeNode node = new TreeNode(name);
			node.Tag = control;
			return node;
		}

		public void Open(string filePath, string backupFilePath = null)
		{
			try
			{
				_file.Import(filePath, SR1_File.ImportFlags.None, SR1_File.Version.Retail_PC);
				_isFileLoaded = true;

				if (backupFilePath != null)
				{
					_fileBackup.Import(backupFilePath, SR1_File.ImportFlags.None, SR1_File.Version.Retail_PC);
					_hasBackup = true;
				}
				else
				{
					_fileBackup.Reset();
					_hasBackup = false;
				}
			}
			catch (Exception ex)
			{
				_file.Reset();
				_fileBackup.Reset();
				_isFileLoaded = false;
				_hasBackup = false;
				return;
			}

			var srObject = (SR1Structures.Object)_file._Structures[0];
			var genericFXObject = (GenericFXObject)_file._Structures[srObject.data.Offset];
			var particlesList = (SR1_StructureSeries<GenericParticleParams>)_file._Structures[genericFXObject.ParticleList.Offset];

			if (_hasBackup)
			{
				var srBackupObject = (SR1Structures.Object)_fileBackup._Structures[0];
				var backupGenericFXObject = (GenericFXObject)_fileBackup._Structures[srBackupObject.data.Offset];
				var backupParticlesList = (SR1_StructureSeries<GenericParticleParams>)_fileBackup._Structures[backupGenericFXObject.ParticleList.Offset];

				editTextureMT3sPanel.Open(particlesList, backupParticlesList);
				//editTextureMT3sPanel.Open(ribbonList, backupRibbonList);
			}
			else
			{
				editTextureMT3sPanel.Open(particlesList, null);
				//editRibbonsPanel.Open(particlesList, null);
			}
		}

		public void Save(string filePath)
		{
			if (!_isFileLoaded)
			{
				return;
			}

			try
			{
				_file.Export(filePath, SR1_File.Version.Retail_PC, MigrateFlags.None, new Overrides());
			}
			catch (Exception ex)
			{
			}
		}

		private void categoriesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			foreach (TreeNode node in categoriesTreeView.Nodes)
			{
				UserControl control = node.Tag as UserControl;
				if (control == null)
				{
					continue;
				}

				if (node == categoriesTreeView.SelectedNode)
				{
					control.Visible = true;
					control.Enabled = true;
					continue;
				}


				control.Visible = false;
				control.Enabled = false;
			}
		}
	}
}
