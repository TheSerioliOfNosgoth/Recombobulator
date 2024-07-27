namespace Recombobulator
{
	partial class VersionSelectForm
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
			this.selectVersionlabel = new System.Windows.Forms.Label();
			this.versionComboBox = new System.Windows.Forms.ComboBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// selectVersionlabel
			// 
			this.selectVersionlabel.AutoSize = true;
			this.selectVersionlabel.Location = new System.Drawing.Point(13, 13);
			this.selectVersionlabel.Name = "selectVersionlabel";
			this.selectVersionlabel.Size = new System.Drawing.Size(84, 13);
			this.selectVersionlabel.TabIndex = 0;
			this.selectVersionlabel.Text = "Select Version...";
			// 
			// versionComboBox
			// 
			this.versionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.versionComboBox.FormattingEnabled = true;
			this.versionComboBox.Location = new System.Drawing.Point(13, 30);
			this.versionComboBox.Name = "versionComboBox";
			this.versionComboBox.Size = new System.Drawing.Size(259, 21);
			this.versionComboBox.TabIndex = 1;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(13, 58);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(95, 58);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// VersionSelectForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 101);
			this.ControlBox = false;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.versionComboBox);
			this.Controls.Add(this.selectVersionlabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "VersionSelectForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Version";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VersionSelectForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label selectVersionlabel;
		private System.Windows.Forms.ComboBox versionComboBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}