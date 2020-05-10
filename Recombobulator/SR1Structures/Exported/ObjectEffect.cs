using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ObjectEffect : SR1_Structure
    {
        SR1_Primative<byte> effectNumber = new SR1_Primative<byte>();
        SR1_PrimativeArray<byte> modifierList = new SR1_PrimativeArray<byte>(3);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            effectNumber.Read(reader, this, "effectNumber");
            modifierList.Read(reader, this, "modifierList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            effectNumber.Write(writer);
            modifierList.Write(writer);
        }
    }
}
