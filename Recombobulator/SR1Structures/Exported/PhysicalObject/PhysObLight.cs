using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PhysObLight : SR1_Structure
	{
		SR1_Primative<byte> length = new SR1_Primative<byte>();
		SR1_Primative<byte> segment = new SR1_Primative<byte>();
		SR1_Primative<short> speed = new SR1_Primative<short>();
		SR1_Pointer<LightTableEntry> lightTable = new SR1_Pointer<LightTableEntry>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			length.Read(reader, this, "length");
			segment.Read(reader, this, "segment");
			speed.Read(reader, this, "speed");
			lightTable.Read(reader, this, "lightTable");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new SR1_StructureArray<LightTableEntry>(length.Value).ReadFromPointer(reader, lightTable);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			length.Write(writer);
			segment.Write(writer);
			speed.Write(writer);
			lightTable.Write(writer);
		}
	}
}