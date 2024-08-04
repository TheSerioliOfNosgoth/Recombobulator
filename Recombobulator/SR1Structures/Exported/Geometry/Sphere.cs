using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Sphere : SR1_Structure
	{
		public readonly Position position = new Position();
		public readonly SR1_Primative<ushort> radius = new SR1_Primative<ushort>();
		public readonly SR1_Primative<uint> radiusSquared = new SR1_Primative<uint>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			position.Read(reader, this, "position");
			radius.Read(reader, this, "radius");
			radiusSquared.Read(reader, this, "radiusSquared");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			position.Write(writer);
			radius.Write(writer);
			radiusSquared.Write(writer);
		}
	}
}
