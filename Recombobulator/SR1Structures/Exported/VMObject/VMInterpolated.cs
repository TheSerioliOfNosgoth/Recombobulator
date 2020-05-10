using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class VMInterpolated : SR1_Structure
    {
        SR1_Primative<short> tvIdx = new SR1_Primative<short>();
        SR1_Primative<short> startIdx = new SR1_Primative<short>();
        SR1_Primative<short> endIdx = new SR1_Primative<short>();
        SR1_Primative<short> time = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            tvIdx.Read(reader, this, "tvIdx");
            startIdx.Read(reader, this, "startIdx");
            endIdx.Read(reader, this, "endIdx");
            time.Read(reader, this, "time");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            tvIdx.Write(writer);
            startIdx.Write(writer);
            endIdx.Write(writer);
            time.Write(writer);
        }
    }
}
