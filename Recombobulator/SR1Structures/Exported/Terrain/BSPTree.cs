using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class BSPTree : SR1_Structure
    {
        SR1_Pointer<BSPNode> bspRoot = new SR1_Pointer<BSPNode>();
        SR1_Pointer<BSPLeaf> startLeaves = new SR1_Pointer<BSPLeaf>();
        SR1_Pointer<BSPLeaf> endLeaves = new SR1_Pointer<BSPLeaf>();
        Position globalOffset = new Position();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        Position localOffset = new Position();
        SR1_Primative<short> ID = new SR1_Primative<short>();
        SR1_Primative<int> splineID = new SR1_Primative<int>();
        SR1_Pointer<Instance> instanceSpline = new SR1_Pointer<Instance>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            bspRoot.Read(reader, this, "bspRoot");
            startLeaves.Read(reader, this, "startLeaves");
            endLeaves.Read(reader, this, "endLeaves");
            globalOffset.Read(reader, this, "globalOffset");
            flags.Read(reader, this, "flags");
            localOffset.Read(reader, this, "localOffset");
            ID.Read(reader, this, "ID");
            splineID.Read(reader, this, "splineID");
            instanceSpline.Read(reader, this, "instanceSpline");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            if ((int)(startLeaves.Offset - bspRoot.Offset) > 0)
            {
                new SR1_StructureSeries<BSPNode>((int)(startLeaves.Offset - bspRoot.Offset)).ReadFromPointer(reader, bspRoot);
            }

            if ((int)(endLeaves.Offset - startLeaves.Offset) > 0)
            {
                new SR1_StructureSeries<BSPLeaf>((int)(endLeaves.Offset - startLeaves.Offset)).ReadFromPointer(reader, startLeaves);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            bspRoot.Write(writer);
            startLeaves.Write(writer);
            endLeaves.Write(writer);
            globalOffset.Write(writer);
            flags.Write(writer);
            localOffset.Write(writer);
            ID.Write(writer);
            splineID.Write(writer);
            instanceSpline.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion)
        {
            base.MigrateVersion(file, targetVersion);

            if ((file._Version == SR1_File.Version.Retail || file._Version == SR1_File.Version.Beta) &&
                targetVersion == SR1_File.Version.Retail_PC)
            {
                // Burn in sunlight.
                // The 0x0040 seems right, but not sure about the 0x0002.
                if ((flags.Value & 0x0002) != 0)
                {
                    flags.Value |= 0x0040;
                }
            }
        }
    }
}
