using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ReaverWaveData : SR1_Structure
    {
        SR1_Primative<short> amplitude = new SR1_Primative<short>();
        SR1_Primative<short> frequency = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            amplitude.Read(reader, this, "amplitude");
            frequency.Read(reader, this, "frequency");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            amplitude.Write(writer);
            frequency.Write(writer);
        }
    }
}
