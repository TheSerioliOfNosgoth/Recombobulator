using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Spline : SR1_Structure
	{
		SR1_Pointer<SplineKey> key = new SR1_Pointer<SplineKey>();
		SR1_Primative<short> numkeys = new SR1_Primative<short>();
		SR1_Primative<byte> type = new SR1_Primative<byte>();
		SR1_Primative<byte> flags = new SR1_Primative<byte>();

		SR1_StructureArray<SplineKey> keyList = new SR1_StructureArray<SplineKey>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			key.Read(reader, this, "key");
			numkeys.Read(reader, this, "numkeys");
			type.Read(reader, this, "type");
			flags.Read(reader, this, "flags");

			keyList = new SR1_StructureArray<SplineKey>(numkeys.Value);
			keyList.Read(reader, this, "keyList");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			key.Write(writer);
			numkeys.Write(writer);
			type.Write(writer);
			flags.Write(writer);

			keyList.Write(writer);
		}
	}
}
