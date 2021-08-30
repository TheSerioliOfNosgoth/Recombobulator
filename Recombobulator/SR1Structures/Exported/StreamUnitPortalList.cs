using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class StreamUnitPortalList : SR1_Structure
    {
        SR1_Primative<int> numPortals = new SR1_Primative<int>();
        SR1_StructureArray<StreamUnitPortal> portals = new SR1_StructureArray<StreamUnitPortal>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            numPortals.Read(reader, this, "numPortals");

            portals = new SR1_StructureArray<StreamUnitPortal>(numPortals.Value);
            portals.Read(reader, this, "portals");

            reader.BaseStream.Position += 4;
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            numPortals.Write(writer);
            portals.Write(writer);
            writer.Write(0x0000000u);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if ((migrateFlags & SR1_File.MigrateFlags.RemovePortals) != 0)
            {
                // 0x004ABBBA has something to do with the portals.
                numPortals.Value = 0;
            }
        }
    }
}
