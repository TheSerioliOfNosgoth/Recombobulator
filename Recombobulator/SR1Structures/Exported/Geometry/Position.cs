using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Position : SR1_Structure
	{
		public readonly SR1_Primative<short> x = new SR1_Primative<short>();
		public readonly SR1_Primative<short> y = new SR1_Primative<short>();
		public readonly SR1_Primative<short> z = new SR1_Primative<short>();

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

		public static void Copy(Position to, Position from)
		{
			to.x.Value = from.x.Value;
			to.y.Value = from.y.Value;
			to.z.Value = from.z.Value;
		}

		public override string ToString()
		{
			return "{ x = " + x + ", y = " + y + ", z = " + z + " }";
		}
	}
}
