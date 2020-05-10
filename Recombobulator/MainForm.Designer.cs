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
            TreeList.TreeListColumn treeListColumn5 = new TreeList.TreeListColumn("Offset", "Offset");
            TreeList.TreeListColumn treeListColumn6 = new TreeList.TreeListColumn("Type", "Type");
            TreeList.TreeListColumn treeListColumn7 = new TreeList.TreeListColumn("Name", "Name");
            TreeList.TreeListColumn treeListColumn8 = new TreeList.TreeListColumn("Value", "Value");
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.briefExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeListView = new TreeList.TreeListView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.summary = new System.Windows.Forms.TextBox();
            this.displayModeTabs = new System.Windows.Forms.TabControl();
            this.fileDataTab = new System.Windows.Forms.TabPage();
            this.testResultsTab = new System.Windows.Forms.TabPage();
            this.testResults = new System.Windows.Forms.TextBox();
            this.exportUpgradedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.displayModeTabs.SuspendLayout();
            this.fileDataTab.SuspendLayout();
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
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exportUpgradedToolStripMenuItem,
            this.bulkTestingToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            this.exportToolStripMenuItem.Enabled = false;
            // 
            // exportUpgradedToolStripMenuItem
            // 
            this.exportUpgradedToolStripMenuItem.Name = "exportUpgradedToolStripMenuItem";
            this.exportUpgradedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportUpgradedToolStripMenuItem.Text = "Export Upgraded";
            this.exportUpgradedToolStripMenuItem.Click += new System.EventHandler(this.ExportUpgradedToolStripMenuItem_Click);
            this.exportUpgradedToolStripMenuItem.Enabled = false;
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
            this.detailedExportToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.detailedExportToolStripMenuItem.Text = "Detailed Export";
            this.detailedExportToolStripMenuItem.Click += new System.EventHandler(this.DetailedExportToolStripMenuItem_Click);
            // 
            // briefExportToolStripMenuItem
            // 
            this.briefExportToolStripMenuItem.Name = "briefExportToolStripMenuItem";
            this.briefExportToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
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
            treeListColumn5.AutoSizeMinSize = 170;
            treeListColumn5.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
            treeListColumn5.Width = 170;
            treeListColumn6.AutoSizeMinSize = 80;
            treeListColumn6.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
            treeListColumn6.Width = 150;
            treeListColumn7.AutoSizeMinSize = 100;
            treeListColumn7.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
            treeListColumn7.Width = 200;
            treeListColumn8.AutoSize = true;
            treeListColumn8.AutoSizeMinSize = 50;
            treeListColumn8.HeaderFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
            treeListColumn8.Width = 50;
            this.treeListView.Columns.AddRange(new TreeList.TreeListColumn[] {
            treeListColumn5,
            treeListColumn6,
            treeListColumn7,
            treeListColumn8});
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Images = null;
            this.treeListView.Location = new System.Drawing.Point(0, 0);
            this.treeListView.Name = "treeListView";
            this.treeListView.Size = new System.Drawing.Size(792, 356);
            this.treeListView.TabIndex = 1;
            this.treeListView.Text = "treeListView";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeListView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.summary);
            this.splitContainer.Size = new System.Drawing.Size(792, 400);
            this.splitContainer.SplitterDistance = 356;
            this.splitContainer.TabIndex = 2;
            // 
            // summary
            // 
            this.summary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summary.Location = new System.Drawing.Point(0, 0);
            this.summary.Multiline = true;
            this.summary.Name = "summary";
            this.summary.ReadOnly = true;
            this.summary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.summary.Size = new System.Drawing.Size(792, 40);
            this.summary.TabIndex = 0;
            // 
            // displayModeTabs
            // 
            this.displayModeTabs.Controls.Add(this.fileDataTab);
            this.displayModeTabs.Controls.Add(this.testResultsTab);
            this.displayModeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayModeTabs.Location = new System.Drawing.Point(0, 24);
            this.displayModeTabs.Margin = new System.Windows.Forms.Padding(0);
            this.displayModeTabs.Name = "displayModeTabs";
            this.displayModeTabs.SelectedIndex = 0;
            this.displayModeTabs.Size = new System.Drawing.Size(800, 426);
            this.displayModeTabs.TabIndex = 3;
            // 
            // fileDataTab
            // 
            this.fileDataTab.Controls.Add(this.splitContainer);
            this.fileDataTab.Location = new System.Drawing.Point(4, 22);
            this.fileDataTab.Margin = new System.Windows.Forms.Padding(0);
            this.fileDataTab.Name = "fileDataTab";
            this.fileDataTab.Size = new System.Drawing.Size(792, 400);
            this.fileDataTab.TabIndex = 0;
            this.fileDataTab.Text = "File Data";
            this.fileDataTab.UseVisualStyleBackColor = true;
            // 
            // testResultsTab
            // 
            this.testResultsTab.Controls.Add(this.testResults);
            this.testResultsTab.Location = new System.Drawing.Point(4, 22);
            this.testResultsTab.Margin = new System.Windows.Forms.Padding(0);
            this.testResultsTab.Name = "testResultsTab";
            this.testResultsTab.Size = new System.Drawing.Size(792, 400);
            this.testResultsTab.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.displayModeTabs.ResumeLayout(false);
            this.fileDataTab.ResumeLayout(false);
            this.testResultsTab.ResumeLayout(false);
            this.testResultsTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulkTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private TreeList.TreeListView treeListView;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox summary;
        private System.Windows.Forms.TabControl displayModeTabs;
        private System.Windows.Forms.TabPage fileDataTab;
        private System.Windows.Forms.TabPage testResultsTab;
        private System.Windows.Forms.TextBox testResults;
        private System.Windows.Forms.ToolStripMenuItem detailedExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem briefExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportUpgradedToolStripMenuItem;
    }
}

