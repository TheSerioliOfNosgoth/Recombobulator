using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SpotLight : SR1_Structure
	{
		NodeType node = new NodeType();
		SR1_Primative<byte> r = new SR1_Primative<byte>();
		SR1_Primative<byte> g = new SR1_Primative<byte>();
		SR1_Primative<byte> b = new SR1_Primative<byte>();
		SR1_Primative<byte> flags = new SR1_Primative<byte>();
		Position centroid = new Position();
		SR1_Primative<short> radius = new SR1_Primative<short>();
		SR1_Primative<int> radiusSquared = new SR1_Primative<int>();
		Position position = new Position();
		Position direction = new Position();
		SR1_Primative<short> cosFalloffAngle = new SR1_Primative<short>();
		SR1_Primative<short> attenuationScale = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			node.Read(reader, this, "node");
			r.Read(reader, this, "r");
			g.Read(reader, this, "g");
			b.Read(reader, this, "b");
			flags.Read(reader, this, "flags");
			centroid.Read(reader, this, "centroid");
			radius.Read(reader, this, "radius");
			radiusSquared.Read(reader, this, "radiusSquared");
			position.Read(reader, this, "position");
			direction.Read(reader, this, "direction");
			cosFalloffAngle.Read(reader, this, "cosFalloffAngle");
			attenuationScale.Read(reader, this, "attenuationScale");
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
			centroid.Write(writer);
			radius.Write(writer);
			radiusSquared.Write(writer);
			position.Write(writer);
			direction.Write(writer);
			cosFalloffAngle.Write(writer);
			attenuationScale.Write(writer);
		}
	}
}
