using Recombobulator.SR1Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Recombobulator.ParticlePanels
{
	public partial class EditPanelBase : UserControl
	{
		protected readonly Dictionary<TextBox, SR1_Structure> _structures = new Dictionary<TextBox, SR1_Structure>();

		public EditPanelBase()
		{
			InitializeComponent();
		}

		protected TextBox AddFieldInternal(SR1_Structure structure, string name, ref int row)
		{
			if (structure.GetType() == typeof(Position) ||
				structure.GetType() == typeof(SVector) ||
				structure.GetType() == typeof(GlyphColors))
			{
				foreach (SR1_Structure member in structure.MembersRead)
				{
					AddFieldInternal(member, name + "." + member.Name, ref row);
				}
				return null;
			}

			fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

			Label nameLabel = new Label();
			nameLabel.Location = new Point(10, 10);
			nameLabel.Text = name;
			nameLabel.Size = new Size(120, 30);

			TextBox valueTextBox = new TextBox();
			valueTextBox.Location = new Point(10, 10);
			valueTextBox.Name = name;
			string valueText = structure.ToString();
			if (structure is SR1_PrimativeBase &&
				((SR1_PrimativeBase)structure).IsShowAsHex())
			{
				valueText = valueText.Substring(2);
			}
			valueTextBox.Text = valueText;
			valueTextBox.Size = new Size(80, 30);
			valueTextBox.Leave += ValueTextBox_Leave;
			valueTextBox.KeyDown += ValueTextBox_KeyDown;

			fieldsPanel.Controls.Add(nameLabel, 0, row);
			fieldsPanel.Controls.Add(valueTextBox, 1, row);
			_structures.Add(valueTextBox, structure);

			row++;

			return valueTextBox;
		}

		protected TextBox AddField(SR1_Structure structure, ref int row)
		{
			return AddFieldInternal(structure, structure.Name, ref row);
		}

		protected TextBox AddField(SR1_Structure structure, string name, ref int row)
		{
			return AddFieldInternal(structure, name, ref row);
		}

		protected TextBox AddField<T>(SR1_PrimativeArray<T> array, int index, string name, ref int row)
		{
			fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

			Label nameLabel = new Label();
			nameLabel.Location = new Point(10, 10);
			nameLabel.Text = name;
			nameLabel.Size = new Size(120, 30);

			TextBox valueTextBox = new TextBox();
			valueTextBox.Location = new Point(10, 10);
			valueTextBox.Name = name;
			string valueText = array[index].ToString();
			valueTextBox.Text = valueText;
			valueTextBox.Size = new Size(80, 30);
			valueTextBox.Leave += ValueTextBox_Leave;
			valueTextBox.KeyDown += ValueTextBox_KeyDown;

			fieldsPanel.Controls.Add(nameLabel, 0, row);
			fieldsPanel.Controls.Add(valueTextBox, 1, row);
			_structures.Add(valueTextBox, array);

			row++;

			return valueTextBox;
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
			if (selectionComboBox.SelectedIndex == -1)
			{
				return;
			}

			TextBox textBox = sender as TextBox;
			SR1_Structure structure = _structures[textBox];
			if (structure != null)
			{
				if (structure is SR1_PrimitiveArrayBase)
				{
					SetValue(textBox, (SR1_PrimitiveArrayBase)structure, selectionComboBox.SelectedIndex);
					return;
				}

				if (structure is SR1_PrimativeBase)
				{
					SetValue(textBox, (SR1_PrimativeBase)structure);
					return;
				}
			}
		}

		protected virtual bool SetValue(TextBox textBox, SR1_PrimativeBase primitive)
		{
			bool result = primitive.TryParse(textBox.Text);

			string valueText = primitive.ToString();
			if (primitive.IsShowAsHex())
			{
				textBox.Text = valueText.Substring(2);
			}
			else
			{
				textBox.Text = valueText;
			}

			return result;
		}

		protected virtual bool SetValue(TextBox textBox, SR1_PrimitiveArrayBase array, int index)
		{
			bool result = array.TryParse(index, textBox.Text);

			string valueText = array.ToString(index);
			if (array.IsShowAsHex())
			{
				textBox.Text = valueText.Substring(2);
			}
			else
			{
				textBox.Text = valueText;
			}

			return result;
		}

		protected bool TrySetColor(string nameToMatch, TextBox textBox, PictureBox pictureBox)
		{
			if (textBox.Name == nameToMatch)
			{
				int.TryParse(textBox.Text, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out int color);
				color |= unchecked((int)0xFF000000);
				pictureBox.BackColor = Color.FromArgb(color);
				return true;
			}

			return false;
		}
	}
}
