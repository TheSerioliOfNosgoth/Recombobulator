using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GenericGlowParams : SR1_Structure
	{
		SR1_Primative<sbyte> StartOnInit = new SR1_Primative<sbyte>();
		SR1_Primative<short> StartOnInit_b = new SR1_Primative<short>();
		SR1_Primative<byte> segment = new SR1_Primative<byte>();
		SR1_Primative<byte> segmentEnd = new SR1_Primative<byte>();
		SR1_Primative<sbyte> numSegments = new SR1_Primative<sbyte>();
		SR1_Primative<short> numSegments_b = new SR1_Primative<short>();
		SR1_Primative<byte> color_num = new SR1_Primative<byte>();
		SR1_Primative<short> color_num_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
		SR1_Primative<byte> numColors = new SR1_Primative<byte>();
		SR1_Primative<short> numColors_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> id = new SR1_Primative<sbyte>();
		SR1_Primative<short> id_b = new SR1_Primative<short>();
		SR1_Primative<int> atuColorCycleRate = new SR1_Primative<int>();
		SR1_Primative<short> width = new SR1_Primative<short>();
		SR1_Primative<short> height = new SR1_Primative<short>();
		SR1_Primative<int> lifetime = new SR1_Primative<int>();
		SR1_Primative<short> fadein_time = new SR1_Primative<short>();
		SR1_Primative<short> fadeout_time = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				StartOnInit.Read(reader, this, "StartOnInit");
				segment.Read(reader, this, "segment");
				segmentEnd.Read(reader, this, "segmentEnd");
				numSegments.Read(reader, this, "numSegments");
				color_num.Read(reader, this, "color_num");
				use_child.Read(reader, this, "use_child");
				numColors.Read(reader, this, "numColors");
				id.Read(reader, this, "id");
				atuColorCycleRate.Read(reader, this, "atuColorCycleRate");
				width.Read(reader, this, "width");
				height.Read(reader, this, "height");
				lifetime.Read(reader, this, "lifetime");
				fadein_time.Read(reader, this, "fadein_time");
				fadeout_time.Read(reader, this, "fadeout_time");
			}
			else // if (reader.File._Version >= SR1_File.Version.May12)
			{
				id_b.Read(reader, this, "id");
				StartOnInit_b.Read(reader, this, "StartOnInit");
				segment.Read(reader, this, "segment");
				segmentEnd.Read(reader, this, "segmentEnd");
				numSegments_b.Read(reader, this, "numSegments");
				color_num_b.Read(reader, this, "color_num");
				numColors_b.Read(reader, this, "numColors");
				atuColorCycleRate.Read(reader, this, "atuColorCycleRate");
				width.Read(reader, this, "width");
				height.Read(reader, this, "height");
				lifetime.Read(reader, this, "lifetime");
				fadein_time.Read(reader, this, "fadein_time");
				fadeout_time.Read(reader, this, "fadeout_time");
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			if (writer.File._Version >= SR1_File.Version.Jun01)
			{
				StartOnInit.Write(writer);
				segment.Write(writer);
				segmentEnd.Write(writer);
				numSegments.Write(writer);
				color_num.Write(writer);
				use_child.Write(writer);
				numColors.Write(writer);
				id.Write(writer);
				atuColorCycleRate.Write(writer);
				width.Write(writer);
				height.Write(writer);
				lifetime.Write(writer);
				fadein_time.Write(writer);
				fadeout_time.Write(writer);
			}
			else // if (writer.File._Version >= SR1_File.Version.May12)
			{
				id_b.Write(writer);
				StartOnInit_b.Write(writer);
				segment.Write(writer);
				segmentEnd.Write(writer);
				numSegments_b.Write(writer);
				color_num_b.Write(writer);
				numColors_b.Write(writer);
				atuColorCycleRate.Write(writer);
				width.Write(writer);
				height.Write(writer);
				lifetime.Write(writer);
				fadein_time.Write(writer);
				fadeout_time.Write(writer);
			}
		}

		public static void Copy(GenericGlowParams to, GenericGlowParams from)
		{
			to.StartOnInit.Value = from.StartOnInit.Value;
			to.StartOnInit_b.Value = from.StartOnInit_b.Value;
			to.segment.Value = from.segment.Value;
			to.segmentEnd.Value = from.segmentEnd.Value;
			to.numSegments.Value = from.numSegments.Value;
			to.numSegments_b.Value = from.numSegments_b.Value;
			to.color_num.Value = from.color_num.Value;
			to.use_child.Value = from.use_child.Value;
			to.numColors.Value = from.numColors.Value;
			to.numColors_b.Value = from.numColors_b.Value;
			to.id.Value = from.id.Value;
			to.id_b.Value = from.id_b.Value;
			to.atuColorCycleRate.Value = from.atuColorCycleRate.Value;
			to.width.Value = from.width.Value;
			to.height.Value = from.height.Value;
			to.lifetime.Value = from.lifetime.Value;
			to.fadein_time.Value = from.fadein_time.Value;
			to.fadeout_time.Value = from.fadeout_time.Value;
		}
	}
}
