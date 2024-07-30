using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MorphColor : SR1_Structure
	{
		public readonly SR1_Primative<byte> r0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> g0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> b0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> r1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> g1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> b1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<short> vindex = new SR1_Primative<short>();
		public readonly SR1_Primative<short> morphColor15 = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			r0.Read(reader, this, "r0", SR1_File.Version.First, SR1_File.Version.Jan23);
			g0.Read(reader, this, "g0", SR1_File.Version.First, SR1_File.Version.Jan23);
			b0.Read(reader, this, "b0", SR1_File.Version.First, SR1_File.Version.Jan23);
			r1.Read(reader, this, "r1", SR1_File.Version.First, SR1_File.Version.Jan23);
			g1.Read(reader, this, "g1", SR1_File.Version.First, SR1_File.Version.Jan23);
			b1.Read(reader, this, "b1", SR1_File.Version.First, SR1_File.Version.Jan23);
			vindex.Read(reader, this, "vindex", SR1_File.Version.First, SR1_File.Version.Jan23);
			morphColor15.Read(reader, this, "morphColor15", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			r0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			g0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			b0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			r1.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			g1.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			b1.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			vindex.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			morphColor15.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}
	}
}
