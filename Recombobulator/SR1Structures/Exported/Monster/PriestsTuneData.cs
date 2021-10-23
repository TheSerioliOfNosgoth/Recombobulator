using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PriestsTuneData : MonsterTuneData
	{
		SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(16);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			unknown.Read(reader, this, "unknown");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			unknown.Write(writer);
		}
	}
}