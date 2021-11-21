
namespace Recombobulator
{
	partial class EditPortalForm
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
			this.fromUnitLabel = new System.Windows.Forms.Label();
			this.fromSignalLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.toSignaLabel = new System.Windows.Forms.Label();
			this.toUnitLabel = new System.Windows.Forms.Label();
			this.toUnitComboBox = new System.Windows.Forms.ComboBox();
			this.fromUnitComboBox = new System.Windows.Forms.ComboBox();
			this.fromSignalComboBox = new System.Windows.Forms.ComboBox();
			this.toSignalComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// fromUnitLabel
			// 
			this.fromUnitLabel.AutoSize = true;
			this.fromUnitLabel.Location = new System.Drawing.Point(12, 9);
			this.fromUnitLabel.Name = "fromUnitLabel";
			this.fromUnitLabel.Size = new System.Drawing.Size(55, 13);
			this.fromUnitLabel.TabIndex = 0;
			this.fromUnitLabel.Text = "From Unit:";
			// 
			// fromSignalLabel
			// 
			this.fromSignalLabel.AutoSize = true;
			this.fromSignalLabel.Location = new System.Drawing.Point(121, 9);
			this.fromSignalLabel.Name = "fromSignalLabel";
			this.fromSignalLabel.Size = new System.Drawing.Size(65, 13);
			this.fromSignalLabel.TabIndex = 1;
			this.fromSignalLabel.Text = "From Signal:";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(12, 91);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 8;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(96, 91);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// toSignaLabel
			// 
			this.toSignaLabel.AutoSize = true;
			this.toSignaLabel.Location = new System.Drawing.Point(121, 48);
			this.toSignaLabel.Name = "toSignaLabel";
			this.toSignaLabel.Size = new System.Drawing.Size(55, 13);
			this.toSignaLabel.TabIndex = 5;
			this.toSignaLabel.Text = "To Signal:";
			// 
			// toUnitLabel
			// 
			this.toUnitLabel.AutoSize = true;
			this.toUnitLabel.Location = new System.Drawing.Point(12, 48);
			this.toUnitLabel.Name = "toUnitLabel";
			this.toUnitLabel.Size = new System.Drawing.Size(45, 13);
			this.toUnitLabel.TabIndex = 4;
			this.toUnitLabel.Text = "To Unit:";
			// 
			// toUnitComboBox
			// 
			this.toUnitComboBox.Location = new System.Drawing.Point(12, 64);
			this.toUnitComboBox.Name = "toUnitComboBox";
			this.toUnitComboBox.Size = new System.Drawing.Size(100, 21);
			this.toUnitComboBox.Sorted = true;
			this.toUnitComboBox.TabIndex = 6;
			this.toUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.toUnitComboBox_SelectedIndexChanged);
			// 
			// fromUnitComboBox
			// 
			this.fromUnitComboBox.Location = new System.Drawing.Point(12, 25);
			this.fromUnitComboBox.Name = "fromUnitComboBox";
			this.fromUnitComboBox.Size = new System.Drawing.Size(100, 21);
			this.fromUnitComboBox.Sorted = true;
			this.fromUnitComboBox.TabIndex = 2;
			this.fromUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.fromUnitComboBox_SelectedIndexChanged);
			// 
			// fromSignalComboBox
			// 
			this.fromSignalComboBox.Location = new System.Drawing.Point(121, 25);
			this.fromSignalComboBox.Name = "fromSignalComboBox";
			this.fromSignalComboBox.Size = new System.Drawing.Size(100, 21);
			this.fromSignalComboBox.Sorted = true;
			this.fromSignalComboBox.TabIndex = 3;
			// 
			// toSignalComboBox
			// 
			this.toSignalComboBox.Location = new System.Drawing.Point(121, 64);
			this.toSignalComboBox.Name = "toSignalComboBox";
			this.toSignalComboBox.Size = new System.Drawing.Size(100, 21);
			this.toSignalComboBox.Sorted = true;
			this.toSignalComboBox.TabIndex = 7;
			// 
			// EditPortalForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(237, 129);
			this.ControlBox = false;
			this.Controls.Add(this.fromSignalComboBox);
			this.Controls.Add(this.toSignalComboBox);
			this.Controls.Add(this.fromUnitComboBox);
			this.Controls.Add(this.toUnitComboBox);
			this.Controls.Add(this.toSignaLabel);
			this.Controls.Add(this.toUnitLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.fromSignalLabel);
			this.Controls.Add(this.fromUnitLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditPortalForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Portal...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label fromUnitLabel;
		private System.Windows.Forms.Label fromSignalLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label toSignaLabel;
		private System.Windows.Forms.Label toUnitLabel;
		private System.Windows.Forms.ComboBox toUnitComboBox;
		private System.Windows.Forms.ComboBox fromUnitComboBox;
		private System.Windows.Forms.ComboBox fromSignalComboBox;
		private System.Windows.Forms.ComboBox toSignalComboBox;
	}
}