namespace Recombobulator
{
    partial class UpgradeForm
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
            this.continueButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.textureStartLabel = new System.Windows.Forms.Label();
            this.textureStartTextBox = new System.Windows.Forms.NumericUpDown();
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textureStartTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.Location = new System.Drawing.Point(12, 85);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 6;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(94, 85);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // textureStartLabel
            // 
            this.textureStartLabel.AutoSize = true;
            this.textureStartLabel.Location = new System.Drawing.Point(12, 35);
            this.textureStartLabel.Name = "textureStartLabel";
            this.textureStartLabel.Size = new System.Drawing.Size(96, 13);
            this.textureStartLabel.TabIndex = 2;
            this.textureStartLabel.Text = "Texture Starting ID";
            // 
            // textureStartTextBox
            // 
            this.textureStartTextBox.Location = new System.Drawing.Point(151, 33);
            this.textureStartTextBox.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.textureStartTextBox.Name = "textureStartTextBox";
            this.textureStartTextBox.Size = new System.Drawing.Size(120, 20);
            this.textureStartTextBox.TabIndex = 3;
            this.textureStartTextBox.Value = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.textureStartTextBox.Leave += new System.EventHandler(this.TextureStartTextBox_Leave);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(13, 9);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(42, 13);
            this.versionLabel.TabIndex = 0;
            this.versionLabel.Text = "Version";
            // 
            // versionComboBox
            // 
            this.versionComboBox.Enabled = false;
            this.versionComboBox.FormattingEnabled = true;
            this.versionComboBox.Location = new System.Drawing.Point(151, 6);
            this.versionComboBox.Name = "versionComboBox";
            this.versionComboBox.Size = new System.Drawing.Size(121, 21);
            this.versionComboBox.TabIndex = 1;
            this.versionComboBox.Text = "Retail PC";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(12, 59);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.ReadOnly = true;
            this.fileNameTextBox.Size = new System.Drawing.Size(179, 20);
            this.fileNameTextBox.TabIndex = 4;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(197, 59);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // UpgradeForm
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 121);
            this.ControlBox = false;
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.versionComboBox);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.textureStartTextBox);
            this.Controls.Add(this.textureStartLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpgradeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upgrade To Version";
            ((System.ComponentModel.ISupportInitialize)(this.textureStartTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label textureStartLabel;
        private System.Windows.Forms.NumericUpDown textureStartTextBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button browseButton;
    }
}