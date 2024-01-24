namespace Recombobulator.ParticlePanels
{
	partial class MainParticlesPanel
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
			this.backPamel = new System.Windows.Forms.Panel();
			this.editFlashesPanel = new Recombobulator.ParticlePanels.EditFlashesPanel();
			this.editBlastRingsPanel = new Recombobulator.ParticlePanels.EditBlastRingsPanel();
			this.editLightningsPanel = new Recombobulator.ParticlePanels.EditLightningsPanel();
			this.editGlowsPanel = new Recombobulator.ParticlePanels.EditGlowsPanel();
			this.editRibbonsPanel = new Recombobulator.ParticlePanels.EditRibbonsPanel();
			this.editParticlesPanel = new Recombobulator.ParticlePanels.EditParticlesPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.backPamel.SuspendLayout();
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
			this.splitContainer.Panel2.Controls.Add(this.backPamel);
			this.splitContainer.Size = new System.Drawing.Size(700, 489);
			this.splitContainer.SplitterDistance = 129;
			this.splitContainer.TabIndex = 1;
			// 
			// categoriesTreeView
			// 
			this.categoriesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.categoriesTreeView.Location = new System.Drawing.Point(0, 0);
			this.categoriesTreeView.Name = "categoriesTreeView";
			this.categoriesTreeView.Size = new System.Drawing.Size(129, 489);
			this.categoriesTreeView.TabIndex = 0;
			this.categoriesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.categoriesTreeView_AfterSelect);
			// 
			// backPamel
			// 
			this.backPamel.AutoScroll = true;
			this.backPamel.Controls.Add(this.editFlashesPanel);
			this.backPamel.Controls.Add(this.editBlastRingsPanel);
			this.backPamel.Controls.Add(this.editLightningsPanel);
			this.backPamel.Controls.Add(this.editGlowsPanel);
			this.backPamel.Controls.Add(this.editRibbonsPanel);
			this.backPamel.Controls.Add(this.editParticlesPanel);
			this.backPamel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backPamel.Location = new System.Drawing.Point(0, 0);
			this.backPamel.Name = "backPamel";
			this.backPamel.Size = new System.Drawing.Size(567, 489);
			this.backPamel.TabIndex = 13;
			// 
			// editFlashesPanel
			// 
			this.editFlashesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editFlashesPanel.Enabled = false;
			this.editFlashesPanel.Location = new System.Drawing.Point(0, 0);
			this.editFlashesPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editFlashesPanel.Name = "editFlashesPanel";
			this.editFlashesPanel.Size = new System.Drawing.Size(567, 489);
			this.editFlashesPanel.TabIndex = 5;
			this.editFlashesPanel.Visible = false;
			// 
			// editBlastRingsPanel
			// 
			this.editBlastRingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editBlastRingsPanel.Enabled = false;
			this.editBlastRingsPanel.Location = new System.Drawing.Point(0, 0);
			this.editBlastRingsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editBlastRingsPanel.Name = "editBlastRingsPanel";
			this.editBlastRingsPanel.Size = new System.Drawing.Size(567, 489);
			this.editBlastRingsPanel.TabIndex = 4;
			this.editBlastRingsPanel.Visible = false;
			// 
			// editLightningsPanel
			// 
			this.editLightningsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editLightningsPanel.Enabled = false;
			this.editLightningsPanel.Location = new System.Drawing.Point(0, 0);
			this.editLightningsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editLightningsPanel.Name = "editLightningsPanel";
			this.editLightningsPanel.Size = new System.Drawing.Size(567, 489);
			this.editLightningsPanel.TabIndex = 3;
			this.editLightningsPanel.Visible = false;
			// 
			// editGlowsPanel
			// 
			this.editGlowsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editGlowsPanel.Enabled = false;
			this.editGlowsPanel.Location = new System.Drawing.Point(0, 0);
			this.editGlowsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editGlowsPanel.Name = "editGlowsPanel";
			this.editGlowsPanel.Size = new System.Drawing.Size(567, 489);
			this.editGlowsPanel.TabIndex = 2;
			this.editGlowsPanel.Visible = false;
			// 
			// editRibbonsPanel
			// 
			this.editRibbonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editRibbonsPanel.Enabled = false;
			this.editRibbonsPanel.Location = new System.Drawing.Point(0, 0);
			this.editRibbonsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editRibbonsPanel.Name = "editRibbonsPanel";
			this.editRibbonsPanel.Size = new System.Drawing.Size(567, 489);
			this.editRibbonsPanel.TabIndex = 1;
			this.editRibbonsPanel.Visible = false;
			// 
			// editParticlesPanel
			// 
			this.editParticlesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editParticlesPanel.Enabled = false;
			this.editParticlesPanel.Location = new System.Drawing.Point(0, 0);
			this.editParticlesPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editParticlesPanel.Name = "editParticlesPanel";
			this.editParticlesPanel.Size = new System.Drawing.Size(567, 489);
			this.editParticlesPanel.TabIndex = 0;
			this.editParticlesPanel.Visible = false;
			// 
			// MainParticlesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "MainParticlesPanel";
			this.Size = new System.Drawing.Size(700, 489);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.backPamel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TreeView categoriesTreeView;
		private System.Windows.Forms.Panel backPamel;
		private EditParticlesPanel editParticlesPanel;
		private EditRibbonsPanel editRibbonsPanel;
		private EditGlowsPanel editGlowsPanel;
		private EditLightningsPanel editLightningsPanel;
		private EditBlastRingsPanel editBlastRingsPanel;
		private EditFlashesPanel editFlashesPanel;
	}
}
