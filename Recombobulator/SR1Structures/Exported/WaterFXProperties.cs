using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class WaterFXProperties : SR1_Structure
    {
        SR1_StructureArray<GenericBubbleParams> genericBubbleParams = new SR1_StructureArray<GenericBubbleParams>(2);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            genericBubbleParams.Read(reader, this, "genericBubbleParams");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            genericBubbleParams.Write(writer);
        }
    }
}