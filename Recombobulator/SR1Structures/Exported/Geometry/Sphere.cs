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

		public static void Copy(Sphere to, Sphere from)
		{
			to.position.x.Value = from.position.x.Value;
			to.position.y.Value = from.position.y.Value;
			to.position.z.Value = from.position.z.Value;
			to.radius.Value = from.radius.Value;
			to.radiusSquared.Value = from.radiusSquared.Value;
		}

		public static void Copy(Sphere_noSq to, Sphere from)
		{
			to.position.x.Value = from.position.x.Value;
			to.position.y.Value = from.position.y.Value;
			to.position.z.Value = from.position.z.Value;
			to.radius.Value = from.radius.Value;
		}
	}
}

