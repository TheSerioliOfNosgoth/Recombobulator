using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObProjectileProperties : PhysObPropertiesBase
    {
        SR1_Primative<int> totalProjectiles = new SR1_Primative<int>();
        SR1_PointerArray<PhysObProjectileData> data = new SR1_PointerArray<PhysObProjectileData>(0, false);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            totalProjectiles.Read(reader, this, "totalProjectiles");

            data = new SR1_PointerArray<PhysObProjectileData>(totalProjectiles.Value + 1, false);
            data.Read(reader, this, "data");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            SR1_Structure temp;
            
            temp = new SR1_StructureArray<PhysObProjectileData>(totalProjectiles.Value).ReadFromPointer(reader, data[0]);

            long nextEnd = End;
            while (nextEnd != temp.Start)
            {
                if (reader.File._Structures.ContainsKey((uint)nextEnd))
                {
                    nextEnd = reader.File._Structures[(uint)nextEnd].End;
                }
                else
                {
                    reader.BaseStream.Position = nextEnd;
                    new PhysObWeaponAttributes().Read(reader, null, "");
                    nextEnd = reader.BaseStream.Position;
                }
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            totalProjectiles.Write(writer);
            data.Write(writer);
        }
    }
}
