using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class LitShaftProperties : SR1_Structure
    {
        SR1_Primative<short> fadeValue = new SR1_Primative<short>();
        SR1_Primative<short> pad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            fadeValue.Read(reader, this, "fadeValue");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            fadeValue.Write(writer);
            pad.Write(writer);
        }
    }
}
