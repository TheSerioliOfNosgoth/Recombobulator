using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MonsterState : SR1_Structure
	{
		SR1_Primative<uint> entryFunction = new SR1_Primative<uint>();
		SR1_Primative<uint> stateFunction = new SR1_Primative<uint>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			entryFunction.Read(reader, this, "entryFunction");
			stateFunction.Read(reader, this, "stateFunction");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			entryFunction.Write(writer);
			stateFunction.Write(writer);
		}
	}
}
