using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Sphere_noSq : SR1_Structure
	{
		Position position = new Position();
		SR1_Primative<ushort> radius = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			position.Read(reader, this, "position");
			radius.Read(reader, this, "radius");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			position.Write(writer);
			radius.Write(writer);
		}
	}
}
