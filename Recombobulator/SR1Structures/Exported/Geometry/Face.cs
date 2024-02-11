using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Face : SR1_Structure
	{
		SR1_Primative<ushort> v0 = new SR1_Primative<ushort>();
		SR1_Primative<ushort> v1 = new SR1_Primative<ushort>();
		SR1_Primative<ushort> v2 = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			v0.Read(reader, this, "v0");
			v1.Read(reader, this, "v1");
			v2.Read(reader, this, "v2");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			v0.Write(writer);
			v1.Write(writer);
			v2.Write(writer);
		}
	}
}
