using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Normal : SR1_Structure
	{
		SR1_Primative<short> x = new SR1_Primative<short>();
		SR1_Primative<short> y = new SR1_Primative<short>();
		SR1_Primative<short> z = new SR1_Primative<short>();
		//SR1_Primative<short> pad = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			x.Read(reader, this, "x");
			y.Read(reader, this, "y");
			z.Read(reader, this, "z");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			x.Write(writer);
			y.Write(writer);
			z.Write(writer);
		}
	}
}
