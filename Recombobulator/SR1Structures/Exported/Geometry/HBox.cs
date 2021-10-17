using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class HBox : SR1_Structure
	{
		SR1_Primative<short> flags = new SR1_Primative<short>();
		SR1_Primative<byte> id = new SR1_Primative<byte>();
		SR1_Primative<byte> rank = new SR1_Primative<byte>();
		SR1_Primative<short> minX = new SR1_Primative<short>();
		SR1_Primative<short> minY = new SR1_Primative<short>();
		SR1_Primative<short> minZ = new SR1_Primative<short>();
		SR1_Primative<short> maxX = new SR1_Primative<short>();
		SR1_Primative<short> maxY = new SR1_Primative<short>();
		SR1_Primative<short> maxZ = new SR1_Primative<short>();
		SR1_Primative<short> refMinX = new SR1_Primative<short>();
		SR1_Primative<short> refMinY = new SR1_Primative<short>();
		SR1_Primative<short> refMinZ = new SR1_Primative<short>();
		SR1_Primative<short> refMaxX = new SR1_Primative<short>();
		SR1_Primative<short> refMaxY = new SR1_Primative<short>();
		SR1_Primative<short> refMaxZ = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			flags.Read(reader, this, "flags");
			id.Read(reader, this, "id");
			rank.Read(reader, this, "rank");
			minX.Read(reader, this, "minX");
			minY.Read(reader, this, "minY");
			minZ.Read(reader, this, "minZ");
			maxX.Read(reader, this, "maxX");
			maxY.Read(reader, this, "maxY");
			maxZ.Read(reader, this, "maxZ");
			refMinX.Read(reader, this, "refMinX");
			refMinY.Read(reader, this, "refMinY");
			refMinZ.Read(reader, this, "refMinZ");
			refMaxX.Read(reader, this, "refMaxX");
			refMaxY.Read(reader, this, "refMaxY");
			refMaxZ.Read(reader, this, "refMaxZ");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags.Write(writer);
			id.Write(writer);
			rank.Write(writer);
			minX.Write(writer);
			minY.Write(writer);
			minZ.Write(writer);
			maxX.Write(writer);
			maxY.Write(writer);
			maxZ.Write(writer);
			refMinX.Write(writer);
			refMinY.Write(writer);
			refMinZ.Write(writer);
			refMaxX.Write(writer);
			refMaxY.Write(writer);
			refMaxZ.Write(writer);
		}
	}
}
