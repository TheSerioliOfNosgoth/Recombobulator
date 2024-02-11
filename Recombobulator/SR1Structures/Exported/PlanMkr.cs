using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class PlanMkr : SR1_Structure
	{
		Position pos = new Position();
		SR1_Primative<short> id = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			pos.Read(reader, this, "pos");
			id.Read(reader, this, "id");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			pos.Write(writer);
			id.Write(writer);
		}
	}
}
