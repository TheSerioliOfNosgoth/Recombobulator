namespace Recombobulator
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			TreeList.TreeListColumn treeListColumn1 = new TreeList.TreeListColumn("Offset", "Offset");
			TreeList.TreeListColumn treeListColumn2 = new TreeList.TreeListColumn("Type", "Type");
			TreeList.TreeListColumn treeListColumn3 = new TreeList.TreeListColumn("Name", "Name");
			TreeList.TreeListColumn treeListColumn4 = new TreeList.TreeListColumn("Value", "Value");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this._mainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addDataFileToProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.particlesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openParticlesPCFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openParticlesPSXFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveParticlesFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptedImportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importUndercityFeb04ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importUndercityFeb16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importSmokestackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importRetreatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importOraclesCaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importAllCutAreasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.compileProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bulkTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detailedExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.briefExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pcmFileTreeListView = new TreeList.TreeListView();
			this.pcmFileSplitContainer = new System.Windows.Forms.SplitContainer();
			this.pcmFileSummary = new System.Windows.Forms.TextBox();
			this.displayModeTabs = new System.Windows.Forms.TabControl();
			this.projectTab = new System.Windows.Forms.TabPage();
			this.projectSplitContainer = new System.Windows.Forms.SplitContainer();
			this.projectTreeView = new System.Windows.Forms.TreeView();
			this.projectTextBox = new System.Windows.Forms.TextBox();
			this.pcmFileDataTab = new System.Windows.Forms.TabPage();
			this.scriptsTab = new System.Windows.Forms.TabPage();
			this.scripts = new System.Windows.Forms.TextBox();
			this.testResultsTab = new System.Windows.Forms.TabPage();
			this.testResults = new System.Windows.Forms.TextBox();
			this.particlesTab = new System.Windows.Forms.TabPage();
			this.particlesPanel = new Recombobulator.ParticlePanels.MainParticlesPanel();
			this.projectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editPortalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importMovieRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pcmFileTreeListView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pcmFileSplitContainer)).BeginInit();
			this.pcmFileSplitContainer.Panel1.SuspendLayout();
			this.pcmFileSplitContainer.Panel2.SuspendLayout();
			this.pcmFileSplitContainer.SuspendLayout();
			this.displayModeTabs.SuspendLayout();
			this.projectTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.projectSplitContainer)).BeginInit();
			this.projectSplitContainer.Panel1.SuspendLayout();
			this.projectSplitContainer.Panel2.SuspendLayout();
			this.projectSplitContainer.SuspendLayout();
			this.pcmFileDataTab.SuspendLayout();
			this.scriptsTab.SuspendLayout();
			this.testResultsTab.SuspendLayout();
			this.particlesTab.SuspendLayout();
			this.projectContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainMenu
			// 
			this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this._mainMenu.Location = new System.Drawing.Point(0, 0);
			this._mainMenu.Name = "_mainMenu";
			this._mainMenu.Size = new System.Drawing.Size(800, 24);
			this._mainMenu.TabIndex = 0;
			this._mainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.dataFileToolStripMenuItem,
            this.particlesFileToolStripMenuItem,
            this.scriptedImportsToolStripMenuItem,
            this.compileProjectToolStripMenuItem,
            this.testExportToolStripMenuItem,
            this.bulkTestingToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newProjectToolStripMenuItem
			// 
			this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
			this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.newProjectToolStripMenuItem.Text = "New Project...";
			this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItem_Click);
			// 
			// openProjectToolStripMenuItem
			// 
			this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
			this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openProjectToolStripMenuItem.Text = "Open Project...";
			this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.OpenProjectToolStripMenuItem_Click);
			// 
			// dataFileToolStripMenuItem
			// 
			this.dataFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataFileToolStripMenuItem,
            this.addDataFileToProjectToolStripMenuItem});
			this.dataFileToolStripMenuItem.Name = "dataFileToolStripMenuItem";
			this.dataFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.dataFileToolStripMenuItem.Text = "Data File";
			// 
			// openDataFileToolStripMenuItem
			// 
			this.openDataFileToolStripMenuItem.Name = "openDataFileToolStripMenuItem";
			this.openDataFileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.openDataFileToolStripMenuItem.Text = "Open...";
			this.openDataFileToolStripMenuItem.Click += new System.EventHandler(this.OpenDataFileToolStripMenuItem_Click);
			// 
			// addDataFileToProjectToolStripMenuItem
			// 
			this.addDataFileToProjectToolStripMenuItem.Enabled = false;
			this.addDataFileToProjectToolStripMenuItem.Name = "addDataFileToProjectToolStripMenuItem";
			this.addDataFileToProjectToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.addDataFileToProjectToolStripMenuItem.Text = "Add To Project...";
			this.addDataFileToProjectToolStripMenuItem.Click += new System.EventHandler(this.AddDataFileToProjectToolStripMenuItem_Click);
			// 
			// particlesFileToolStripMenuItem
			// 
			this.particlesFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openParticlesPCFileToolStripMenuItem,
            this.openParticlesPSXFileToolStripMenuItem,
            this.saveParticlesFileToolStripMenuItem});
			this.particlesFileToolStripMenuItem.Name = "particlesFileToolStripMenuItem";
			this.particlesFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.particlesFileToolStripMenuItem.Text = "Particles File";
			// 
			// openParticlesPCFileToolStripMenuItem
			// 
			this.openParticlesPCFileToolStripMenuItem.Name = "openParticlesPCFileToolStripMenuItem";
			this.openParticlesPCFileToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.openParticlesPCFileToolStripMenuItem.Text = "Open PC...";
			this.openParticlesPCFileToolStripMenuItem.Click += new System.EventHandler(this.OpenParticlesPCFileToolStripMenuItem_Click);
			// 
			// openParticlesPSXFileToolStripMenuItem
			// 
			this.openParticlesPSXFileToolStripMenuItem.Name = "openParticlesPSXFileToolStripMenuItem";
			this.openParticlesPSXFileToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.openParticlesPSXFileToolStripMenuItem.Text = "Open PSX...";
			this.openParticlesPSXFileToolStripMenuItem.Click += new System.EventHandler(this.OpenParticlesPSXFileToolStripMenuItem_Click);
			// 
			// saveParticlesFileToolStripMenuItem
			// 
			this.saveParticlesFileToolStripMenuItem.Enabled = false;
			this.saveParticlesFileToolStripMenuItem.Name = "saveParticlesFileToolStripMenuItem";
			this.saveParticlesFileToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.saveParticlesFileToolStripMenuItem.Text = "Save...";
			this.saveParticlesFileToolStripMenuItem.Click += new System.EventHandler(this.SaveParticlesFileToolStripMenuItem_Click);
			// 
			// scriptedImportsToolStripMenuItem
			// 
			this.scriptedImportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importUndercityFeb04ToolStripMenuItem,
            this.importUndercityFeb16ToolStripMenuItem,
            this.importSmokestackToolStripMenuItem,
            this.importRetreatToolStripMenuItem,
            this.importOraclesCaveToolStripMenuItem,
            this.importAllCutAreasToolStripMenuItem,
            this.importMovieRoomsToolStripMenuItem});
			this.scriptedImportsToolStripMenuItem.Enabled = false;
			this.scriptedImportsToolStripMenuItem.Name = "scriptedImportsToolStripMenuItem";
			this.scriptedImportsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.scriptedImportsToolStripMenuItem.Text = "Scripted Imports";
			// 
			// importUndercityFeb04ToolStripMenuItem
			// 
			this.importUndercityFeb04ToolStripMenuItem.Name = "importUndercityFeb04ToolStripMenuItem";
			this.importUndercityFeb04ToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importUndercityFeb04ToolStripMenuItem.Text = "Import Undercity - Feb 04...";
			this.importUndercityFeb04ToolStripMenuItem.Click += new System.EventHandler(this.ImportUndercityFeb04ToolStripMenuItem_Click);
			// 
			// importUndercityFeb16ToolStripMenuItem
			// 
			this.importUndercityFeb16ToolStripMenuItem.Name = "importUndercityFeb16ToolStripMenuItem";
			this.importUndercityFeb16ToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importUndercityFeb16ToolStripMenuItem.Text = "Import Undercity - Feb 16...";
			this.importUndercityFeb16ToolStripMenuItem.Click += new System.EventHandler(this.ImportUndercityFeb16ToolStripMenuItem_Click);
			// 
			// importSmokestackToolStripMenuItem
			// 
			this.importSmokestackToolStripMenuItem.Name = "importSmokestackToolStripMenuItem";
			this.importSmokestackToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importSmokestackToolStripMenuItem.Text = "Import Smokestack...";
			this.importSmokestackToolStripMenuItem.Click += new System.EventHandler(this.ImportSmokestackToolStripMenuItem_Click);
			// 
			// importRetreatToolStripMenuItem
			// 
			this.importRetreatToolStripMenuItem.Name = "importRetreatToolStripMenuItem";
			this.importRetreatToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importRetreatToolStripMenuItem.Text = "Import Retreat...";
			this.importRetreatToolStripMenuItem.Click += new System.EventHandler(this.ImportRetreatToolStripMenuItem_Click);
			// 
			// importOraclesCaveToolStripMenuItem
			// 
			this.importOraclesCaveToolStripMenuItem.Name = "importOraclesCaveToolStripMenuItem";
			this.importOraclesCaveToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importOraclesCaveToolStripMenuItem.Text = "Import Oracle\'s Cave...";
			this.importOraclesCaveToolStripMenuItem.Click += new System.EventHandler(this.ImportOraclesCaveToolStripMenuItem_Click);
			// 
			// importAllCutAreasToolStripMenuItem
			// 
			this.importAllCutAreasToolStripMenuItem.Name = "importAllCutAreasToolStripMenuItem";
			this.importAllCutAreasToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importAllCutAreasToolStripMenuItem.Text = "Import All Cut Areas...";
			this.importAllCutAreasToolStripMenuItem.Click += new System.EventHandler(this.ImportAllCutAreasToolStripMenuItem_Click);
			// 
			// compileProjectToolStripMenuItem
			// 
			this.compileProjectToolStripMenuItem.Enabled = false;
			this.compileProjectToolStripMenuItem.Name = "compileProjectToolStripMenuItem";
			this.compileProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.compileProjectToolStripMenuItem.Text = "Compile Project...";
			this.compileProjectToolStripMenuItem.Click += new System.EventHandler(this.CompileProjectToolStripMenuItem_Click);
			// 
			// testExportToolStripMenuItem
			// 
			this.testExportToolStripMenuItem.Enabled = false;
			this.testExportToolStripMenuItem.Name = "testExportToolStripMenuItem";
			this.testExportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.testExportToolStripMenuItem.Text = "Test Export";
			this.testExportToolStripMenuItem.Click += new System.EventHandler(this.TestExportToolStripMenuItem_Click);
			// 
			// bulkTestingToolStripMenuItem
			// 
			this.bulkTestingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailedExportToolStripMenuItem,
            this.briefExportToolStripMenuItem});
			this.bulkTestingToolStripMenuItem.Name = "bulkTestingToolStripMenuItem";
			this.bulkTestingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.bulkTestingToolStripMenuItem.Text = "Bulk Testing";
			// 
			// detailedExportToolStripMenuItem
			// 
			this.detailedExportToolStripMenuItem.Name = "detailedExportToolStripMenuItem";
			this.detailedExportToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.detailedExportToolStripMenuItem.Text = "Detailed Export";
			this.detailedExportToolStripMenuItem.Click += new System.EventHandler(this.DetailedExportToolStripMenuItem_Click);
			// 
			// briefExportToolStripMenuItem
			// 
			this.briefExportToolStripMenuItem.Name = "briefExportToolStripMenuItem";
			this.briefExportToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.briefExportToolStripMenuItem.Text = "Brief Export";
			this.briefExportToolStripMenuItem.Click += new System.EventHandler(this.BriefExportToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// pcmFileTreeListView
			// 
			treeListColumn1.AutoSizeMinSize = 170;
			treeListColumn1.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
			treeListColumn1.Width = 170;
			treeListColumn2.AutoSizeMinSize = 80;
			treeListColumn2.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
			treeListColumn2.Width = 150;
			treeListColumn3.AutoSizeMinSize = 100;
			treeListColumn3.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
			treeListColumn3.Width = 200;
			treeListColumn4.AutoSize = true;
			treeListColumn4.AutoSizeMinSize = 50;
			treeListColumn4.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
			treeListColumn4.Width = 50;
			this.pcmFileTreeListView.Columns.AddRange(new TreeList.TreeListColumn[] {
            treeListColumn1,
            treeListColumn2,
            treeListColumn3,
            treeListColumn4});
			this.pcmFileTreeListView.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.pcmFileTreeListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcmFileTreeListView.Images = null;
			this.pcmFileTreeListView.Location = new System.Drawing.Point(0, 0);
			this.pcmFileTreeListView.Name = "pcmFileTreeListView";
			this.pcmFileTreeListView.Size = new System.Drawing.Size(792, 356);
			this.pcmFileTreeListView.TabIndex = 1;
			this.pcmFileTreeListView.Text = "treeListView";
			// 
			// pcmFileSplitContainer
			// 
			this.pcmFileSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcmFileSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.pcmFileSplitContainer.Margin = new System.Windows.Forms.Padding(0);
			this.pcmFileSplitContainer.Name = "pcmFileSplitContainer";
			this.pcmFileSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// pcmFileSplitContainer.Panel1
			// 
			this.pcmFileSplitContainer.Panel1.Controls.Add(this.pcmFileTreeListView);
			// 
			// pcmFileSplitContainer.Panel2
			// 
			this.pcmFileSplitContainer.Panel2.Controls.Add(this.pcmFileSummary);
			this.pcmFileSplitContainer.Size = new System.Drawing.Size(792, 400);
			this.pcmFileSplitContainer.SplitterDistance = 356;
			this.pcmFileSplitContainer.TabIndex = 2;
			// 
			// pcmFileSummary
			// 
			this.pcmFileSummary.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pcmFileSummary.Location = new System.Drawing.Point(0, 0);
			this.pcmFileSummary.Multiline = true;
			this.pcmFileSummary.Name = "pcmFileSummary";
			this.pcmFileSummary.ReadOnly = true;
			this.pcmFileSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.pcmFileSummary.Size = new System.Drawing.Size(792, 40);
			this.pcmFileSummary.TabIndex = 0;
			// 
			// displayModeTabs
			// 
			this.displayModeTabs.Controls.Add(this.projectTab);
			this.displayModeTabs.Controls.Add(this.pcmFileDataTab);
			this.displayModeTabs.Controls.Add(this.scriptsTab);
			this.displayModeTabs.Controls.Add(this.testResultsTab);
			this.displayModeTabs.Controls.Add(this.particlesTab);
			this.displayModeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.displayModeTabs.Location = new System.Drawing.Point(0, 24);
			this.displayModeTabs.Margin = new System.Windows.Forms.Padding(0);
			this.displayModeTabs.Name = "displayModeTabs";
			this.displayModeTabs.SelectedIndex = 0;
			this.displayModeTabs.Size = new System.Drawing.Size(800, 426);
			this.displayModeTabs.TabIndex = 3;
			// 
			// projectTab
			// 
			this.projectTab.Controls.Add(this.projectSplitContainer);
			this.projectTab.Location = new System.Drawing.Point(4, 22);
			this.projectTab.Name = "projectTab";
			this.projectTab.Padding = new System.Windows.Forms.Padding(3);
			this.projectTab.Size = new System.Drawing.Size(792, 400);
			this.projectTab.TabIndex = 3;
			this.projectTab.Text = "Project";
			this.projectTab.UseVisualStyleBackColor = true;
			// 
			// projectSplitContainer
			// 
			this.projectSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectSplitContainer.Location = new System.Drawing.Point(3, 3);
			this.projectSplitContainer.Name = "projectSplitContainer";
			// 
			// projectSplitContainer.Panel1
			// 
			this.projectSplitContainer.Panel1.Controls.Add(this.projectTreeView);
			// 
			// projectSplitContainer.Panel2
			// 
			this.projectSplitContainer.Panel2.Controls.Add(this.projectTextBox);
			this.projectSplitContainer.Size = new System.Drawing.Size(786, 394);
			this.projectSplitContainer.SplitterDistance = 262;
			this.projectSplitContainer.TabIndex = 0;
			// 
			// projectTreeView
			// 
			this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectTreeView.Location = new System.Drawing.Point(0, 0);
			this.projectTreeView.Name = "projectTreeView";
			this.projectTreeView.Size = new System.Drawing.Size(262, 394);
			this.projectTreeView.TabIndex = 0;
			this.projectTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectTreeView_AfterSelect);
			this.projectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectTreeView_NodeMouseClick);
			// 
			// projectTextBox
			// 
			this.projectTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.projectTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.projectTextBox.Location = new System.Drawing.Point(0, 0);
			this.projectTextBox.Multiline = true;
			this.projectTextBox.Name = "projectTextBox";
			this.projectTextBox.ReadOnly = true;
			this.projectTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.projectTextBox.Size = new System.Drawing.Size(520, 394);
			this.projectTextBox.TabIndex = 0;
			// 
			// pcmFileDataTab
			// 
			this.pcmFileDataTab.Controls.Add(this.pcmFileSplitContainer);
			this.pcmFileDataTab.Location = new System.Drawing.Point(4, 22);
			this.pcmFileDataTab.Margin = new System.Windows.Forms.Padding(0);
			this.pcmFileDataTab.Name = "pcmFileDataTab";
			this.pcmFileDataTab.Size = new System.Drawing.Size(792, 400);
			this.pcmFileDataTab.TabIndex = 0;
			this.pcmFileDataTab.Text = "File Data";
			this.pcmFileDataTab.UseVisualStyleBackColor = true;
			// 
			// scriptsTab
			// 
			this.scriptsTab.Controls.Add(this.scripts);
			this.scriptsTab.Location = new System.Drawing.Point(4, 22);
			this.scriptsTab.Margin = new System.Windows.Forms.Padding(0);
			this.scriptsTab.Name = "scriptsTab";
			this.scriptsTab.Size = new System.Drawing.Size(792, 400);
			this.scriptsTab.TabIndex = 1;
			this.scriptsTab.Text = "Scripts";
			this.scriptsTab.UseVisualStyleBackColor = true;
			// 
			// scripts
			// 
			this.scripts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scripts.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scripts.Location = new System.Drawing.Point(0, 0);
			this.scripts.Multiline = true;
			this.scripts.Name = "scripts";
			this.scripts.ReadOnly = true;
			this.scripts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.scripts.Size = new System.Drawing.Size(792, 400);
			this.scripts.TabIndex = 0;
			// 
			// testResultsTab
			// 
			this.testResultsTab.Controls.Add(this.testResults);
			this.testResultsTab.Location = new System.Drawing.Point(4, 22);
			this.testResultsTab.Margin = new System.Windows.Forms.Padding(0);
			this.testResultsTab.Name = "testResultsTab";
			this.testResultsTab.Size = new System.Drawing.Size(792, 400);
			this.testResultsTab.TabIndex = 2;
			this.testResultsTab.Text = "Test Results";
			this.testResultsTab.UseVisualStyleBackColor = true;
			// 
			// testResults
			// 
			this.testResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.testResults.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.testResults.Location = new System.Drawing.Point(0, 0);
			this.testResults.Multiline = true;
			this.testResults.Name = "testResults";
			this.testResults.ReadOnly = true;
			this.testResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.testResults.Size = new System.Drawing.Size(792, 400);
			this.testResults.TabIndex = 0;
			// 
			// particlesTab
			// 
			this.particlesTab.Controls.Add(this.particlesPanel);
			this.particlesTab.Location = new System.Drawing.Point(4, 22);
			this.particlesTab.Name = "particlesTab";
			this.particlesTab.Size = new System.Drawing.Size(792, 400);
			this.particlesTab.TabIndex = 4;
			this.particlesTab.Text = "Particles";
			this.particlesTab.UseVisualStyleBackColor = true;
			// 
			// particlesPanel
			// 
			this.particlesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.particlesPanel.Location = new System.Drawing.Point(0, 0);
			this.particlesPanel.Name = "particlesPanel";
			this.particlesPanel.Size = new System.Drawing.Size(792, 400);
			this.particlesPanel.TabIndex = 0;
			// 
			// projectContextMenu
			// 
			this.projectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPortalToolStripMenuItem});
			this.projectContextMenu.Name = "projectContextMenu";
			this.projectContextMenu.Size = new System.Drawing.Size(138, 26);
			// 
			// editPortalToolStripMenuItem
			// 
			this.editPortalToolStripMenuItem.Enabled = false;
			this.editPortalToolStripMenuItem.Name = "editPortalToolStripMenuItem";
			this.editPortalToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.editPortalToolStripMenuItem.Text = "Edit Portal...";
			this.editPortalToolStripMenuItem.Click += new System.EventHandler(this.EditPortalToolStripMenuItem_Click);
			// 
			// importMovieRoomsToolStripMenuItem
			// 
			this.importMovieRoomsToolStripMenuItem.Name = "importMovieRoomsToolStripMenuItem";
			this.importMovieRoomsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			this.importMovieRoomsToolStripMenuItem.Text = "Import Movie Rooms...";
			this.importMovieRoomsToolStripMenuItem.Click += new System.EventHandler(this.ImportMovieRoomsToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.displayModeTabs);
			this.Controls.Add(this._mainMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this._mainMenu;
			this.Name = "MainForm";
			this.Text = "Recombobulator";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this._mainMenu.ResumeLayout(false);
			this._mainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pcmFileTreeListView)).EndInit();
			this.pcmFileSplitContainer.Panel1.ResumeLayout(false);
			this.pcmFileSplitContainer.Panel2.ResumeLayout(false);
			this.pcmFileSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pcmFileSplitContainer)).EndInit();
			this.pcmFileSplitContainer.ResumeLayout(false);
			this.displayModeTabs.ResumeLayout(false);
			this.projectTab.ResumeLayout(false);
			this.projectSplitContainer.Panel1.ResumeLayout(false);
			this.projectSplitContainer.Panel2.ResumeLayout(false);
			this.projectSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.projectSplitContainer)).EndInit();
			this.projectSplitContainer.ResumeLayout(false);
			this.pcmFileDataTab.ResumeLayout(false);
			this.scriptsTab.ResumeLayout(false);
			this.scriptsTab.PerformLayout();
			this.testResultsTab.ResumeLayout(false);
			this.testResultsTab.PerformLayout();
			this.particlesTab.ResumeLayout(false);
			this.projectContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip _mainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem testExportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bulkTestingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private TreeList.TreeListView pcmFileTreeListView;
		private System.Windows.Forms.SplitContainer pcmFileSplitContainer;
		private System.Windows.Forms.TextBox pcmFileSummary;
		private System.Windows.Forms.TabControl displayModeTabs;
		private System.Windows.Forms.TabPage pcmFileDataTab;
		private System.Windows.Forms.TabPage scriptsTab;
		private System.Windows.Forms.TextBox scripts;
		private System.Windows.Forms.TabPage testResultsTab;
		private System.Windows.Forms.TextBox testResults;
		private System.Windows.Forms.ToolStripMenuItem detailedExportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem briefExportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
		private System.Windows.Forms.TabPage projectTab;
		private System.Windows.Forms.ToolStripMenuItem compileProjectToolStripMenuItem;
		private System.Windows.Forms.SplitContainer projectSplitContainer;
		private System.Windows.Forms.TreeView projectTreeView;
		private System.Windows.Forms.TextBox projectTextBox;
		private System.Windows.Forms.ContextMenuStrip projectContextMenu;
		private System.Windows.Forms.ToolStripMenuItem editPortalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scriptedImportsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importUndercityFeb04ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importSmokestackToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importRetreatToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importOraclesCaveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importAllCutAreasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importUndercityFeb16ToolStripMenuItem;
		private System.Windows.Forms.TabPage particlesTab;
		private ParticlePanels.MainParticlesPanel particlesPanel;
		private System.Windows.Forms.ToolStripMenuItem dataFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem particlesFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDataFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addDataFileToProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openParticlesPCFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveParticlesFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openParticlesPSXFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importMovieRoomsToolStripMenuItem;
	}
}

