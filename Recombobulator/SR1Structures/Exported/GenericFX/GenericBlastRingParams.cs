using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GenericBlastRingParams : SR1_Structure
	{
		SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
		SR1_Primative<short> type_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
		SR1_Primative<short> lifeTime = new SR1_Primative<short>();
		Position offset = new Position();
		SVector offset_b = new SVector();
		SR1_Primative<short> matrixSeg = new SR1_Primative<short>();
		SR1_Primative<sbyte> segment = new SR1_Primative<sbyte>();
		SR1_Primative<short> segment_b = new SR1_Primative<short>();
		SR1_Primative<sbyte> sortInWorld = new SR1_Primative<sbyte>();
		SR1_Primative<short> sortInWorld_b = new SR1_Primative<short>();
		SR1_Primative<short> radius = new SR1_Primative<short>();
		SR1_Primative<short> size1 = new SR1_Primative<short>();
		SR1_Primative<short> size2 = new SR1_Primative<short>();
		SR1_Primative<short> endRadius = new SR1_Primative<short>();
		SR1_Primative<short> colorchange_radius = new SR1_Primative<short>();
		SR1_Primative<int> vel = new SR1_Primative<int>();
		SR1_Primative<int> accl = new SR1_Primative<int>();
		SR1_Primative<short> height1 = new SR1_Primative<short>();
		SR1_Primative<short> height2 = new SR1_Primative<short>();
		SR1_Primative<short> height3 = new SR1_Primative<short>();
		SR1_Primative<short> predator_offset = new SR1_Primative<short>();
		SR1_Primative<int> startColor = new SR1_Primative<int>().ShowAsHex(true);
		SR1_Primative<int> endColor = new SR1_Primative<int>().ShowAsHex(true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				type.Read(reader, this, "type");
				use_child.Read(reader, this, "use_child");
				lifeTime.Read(reader, this, "lifeTime");
				offset.Read(reader, this, "offset");
				matrixSeg.Read(reader, this, "matrixSeg");
				segment.Read(reader, this, "segment");
				sortInWorld.Read(reader, this, "sortInWorld");
				radius.Read(reader, this, "radius");
				size1.Read(reader, this, "size1");
				size2.Read(reader, this, "size2");
				endRadius.Read(reader, this, "endRadius");
				colorchange_radius.Read(reader, this, "colorchange_radius");
				vel.Read(reader, this, "vel");
				accl.Read(reader, this, "accl");
				height1.Read(reader, this, "height1");
				height2.Read(reader, this, "height2");
				height3.Read(reader, this, "height3");
				predator_offset.Read(reader, this, "predator_offset");
				startColor.Read(reader, this, "startColor");
				endColor.Read(reader, this, "endColor");
			}
			else // if (reader.File._Version >= SR1_File.Version.May12)
			{
				type_b.Read(reader, this, "type");
				lifeTime.Read(reader, this, "lifeTime");
				matrixSeg.Read(reader, this, "matrixSeg");
				segment_b.Read(reader, this, "segment");
				offset_b.Read(reader, this, "offset");
				sortInWorld_b.Read(reader, this, "sortInWorld");
				radius.Read(reader, this, "radius");
				size1.Read(reader, this, "size1");
				size2.Read(reader, this, "size2");
				endRadius.Read(reader, this, "endRadius");
				colorchange_radius.Read(reader, this, "colorchange_radius");
				vel.Read(reader, this, "vel");
				accl.Read(reader, this, "accl");
				height1.Read(reader, this, "height1");
				height2.Read(reader, this, "height2");
				height3.Read(reader, this, "height3");
				predator_offset.Read(reader, this, "predator_offset");
				startColor.Read(reader, this, "startColor");
				endColor.Read(reader, this, "endColor");
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
				offset.Write(writer);
				matrixSeg.Write(writer);
				segment.Write(writer);
				sortInWorld.Write(writer);
				radius.Write(writer);
				size1.Write(writer);
				size2.Write(writer);
				endRadius.Write(writer);
				colorchange_radius.Write(writer);
				vel.Write(writer);
				accl.Write(writer);
				height1.Write(writer);
				height2.Write(writer);
				height3.Write(writer);
				predator_offset.Write(writer);
				startColor.Write(writer);
				endColor.Write(writer);
			}
			else // if (writer.File._Version >= SR1_File.Version.May12)
			{
				type_b.Write(writer);
				lifeTime.Write(writer);
				matrixSeg.Write(writer);
				segment_b.Write(writer);
				offset_b.Write(writer);
				sortInWorld_b.Write(writer);
				radius.Write(writer);
				size1.Write(writer);
				size2.Write(writer);
				endRadius.Write(writer);
				colorchange_radius.Write(writer);
				vel.Write(writer);
				accl.Write(writer);
				height1.Write(writer);
				height2.Write(writer);
				height3.Write(writer);
				predator_offset.Write(writer);
				startColor.Write(writer);
				endColor.Write(writer);
			}
		}

		public static void Copy(GenericBlastRingParams to, GenericBlastRingParams from)
		{
			to.type.Value = from.type.Value;
			to.type_b.Value = from.type_b.Value;
			to.use_child.Value = from.use_child.Value;
			to.lifeTime.Value = from.lifeTime.Value;
			Position.Copy(to.offset, from.offset);
			SVector.Copy(to.offset_b, from.offset_b);
			to.matrixSeg.Value = from.matrixSeg.Value;
			to.segment.Value = from.segment.Value;
			to.segment_b.Value = from.segment_b.Value;
			to.sortInWorld.Value = from.sortInWorld.Value;
			to.sortInWorld_b.Value = from.sortInWorld_b.Value;
			to.radius.Value = from.radius.Value;
			to.size1.Value = from.size1.Value;
			to.size2.Value = from.size2.Value;
			to.endRadius.Value = from.endRadius.Value;
			to.colorchange_radius.Value = from.colorchange_radius.Value;
			to.vel.Value = from.vel.Value;
			to.accl.Value = from.accl.Value;
			to.height1.Value = from.height1.Value;
			to.height2.Value = from.height2.Value;
			to.height3.Value = from.height3.Value;
			to.predator_offset.Value = from.predator_offset.Value;
			to.startColor.Value = from.startColor.Value;
			to.endColor.Value = from.endColor.Value;
		}
	}
}
