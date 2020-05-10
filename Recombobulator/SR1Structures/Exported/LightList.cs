using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class LightList : SR1_Structure
    {
        CVector ambient = new CVector();
        SR1_Primative<int> numLightGroups = new SR1_Primative<int>();
        SR1_Pointer<LightGroup> lightGroupList = new SR1_Pointer<LightGroup>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            ambient.Read(reader, this, "ambient");
            numLightGroups.Read(reader, this, "numLightGroups");
            lightGroupList.Read(reader, this, "lightGroupList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new SR1_StructureArray<LightGroup>(numLightGroups.Value).ReadFromPointer(reader, lightGroupList);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            ambient.Write(writer);
            numLightGroups.Write(writer);
            lightGroupList.Write(writer);
        }
    }
}
