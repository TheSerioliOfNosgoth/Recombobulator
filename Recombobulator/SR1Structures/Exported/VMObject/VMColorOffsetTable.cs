using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class VMColorOffsetTable : VMOffsetTable
    {
        SR1_Primative<int> numVMOffsets = new SR1_Primative<int>();
        SR1_StructureArray<VMColorOffset> offsets = new SR1_StructureArray<VMColorOffset>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            numVMOffsets.Read(reader, this, "numVMOffsets");

            offsets = (SR1_StructureArray<VMColorOffset>)(new SR1_StructureArray<VMColorOffset>(numVMOffsets.Value)).SetPadding(4);
            offsets.Read(reader, this, "offsets");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            numVMOffsets.Write(writer);
            offsets.Write(writer);
        }
    }
}
