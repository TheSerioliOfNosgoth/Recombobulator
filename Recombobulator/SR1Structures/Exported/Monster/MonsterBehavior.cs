using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MonsterBehavior : SR1_Structure
	{
		public readonly SR1_Primative<sbyte> alertness = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> idleFreq = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numIdles = new SR1_Primative<sbyte>();
		public readonly SR1_PrimativeArray<sbyte> idleList = new SR1_PrimativeArray<sbyte>(5);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			alertness.Read(reader, this, "alertness");
			idleFreq.Read(reader, this, "idleFreq");
			numIdles.Read(reader, this, "numIdles");
			idleList.Read(reader, this, "idleList");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			alertness.Write(writer);
			idleFreq.Write(writer);
			numIdles.Write(writer);
			idleList.Write(writer);
		}
	}
}
