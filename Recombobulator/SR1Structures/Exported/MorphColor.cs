using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MorphColor : SR1_Structure
	{
		SR1_Primative<short> morphColor15 = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			morphColor15.Read(reader, this, "morphColor15");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			morphColor15.Write(writer);
		}
	}
}
