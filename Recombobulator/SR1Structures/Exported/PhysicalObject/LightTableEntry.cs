using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class LightTableEntry : SR1_Structure
    {
        SR1_Primative<short> r = new SR1_Primative<short>();
        SR1_Primative<short> g = new SR1_Primative<short>();
        SR1_Primative<short> b = new SR1_Primative<short>();
        SR1_Primative<short> radius = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            r.Read(reader, this, "r");
            g.Read(reader, this, "g");
            b.Read(reader, this, "b");
            radius.Read(reader, this, "radius");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            r.Write(writer);
            g.Write(writer);
            b.Write(writer);
            radius.Write(writer);
        }
    }
}