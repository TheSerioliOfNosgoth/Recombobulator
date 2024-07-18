namespace Recombobulator.ParticlePanels
{
	partial class EditGlyphTuneDataPanel
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
			this.mainPanel = new System.Windows.Forms.Panel();
			this.glyphDarknessTextBox = new System.Windows.Forms.TextBox();
			this.glyphDarknessLabel = new System.Windows.Forms.Label();
			this.glyphSizeTextBox = new System.Windows.Forms.TextBox();
			this.glyphSizeLabel = new System.Windows.Forms.Label();
			this.backPanel.SuspendLayout();
			this.toolBoxPanel.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// backPanel
			// 
			this.backPanel.Location = new System.Drawing.Point(0, 140);
			this.backPanel.Size = new System.Drawing.Size(515, 360);
			// 
			// toolBoxPanel
			// 
			this.toolBoxPanel.Location = new System.Drawing.Point(0, 80);
			// 
			// selectionLabel
			// 
			this.selectionLabel.Size = new System.Drawing.Size(71, 13);
			this.selectionLabel.Text = "Current Glyph";
			// 
			// pasteButton
			// 
			this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
			// 
			// selectionComboBox
			// 
			this.selectionComboBox.SelectedIndexChanged += new System.EventHandler(this.selectionComboBox_SelectedIndexChanged);
			// 
			// copyButton
			// 
			this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.glyphDarknessTextBox);
			this.mainPanel.Controls.Add(this.glyphDarknessLabel);
			this.mainPanel.Controls.Add(this.glyphSizeTextBox);
			this.mainPanel.Controls.Add(this.glyphSizeLabel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(515, 80);
			this.mainPanel.TabIndex = 16;
			// 
			// glyphDarknessTextBox
			// 
			this.glyphDarknessTextBox.Location = new System.Drawing.Point(153, 39);
			this.glyphDarknessTextBox.Name = "glyphDarknessTextBox";
			this.glyphDarknessTextBox.Size = new System.Drawing.Size(80, 20);
			this.glyphDarknessTextBox.TabIndex = 3;
			// 
			// glyphDarknessLabel
			// 
			this.glyphDarknessLabel.Location = new System.Drawing.Point(3, 39);
			this.glyphDarknessLabel.Name = "glyphDarknessLabel";
			this.glyphDarknessLabel.Size = new System.Drawing.Size(120, 30);
			this.glyphDarknessLabel.TabIndex = 2;
			this.glyphDarknessLabel.Text = "glyph_darkness";
			// 
			// glyphSizeTextBox
			// 
			this.glyphSizeTextBox.Location = new System.Drawing.Point(153, 9);
			this.glyphSizeTextBox.Name = "glyphSizeTextBox";
			this.glyphSizeTextBox.Size = new System.Drawing.Size(80, 20);
			this.glyphSizeTextBox.TabIndex = 1;
			// 
			// glyphSizeLabel
			// 
			this.glyphSizeLabel.Location = new System.Drawing.Point(3, 9);
			this.glyphSizeLabel.Name = "glyphSizeLabel";
			this.glyphSizeLabel.Size = new System.Drawing.Size(120, 30);
			this.glyphSizeLabel.TabIndex = 0;
			this.glyphSizeLabel.Text = "glyph_size";
			// 
			// EditGlyphTuneDataPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.mainPanel);
			this.Name = "EditGlyphTuneDataPanel";
			this.Controls.SetChildIndex(this.mainPanel, 0);
			this.Controls.SetChildIndex(this.toolBoxPanel, 0);
			this.Controls.SetChildIndex(this.backPanel, 0);
			this.backPanel.ResumeLayout(false);
			this.toolBoxPanel.ResumeLayout(false);
			this.toolBoxPanel.PerformLayout();
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.TextBox glyphSizeTextBox;
		private System.Windows.Forms.Label glyphSizeLabel;
		private System.Windows.Forms.Label glyphDarknessLabel;
		private System.Windows.Forms.TextBox glyphDarknessTextBox;
	}
}
