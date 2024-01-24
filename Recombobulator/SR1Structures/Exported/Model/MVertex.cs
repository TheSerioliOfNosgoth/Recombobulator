using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class MVertex : SR1_Structure
	{
		Vertex vertex = new Vertex();
		SR1_Primative<ushort> normal = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			vertex.Read(reader, this, "vertex");
			normal.Read(reader, this, "normal");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			vertex.Write(writer);
			normal.Write(writer);
		}
	}
}
