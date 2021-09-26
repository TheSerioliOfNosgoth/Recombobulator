using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class StreamUnitPortal : SR1_Structure
    {
        SR1_PrimativeArray<char> tolevelname = new SR1_PrimativeArray<char>(16);
        SR1_Primative<int> MSignalID = new SR1_Primative<int>();
        SR1_Primative<int> streamID = new SR1_Primative<int>();
        SR1_Primative<short> minx = new SR1_Primative<short>();
        SR1_Primative<short> miny = new SR1_Primative<short>();
        SR1_Primative<short> minz = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_Primative<short> maxx = new SR1_Primative<short>();
        SR1_Primative<short> maxy = new SR1_Primative<short>();
        SR1_Primative<short> maxz = new SR1_Primative<short>();
        SR1_Primative<short> pad2 = new SR1_Primative<short>();
        SR1_Pointer<StreamUnit> toStreamUnit = new SR1_Pointer<StreamUnit>();
	    SR1_StructureArray<SVector> t1 = new SR1_StructureArray<SVector>(3);
        SR1_StructureArray<SVector> t2 = new SR1_StructureArray<SVector>(3);

        public bool OmitFromMigration { get; private set; } = false;

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            tolevelname.Read(reader, this, "tolevelname");
            MSignalID.Read(reader, this, "MSignalID");
            streamID.Read(reader, this, "streamID");
            minx.Read(reader, this, "minx");
            miny.Read(reader, this, "miny");
            minz.Read(reader, this, "minz");
            flags.Read(reader, this, "flags");
            maxx.Read(reader, this, "maxx");
            maxy.Read(reader, this, "maxy");
            maxz.Read(reader, this, "maxz");
            pad2.Read(reader, this, "pad2");
            toStreamUnit.Read(reader, this, "toStreamUnit");
            t1.Read(reader, this, "t1");
            t2.Read(reader, this, "t2");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            tolevelname.Write(writer);
            MSignalID.Write(writer);
            streamID.Write(writer);
            minx.Write(writer);
            miny.Write(writer);
            minz.Write(writer);
            flags.Write(writer);
            maxx.Write(writer);
            maxy.Write(writer);
            maxz.Write(writer);
            pad2.Write(writer);
            toStreamUnit.Write(writer);
            t1.Write(writer);
            t2.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if ((migrateFlags & SR1_File.MigrateFlags.RemovePortals) != 0)
            {
                OmitFromMigration = true;
            }
        }
    }
}
