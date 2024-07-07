namespace Recombobulator.ParticlePanels
{
    partial class EditFlashesPanel
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
			this.colorBox = new System.Windows.Forms.PictureBox();
			this.backPanel.SuspendLayout();
			this.particlesPanel.SuspendLayout();
			this.toolBoxPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorBox)).BeginInit();
			this.SuspendLayout();
			// 
			// particlesPanel
			// 
			this.particlesPanel.Controls.Add(this.colorBox);
			// 
			// selectionLabel
			// 
			this.selectionLabel.Size = new System.Drawing.Size(69, 13);
			this.selectionLabel.Text = "Current Flash";
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
			// colorBox
			// 
			this.colorBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorBox.Location = new System.Drawing.Point(95, 65);
			this.colorBox.Margin = new System.Windows.Forms.Padding(0);
			this.colorBox.Name = "colorBox";
			this.colorBox.Size = new System.Drawing.Size(50, 50);
			this.colorBox.TabIndex = 5;
			this.colorBox.TabStop = false;
			this.colorBox.Visible = false;
			// 
			// EditFlashesPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditFlashesPanel";
			this.backPanel.ResumeLayout(false);
			this.particlesPanel.ResumeLayout(false);
			this.toolBoxPanel.ResumeLayout(false);
			this.toolBoxPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.colorBox)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.PictureBox colorBox;
	}
}
