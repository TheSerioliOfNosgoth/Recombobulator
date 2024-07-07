namespace Recombobulator.ParticlePanels
{
	partial class MainObjectPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.categoriesTreeView = new System.Windows.Forms.TreeView();
			this.backPanel = new System.Windows.Forms.Panel();
			this.editTextureMT3sPanel = new EditTextureMT3sPanel();
			this.editGlyphTuneDataPanel = new EditGlyphTuneDataPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.backPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.categoriesTreeView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.backPanel);
			this.splitContainer.Size = new System.Drawing.Size(700, 489);
			this.splitContainer.SplitterDistance = 129;
			this.splitContainer.TabIndex = 0;
			// 
			// categoriesTreeView
			// 
			this.categoriesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.categoriesTreeView.Location = new System.Drawing.Point(0, 0);
			this.categoriesTreeView.Name = "categoriesTreeView";
			this.categoriesTreeView.Size = new System.Drawing.Size(129, 489);
			this.categoriesTreeView.TabIndex = 1;
			this.categoriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.categoriesTreeView_AfterSelect);
			// 
			// backPanel
			// 
			this.backPanel.AutoScroll = true;
			this.backPanel.Controls.Add(this.editGlyphTuneDataPanel);
			this.backPanel.Controls.Add(this.editTextureMT3sPanel);
			this.backPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backPanel.Location = new System.Drawing.Point(0, 0);
			this.backPanel.Name = "backPanel";
			this.backPanel.Size = new System.Drawing.Size(567, 489);
			this.backPanel.TabIndex = 2;
			// 
			// editTextureMT3sPanel
			// 
			this.editTextureMT3sPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editTextureMT3sPanel.Enabled = false;
			this.editTextureMT3sPanel.Location = new System.Drawing.Point(0, 0);
			this.editTextureMT3sPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editTextureMT3sPanel.Name = "editTextureMT3sPanel";
			this.editTextureMT3sPanel.Size = new System.Drawing.Size(567, 489);
			this.editTextureMT3sPanel.TabIndex = 3;
			this.editTextureMT3sPanel.Visible = false;
			// 
			// editGlyphTuneDataPanel
			// 
			this.editGlyphTuneDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editGlyphTuneDataPanel.Enabled = false;
			this.editGlyphTuneDataPanel.Location = new System.Drawing.Point(0, 0);
			this.editGlyphTuneDataPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editGlyphTuneDataPanel.Name = "editGlyphTuneDataPanel";
			this.editGlyphTuneDataPanel.Size = new System.Drawing.Size(567, 489);
			this.editGlyphTuneDataPanel.TabIndex = 4;
			this.editGlyphTuneDataPanel.Visible = false;
			// 
			// MainObjectPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "MainObjectPanel";
			this.Size = new System.Drawing.Size(700, 489);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.backPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TreeView categoriesTreeView;
		private System.Windows.Forms.Panel backPanel;
		private EditTextureMT3sPanel editTextureMT3sPanel;
		private EditGlyphTuneDataPanel editGlyphTuneDataPanel;
	}
}
