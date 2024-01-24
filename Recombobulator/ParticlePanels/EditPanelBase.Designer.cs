namespace Recombobulator.ParticlePanels
{
    partial class EditPanelBase
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
			this.backPamel = new System.Windows.Forms.Panel();
			this.particlesPanel = new System.Windows.Forms.Panel();
			this.fieldsPanel = new System.Windows.Forms.TableLayoutPanel();
			this.toolBoxPanel = new System.Windows.Forms.Panel();
			this.addButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.selectionLabel = new System.Windows.Forms.Label();
			this.pasteButton = new System.Windows.Forms.Button();
			this.selectionComboBox = new System.Windows.Forms.ComboBox();
			this.copyButton = new System.Windows.Forms.Button();
			this.insertButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.backPamel.SuspendLayout();
			this.toolBoxPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// backPamel
			// 
			this.backPamel.AutoScroll = true;
			this.backPamel.Controls.Add(this.particlesPanel);
			this.backPamel.Controls.Add(this.fieldsPanel);
			this.backPamel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backPamel.Location = new System.Drawing.Point(0, 60);
			this.backPamel.Name = "backPamel";
			this.backPamel.Size = new System.Drawing.Size(515, 440);
			this.backPamel.TabIndex = 15;
			// 
			// particlesPanel
			// 
			this.particlesPanel.Location = new System.Drawing.Point(254, 10);
			this.particlesPanel.Margin = new System.Windows.Forms.Padding(0);
			this.particlesPanel.Name = "particlesPanel";
			this.particlesPanel.Size = new System.Drawing.Size(240, 940);
			this.particlesPanel.TabIndex = 2;
			// 
			// fieldsPanel
			// 
			this.fieldsPanel.ColumnCount = 2;
			this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.fieldsPanel.Location = new System.Drawing.Point(0, 10);
			this.fieldsPanel.Margin = new System.Windows.Forms.Padding(0);
			this.fieldsPanel.Name = "fieldsPanel";
			this.fieldsPanel.Size = new System.Drawing.Size(250, 940);
			this.fieldsPanel.TabIndex = 1;
			// 
			// toolBoxPanel
			// 
			this.toolBoxPanel.Controls.Add(this.addButton);
			this.toolBoxPanel.Controls.Add(this.resetButton);
			this.toolBoxPanel.Controls.Add(this.selectionLabel);
			this.toolBoxPanel.Controls.Add(this.pasteButton);
			this.toolBoxPanel.Controls.Add(this.selectionComboBox);
			this.toolBoxPanel.Controls.Add(this.copyButton);
			this.toolBoxPanel.Controls.Add(this.insertButton);
			this.toolBoxPanel.Controls.Add(this.removeButton);
			this.toolBoxPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.toolBoxPanel.Location = new System.Drawing.Point(0, 0);
			this.toolBoxPanel.Margin = new System.Windows.Forms.Padding(0);
			this.toolBoxPanel.Name = "toolBoxPanel";
			this.toolBoxPanel.Size = new System.Drawing.Size(515, 60);
			this.toolBoxPanel.TabIndex = 14;
			// 
			// addButton
			// 
			this.addButton.Enabled = false;
			this.addButton.Location = new System.Drawing.Point(257, 4);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(75, 23);
			this.addButton.TabIndex = 10;
			this.addButton.Text = "Add";
			this.addButton.UseVisualStyleBackColor = true;
			// 
			// resetButton
			// 
			this.resetButton.Enabled = false;
			this.resetButton.Location = new System.Drawing.Point(338, 33);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(75, 23);
			this.resetButton.TabIndex = 9;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			// 
			// selectionLabel
			// 
			this.selectionLabel.AutoSize = true;
			this.selectionLabel.Location = new System.Drawing.Point(3, 9);
			this.selectionLabel.Name = "selectionLabel";
			this.selectionLabel.Size = new System.Drawing.Size(0, 13);
			this.selectionLabel.TabIndex = 4;
			// 
			// pasteButton
			// 
			this.pasteButton.Enabled = false;
			this.pasteButton.Location = new System.Drawing.Point(419, 33);
			this.pasteButton.Name = "pasteButton";
			this.pasteButton.Size = new System.Drawing.Size(75, 23);
			this.pasteButton.TabIndex = 8;
			this.pasteButton.Text = "Paste";
			this.pasteButton.UseVisualStyleBackColor = true;
			// 
			// selectionComboBox
			// 
			this.selectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.selectionComboBox.FormattingEnabled = true;
			this.selectionComboBox.Location = new System.Drawing.Point(98, 4);
			this.selectionComboBox.Name = "selectionComboBox";
			this.selectionComboBox.Size = new System.Drawing.Size(152, 21);
			this.selectionComboBox.TabIndex = 3;
			// 
			// copyButton
			// 
			this.copyButton.Enabled = false;
			this.copyButton.Location = new System.Drawing.Point(419, 4);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 23);
			this.copyButton.TabIndex = 7;
			this.copyButton.Text = "Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			// 
			// insertButton
			// 
			this.insertButton.Enabled = false;
			this.insertButton.Location = new System.Drawing.Point(338, 4);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(75, 23);
			this.insertButton.TabIndex = 5;
			this.insertButton.Text = "Insert";
			this.insertButton.UseVisualStyleBackColor = true;
			// 
			// removeButton
			// 
			this.removeButton.Enabled = false;
			this.removeButton.Location = new System.Drawing.Point(257, 33);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(75, 23);
			this.removeButton.TabIndex = 6;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			// 
			// EditPanelBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.backPamel);
			this.Controls.Add(this.toolBoxPanel);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "EditPanelBase";
			this.Size = new System.Drawing.Size(515, 500);
			this.backPamel.ResumeLayout(false);
			this.toolBoxPanel.ResumeLayout(false);
			this.toolBoxPanel.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel backPamel;
        protected System.Windows.Forms.Panel particlesPanel;
        protected System.Windows.Forms.TableLayoutPanel fieldsPanel;
        protected System.Windows.Forms.Panel toolBoxPanel;
        protected System.Windows.Forms.Label selectionLabel;
        protected System.Windows.Forms.Button pasteButton;
        protected System.Windows.Forms.ComboBox selectionComboBox;
        protected System.Windows.Forms.Button copyButton;
        protected System.Windows.Forms.Button insertButton;
        protected System.Windows.Forms.Button removeButton;
        protected System.Windows.Forms.Button resetButton;
        protected System.Windows.Forms.Button addButton;
    }
}
