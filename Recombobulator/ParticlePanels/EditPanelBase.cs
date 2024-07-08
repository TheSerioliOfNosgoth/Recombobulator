using Recombobulator.SR1Structures;
using System.Drawing;
using System.Windows.Forms;

namespace Recombobulator.ParticlePanels
{
	public partial class EditPanelBase : UserControl
	{
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

			fieldsPanel.Controls.Add(nameLabel, 0, row);
			fieldsPanel.Controls.Add(valueTextBox, 1, row);

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

			fieldsPanel.Controls.Add(nameLabel, 0, row);
			fieldsPanel.Controls.Add(valueTextBox, 1, row);

			row++;

			return valueTextBox;
		}
	}
}
