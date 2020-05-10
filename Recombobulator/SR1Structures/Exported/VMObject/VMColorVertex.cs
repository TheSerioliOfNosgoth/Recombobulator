using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class VMColorVertex : VMVertex
    {
        SR1_Primative<short> tvIdx = new SR1_Primative<short>();
        SR1_Primative<short> offset = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            tvIdx.Read(reader, this, "tvIdx");
            offset.Read(reader, this, "offset");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            tvIdx.Write(writer);
            offset.Write(writer);
        }
    }
}
