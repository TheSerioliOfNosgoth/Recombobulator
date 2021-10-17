using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMColorOffset : SR1_Structure
	{
		SR1_Primative<sbyte> dr = new SR1_Primative<sbyte>();
		SR1_Primative<sbyte> dg = new SR1_Primative<sbyte>();
		SR1_Primative<sbyte> db = new SR1_Primative<sbyte>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			dr.Read(reader, this, "dr");
			dg.Read(reader, this, "dg");
			db.Read(reader, this, "db");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			dr.Write(writer);
			dg.Write(writer);
			db.Write(writer);
		}
	}
}
