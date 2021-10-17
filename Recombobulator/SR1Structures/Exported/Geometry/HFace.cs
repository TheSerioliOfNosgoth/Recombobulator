using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class HFace : SR1_Structure
	{
		SR1_Primative<short> v0 = new SR1_Primative<short>();
		SR1_Primative<short> v1 = new SR1_Primative<short>();
		SR1_Primative<short> v2 = new SR1_Primative<short>();
		SR1_Primative<byte> attr = new SR1_Primative<byte>();
		SR1_Primative<sbyte> pad = new SR1_Primative<sbyte>();
		SR1_Primative<ushort> normal = new SR1_Primative<ushort>();
		SR1_Primative<ushort> n0 = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			v0.Read(reader, this, "v0");
			v1.Read(reader, this, "v1");
			v2.Read(reader, this, "v2");
			attr.Read(reader, this, "attr");
			pad.Read(reader, this, "pad");
			normal.Read(reader, this, "normal");
			n0.Read(reader, this, "n0");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			v0.Write(writer);
			v1.Write(writer);
			v2.Write(writer);
			attr.Write(writer);
			pad.Write(writer);
			normal.Write(writer);
			n0.Write(writer);
		}
	}
}
