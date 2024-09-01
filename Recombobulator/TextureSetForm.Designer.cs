namespace Recombobulator
{
	partial class TextureSetForm
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.textureSetLabel = new System.Windows.Forms.Label();
			this.textureSetCombo = new System.Windows.Forms.ComboBox();
			this.texturesBackPanel = new System.Windows.Forms.Panel();
			this.texturesTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.texturesBackPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(13, 249);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Enabled = false;
			this.cancelButton.Location = new System.Drawing.Point(94, 249);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Visible = false;
			// 
			// textureSetLabel
			// 
			this.textureSetLabel.AutoSize = true;
			this.textureSetLabel.Location = new System.Drawing.Point(12, 15);
			this.textureSetLabel.Name = "textureSetLabel";
			this.textureSetLabel.Size = new System.Drawing.Size(62, 13);
			this.textureSetLabel.TabIndex = 2;
			this.textureSetLabel.Text = "Texture Set";
			// 
			// textureSetCombo
			// 
			this.textureSetCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.textureSetCombo.Enabled = false;
			this.textureSetCombo.FormattingEnabled = true;
			this.textureSetCombo.Location = new System.Drawing.Point(151, 12);
			this.textureSetCombo.Name = "textureSetCombo";
			this.textureSetCombo.Size = new System.Drawing.Size(121, 21);
			this.textureSetCombo.TabIndex = 24;
			// 
			// texturesBackPanel
			// 
			this.texturesBackPanel.AutoScroll = true;
			this.texturesBackPanel.Controls.Add(this.texturesTablePanel);
			this.texturesBackPanel.Location = new System.Drawing.Point(13, 39);
			this.texturesBackPanel.Name = "texturesBackPanel";
			this.texturesBackPanel.Size = new System.Drawing.Size(259, 204);
			this.texturesBackPanel.TabIndex = 26;
			// 
			// texturesTablePanel
			// 
			this.texturesTablePanel.AutoSize = true;
			this.texturesTablePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.texturesTablePanel.ColumnCount = 1;
			this.texturesTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.texturesTablePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.texturesTablePanel.Location = new System.Drawing.Point(0, 0);
			this.texturesTablePanel.Name = "texturesTablePanel";
			this.texturesTablePanel.RowCount = 1;
			this.texturesTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 204F));
			this.texturesTablePanel.Size = new System.Drawing.Size(259, 204);
			this.texturesTablePanel.TabIndex = 26;
			// 
			// TextureSetForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(284, 281);
			this.ControlBox = false;
			this.Controls.Add(this.textureSetCombo);
			this.Controls.Add(this.textureSetLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.texturesBackPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TextureSetForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Texture Set...";
			this.texturesBackPanel.ResumeLayout(false);
			this.texturesBackPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label textureSetLabel;
		private System.Windows.Forms.ComboBox textureSetCombo;
		private System.Windows.Forms.Panel texturesBackPanel;
		private System.Windows.Forms.TableLayoutPanel texturesTablePanel;
	}
}