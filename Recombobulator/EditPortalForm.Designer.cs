
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
			this.fromUnitTextBox = new System.Windows.Forms.TextBox();
			this.fromSignalLabel = new System.Windows.Forms.Label();
			this.toUnitTextBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.toSignalTextBox = new System.Windows.Forms.TextBox();
			this.toSignaLabel = new System.Windows.Forms.Label();
			this.fromSignalTextBox = new System.Windows.Forms.TextBox();
			this.toUnitLabel = new System.Windows.Forms.Label();
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
			// fromUnitTextBox
			// 
			this.fromUnitTextBox.Location = new System.Drawing.Point(12, 25);
			this.fromUnitTextBox.Name = "fromUnitTextBox";
			this.fromUnitTextBox.Size = new System.Drawing.Size(100, 20);
			this.fromUnitTextBox.TabIndex = 1;
			// 
			// fromSignalLabel
			// 
			this.fromSignalLabel.AutoSize = true;
			this.fromSignalLabel.Location = new System.Drawing.Point(121, 9);
			this.fromSignalLabel.Name = "fromSignalLabel";
			this.fromSignalLabel.Size = new System.Drawing.Size(65, 13);
			this.fromSignalLabel.TabIndex = 2;
			this.fromSignalLabel.Text = "From Signal:";
			// 
			// toUnitTextBox
			// 
			this.toUnitTextBox.Location = new System.Drawing.Point(12, 64);
			this.toUnitTextBox.Name = "toUnitTextBox";
			this.toUnitTextBox.Size = new System.Drawing.Size(100, 20);
			this.toUnitTextBox.TabIndex = 3;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(12, 91);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(96, 91);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// toSignalTextBox
			// 
			this.toSignalTextBox.Location = new System.Drawing.Point(121, 64);
			this.toSignalTextBox.Name = "toSignalTextBox";
			this.toSignalTextBox.Size = new System.Drawing.Size(100, 20);
			this.toSignalTextBox.TabIndex = 9;
			// 
			// toSignaLabel
			// 
			this.toSignaLabel.AutoSize = true;
			this.toSignaLabel.Location = new System.Drawing.Point(121, 48);
			this.toSignaLabel.Name = "toSignaLabel";
			this.toSignaLabel.Size = new System.Drawing.Size(55, 13);
			this.toSignaLabel.TabIndex = 8;
			this.toSignaLabel.Text = "To Signal:";
			// 
			// fromSignalTextBox
			// 
			this.fromSignalTextBox.Location = new System.Drawing.Point(121, 25);
			this.fromSignalTextBox.Name = "fromSignalTextBox";
			this.fromSignalTextBox.Size = new System.Drawing.Size(100, 20);
			this.fromSignalTextBox.TabIndex = 7;
			// 
			// toUnitLabel
			// 
			this.toUnitLabel.AutoSize = true;
			this.toUnitLabel.Location = new System.Drawing.Point(12, 48);
			this.toUnitLabel.Name = "toUnitLabel";
			this.toUnitLabel.Size = new System.Drawing.Size(45, 13);
			this.toUnitLabel.TabIndex = 6;
			this.toUnitLabel.Text = "To Unit:";
			// 
			// EditPortalForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(237, 129);
			this.ControlBox = false;
			this.Controls.Add(this.toSignalTextBox);
			this.Controls.Add(this.toSignaLabel);
			this.Controls.Add(this.fromSignalTextBox);
			this.Controls.Add(this.toUnitLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.toUnitTextBox);
			this.Controls.Add(this.fromSignalLabel);
			this.Controls.Add(this.fromUnitTextBox);
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
		private System.Windows.Forms.TextBox fromUnitTextBox;
		private System.Windows.Forms.Label fromSignalLabel;
		private System.Windows.Forms.TextBox toUnitTextBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox toSignalTextBox;
		private System.Windows.Forms.Label toSignaLabel;
		private System.Windows.Forms.TextBox fromSignalTextBox;
		private System.Windows.Forms.Label toUnitLabel;
	}
}