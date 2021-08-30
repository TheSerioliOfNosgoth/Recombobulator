namespace Recombobulator
{
    partial class AddFileForm
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
            this.textureSetLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.versionComboBox = new System.Windows.Forms.ComboBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.textureSetCombo = new System.Windows.Forms.ComboBox();
            this.textureList = new System.Windows.Forms.ListBox();
            this.requiredObjectList = new System.Windows.Forms.ListBox();
            this.requiredObjectsLabel = new System.Windows.Forms.Label();
            this.removeEventsCheckBox = new System.Windows.Forms.CheckBox();
            this.removePortalsCheckBox = new System.Windows.Forms.CheckBox();
            this.removeVMOsCheckBox = new System.Windows.Forms.CheckBox();
            this.removeSignalsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.Location = new System.Drawing.Point(10, 360);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 12;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(91, 360);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // textureSetLabel
            // 
            this.textureSetLabel.AutoSize = true;
            this.textureSetLabel.Location = new System.Drawing.Point(11, 62);
            this.textureSetLabel.Name = "textureSetLabel";
            this.textureSetLabel.Size = new System.Drawing.Size(62, 13);
            this.textureSetLabel.TabIndex = 3;
            this.textureSetLabel.Text = "Texture Set";
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
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(13, 33);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(259, 20);
            this.pathTextBox.TabIndex = 2;
            // 
            // textureSetCombo
            // 
            this.textureSetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textureSetCombo.FormattingEnabled = true;
            this.textureSetCombo.Location = new System.Drawing.Point(151, 59);
            this.textureSetCombo.Name = "textureSetCombo";
            this.textureSetCombo.Size = new System.Drawing.Size(121, 21);
            this.textureSetCombo.TabIndex = 4;
            this.textureSetCombo.SelectedIndexChanged += new System.EventHandler(this.textureSetCombo_SelectedIndexChanged);
            // 
            // textureList
            // 
            this.textureList.FormattingEnabled = true;
            this.textureList.Location = new System.Drawing.Point(12, 86);
            this.textureList.Name = "textureList";
            this.textureList.ScrollAlwaysVisible = true;
            this.textureList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.textureList.Size = new System.Drawing.Size(260, 69);
            this.textureList.TabIndex = 5;
            // 
            // requiredObjectList
            // 
            this.requiredObjectList.FormattingEnabled = true;
            this.requiredObjectList.Location = new System.Drawing.Point(12, 188);
            this.requiredObjectList.Name = "requiredObjectList";
            this.requiredObjectList.ScrollAlwaysVisible = true;
            this.requiredObjectList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.requiredObjectList.Size = new System.Drawing.Size(260, 69);
            this.requiredObjectList.TabIndex = 7;
            // 
            // requiredObjectsLabel
            // 
            this.requiredObjectsLabel.AutoSize = true;
            this.requiredObjectsLabel.Location = new System.Drawing.Point(11, 164);
            this.requiredObjectsLabel.Name = "requiredObjectsLabel";
            this.requiredObjectsLabel.Size = new System.Drawing.Size(133, 13);
            this.requiredObjectsLabel.TabIndex = 6;
            this.requiredObjectsLabel.Text = "Additional Objects Needed";
            // 
            // removeEventsCheckBox
            // 
            this.removeEventsCheckBox.AutoSize = true;
            this.removeEventsCheckBox.Checked = true;
            this.removeEventsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeEventsCheckBox.Enabled = false;
            this.removeEventsCheckBox.Location = new System.Drawing.Point(10, 309);
            this.removeEventsCheckBox.Name = "removeEventsCheckBox";
            this.removeEventsCheckBox.Size = new System.Drawing.Size(102, 17);
            this.removeEventsCheckBox.TabIndex = 10;
            this.removeEventsCheckBox.Text = "Remove Events";
            this.removeEventsCheckBox.UseVisualStyleBackColor = true;
            // 
            // removePortalsCheckBox
            // 
            this.removePortalsCheckBox.AutoSize = true;
            this.removePortalsCheckBox.Checked = true;
            this.removePortalsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removePortalsCheckBox.Location = new System.Drawing.Point(12, 263);
            this.removePortalsCheckBox.Name = "removePortalsCheckBox";
            this.removePortalsCheckBox.Size = new System.Drawing.Size(101, 17);
            this.removePortalsCheckBox.TabIndex = 8;
            this.removePortalsCheckBox.Text = "Remove Portals";
            this.removePortalsCheckBox.UseVisualStyleBackColor = true;
            // 
            // removeVMOsCheckBox
            // 
            this.removeVMOsCheckBox.AutoSize = true;
            this.removeVMOsCheckBox.Checked = true;
            this.removeVMOsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeVMOsCheckBox.Location = new System.Drawing.Point(9, 332);
            this.removeVMOsCheckBox.Name = "removeVMOsCheckBox";
            this.removeVMOsCheckBox.Size = new System.Drawing.Size(171, 17);
            this.removeVMOsCheckBox.TabIndex = 11;
            this.removeVMOsCheckBox.Text = "Remove Vertex Morph Objects";
            this.removeVMOsCheckBox.UseVisualStyleBackColor = true;
            // 
            // removeSignalsCheckBox
            // 
            this.removeSignalsCheckBox.AutoSize = true;
            this.removeSignalsCheckBox.Checked = true;
            this.removeSignalsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.removeSignalsCheckBox.Location = new System.Drawing.Point(11, 286);
            this.removeSignalsCheckBox.Name = "removeSignalsCheckBox";
            this.removeSignalsCheckBox.Size = new System.Drawing.Size(103, 17);
            this.removeSignalsCheckBox.TabIndex = 9;
            this.removeSignalsCheckBox.Text = "Remove Signals";
            this.removeSignalsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddFileForm
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 396);
            this.ControlBox = false;
            this.Controls.Add(this.removeSignalsCheckBox);
            this.Controls.Add(this.removeVMOsCheckBox);
            this.Controls.Add(this.removeEventsCheckBox);
            this.Controls.Add(this.removePortalsCheckBox);
            this.Controls.Add(this.requiredObjectList);
            this.Controls.Add(this.requiredObjectsLabel);
            this.Controls.Add(this.textureList);
            this.Controls.Add(this.textureSetCombo);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.versionComboBox);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.textureSetLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFileForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add To Project...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label textureSetLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.ComboBox versionComboBox;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ComboBox textureSetCombo;
        private System.Windows.Forms.ListBox textureList;
        private System.Windows.Forms.ListBox requiredObjectList;
        private System.Windows.Forms.Label requiredObjectsLabel;
        private System.Windows.Forms.CheckBox removeEventsCheckBox;
        private System.Windows.Forms.CheckBox removePortalsCheckBox;
        private System.Windows.Forms.CheckBox removeVMOsCheckBox;
        private System.Windows.Forms.CheckBox removeSignalsCheckBox;
    }
}