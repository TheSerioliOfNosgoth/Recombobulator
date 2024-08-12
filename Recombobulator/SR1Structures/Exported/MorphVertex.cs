using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MorphVertex : SR1_Structure
	{
		SR1_Primative<short> x = new SR1_Primative<short>();
		SR1_Primative<short> y = new SR1_Primative<short>();
		SR1_Primative<short> z = new SR1_Primative<short>();
		SR1_Primative<short> vindex = new SR1_Primative<short>();
		SR1_Primative<short> hx = new SR1_Primative<short>();
		SR1_Primative<short> hy = new SR1_Primative<short>();
		SR1_Primative<short> hz = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			x.Read(reader, this, "x");
			y.Read(reader, this, "y");
			z.Read(reader, this, "z");
			vindex.Read(reader, this, "vindex");
			hx.Read(reader, this, "hx");
			hy.Read(reader, this, "hy");
			hz.Read(reader, this, "hz");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public static void Copy(MorphVertex to, MorphVertex from)
		{
			to.x.Value = from.x.Value;
			to.y.Value = from.y.Value;
			to.z.Value = from.z.Value;
			to.vindex.Value = from.vindex.Value;
			to.hx.Value = from.hx.Value;
			to.hy.Value = from.hy.Value;
			to.hz.Value = from.hz.Value;
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			x.Write(writer);
			y.Write(writer);
			z.Write(writer);
			vindex.Write(writer);
			hx.Write(writer);
			hy.Write(writer);
			hz.Write(writer);
		}
	}
}
