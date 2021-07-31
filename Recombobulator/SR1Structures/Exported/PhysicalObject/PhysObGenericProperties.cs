using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObGenericProperties : SR1_Structure
    {
        PhysObProperties Properties = new PhysObProperties();
        SR1_Primative<uint> pad = new SR1_Primative<uint>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            pad.Write(writer);
        }
    }
}
