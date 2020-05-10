using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MiniMatrix : SR1_Structure
    {
        SR1_PrimativeArray<short> m = new SR1_PrimativeArray<short>(3, 3);
        SR1_Primative<short> pad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            m.Read(reader, this, "m");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            m.Write(writer);
            pad.Write(writer);
        }
    }
}
