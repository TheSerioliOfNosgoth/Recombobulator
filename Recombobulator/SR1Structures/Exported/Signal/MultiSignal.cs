using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MultiSignal : SR1_Structure
    {
        SR1_Primative<int> numSignals = new SR1_Primative<int>();
        SR1_Primative<short> signalNum = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_StructureArray<Signal> signalList = new SR1_StructureArray<Signal>(0);
        SR1_Primative<int> pad = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            numSignals.Read(reader, this, "numSignals");
            signalNum.Read(reader, this, "signalNum");
            flags.Read(reader, this, "flags");

            signalList = new SR1_StructureArray<Signal>(numSignals.Value);
            signalList.Read(reader, this, "signalList");

            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            numSignals.Write(writer);
            signalNum.Write(writer);
            flags.Write(writer);
            signalList.Write(writer);
            pad.Write(writer);
        }
    }
}
