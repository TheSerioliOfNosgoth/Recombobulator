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
	public partial class MainParticlesPanel : UserControl
	{
		SR1_File _file = new SR1_File();
		SR1_File _fileBackup = new SR1_File();
		bool _isFileLoaded = false;
		bool _hasBackup = false;

		public MainParticlesPanel()
		{
			InitializeComponent();

			categoriesTreeView.Nodes.Add(CreateNode("Particles", editParticlesPanel));
			categoriesTreeView.Nodes.Add(CreateNode("Ribbons", editRibbonsPanel));
			categoriesTreeView.Nodes.Add(CreateNode("Glows", editGlowsPanel));
			categoriesTreeView.Nodes.Add(CreateNode("Lightnings", editLightningsPanel));
			categoriesTreeView.Nodes.Add(CreateNode("BlastRings", editBlastRingsPanel));
			categoriesTreeView.Nodes.Add(CreateNode("Flashes", editFlashesPanel));

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
			var ribbonList = (SR1_StructureSeries<GenericRibbonParams>)_file._Structures[genericFXObject.RibbonList.Offset];
			var glowList = (SR1_StructureSeries<GenericGlowParams>)_file._Structures[genericFXObject.GlowList.Offset];
			var lightningList = (SR1_StructureSeries<GenericLightningParams>)_file._Structures[genericFXObject.LightningList.Offset];
			var blastList = (SR1_StructureSeries<GenericBlastRingParams>)_file._Structures[genericFXObject.BlastList.Offset];
			var flashList = (SR1_StructureSeries<GenericFlashParams>)_file._Structures[genericFXObject.FlashList.Offset];

			if (_hasBackup)
			{
				var srBackupObject = (SR1Structures.Object)_fileBackup._Structures[0];
				var backupGenericFXObject = (GenericFXObject)_fileBackup._Structures[srBackupObject.data.Offset];
				var backupParticlesList = (SR1_StructureSeries<GenericParticleParams>)_fileBackup._Structures[backupGenericFXObject.ParticleList.Offset];
				var backupRibbonList = (SR1_StructureSeries<GenericRibbonParams>)_fileBackup._Structures[genericFXObject.RibbonList.Offset];
				var backupGlowList = (SR1_StructureSeries<GenericGlowParams>)_fileBackup._Structures[genericFXObject.GlowList.Offset];
				var backupLightningList = (SR1_StructureSeries<GenericLightningParams>)_fileBackup._Structures[genericFXObject.LightningList.Offset];
				var backupBlastList = (SR1_StructureSeries<GenericBlastRingParams>)_fileBackup._Structures[genericFXObject.BlastList.Offset];
				var backupFlashList = (SR1_StructureSeries<GenericFlashParams>)_fileBackup._Structures[genericFXObject.FlashList.Offset];

				editParticlesPanel.Open(particlesList, backupParticlesList);
				editRibbonsPanel.Open(ribbonList, backupRibbonList);
				editGlowsPanel.Open(glowList, backupGlowList);
				editLightningsPanel.Open(lightningList, backupLightningList);
				editBlastRingsPanel.Open(blastList, backupBlastList);
				editFlashesPanel.Open(flashList, backupFlashList);
			}
			else
			{
				editParticlesPanel.Open(particlesList, null);
				editRibbonsPanel.Open(particlesList, null);
				editGlowsPanel.Open(particlesList, null);
				editLightningsPanel.Open(particlesList, null);
				editBlastRingsPanel.Open(particlesList, null);
				editFlashesPanel.Open(particlesList, null);
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
