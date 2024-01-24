using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GenericLightningParams : SR1_Structure
	{
		SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
		SR1_Primative<short> type_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
		SR1_Primative<short> lifeTime = new SR1_Primative<short>();
		SR1_Primative<short> deg = new SR1_Primative<short>();
		SR1_Primative<short> deg_inc = new SR1_Primative<short>();
		Position start_offset = new Position();
		SR1_Primative<sbyte> startSeg = new SR1_Primative<sbyte>();
		SR1_Primative<short> startSeg_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> endSeg = new SR1_Primative<sbyte>();
		SR1_Primative<short> endSeg_b = new SR1_Primative<short>();
		Position end_offset = new Position();
		SR1_Primative<sbyte> matrixSeg = new SR1_Primative<sbyte>();
		SR1_Primative<short> matrixSeg_b = new SR1_Primative<short>();
		SR1_Primative<byte> segs = new SR1_Primative<byte>();
		SR1_Primative<short> segs_b = new SR1_Primative<short>();
		SR1_Primative<short> width = new SR1_Primative<short>();
		SR1_Primative<short> small_width = new SR1_Primative<short>();
		SR1_Primative<short> sine_size = new SR1_Primative<short>();
		SR1_Primative<short> variation = new SR1_Primative<short>();
		SR1_Primative<int> color = new SR1_Primative<int>().ShowAsHex(true);
		SR1_Primative<int> glow_color = new SR1_Primative<int>().ShowAsHex(true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				type.Read(reader, this, "type");
				use_child.Read(reader, this, "use_child");
				lifeTime.Read(reader, this, "lifeTime");
				deg.Read(reader, this, "deg");
				deg_inc.Read(reader, this, "deg_inc");
				start_offset.Read(reader, this, "start_offset");
				startSeg.Read(reader, this, "startSeg");
				endSeg.Read(reader, this, "endSeg");
				end_offset.Read(reader, this, "end_offset");
				matrixSeg.Read(reader, this, "matrixSeg");
				segs.Read(reader, this, "segs");
				width.Read(reader, this, "width");
				small_width.Read(reader, this, "small_width");
				sine_size.Read(reader, this, "sine_size");
				variation.Read(reader, this, "variation");
				color.Read(reader, this, "color");
				glow_color.Read(reader, this, "glow_color");
			}
			else // if (reader.File._Version >= SR1_File.Version.May12)
			{
				type_b.Read(reader, this, "type");
				lifeTime.Read(reader, this, "lifeTime");
				deg.Read(reader, this, "deg");
				deg_inc.Read(reader, this, "deg_inc");
				start_offset.Read(reader, this, "start_offset");
				startSeg_b.Read(reader, this, "startSeg");
				end_offset.Read(reader, this, "end_offset");
				endSeg_b.Read(reader, this, "endSeg");
				matrixSeg_b.Read(reader, this, "matrixSeg");
				width.Read(reader, this, "width");
				small_width.Read(reader, this, "small_width");
				segs_b.Read(reader, this, "segs");
				sine_size.Read(reader, this, "sine_size");
				variation.Read(reader, this, "variation");
				color.Read(reader, this, "color");
				glow_color.Read(reader, this, "glow_color");
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			if (writer.File._Version >= SR1_File.Version.Jun01)
			{
				type.Write(writer);
				use_child.Write(writer);
				lifeTime.Write(writer);
				deg.Write(writer);
				deg_inc.Write(writer);
				start_offset.Write(writer);
				startSeg.Write(writer);
				endSeg.Write(writer);
				end_offset.Write(writer);
				matrixSeg.Write(writer);
				segs.Write(writer);
				width.Write(writer);
				small_width.Write(writer);
				sine_size.Write(writer);
				variation.Write(writer);
				color.Write(writer);
				glow_color.Write(writer);
			}
			else // if (writer.File._Version >= SR1_File.Version.May12)
			{
				type_b.Write(writer);
				lifeTime.Write(writer);
				deg.Write(writer);
				deg_inc.Write(writer);
				start_offset.Write(writer);
				startSeg_b.Write(writer);
				end_offset.Write(writer);
				endSeg_b.Write(writer);
				matrixSeg_b.Write(writer);
				width.Write(writer);
				small_width.Write(writer);
				segs_b.Write(writer);
				sine_size.Write(writer);
				variation.Write(writer);
				color.Write(writer);
				glow_color.Write(writer);
			}
		}

		public static void Copy(GenericLightningParams to, GenericLightningParams from)
		{
			to.type.Value = from.type.Value;
			to.type_b.Value = from.type_b.Value;
			to.use_child.Value = from.use_child.Value;
			to.lifeTime.Value = from.lifeTime.Value;
			to.deg.Value = from.deg.Value;
			to.deg_inc.Value = from.deg_inc.Value;
			Position.Copy(to.start_offset, from.start_offset);
			to.startSeg.Value = from.startSeg.Value;
			to.startSeg_b.Value = from.startSeg_b.Value;
			to.endSeg.Value = from.endSeg.Value;
			to.endSeg_b.Value = from.endSeg_b.Value;
			Position.Copy(to.end_offset, from.end_offset);
			to.matrixSeg.Value = from.matrixSeg.Value;
			to.matrixSeg_b.Value = from.matrixSeg_b.Value;
			to.segs.Value = from.segs.Value;
			to.segs_b.Value = from.segs_b.Value;
			to.width.Value = from.width.Value;
			to.small_width.Value = from.small_width.Value;
			to.sine_size.Value = from.sine_size.Value;
			to.variation.Value = from.variation.Value;
			to.color.Value = from.color.Value;
			to.glow_color.Value = from.glow_color.Value;
		}
	}
}
