using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class StreamUnitPortalList : SR1_Structure
    {
        SR1_Primative<int> numPortals = new SR1_Primative<int>();
        SR1_StructureArray<StreamUnitPortal> portals = new SR1_StructureArray<StreamUnitPortal>(0);
        SR1_Primative<int> pad = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            numPortals.Read(reader, this, "numPortals");
            portals = new SR1_StructureArray<StreamUnitPortal>(numPortals.Value);
            portals.Read(reader, this, "portals");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            numPortals.Write(writer);
            portals.Write(writer);
            pad.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if ((migrateFlags & SR1_File.MigrateFlags.RemovePortals) != 0)
            {
                numPortals.Value = 0;
            }
        }
    }
}
