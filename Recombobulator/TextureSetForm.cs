using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SR1Repository;

namespace Recombobulator
{
	public partial class TextureSetForm : Form
	{
		Repository _repository = null;
		TexSet _textureSet = null;

		List<TextBox> _textureTextBoxes = new List<TextBox>();

		public TextureSetForm()
		{
			InitializeComponent();
		}

		public void Initialize(Repository repository, TexSet textureSet)
		{
			_repository = repository;
			_textureSet = textureSet;

			textureSetCombo.Items.Add(_textureSet.Name);
			textureSetCombo.SelectedIndex = 0;

			texturesTablePanel.Controls.Clear();
			texturesTablePanel.RowStyles.Clear();

			for (int t = 0; t < textureSet.TextureIDs.Length; t++)
			{
				texturesTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

				Label nameLabel = new Label();
				nameLabel.Margin = new Padding(0);
				nameLabel.Location = new Point(10, 0);
				nameLabel.Text = "Texture " + t.ToString() + ":";
				nameLabel.Size = new Size(100, 30);

				short textureValue = unchecked((short)_textureSet.TextureIDs[t]);

				TextBox valueTextBox = new TextBox();
				valueTextBox.Margin = new Padding(0);
				valueTextBox.Location = new Point(120, 0);
				valueTextBox.Text = textureValue.ToString();
				valueTextBox.Size = new Size(100, 30);
				valueTextBox.KeyDown += ValueTextBox_KeyDown;
				valueTextBox.Leave += ValueTextBox_Leave;

				_textureTextBoxes.Add(valueTextBox);

				Panel innerPanel = new Panel();
				innerPanel.Dock = DockStyle.Fill;
				innerPanel.Controls.Add(nameLabel);
				innerPanel.Controls.Add(valueTextBox);

				texturesTablePanel.Controls.Add(innerPanel, 0, t);
			}
		}

		protected void ValueTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				ValueTextBox_Leave(sender, e);
			}
		}

		protected void ValueTextBox_Leave(object sender, System.EventArgs e)
		{
			TextBox valueTextBox = (TextBox)sender;
			int textureIndex = _textureTextBoxes.IndexOf(valueTextBox);

			if (int.TryParse(valueTextBox.Text, out int textureValue))
			{
				if (textureValue < -1)
				{
					textureValue = -1;
				}
				else if (textureValue > short.MaxValue ||
					textureValue >= _repository.Textures.Count)
				{
					textureValue = _repository.Textures.Count - 1;
				}

				_textureSet.TextureIDs[textureIndex] = unchecked((ushort)textureValue);
			}

			textureValue = unchecked((short)_textureSet.TextureIDs[textureIndex]);
			valueTextBox.Text = textureValue.ToString();
		}
	}
}
