using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SplineKey : SR1_Structure
	{
		SR1_Primative<short> count = new SR1_Primative<short>();
		VecS point = new VecS();
		VecL dd = new VecL();
		VecL ds = new VecL();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			count.Read(reader, this, "count");
			point.Read(reader, this, "point");
			dd.Read(reader, this, "dd");
			ds.Read(reader, this, "ds");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			count.Write(writer);
			point.Write(writer);
			dd.Write(writer);
			ds.Write(writer);
		}
	}
}
