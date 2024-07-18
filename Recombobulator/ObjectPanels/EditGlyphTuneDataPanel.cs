using Recombobulator.SR1Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Recombobulator.ParticlePanels
{
	public partial class EditGlyphTuneDataPanel : EditPanelBase
	{
		GlyphTuneData _glyph = null;
		GlyphTuneData _glyphBackup = null;
		const int _numGlyphs = 8;

		public EditGlyphTuneDataPanel()
		{
			InitializeComponent();
		}

		public void Open(object glyph, object glyphBackup = null)
		{
			_glyph = (GlyphTuneData)glyph;
			_glyphBackup = (GlyphTuneData)glyphBackup;

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.Items.Clear();
			for (int i = 0; i < _numGlyphs; i++)
			{
				selectionComboBox.Items.Add("glyph-" + i.ToString());
			}

			selectionComboBox.SelectedIndex = 0;
		}

		private void selectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			int newIndex = selectionComboBox.SelectedIndex;

			insertButton.Enabled = false;
			removeButton.Enabled = false;
			resetButton.Enabled = false;
			addButton.Enabled = false;
			copyButton.Enabled = false;
			pasteButton.Enabled = false;

			fieldsPanel.Controls.Clear();
			fieldsPanel.RowStyles.Clear();
			_structures.Clear();

			if (newIndex < 0)
			{
				return;
			}

			if (_glyphBackup != null)
			{
				copyButton.Enabled = true;
				pasteButton.Enabled = true;
				resetButton.Enabled = true;
			}

			fieldsPanel.RowCount = _numGlyphs;

			int row = 0;
			AddField(_glyph.glyph_costs, newIndex, "cost", ref row);
			AddField(_glyph.glyph_range, newIndex, "range", ref row);
			AddField(_glyph.color_array[newIndex], "color", ref row);

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}
	}
}
