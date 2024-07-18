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
		sbyte _copiedCost = 0;
		short _copiedRange = 0;
		GlyphColors _copiedColors = new GlyphColors();
		const int _numGlyphs = 8;

		public EditGlyphTuneDataPanel()
		{
			InitializeComponent();

			glyphSizeTextBox.Leave += ValueTextBox_Leave;
			glyphSizeTextBox.KeyDown += ValueTextBox_KeyDown;
			glyphDarknessTextBox.Leave += ValueTextBox_Leave;
			glyphDarknessTextBox.KeyDown += ValueTextBox_KeyDown;
		}

		public void Open(object glyph, object glyphBackup = null)
		{
			_glyph = (GlyphTuneData)glyph;
			_glyphBackup = (GlyphTuneData)glyphBackup;

			string glyphSizeText = _glyph.glyph_size.ToString();
			glyphSizeTextBox.Text = glyphSizeText;
			string glyphDarknessText = _glyph.glyph_darkness.ToString();
			glyphDarknessTextBox.Text = glyphDarknessText;

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
			_structures.Add(glyphSizeTextBox, _glyph.glyph_size);
			_structures.Add(glyphDarknessTextBox, _glyph.glyph_darkness);

			if (newIndex < 0)
			{
				return;
			}

			copyButton.Enabled = true;
			pasteButton.Enabled = true;

			if (_glyphBackup != null)
			{
				resetButton.Enabled = true;
			}
			else
			{
				resetButton.Enabled = false;
			}

			fieldsPanel.RowCount = _numGlyphs;

			int row = 0;
			AddField(_glyph.glyph_costs, newIndex, "cost", ref row);
			AddField(_glyph.glyph_range, newIndex, "range", ref row);
			AddField(_glyph.color_array[newIndex], "color", ref row);

			fieldsPanel.Visible = true;
			fieldsPanel.Enabled = true;
		}

		private void copyButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _numGlyphs)
			{
				return;
			}

			_copiedCost = _glyph.glyph_costs[currentIndex];
			_copiedRange = _glyph.glyph_range[currentIndex];
			GlyphColors.Copy(_copiedColors, (GlyphColors)_glyph.color_array[currentIndex]);
		}

		private void pasteButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (currentIndex < 0 ||
				currentIndex >= _numGlyphs)
			{
				return;
			}

			_glyph.glyph_costs[currentIndex] = _copiedCost;
			_glyph.glyph_range[currentIndex] = _copiedRange;
			GlyphColors.Copy((GlyphColors)_glyph.color_array[currentIndex], _copiedColors);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			int currentIndex = selectionComboBox.SelectedIndex;

			if (_glyphBackup == null ||
				currentIndex < 0 ||
				currentIndex >= _numGlyphs)
			{
				return;
			}

			GlyphColors.Copy(
				(GlyphColors)_glyph.color_array[currentIndex],
				(GlyphColors)_glyphBackup.color_array[currentIndex]
			);

			selectionComboBox.SelectedIndex = -1;
			selectionComboBox.SelectedIndex = currentIndex;
		}
	}
}
