using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Terrain : SR1_Structure
	{
		// Use      //.*\r?\n     to search and remove comments.

		SR1_Pointer<BSPNode> bspRoot = new SR1_Pointer<BSPNode>();
		SR1_Pointer<BSPLeaf> startLeaves = new SR1_Pointer<BSPLeaf>();
		SR1_Primative<int> numNodes = new SR1_Primative<int>();
		SR1_Primative<short> UnitChangeFlags = new SR1_Primative<short>();
		SR1_Primative<short> spad = new SR1_Primative<short>();
		SR1_Primative<int> lpad2 = new SR1_Primative<int>();
		SR1_Primative<int> vplLength = new SR1_Primative<int>();
		SR1_PrimativePointer<byte> vpList = new SR1_PrimativePointer<byte>();
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
		public SR1_Pointer<StreamUnitPortalList> StreamUnits = new SR1_Pointer<StreamUnitPortalList>(); // void in sym, StreamUnitPortalList created for this tool.
		SR1_Pointer<BSPLeaf> endLeaves = new SR1_Pointer<BSPLeaf>();
		SR1_Pointer<TextureFT3> StartTextureList = new SR1_Pointer<TextureFT3>();
		SR1_Pointer<TextureFT3> EndTextureList = new SR1_Pointer<TextureFT3>();
		SR1_Pointer<SBSPLeaf> sbspStartLeaves = new SR1_Pointer<SBSPLeaf>();
		SR1_Pointer<SBSPLeaf> sbspEndLeaves = new SR1_Pointer<SBSPLeaf>();
		SR1_Pointer<MorphVertex> MorphDiffList = new SR1_Pointer<MorphVertex>();
		SR1_Pointer<MorphColor> MorphColorList = new SR1_Pointer<MorphColor>();
		SR1_Primative<int> numBSPTrees = new SR1_Primative<int>();
		SR1_Pointer<BSPTree> BSPTreeArray = new SR1_Pointer<BSPTree>();
		SR1_PrimativePointer<ushort> morphNormalIdx = new SR1_PrimativePointer<ushort>(); // Not sure if array or pointer. One short, followed by ushort[numFaces]. Alternate TFace.Normal's?
		public SR1_Pointer<MultiSignal> signals = new SR1_Pointer<MultiSignal>();
		SR1_Pointer<TexAniAssocData> texAniAssocData = new SR1_Pointer<TexAniAssocData>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			bspRoot.Read(reader, this, "bspRoot", SR1_File.Version.First, SR1_File.Version.Jan23);
			startLeaves.Read(reader, this, "startLeaves", SR1_File.Version.First, SR1_File.Version.Jan23);
			numNodes.Read(reader, this, "numNodes", SR1_File.Version.First, SR1_File.Version.Jan23);
			vplLength.Read(reader, this, "vplLength", SR1_File.Version.First, SR1_File.Version.Apr14);
			vpList.Read(reader, this, "vpList", SR1_File.Version.First, SR1_File.Version.Apr14);
			UnitChangeFlags.Read(reader, this, "UnitChangeFlags", SR1_File.Version.Apr14, SR1_File.Version.Next);
			spad.Read(reader, this, "spad", SR1_File.Version.Apr14, SR1_File.Version.Next);
			lpad2.Read(reader, this, "lpad2", SR1_File.Version.Apr14, SR1_File.Version.Next);
			numIntros.Read(reader, this, "numIntros");
			introList.Read(reader, this, "introList");
			numVertices.Read(reader, this, "numVertices");
			numFaces.Read(reader, this, "numFaces");
			numNormals.Read(reader, this, "numNormals");
			vertexList.Read(reader, this, "vertexList");
			faceList.Read(reader, this, "faceList");
			normalList.Read(reader, this, "normalList");
			aniList.Read(reader, this, "aniList");
			sbspRoot.Read(reader, this, "sbspRoot", SR1_File.Version.First, SR1_File.Version.Jun01);
			pad.Read(reader, this, "pad", SR1_File.Version.Jun01, SR1_File.Version.Next);
			StreamUnits.Read(reader, this, "StreamUnits");
			endLeaves.Read(reader, this, "endLeaves", SR1_File.Version.First, SR1_File.Version.Jan23);
			StartTextureList.Read(reader, this, "StartTextureList");
			EndTextureList.Read(reader, this, "EndTextureList");
			EndTextureList.PointsToEndOfStruct = true;
			sbspStartLeaves.Read(reader, this, "sbspStartLeaves", SR1_File.Version.First, SR1_File.Version.Jun01);
			sbspEndLeaves.Read(reader, this, "sbspEndLeaves", SR1_File.Version.First, SR1_File.Version.Jun01);
			MorphDiffList.Read(reader, this, "MorphDiffList");
			MorphColorList.Read(reader, this, "MorphColorList");
			numBSPTrees.Read(reader, this, "numBSPTrees", SR1_File.Version.Jan23, SR1_File.Version.Next);
			BSPTreeArray.Read(reader, this, "BSPTreeArray", SR1_File.Version.Jan23, SR1_File.Version.Next);
			morphNormalIdx.Read(reader, this, "morphNormalIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			signals.Read(reader, this, "signals", SR1_File.Version.Jan23, SR1_File.Version.Next);
			texAniAssocData.Read(reader, this, "texAniAssocData", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			SR1_Structure temp = null;

			if (numVertices.Value > 0)
			{
				new SR1_StructureArray<TVertex>(numVertices.Value).ReadFromPointer(reader, vertexList);
			}

			SR1_StructureArray<TFace> faces = new SR1_StructureArray<TFace>(numFaces.Value);
			if (numFaces.Value > 0)
			{
				faces.ReadFromPointer(reader, faceList);
			}

			if (numNormals.Value > 0)
			{
				temp = new SR1_StructureArray<Normal>(numNormals.Value).SetPadding(4).ReadFromPointer(reader, normalList);

				// 2 mystery bytes after normalList. Always 0x2A and 0xCD.
				if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
				{
					reader.BaseStream.Position = temp.End;
					new SR1_Primative<ushort>().Read(reader, null, "");

					if (numFaces.Value <= 0)
					{
						new SR1_Primative<ushort>().Read(reader, null, "");
					}
				}
			}

			DrMoveAniTex drMoveAniTex = new DrMoveAniTex();
			drMoveAniTex.ReadFromPointer(reader, aniList);

			if (reader.File._Version <= SR1_File.Version.May12)
			{
				if (sbspRoot.Offset != 0 && sbspRoot.Offset < sbspStartLeaves.Offset)
				{
					new SR1_StructureSeries<BSPNode>((int)(sbspStartLeaves.Offset - sbspRoot.Offset)).ReadFromPointer(reader, sbspRoot);
				}
			}

			StreamUnitPortalList portalList = new StreamUnitPortalList();
			portalList.ReadFromPointer(reader, StreamUnits);

			SR1_StructureSeries<TextureFT3> textures = new SR1_StructureSeries<TextureFT3>((int)(EndTextureList.Offset - StartTextureList.Offset));
			textures.ReadFromPointer(reader, StartTextureList);

			if (reader.File._Version <= SR1_File.Version.May12)
			{
				new SR1_StructureSeries<SBSPLeaf>((int)(sbspEndLeaves.Offset - sbspStartLeaves.Offset)).ReadFromPointer(reader, sbspStartLeaves);

				if (reader.IntroListDictionary.Count > 0)
				{
					SR1_StructureList<SR1_PointerArray<Intro>> introListSet = new SR1_StructureList<SR1_PointerArray<Intro>>();

					foreach (KeyValuePair<uint, SR1_PointerArray<Intro>> introList in reader.IntroListDictionary)
					{
						introListSet.Add(introList.Value);
					}

					introListSet.ReadFromPointer(reader, sbspEndLeaves);
				}
			}

			new SR1_StructureSeries<MorphVertex>((int)(MorphColorList.Offset - MorphDiffList.Offset)).ReadFromPointer(reader, MorphDiffList);

			int morphColorPadding = (reader.File._Version >= SR1_File.Version.Apr14) ? 4 : 2;
			new SR1_StructureArray<MorphColor>(numVertices.Value).SetPadding(morphColorPadding).ReadFromPointer(reader, MorphColorList);

			if (reader.File._Version < SR1_File.Version.Jan23)
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
			else
			{
				SR1_StructureArray<BSPTree> bspTrees = new SR1_StructureArray<BSPTree>(numBSPTrees.Value);
				bspTrees.ReadFromPointer(reader, BSPTreeArray);

				if (bspTrees.Count > 0 && faces.Count > 0)
				{
					BSPTree tree = (BSPTree)bspTrees[numBSPTrees.Value - 1];

					if (tree.ID.Value == -1 && tree.startLeaves.Offset != 0 &&
						reader.File._Structures.ContainsKey(tree.startLeaves.Offset) &&
						reader.Level.SignalListStart.Offset != 0 &&
						reader.File._Structures.ContainsKey(reader.Level.SignalListStart.Offset))
					{
						SR1_StructureSeries<BSPLeaf> leaves =
							(SR1_StructureSeries<BSPLeaf>)reader.File._Structures[tree.startLeaves.Offset];
						SR1_StructureSeries<MultiSignal> multiSignals =
							(SR1_StructureSeries<MultiSignal>)reader.File._Structures[reader.Level.SignalListStart.Offset];

						foreach (BSPLeaf leaf in leaves)
						{
							uint faceIndex = (leaf.faceList.Offset - faces.Start) / 12;
							short numFaces = leaf.numFaces.Value;
							for (short f = 0; f < numFaces; f++)
							{
								TFace tFace = (TFace)faces[(int)faceIndex + f];
								tFace.IsInSignalGroup = true;

								foreach (MultiSignal mSignal in multiSignals)
								{
									if (mSignal.Start == (signals.Offset + tFace.textoff.Value))
									{
										tFace.MultiSignal = mSignal;
										if (mSignal.numSignals.Value > 0)
										{
											tFace.Signal = (Signal)mSignal.signalList[0];
										}
										break;
									}
								}

								if (tFace.MultiSignal != null)
								{
									foreach (StreamUnitPortal portal in portalList.portals)
									{
										if (portal.MSignalID.Value == tFace.MultiSignal.signalNum.Value)
										{
											tFace.Portal = portal;
											break;
										}
									}
								}
							}
						}
					}
				}
			}

			foreach (TFace face in faces)
			{
				if (!face.IsInSignalGroup)
				{
					int textureSize = (reader.File._Version >= SR1_File.Version.Apr14) ? 12 : 16;
					int textureIndex = face.textoff.Value / textureSize;
					if (textureIndex < textures.Count)
					{
						face.Texture = (TextureFT3)textures[textureIndex];
						face.Texture.NumReferences++;
						if ((face.attr.Value & 0x08) != 0)
						{
							face.Texture.HasTranslucentPolygon = true;
						}
					}
				}
			}

			new SR1_PrimativeArray<ushort>(numFaces.Value).SetPadding(4).ReadFromPointer(reader, morphNormalIdx);

			for (int t = 0; t < textures.Count; t++)
			{
				TextureFT3 texture = (TextureFT3)textures[t];
				int aniTexIndex = drMoveAniTex.GetAnimatedTextureIndex(reader.File, texture);
				if (aniTexIndex >= 0)
				{
					texture.AniTexIndex = aniTexIndex;
				}
			}

			if (reader.File._Version == SR1_File.Version.Retail_PC)
			{
				new TexAniAssocData().ReadFromPointer(reader, texAniAssocData);
			}

			if (reader.Level.Name == "cathy28")
			{
				reader.BaseStream.Position = BSPTreeArray.Offset - 1;
				bool found = false;
				while (true)
				{
					if (reader.File._Structures.ContainsKey((uint)reader.BaseStream.Position))
					{
						SR1_Structure structure = reader.File._Structures[(uint)reader.BaseStream.Position];
						if (structure.GetType() == typeof(SR1_StructureSeries<BSPNode>))
						{
							found = true;
						}
						else if (structure.GetType() != typeof(SR1_StructureSeries<BSPLeaf>))
						{
							break;
						}

						if (found)
						{
							reader.BaseStream.Position = structure.End;
							continue;
						}
					}

					if (found)
					{
						new SR1_StructureArray<BSPLeaf>(1).Read(reader, null, "");
						break;
					}

					reader.BaseStream.Position--;
				}
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			bspRoot.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			startLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			numNodes.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			vplLength.Write(writer, SR1_File.Version.First, SR1_File.Version.Apr14);
			vpList.Write(writer, SR1_File.Version.First, SR1_File.Version.Apr14);
			UnitChangeFlags.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			spad.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			lpad2.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			numIntros.Write(writer);
			introList.Write(writer);
			numVertices.Write(writer);
			numFaces.Write(writer);
			numNormals.Write(writer);
			vertexList.Write(writer);
			faceList.Write(writer);
			normalList.Write(writer);
			aniList.Write(writer);
			sbspRoot.Write(writer, SR1_File.Version.First, SR1_File.Version.Jun01);
			pad.Write(writer, SR1_File.Version.Jun01, SR1_File.Version.Next);
			StreamUnits.Write(writer);
			endLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			StartTextureList.Write(writer);
			EndTextureList.Write(writer);
			sbspStartLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Jun01);
			sbspEndLeaves.Write(writer, SR1_File.Version.First, SR1_File.Version.Jun01);
			MorphDiffList.Write(writer);
			MorphColorList.Write(writer);
			numBSPTrees.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			BSPTreeArray.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			morphNormalIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			signals.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			texAniAssocData.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version <= SR1_File.Version.May12 && targetVersion >= SR1_File.Version.Retail_PC)
			{
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

			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Jan23)
			{
				SR1_StructureArray<BSPTree> newBSPTrees = new SR1_StructureArray<BSPTree>(1);
				BSPTree newBSPTree = (BSPTree)newBSPTrees[0];

				// Set them to the existing root and leaves.
				newBSPTree.bspRoot.Offset = bspRoot.Offset;
				newBSPTree.startLeaves.Offset = startLeaves.Offset;
				newBSPTree.endLeaves.Offset = endLeaves.Offset;

				// Insert the BSPTree array where the root used to be.
				file._MigrationStructures.Add(bspRoot.Start, newBSPTrees);

				numBSPTrees.Value = 1;
				BSPTreeArray.Offset = bspRoot.Start;
				BSPTreeArray.PointsToMigStruct = true;
			}

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				if ((migrateFlags & SR1_File.MigrateFlags.RemoveAnimatedTextures) == 0 &&
					aniList.Offset != 0)
				{
					List<int> entryList = new List<int>();

					SR1_StructureSeries<TextureFT3> textures =
						(SR1_StructureSeries<TextureFT3>)file._Structures[StartTextureList.Offset];
					DrMoveAniTex drMoveAniTex = (DrMoveAniTex)file._Structures[aniList.Offset];

					if (textures != null && drMoveAniTex != null)
					{
						int numAniTextures = drMoveAniTex.numAniTextures.Value;
						int numTextures = textures.Count;

						for (int a = 0; a < numAniTextures; a++)
						{
							int refCountIndex = entryList.Count;
							int refCount = 0;
							for (int t = 0; t < numTextures; t++)
							{
								TextureFT3 texture = (TextureFT3)textures[t];
								if (texture.AniTexIndex == a)
								{
									if (refCount == 0)
									{
										entryList.Add(0);
									}

									refCount++;

									entryList.Add(t);
								}
							}

							entryList[refCountIndex] = refCount;
						}
					}

					SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
					uint position = lastStructure.End;

					file._MigrationStructures.Add(position, new TexAniAssocData(entryList));
					texAniAssocData.Offset = position;
					texAniAssocData.PointsToMigStruct = true;
				}
				else
				{
					if (aniList.Offset != 0)
					{
						file._Structures.Remove(aniList.Offset);
						aniList.Offset = 0;
					}

					SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
					uint position = lastStructure.End;

					file._MigrationStructures.Add(position, new TexAniAssocData());
					texAniAssocData.Offset = position;
					texAniAssocData.PointsToMigStruct = true;
				}
			}
		}
	}
}
