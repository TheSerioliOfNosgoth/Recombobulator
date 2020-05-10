using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class HSphere : SR1_Structure
    {
        SR1_Primative<int> attr = new SR1_Primative<int>();
        SR1_Primative<byte> id = new SR1_Primative<byte>();
        SR1_Primative<byte> rank = new SR1_Primative<byte>();
        SR1_Primative<short> pad = new SR1_Primative<short>();
        Position position = new Position();
        SR1_Primative<ushort> radius = new SR1_Primative<ushort>();
        SR1_Primative<uint> radiusSquared = new SR1_Primative<uint>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            attr.Read(reader, this, "attr");
            id.Read(reader, this, "id");
            rank.Read(reader, this, "rank");
            pad.Read(reader, this, "pad");
            position.Read(reader, this, "position");
            radius.Read(reader, this, "radius");
            radiusSquared.Read(reader, this, "radiusSquared");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            attr.Write(writer);
            id.Write(writer);
            rank.Write(writer);
            pad.Write(writer);
            position.Write(writer);
            radius.Write(writer);
            radiusSquared.Write(writer);
        }
    }
}
