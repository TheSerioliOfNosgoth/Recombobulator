using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericTune : SR1_Structure
    {
        public readonly SR1_Primative<uint> flags = new SR1_Primative<uint>().ShowAsHex(true);
        public readonly SR1_Pointer<FXSplinter> shatterData = new SR1_Pointer<FXSplinter>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            flags.Read(reader, this, "flags");
            shatterData.Read(reader, this, "shatterData");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new FXSplinter().ReadFromPointer(reader, shatterData);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            flags.Write(writer);
            shatterData.Write(writer);
        }
    }
}
