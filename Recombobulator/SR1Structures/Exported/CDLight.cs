using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class CDLight : SR1_Structure
	{
		NodeType node = new NodeType();
		SR1_Primative<byte> r = new SR1_Primative<byte>();
		SR1_Primative<byte> g = new SR1_Primative<byte>();
		SR1_Primative<byte> b = new SR1_Primative<byte>();
		SR1_Primative<byte> flags = new SR1_Primative<byte>();
		Sphere sphere = new Sphere();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			node.Read(reader, this, "node");
			r.Read(reader, this, "r");
			g.Read(reader, this, "g");
			b.Read(reader, this, "b");
			flags.Read(reader, this, "flags");
			sphere.Read(reader, this, "sphere");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			node.Write(writer);
			r.Write(writer);
			g.Write(writer);
			b.Write(writer);
			flags.Write(writer);
			sphere.Write(writer);
		}
	}
}
