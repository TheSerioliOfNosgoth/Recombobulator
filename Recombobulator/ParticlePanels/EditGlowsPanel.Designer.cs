namespace Recombobulator.ParticlePanels
{
    partial class EditGlowsPanel
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
			this.backPanel.SuspendLayout();
			this.toolBoxPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// selectionLabel
			// 
			this.selectionLabel.Size = new System.Drawing.Size(68, 13);
			this.selectionLabel.Text = "Current Glow";
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
			// EditGlowsPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "EditGlowsPanel";
			this.backPanel.ResumeLayout(false);
			this.toolBoxPanel.ResumeLayout(false);
			this.toolBoxPanel.PerformLayout();
			this.ResumeLayout(false);

        }

		#endregion
	}
}
