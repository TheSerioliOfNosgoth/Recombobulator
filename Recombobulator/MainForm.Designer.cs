﻿namespace Recombobulator
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
            TreeList.TreeListColumn treeListColumn1 = new TreeList.TreeListColumn("Offset", "Offset");
            TreeList.TreeListColumn treeListColumn2 = new TreeList.TreeListColumn("Type", "Type");
            TreeList.TreeListColumn treeListColumn3 = new TreeList.TreeListColumn("Name", "Name");
            TreeList.TreeListColumn treeListColumn4 = new TreeList.TreeListColumn("Value", "Value");
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.briefExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pcmFileTreeListView = new TreeList.TreeListView();
            this.pcmFileSplitContainer = new System.Windows.Forms.SplitContainer();
            this.pcmFileSummary = new System.Windows.Forms.TextBox();
            this.displayModeTabs = new System.Windows.Forms.TabControl();
            this.projectTab = new System.Windows.Forms.TabPage();
            this.pcmFileDataTab = new System.Windows.Forms.TabPage();
            this.scriptsTab = new System.Windows.Forms.TabPage();
            this.scripts = new System.Windows.Forms.TextBox();
            this.testResultsTab = new System.Windows.Forms.TabPage();
            this.testResults = new System.Windows.Forms.TextBox();
            this.compileProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcmFileTreeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcmFileSplitContainer)).BeginInit();
            this.pcmFileSplitContainer.Panel1.SuspendLayout();
            this.pcmFileSplitContainer.Panel2.SuspendLayout();
            this.pcmFileSplitContainer.SuspendLayout();
            this.displayModeTabs.SuspendLayout();
            this.pcmFileDataTab.SuspendLayout();
            this.scriptsTab.SuspendLayout();
            this.testResultsTab.SuspendLayout();
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
            this.openFileToolStripMenuItem,
            this.addToProjectToolStripMenuItem,
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
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openFileToolStripMenuItem.Text = "Open File...";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItem_Click);
            // 
            // testExportToolStripMenuItem
            // 
            this.testExportToolStripMenuItem.Enabled = false;
            this.testExportToolStripMenuItem.Name = "testExportToolStripMenuItem";
            this.testExportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.testExportToolStripMenuItem.Text = "Test Export";
            this.testExportToolStripMenuItem.Click += new System.EventHandler(this.TestExportToolStripMenuItem_Click);
            // 
            // addToProjectToolStripMenuItem
            // 
            this.addToProjectToolStripMenuItem.Enabled = false;
            this.addToProjectToolStripMenuItem.Name = "addToProjectToolStripMenuItem";
            this.addToProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addToProjectToolStripMenuItem.Text = "Add To Project...";
            this.addToProjectToolStripMenuItem.Click += new System.EventHandler(this.AddToProjectToolStripMenuItem_Click);
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
            // treeListView
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
            this.pcmFileTreeListView.Name = "treeListView";
            this.pcmFileTreeListView.Size = new System.Drawing.Size(792, 356);
            this.pcmFileTreeListView.TabIndex = 1;
            this.pcmFileTreeListView.Text = "treeListView";
            // 
            // splitContainer
            // 
            this.pcmFileSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcmFileSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.pcmFileSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.pcmFileSplitContainer.Name = "splitContainer";
            this.pcmFileSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.pcmFileSplitContainer.Panel1.Controls.Add(this.pcmFileTreeListView);
            // 
            // splitContainer.Panel2
            // 
            this.pcmFileSplitContainer.Panel2.Controls.Add(this.pcmFileSummary);
            this.pcmFileSplitContainer.Size = new System.Drawing.Size(792, 400);
            this.pcmFileSplitContainer.SplitterDistance = 356;
            this.pcmFileSplitContainer.TabIndex = 2;
            // 
            // summary
            // 
            this.pcmFileSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcmFileSummary.Location = new System.Drawing.Point(0, 0);
            this.pcmFileSummary.Multiline = true;
            this.pcmFileSummary.Name = "summary";
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
            this.projectTab.Location = new System.Drawing.Point(4, 22);
            this.projectTab.Name = "projectTab";
            this.projectTab.Padding = new System.Windows.Forms.Padding(3);
            this.projectTab.Size = new System.Drawing.Size(792, 400);
            this.projectTab.TabIndex = 3;
            this.projectTab.Text = "Project";
            this.projectTab.UseVisualStyleBackColor = true;
            // 
            // fileDataTab
            // 
            this.pcmFileDataTab.Controls.Add(this.pcmFileSplitContainer);
            this.pcmFileDataTab.Location = new System.Drawing.Point(4, 22);
            this.pcmFileDataTab.Margin = new System.Windows.Forms.Padding(0);
            this.pcmFileDataTab.Name = "fileDataTab";
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
            this.testResults.Location = new System.Drawing.Point(0, 0);
            this.testResults.Multiline = true;
            this.testResults.Name = "testResults";
            this.testResults.ReadOnly = true;
            this.testResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.testResults.Size = new System.Drawing.Size(792, 400);
            this.testResults.TabIndex = 0;
            // 
            // compileProjectToolStripMenuItem
            // 
            this.compileProjectToolStripMenuItem.Enabled = false;
            this.compileProjectToolStripMenuItem.Name = "compileProjectToolStripMenuItem";
            this.compileProjectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.compileProjectToolStripMenuItem.Text = "Compile Project...";
            this.compileProjectToolStripMenuItem.Click += new System.EventHandler(this.compileProjectToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.displayModeTabs);
            this.Controls.Add(this._mainMenu);
            this.MainMenuStrip = this._mainMenu;
            this.Name = "MainForm";
            this.Text = "Recombobulator";
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcmFileTreeListView)).EndInit();
            this.pcmFileSplitContainer.Panel1.ResumeLayout(false);
            this.pcmFileSplitContainer.Panel2.ResumeLayout(false);
            this.pcmFileSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcmFileSplitContainer)).EndInit();
            this.pcmFileSplitContainer.ResumeLayout(false);
            this.displayModeTabs.ResumeLayout(false);
            this.pcmFileDataTab.ResumeLayout(false);
            this.scriptsTab.ResumeLayout(false);
            this.scriptsTab.PerformLayout();
            this.testResultsTab.ResumeLayout(false);
            this.testResultsTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem addToProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.TabPage projectTab;
        private System.Windows.Forms.ToolStripMenuItem compileProjectToolStripMenuItem;
    }
}

