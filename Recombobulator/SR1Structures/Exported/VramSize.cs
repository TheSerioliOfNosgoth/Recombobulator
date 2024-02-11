using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class VramSize : SR1_Structure
	{
		public readonly SR1_Primative<short> x = new SR1_Primative<short>();
		public readonly SR1_Primative<short> y = new SR1_Primative<short>();
		public readonly SR1_Primative<short> w = new SR1_Primative<short>();
		public readonly SR1_Primative<short> h = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			x.Read(reader, this, "x");
			y.Read(reader, this, "y");
			w.Read(reader, this, "w");
			h.Read(reader, this, "h");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			x.Write(writer);
			y.Write(writer);
			w.Write(writer);
			h.Write(writer);
		}
	}
}
