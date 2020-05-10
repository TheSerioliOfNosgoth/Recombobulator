using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class VMMoveOffset : SR1_Structure
    {
        SR1_Primative<short> dx = new SR1_Primative<short>();
        SR1_Primative<short> dy = new SR1_Primative<short>();
        SR1_Primative<short> dz = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            dx.Read(reader, this, "dx");
            dy.Read(reader, this, "dy");
            dz.Read(reader, this, "dz");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            dx.Write(writer);
            dy.Write(writer);
            dz.Write(writer);
        }
    }
}
