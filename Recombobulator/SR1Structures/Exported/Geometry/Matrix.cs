using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Matrix : SR1_Structure
	{
		SR1_PrimativeArray<short> m = new SR1_PrimativeArray<short>(3, 3);
		SR1_PrimativeArray<int> t = new SR1_PrimativeArray<int>(3);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			m.Read(reader, this, "m");
			t.Read(reader, this, "t");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			m.Write(writer);
			t.Write(writer);
		}
	}
}
