namespace Recombobulator.ParticlePanels
{
    partial class EditBlastRingsPanel
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
			this.endColorBox = new System.Windows.Forms.PictureBox();
			this.startColorBox = new System.Windows.Forms.PictureBox();
			this.backPanel.SuspendLayout();
			this.particlesPanel.SuspendLayout();
			this.toolBoxPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.endColorBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.startColorBox)).BeginInit();
			this.SuspendLayout();
			// 
			// particlesPanel
			// 
			this.particlesPanel.Controls.Add(this.endColorBox);
			this.particlesPanel.Controls.Add(this.startColorBox);
			// 
			// selectionLabel
			// 
			this.selectionLabel.Size = new System.Drawing.Size(92, 13);
			this.selectionLabel.Text = "Current Blast Ring";
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
			// insertButton
			// 
			this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// addButton
			// 
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// endColorBox
			// 
			this.endColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.endColorBox.Location = new System.Drawing.Point(165, 605);
			this.endColorBox.Margin = new System.Windows.Forms.Padding(0);
			this.endColorBox.Name = "endColorBox";
			this.endColorBox.Size = new System.Drawing.Size(50, 50);
			this.endColorBox.TabIndex = 6;
			this.endColorBox.TabStop = false;
			this.endColorBox.Visible = false;
			// 
			// startColorBox
			// 
			this.startColorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.startColorBox.Location = new System.Drawing.Point(95, 605);
			this.startColorBox.Margin = new System.Windows.Forms.Padding(0);
			this.startColorBox.Name = "startColorBox";
			this.startColorBox.Size = new System.Drawing.Size(50, 50);
			this.startColorBox.TabIndex = 5;
			this.startColorBox.TabStop = false;
			this.startColorBox.Visible = false;
			// 
			// EditBlastRingsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditBlastRingsPanel";
			this.backPanel.ResumeLayout(false);
			this.particlesPanel.ResumeLayout(false);
			this.toolBoxPanel.ResumeLayout(false);
			this.toolBoxPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.endColorBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.startColorBox)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.PictureBox endColorBox;
		private System.Windows.Forms.PictureBox startColorBox;
	}
}
