using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class UpdraftProperties : PhysObPropertiesBase
    {
        new public readonly PhysObProperties Properties;
        SR1_StructureArray<PhysObDraftProperties> physObDraftProperties = new SR1_StructureArray<PhysObDraftProperties>(13);

        public UpdraftProperties()
        {
            Properties = ((PhysObPropertiesBase)physObDraftProperties[0]).Properties;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            physObDraftProperties.Read(reader, this, "physObDraftProperties");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            physObDraftProperties.Write(writer);
        }
    }
}