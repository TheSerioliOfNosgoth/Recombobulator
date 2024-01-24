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

		protected TextBox AddField(SR1_Structure structure, string prefix, ref int row)
		{
			if (structure.GetType() == typeof(Position))
			{
				Position position = (Position)structure;
				AddField(position.x, structure.Name + ".", ref row);
				AddField(position.y, structure.Name + ".", ref row);
				AddField(position.z, structure.Name + ".", ref row);
				return null;
			}

			if (structure.GetType() == typeof(SVector))
			{
				SVector svector = (SVector)structure;
				AddField(svector.x, structure.Name + ".", ref row);
				AddField(svector.y, structure.Name + ".", ref row);
				AddField(svector.z, structure.Name + ".", ref row);
				return null;
			}

			fieldsPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));

			Label nameLabel = new Label();
			nameLabel.Location = new Point(10, 10);
			nameLabel.Text = prefix + structure.Name;
			nameLabel.Size = new Size(120, 30);

			TextBox valueTextBox = new TextBox();
			valueTextBox.Location = new Point(10, 10);
			valueTextBox.Name = structure.Name;
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
	}
}
