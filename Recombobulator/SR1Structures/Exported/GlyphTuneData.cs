using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GlyphTuneData : SR1_Structure
	{
		SR1_Primative<short> glyph_size = new SR1_Primative<short>();
		SR1_Primative<short> glyph_darkness = new SR1_Primative<short>();
		SR1_PrimativeArray<sbyte> glyph_costs = new SR1_PrimativeArray<sbyte>(8);
		SR1_PrimativeArray<short> glyph_range = new SR1_PrimativeArray<short>(8);
		SR1_StructureArray<GlyphColors> color_array = new SR1_StructureArray<GlyphColors>(8);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			glyph_size.Read(reader, this, "glyph_size");
			glyph_darkness.Read(reader, this, "glyph_darkness");
			glyph_costs.Read(reader, this, "glyph_costs");
			glyph_range.Read(reader, this, "glyph_range");
			color_array.Read(reader, this, "color_array");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			glyph_size.Write(writer);
			glyph_darkness.Write(writer);
			glyph_costs.Write(writer);
			glyph_range.Write(writer);
			color_array.Write(writer);
		}
	}
}
