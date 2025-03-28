using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VecL : SR1_Structure
	{
		SR1_Primative<int> x = new SR1_Primative<int>();
		SR1_Primative<int> y = new SR1_Primative<int>();
		SR1_Primative<int> z = new SR1_Primative<int>();

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

		public override string ToString()
		{
			return "{ x = " + x + ", y = " + y + ", z = " + z + " }";
		}
	}
}
