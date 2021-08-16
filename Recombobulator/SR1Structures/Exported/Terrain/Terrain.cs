using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Terrain : SR1_Structure
    {
        // Use      //.*\r?\n     to search and remove comments.

        SR1_Primative<short> UnitChangeFlags = new SR1_Primative<short>();
        SR1_Primative<short> spad = new SR1_Primative<short>();
        SR1_Primative<int> lpad2 = new SR1_Primative<int>();
        SR1_Primative<int> numIntros = new SR1_Primative<int>();
        SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>();
        SR1_Primative<int> numVertices = new SR1_Primative<int>();
        SR1_Primative<int> numFaces = new SR1_Primative<int>();
        SR1_Primative<int> numNormals = new SR1_Primative<int>();
        SR1_Pointer<TVertex> vertexList = new SR1_Pointer<TVertex>();
        SR1_Pointer<TFace> faceList = new SR1_Pointer<TFace>();
        SR1_Pointer<Normal> normalList = new SR1_Pointer<Normal>();
        SR1_Pointer<DrMoveAniTex> aniList = new SR1_Pointer<DrMoveAniTex>();
        SR1_Primative<int> pad = new SR1_Primative<int>();
        SR1_Pointer<BSPNode> sbspRoot = new SR1_Pointer<BSPNode>();
        SR1_Pointer<StreamUnitPortalList> StreamUnits = new SR1_Pointer<StreamUnitPortalList>(); // void in sym, StreamUnitPortalList created for this tool.
        SR1_Pointer<TextureFT3> StartTextureList = new SR1_Pointer<TextureFT3>();
        SR1_Pointer<TextureFT3> EndTextureList = new SR1_Pointer<TextureFT3>();
        SR1_Pointer<SBSPLeaf> sbspStartLeaves = new SR1_Pointer<SBSPLeaf>();
        SR1_Pointer<SBSPLeaf> sbspEndLeaves = new SR1_Pointer<SBSPLeaf>();
        SR1_Pointer<MorphVertex> MorphDiffList = new SR1_Pointer<MorphVertex>();
        SR1_Pointer<MorphColor> MorphColorList = new SR1_Pointer<MorphColor>();
        SR1_Primative<int> numBSPTrees = new SR1_Primative<int>();
        SR1_Pointer<BSPTree> BSPTreeArray = new SR1_Pointer<BSPTree>();
        SR1_PrimativePointer<ushort> morphNormalIdx = new SR1_PrimativePointer<ushort>(); // Not sure if array or pointer. One short, followed by ushort[numFaces]. Alternate TFace.Normal's?
        SR1_Pointer<MultiSignal> signals = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<UnknownPCList> unknownPCList = new SR1_Pointer<UnknownPCList>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            UnitChangeFlags.Read(reader, this, "UnitChangeFlags");
            spad.Read(reader, this, "spad");
            lpad2.Read(reader, this, "lpad2");
            numIntros.Read(reader, this, "numIntros");
            introList.Read(reader, this, "introList");
            numVertices.Read(reader, this, "numVertices");
            numFaces.Read(reader, this, "numFaces");
            numNormals.Read(reader, this, "numNormals");
            vertexList.Read(reader, this, "vertexList");
            faceList.Read(reader, this, "faceList");
            normalList.Read(reader, this, "normalList");
            aniList.Read(reader, this, "aniList");
            sbspRoot.Read(reader, this, "sbspRoot", SR1_File.Version.First, SR1_File.Version.Retail);
            pad.Read(reader, this, "pad", SR1_File.Version.Retail, SR1_File.Version.Next);
            StreamUnits.Read(reader, this, "StreamUnits");
            StartTextureList.Read(reader, this, "StartTextureList");
            EndTextureList.Read(reader, this, "EndTextureList");
            sbspStartLeaves.Read(reader, this, "sbspStartLeaves", SR1_File.Version.First, SR1_File.Version.Retail);
            sbspEndLeaves.Read(reader, this, "sbspEndLeaves", SR1_File.Version.First, SR1_File.Version.Retail);
            MorphDiffList.Read(reader, this, "MorphDiffList");
            MorphColorList.Read(reader, this, "MorphColorList");
            numBSPTrees.Read(reader, this, "numBSPTrees");
            BSPTreeArray.Read(reader, this, "BSPTreeArray");
            morphNormalIdx.Read(reader, this, "morphNormalIdx");
            signals.Read(reader, this, "signals");
            unknownPCList.Read(reader, this, "unknownPCList", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            SR1_Structure temp = null;

            new SR1_StructureArray<TVertex>(numVertices.Value).ReadFromPointer(reader, vertexList);
            new SR1_StructureArray<TFace>(numFaces.Value).ReadFromPointer(reader, faceList);
            temp = new SR1_StructureArray<Normal>(numNormals.Value).SetPadding(4).ReadFromPointer(reader, normalList);

            // 2 mystery bytes after normalList. Always 0x2A and 0xCD.
            if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
            {
                reader.BaseStream.Position = temp.End;
                new SR1_Primative<ushort>().Read(reader, null, "");
            }

            new DrMoveAniTex().ReadFromPointer(reader, aniList);

            if (reader.File._Version <= SR1_File.Version.Beta)
            {
                if (sbspRoot.Offset != 0 && sbspRoot.Offset < sbspStartLeaves.Offset)
                {
                    new SR1_StructureSeries<BSPNode>((int)(sbspStartLeaves.Offset - sbspRoot.Offset)).ReadFromPointer(reader, sbspRoot);
                }
            }

            new StreamUnitPortalList().ReadFromPointer(reader, StreamUnits);
            new SR1_StructureSeries<TextureFT3>((int)(EndTextureList.Offset - StartTextureList.Offset)).ReadFromPointer(reader, StartTextureList);

            if (reader.File._Version <= SR1_File.Version.Beta)
            {
                new SR1_StructureSeries<SBSPLeaf>((int)(sbspEndLeaves.Offset - sbspStartLeaves.Offset)).ReadFromPointer(reader, sbspStartLeaves);

                if (reader.IntroListDictionary.Count > 0)
                {
                    SR1_StructureList<SR1_PointerArray<Intro>> introListSet = new SR1_StructureList<SR1_PointerArray<Intro>>();

                    foreach (KeyValuePair<uint, SR1_PointerArray <Intro>> introList in reader.IntroListDictionary)
                    {
                        introListSet.Add(introList.Value);
                    }

                    introListSet.ReadFromPointer(reader, sbspEndLeaves);
                }
            }

            new SR1_StructureSeries<MorphVertex>((int)(MorphColorList.Offset - MorphDiffList.Offset)).ReadFromPointer(reader, MorphDiffList);
            new SR1_StructureArray<MorphColor>(numVertices.Value).SetPadding(4).ReadFromPointer(reader, MorphColorList);
            new SR1_StructureArray<BSPTree>(numBSPTrees.Value).ReadFromPointer(reader, BSPTreeArray);
            new SR1_PrimativeArray<ushort>(numFaces.Value).SetPadding(4).ReadFromPointer(reader, morphNormalIdx);
            if (reader.File._Version == SR1_File.Version.Retail_PC)
            {
                new UnknownPCList().ReadFromPointer(reader, unknownPCList);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            UnitChangeFlags.Write(writer);
            spad.Write(writer);
            lpad2.Write(writer);
            numIntros.Write(writer);
            introList.Write(writer);
            numVertices.Write(writer);
            numFaces.Write(writer);
            numNormals.Write(writer);
            vertexList.Write(writer);
            faceList.Write(writer);
            normalList.Write(writer);
            aniList.Write(writer);
            sbspRoot.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail);
            pad.Write(writer, SR1_File.Version.Retail, SR1_File.Version.Next);
            StreamUnits.Write(writer);
            StartTextureList.Write(writer);
            EndTextureList.Write(writer);
            sbspStartLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail);
            sbspEndLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail);
            MorphDiffList.Write(writer);
            MorphColorList.Write(writer);
            numBSPTrees.Write(writer);
            BSPTreeArray.Write(writer);
            morphNormalIdx.Write(writer);
            signals.Write(writer);
            unknownPCList.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion)
        {
            base.MigrateVersion(file, targetVersion);

            if (file._Version == SR1_File.Version.Beta && targetVersion == SR1_File.Version.Retail_PC)
            {
                MembersRead.Insert(MembersRead.IndexOf(sbspRoot), pad);
                MembersRead.Remove(sbspRoot);
                MembersRead.Remove(sbspStartLeaves);
                MembersRead.Remove(sbspEndLeaves);
                MembersRead.Insert(MembersRead.Count, unknownPCList);

                SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
                uint position = lastStructure.End;

                unknownPCList.Offset = position;
                UnknownPCList newUnknownPCList = new UnknownPCList();
                file._Structures.Add(position, newUnknownPCList);
                file._MigrationStructures.Add(position, newUnknownPCList);

                if (sbspRoot.Offset != 0)
                {
                    file._Structures.Remove(sbspRoot.Offset);
                    sbspRoot.Offset = 0;
                }

                if (sbspStartLeaves.Offset != 0)
                {
                    file._Structures.Remove(sbspStartLeaves.Offset);
                    sbspStartLeaves.Offset = 0;
                    sbspEndLeaves.Offset = 0;
                }
            }

            if (file._Version == SR1_File.Version.Retail && targetVersion == SR1_File.Version.Retail_PC)
            {
                MembersRead.Add(unknownPCList);

                SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
                uint position = lastStructure.End;

                unknownPCList.Offset = position;
                UnknownPCList newUnknownPCList = new UnknownPCList();
                file._Structures.Add(position, newUnknownPCList);
                file._MigrationStructures.Add(position, newUnknownPCList);
            }
        }
    }
}
