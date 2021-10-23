using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SplineDef : SR1_Structure
	{
		SR1_Primative<short> currkey = new SR1_Primative<short>();
		SR1_Primative<ushort> denomFlag = new SR1_Primative<ushort>();
		SR1_Primative<int> fracCurr = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			currkey.Read(reader, this, "currkey");
			denomFlag.Read(reader, this, "denomFlag");
			fracCurr.Read(reader, this, "fracCurr");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			currkey.Write(writer);
			denomFlag.Write(writer);
			fracCurr.Write(writer);
		}
	}
}
