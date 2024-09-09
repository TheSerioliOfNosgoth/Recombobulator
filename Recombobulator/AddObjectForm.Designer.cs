namespace Recombobulator
{
	partial class AddObjectForm
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
			this.renameButton = new System.Windows.Forms.Button();
			this.textureSetCombo = new System.Windows.Forms.ComboBox();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.versionComboBox = new System.Windows.Forms.ComboBox();
			this.versionLabel = new System.Windows.Forms.Label();
			this.textureSetLabel = new System.Windows.Forms.Label();
			this.applyTranslucencyCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// continueButton
			// 
			this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.continueButton.Location = new System.Drawing.Point(14, 111);
			this.continueButton.Name = "continueButton";
			this.continueButton.Size = new System.Drawing.Size(75, 23);
			this.continueButton.TabIndex = 6;
			this.continueButton.Text = "Continue";
			this.continueButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(95, 111);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// renameButton
			// 
			this.renameButton.Location = new System.Drawing.Point(209, 33);
			this.renameButton.Name = "renameButton";
			this.renameButton.Size = new System.Drawing.Size(63, 23);
			this.renameButton.TabIndex = 3;
			this.renameButton.Text = "Rename";
			this.renameButton.UseVisualStyleBackColor = true;
			this.renameButton.Click += new System.EventHandler(this.renameButton_Click);
			// 
			// textureSetCombo
			// 
			this.textureSetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.textureSetCombo.FormattingEnabled = true;
			this.textureSetCombo.Location = new System.Drawing.Point(151, 59);
			this.textureSetCombo.Name = "textureSetCombo";
			this.textureSetCombo.Size = new System.Drawing.Size(121, 21);
			this.textureSetCombo.TabIndex = 5;
			this.textureSetCombo.SelectedIndexChanged += new System.EventHandler(this.textureSetCombo_SelectedIndexChanged);
			// 
			// pathTextBox
			// 
			this.pathTextBox.Location = new System.Drawing.Point(13, 33);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.ReadOnly = true;
			this.pathTextBox.Size = new System.Drawing.Size(189, 20);
			this.pathTextBox.TabIndex = 2;
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
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(13, 9);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(42, 13);
			this.versionLabel.TabIndex = 0;
			this.versionLabel.Text = "Version";
			// 
			// textureSetLabel
			// 
			this.textureSetLabel.AutoSize = true;
			this.textureSetLabel.Location = new System.Drawing.Point(11, 62);
			this.textureSetLabel.Name = "textureSetLabel";
			this.textureSetLabel.Size = new System.Drawing.Size(62, 13);
			this.textureSetLabel.TabIndex = 4;
			this.textureSetLabel.Text = "Texture Set";
			// 
			// applyTranslucencyCheckBox
			// 
			this.applyTranslucencyCheckBox.AutoSize = true;
			this.applyTranslucencyCheckBox.Checked = true;
			this.applyTranslucencyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.applyTranslucencyCheckBox.Location = new System.Drawing.Point(16, 88);
			this.applyTranslucencyCheckBox.Name = "applyTranslucencyCheckBox";
			this.applyTranslucencyCheckBox.Size = new System.Drawing.Size(119, 17);
			this.applyTranslucencyCheckBox.TabIndex = 19;
			this.applyTranslucencyCheckBox.Text = "Apply Translucency";
			this.applyTranslucencyCheckBox.UseVisualStyleBackColor = true;
			// 
			// AddObjectForm
			// 
			this.AcceptButton = this.continueButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 148);
			this.ControlBox = false;
			this.Controls.Add(this.applyTranslucencyCheckBox);
			this.Controls.Add(this.renameButton);
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
			this.Name = "AddObjectForm";
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
		private System.Windows.Forms.Button renameButton;
		private System.Windows.Forms.CheckBox applyTranslucencyCheckBox;
	}
}