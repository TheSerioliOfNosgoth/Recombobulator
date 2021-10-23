using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Mirror : SR1_Structure
	{
		SR1_Primative<short> a = new SR1_Primative<short>();
		SR1_Primative<short> b = new SR1_Primative<short>();
		SR1_Primative<short> c = new SR1_Primative<short>();
		SR1_Primative<short> d = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			a.Read(reader, this, "a");
			b.Read(reader, this, "b");
			c.Read(reader, this, "c");
			d.Read(reader, this, "d");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			a.Write(writer);
			b.Write(writer);
			c.Write(writer);
			d.Write(writer);
		}
	}
}