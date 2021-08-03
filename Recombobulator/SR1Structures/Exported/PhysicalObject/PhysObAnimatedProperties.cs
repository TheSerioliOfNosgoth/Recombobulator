using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObAnimatedProperties : PhysObPropertiesBase
    {
        SR1_Primative<ushort> flags = new SR1_Primative<ushort>();
        SR1_Primative<ushort> pad = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            flags.Read(reader, this, "flags");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            flags.Write(writer);
            pad.Write(writer);
        }
    }
}
