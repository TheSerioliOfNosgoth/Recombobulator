using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObProjectileProperties : PhysObPropertiesBase
    {
        SR1_Primative<int> totalProjectiles = new SR1_Primative<int>();
        SR1_Pointer<PhysObProjectileData> data = new SR1_Pointer<PhysObProjectileData>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            totalProjectiles.Read(reader, this, "totalProjectiles");
            data.Read(reader, this, "data");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new SR1_StructureArray<PhysObProjectileData>(totalProjectiles.Value).ReadFromPointer(reader, data);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            totalProjectiles.Write(writer);
            data.Write(writer);
        }
    }
}
