﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Terrain : SR1_Structure
	{
		// Use      //.*\r?\n     to search and remove comments.

		SR1_Pointer<BSPNode> bspRoot = new SR1_Pointer<BSPNode>(PtrHeuristic.Start);
		SR1_Pointer<BSPLeaf> startLeaves = new SR1_Pointer<BSPLeaf>(PtrHeuristic.Start);
		SR1_Primative<int> numNodes = new SR1_Primative<int>();
		SR1_Primative<int> vplLength = new SR1_Primative<int>();
		SR1_PrimativePointer<byte> vpList = new SR1_PrimativePointer<byte>();
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
		SR1_Pointer<BSPNode> sbspRoot = new SR1_Pointer<BSPNode>(PtrHeuristic.Start);
		SR1_Primative<int> pad = new SR1_Primative<int>();
		public SR1_Pointer<StreamUnitPortalList> StreamUnits = new SR1_Pointer<StreamUnitPortalList>(); // void in sym, StreamUnitPortalList created for this tool.
		SR1_Pointer<BSPLeaf> endLeaves = new SR1_Pointer<BSPLeaf>(PtrHeuristic.End);
		SR1_Pointer<TextureFT3> StartTextureList = new SR1_Pointer<TextureFT3>(PtrHeuristic.Start);
		SR1_Pointer<TextureFT3> EndTextureList = new SR1_Pointer<TextureFT3>(PtrHeuristic.End);
		SR1_Pointer<SBSPLeaf> sbspStartLeaves = new SR1_Pointer<SBSPLeaf>(PtrHeuristic.Start);
		SR1_Pointer<SBSPLeaf> sbspEndLeaves = new SR1_Pointer<SBSPLeaf>(PtrHeuristic.End);
		SR1_Pointer<MorphVertex> MorphDiffList = new SR1_Pointer<MorphVertex>();
		SR1_Pointer<MorphColor> MorphColorList = new SR1_Pointer<MorphColor>();
		SR1_Primative<int> numBSPTrees = new SR1_Primative<int>();
		SR1_Pointer<BSPTree> BSPTreeArray = new SR1_Pointer<BSPTree>();
		SR1_PrimativePointer<ushort> morphNormalIdx = new SR1_PrimativePointer<ushort>(); // Not sure if array or pointer. One short, followed by ushort[numFaces]. Alternate TFace.Normal's?
		public SR1_Pointer<MultiSignal> signals = new SR1_Pointer<MultiSignal>();
		SR1_Pointer<TexAniAssocData> texAniAssocData = new SR1_Pointer<TexAniAssocData>();

		SR1_StructureArray<TVertex> _vertices = null;
		SR1_StructureArray<TFace> _faces = null;
		SR1_StructureArray<Normal> _normals = null;
		DrMoveAniTex _drMoveAniTex = null;
		StreamUnitPortalList _portalList = null;
		SR1_StructureSeries<TextureFT3> _textures = null;
		SR1_StructureSeries<MorphVertex> _morphVertices = null;
		SR1_StructureArray<MorphColor> _morphColors = null;
		SR1_PrimativeArray<ushort> _morphNormals = null;

		// Used to remember where to insert the morph normals, on migrating from Proto1
		// because of weird padding at the end of them.
		uint NormalListEnd = 0;

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
			#region Geometry

			_vertices = new SR1_StructureArray<TVertex>(numVertices.Value);
			if (numVertices.Value > 0)
			{
				_vertices.ReadFromPointer(reader, vertexList);
			}

			_faces = new SR1_StructureArray<TFace>(numFaces.Value);
			if (numFaces.Value > 0)
			{
				_faces.ReadFromPointer(reader, faceList);
			}

			_normals = new SR1_StructureArray<Normal>(numNormals.Value);
			if (numNormals.Value > 0)
			{
				_normals.SetPadding(4);
				_normals.ReadFromPointer(reader, normalList);

				// Mystery byte after normalList. Always 0x2A.
				// This is *not* alignment.
				if (_normals.End != 0 && !reader.File._Structures.ContainsKey(_normals.End))
				{
					reader.BaseStream.Position = _normals.End;
					new SR1_Primative<byte>().ShowAsHex(true).Read(reader, null, "");
				}

				// Used to remember where to insert the morph normals, on migrating from Proto1
				// because of weird padding at the end of them.
				NormalListEnd = (uint)reader.BaseStream.Position;
			}

			#endregion

			_drMoveAniTex = new DrMoveAniTex();
			_drMoveAniTex.ReadFromPointer(reader, aniList);

			if (reader.File._Version <= SR1_File.Version.May12)
			{
				if (sbspRoot.Offset != 0 && sbspRoot.Offset < sbspStartLeaves.Offset)
				{
					new SR1_StructureSeries<BSPNode>((int)(sbspStartLeaves.Offset - sbspRoot.Offset)).ReadFromPointer(reader, sbspRoot);
				}
			}

			_portalList = new StreamUnitPortalList();
			_portalList.ReadFromPointer(reader, StreamUnits);

			_textures = new SR1_StructureSeries<TextureFT3>((int)(EndTextureList.Offset - StartTextureList.Offset));
			_textures.ReadFromPointer(reader, StartTextureList);

			#region Morphs

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

			_morphVertices = new SR1_StructureSeries<MorphVertex>((int)(MorphColorList.Offset - MorphDiffList.Offset));
			_morphVertices.ReadFromPointer(reader, MorphDiffList);

			if (reader.File._Version >= SR1_File.Version.Jan23)
			{
				_morphColors = new SR1_StructureArray<MorphColor>(numVertices.Value);

				if (numVertices.Value > 0)
				{
					int morphColorPadding = (reader.File._Version >= SR1_File.Version.Apr14) ? 4 : 2;
					_morphColors.SetPadding(morphColorPadding);
					_morphColors.ReadFromPointer(reader, MorphColorList);
				}
			}
			else if (MorphColorList.Offset != 0)
			{
				int numMorphColors = 0;
				MorphColor morphColor = new MorphColor();

				reader.BaseStream.Position = MorphColorList.Offset;

				do
				{
					morphColor.ReadTemp(reader);
					numMorphColors++;
				}
				while (morphColor.vindex.Value != -1);

				_morphColors = new SR1_StructureArray<MorphColor>(numMorphColors);

				if (numMorphColors > 0)
				{
					_morphColors.ReadFromPointer(reader, MorphColorList);
				}
			}
			else
			{
				_morphColors = new SR1_StructureArray<MorphColor>(0);
			}

			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				_morphNormals = new SR1_PrimativeArray<ushort>(0);
			}
			else
			{
				_morphNormals = new SR1_PrimativeArray<ushort>(numFaces.Value);

				if (numFaces.Value > 0)
				{
					_morphNormals.SetPadding(4);
					_morphNormals.Align = 2;
					_morphNormals.ReadFromPointer(reader, morphNormalIdx);
				}
			}

			#endregion

			#region BSPTrees

			uint signalLeavesOffset = 0;

			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				if ((int)(startLeaves.Offset - bspRoot.Offset) > 0)
				{
					var bspNodes = new SR1_StructureSeries<BSPNode>((int)(startLeaves.Offset - bspRoot.Offset));
					bspNodes.ReadFromPointer(reader, bspRoot);
					bspNodes.Align = 4;
				}

				if ((int)(endLeaves.Offset - startLeaves.Offset) > 0)
				{
					SR1_StructureSeries<BSPLeaf> leaves = new SR1_StructureSeries<BSPLeaf>((int)(endLeaves.Offset - startLeaves.Offset));
					leaves.ReadFromPointer(reader, startLeaves);
				}

				signalLeavesOffset = startLeaves.Offset;
			}
			else
			{
				SR1_StructureArray<BSPTree> bspTrees = new SR1_StructureArray<BSPTree>(numBSPTrees.Value);
				bspTrees.ReadFromPointer(reader, BSPTreeArray);

				if (bspTrees.Count > 0)
				{
					BSPTree tree = (BSPTree)bspTrees[numBSPTrees.Value - 1];
					if (tree.ID.Value == -1)
					{
						signalLeavesOffset = tree.startLeaves.Offset;
					}
				}
			}

			#endregion

			#region MapFaces

			if (numFaces.Value > 0)
			{
				if (signalLeavesOffset != 0)
				{
					MapSignalFaces(
						reader,
						faceList.Offset,
						signalLeavesOffset,
						reader.Level.SignalListStart.Offset,
						StreamUnits.Offset
					);
				}

				if (_textures.Count > 0)
				{
					MapTextureFaces(
						reader,
						faceList.Offset,
						StartTextureList.Offset
					);
				}
			}

			#endregion

			for (int t = 0; t < _textures.Count; t++)
			{
				TextureFT3 texture = (TextureFT3)_textures[t];
				int aniTexIndex = _drMoveAniTex.GetAnimatedTextureIndex(reader.File, texture);
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

		private void MapSignalFaces(
			SR1_Reader reader,
			uint facesOffset, uint leavesOffset,
			uint signalsOffset, uint streamUnitsOffset)
		{
			if (facesOffset == 0 || !reader.File._Structures.ContainsKey(facesOffset) ||
				leavesOffset == 0 || !reader.File._Structures.ContainsKey(leavesOffset) ||
				signalsOffset == 0 || !reader.File._Structures.ContainsKey(signalsOffset))
			{
				return;
			}

			var faces = (SR1_StructureArray<TFace>)reader.File._Structures[facesOffset];
			var multiSignals = (SR1_StructureSeries<MultiSignal>)reader.File._Structures[signalsOffset];

			SR1_Structure nodesOrLeaves = reader.File._Structures[leavesOffset];
			if (nodesOrLeaves.GetType() != typeof(SR1_StructureSeries<BSPLeaf>))
			{
				return;
			}

			var leaves = (SR1_StructureSeries<BSPLeaf>)nodesOrLeaves;

			foreach (BSPLeaf leaf in leaves)
			{
				int faceSize = (reader.File._Version >= SR1_File.Version.Jan23) ? 12 : 16;
				int faceIndex = (int)(leaf.faceList.Offset - faces.Start) / faceSize;
				short numFaces = leaf.numFaces.Value;

				for (short f = 0; f < numFaces; f++)
				{
					TFace tFace = (TFace)faces[faceIndex + f];

					tFace.IsInSignalGroup = true;
					if (reader.File._Version < SR1_File.Version.Jan23)
					{
						tFace.IsInSignalGroup = ((tFace.attr0.Value & 0x4000) != 0);
					}

					foreach (MultiSignal mSignal in multiSignals)
					{
						uint signalOffset = tFace.texture.Offset;
						if (reader.File._Version >= SR1_File.Version.Jan23)
						{
							signalOffset = signals.Offset + tFace.textoff.Value;
						}

						if (mSignal.Start == signalOffset)
						{
							tFace.MultiSignal = mSignal;
							if (mSignal.numSignals.Value > 0)
							{
								tFace.Signal = (Signal)mSignal.signalList[0];
							}
							break;
						}
					}

					if (tFace.MultiSignal != null && streamUnitsOffset != 0 &&
						reader.File._Structures.ContainsKey(streamUnitsOffset))
					{
						var streamUnits = (StreamUnitPortalList)reader.File._Structures[streamUnitsOffset];

						foreach (StreamUnitPortal portal in streamUnits.portals)
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

		private void MapTextureFaces(
			SR1_Reader reader,
			uint facesOffset,
			uint texturesOffset)
		{
			if (facesOffset == 0 || !reader.File._Structures.ContainsKey(facesOffset) ||
				texturesOffset == 0 || !reader.File._Structures.ContainsKey(texturesOffset))
			{
				return;
			}

			var faces = (SR1_StructureArray<TFace>)reader.File._Structures[facesOffset];
			var textures = (SR1_StructureSeries<TextureFT3>)reader.File._Structures[texturesOffset];

			foreach (TFace tFace in faces)
			{
				if (!tFace.IsInSignalGroup)
				{
					int textureOffset = tFace.textoff.Value;
					if (reader.File._Version < SR1_File.Version.Jan23)
					{
						textureOffset = (int)(tFace.texture.Offset - textures.Start);
					}

					int textureSize = (reader.File._Version >= SR1_File.Version.Apr14) ? 12 : 16;
					int textureIndex = textureOffset / textureSize;

					if (textureIndex >= 0 && textureIndex < textures.Count)
					{
						tFace.Texture = (TextureFT3)textures[textureIndex];
						tFace.TextureIndex = textureIndex;
					}
				}

				if (tFace.Texture != null)
				{
					tFace.Texture.NumReferences++;

					if (reader.File._Version >= SR1_File.Version.Jan23 &&
						(tFace.attr.Value & 0x08) != 0)
					{
						tFace.Texture.HasTranslucentPolygon = true;
					}
				}
			}
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
			uint position = lastStructure.End;

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
				var faces = (SR1_StructureArray<TFace>)file._Structures[faceList.Offset];
				var bspNodes = (SR1_StructureSeries<BSPNode>)file._Structures[bspRoot.Offset];
				var bspLeaves = (SR1_StructureSeries<BSPLeaf>)file._Structures[startLeaves.Offset];
				var bspRootStruct = (BSPNode)bspNodes[0];

				#region BSPTrees

				// Create a new array of BSPTres.
				SR1_StructureArray<BSPTree> newBSPTrees = new SR1_StructureArray<BSPTree>(2);

				#region EnvTree

				BSPTree envTree = (BSPTree)newBSPTrees[0];

				// Set the environment BSPTree fields to the existing root and leaves.
				envTree.bspRoot.Offset = bspRoot.Offset;
				envTree.startLeaves.Offset = startLeaves.Offset;
				envTree.endLeaves.Offset = endLeaves.Offset;
				envTree.ID.Value = 0;

				#endregion

				#region SigTree

				/*List<TFace> sigFaces = new List<TFace>();

				foreach(TFace tFace in faces)
				{
					if (tFace.IsInSignalGroup)
					{
						TFace sigFace = new TFace();
						sigFaces.Add(sigFace);
					}
				}

				// TODO - Make the faces list growable.
				foreach (TFace sigFace in sigFaces)
				{
					
				}*/

				// Create a leaf for the signal tree. Only one is needed.
				BSPLeaf sigLeaf = new BSPLeaf();

				Sphere.Copy(sigLeaf.sphere, bspRootStruct.sphere);
				Sphere.Copy(sigLeaf.spectralSphere, bspRootStruct.spectralSphere);

				short radius = (short)sigLeaf.sphere.radius.Value;

				sigLeaf.box.minX.Value = (short)(sigLeaf.sphere.position.x.Value - radius);
				sigLeaf.box.minY.Value = (short)(sigLeaf.sphere.position.y.Value - radius);
				sigLeaf.box.minZ.Value = (short)(sigLeaf.sphere.position.z.Value - radius);
				sigLeaf.box.maxX.Value = (short)(sigLeaf.sphere.position.x.Value + radius);
				sigLeaf.box.maxY.Value = (short)(sigLeaf.sphere.position.y.Value + radius);
				sigLeaf.box.maxZ.Value = (short)(sigLeaf.sphere.position.z.Value + radius);

				short spectralRadius = (short)sigLeaf.spectralSphere.radius.Value;

				sigLeaf.spectralBox.minX.Value = (short)(sigLeaf.spectralSphere.position.x.Value - spectralRadius);
				sigLeaf.spectralBox.minY.Value = (short)(sigLeaf.spectralSphere.position.y.Value - spectralRadius);
				sigLeaf.spectralBox.minZ.Value = (short)(sigLeaf.spectralSphere.position.z.Value - spectralRadius);
				sigLeaf.spectralBox.maxX.Value = (short)(sigLeaf.spectralSphere.position.x.Value + spectralRadius);
				sigLeaf.spectralBox.maxY.Value = (short)(sigLeaf.spectralSphere.position.y.Value + spectralRadius);
				sigLeaf.spectralBox.maxZ.Value = (short)(sigLeaf.spectralSphere.position.z.Value + spectralRadius);

				// TODO - Set faceList and numFaces to the ones i made above.

				// MigrateVersion is only called on MembersReal, which won't include
				// this new one, so do that here.
				sigLeaf.MigrateVersion(file, targetVersion, migrateFlags);

				// Insert the BSPLeaf at the end of the leaves list.
				bspLeaves.Add(sigLeaf);

				BSPTree sigTree = (BSPTree)newBSPTrees[1];

				// Set the signal BSPTree fields to the new signal leaves.
				sigTree.bspRoot.Offset = endLeaves.Offset;
				sigTree.bspRoot.Heuristic = PtrHeuristic.End;
				sigTree.startLeaves.Offset = endLeaves.Offset;
				sigTree.startLeaves.Heuristic = PtrHeuristic.End;
				sigTree.endLeaves.Offset = endLeaves.Offset;
				sigTree.endLeaves.Heuristic = PtrHeuristic.End;
				sigTree.ID.Value = -1;

				#endregion

				// Insert the BSPTree array where the root used to be.
				file._MigrationStructures.Add(endLeaves.Offset, newBSPTrees);

				numBSPTrees.Value = 2;
				BSPTreeArray.Offset = endLeaves.Offset;
				BSPTreeArray.Heuristic = PtrHeuristic.Migration;

				#endregion

				#region MorphColors

				// Create a new array of morph colors with one per vertex.
				SR1_StructureArray<MorphColor> newMorphColors = new SR1_StructureArray<MorphColor>(numVertices.Value);
				newMorphColors.SetPadding(4);

				// The morph colors originally applies to a subset of the vertices,
				// but now they apply to every veretex, so copy the regular colors
				// from the vertices, then overwrite any found in the old morph
				// colors.
				var vertices = (SR1_StructureArray<TVertex>)file._Structures[vertexList.Offset];
				int c = 0;
				foreach (TVertex vertex in vertices)
				{
					MorphColor morphColor = (MorphColor)newMorphColors[c];
					morphColor.morphColor15.Value = unchecked((short)vertex.rbg15.Value);
					c++;
				}
				var oldMorphColors = (SR1_StructureArray<MorphColor>)file._Structures[MorphColorList.Offset];
				foreach (MorphColor oldMorphColor in oldMorphColors)
				{
					c = oldMorphColor.vindex.Value;

					if (c >= 0)
					{
						MorphColor morphColor = (MorphColor)newMorphColors[c];

						// MigrateVersion happens in Terrain before the MorphColors,
						// so those won't have been converted yet, and the new ones
						// aren't even in the list, so copy them here and migrate
						// them as well.
						MorphColor.Copy(morphColor, oldMorphColor);
						morphColor.MigrateVersion(file, targetVersion, migrateFlags);
					}
				}

				// Remove the old morph colors.
				file._Structures.Remove(MorphColorList.Offset);

				// Insert the new morph colors where the old ones were.
				file._MigrationStructures.Add(MorphColorList.Offset, newMorphColors);
				MorphColorList.Heuristic = PtrHeuristic.Migration;

				#endregion

				#region MorphNormals

				// Create a new array of morph normals.
				SR1_PrimativeArray<ushort> newMorphNormals = new SR1_PrimativeArray<ushort>(numFaces.Value);
				newMorphNormals.SetPadding(4);
				newMorphNormals.Align = 2;

				// There were no morph normals originally, but they would be stored in
				// the same array, so just copy indices of regular ones from the faces.
				int n = 0;
				foreach (TFace face in faces)
				{
					newMorphNormals[n] = face.normal.Value;
					n++;
				}

				// Insert the array of morph normals after the regular normals.
				file._MigrationStructures.Add(NormalListEnd, newMorphNormals);

				morphNormalIdx.Offset = NormalListEnd;
				morphNormalIdx.Heuristic = PtrHeuristic.Migration;

				#endregion
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

					file._MigrationStructures.Add(position, new TexAniAssocData(entryList));
					texAniAssocData.Offset = position;
					texAniAssocData.Heuristic = PtrHeuristic.Migration;
				}
				else
				{
					if (aniList.Offset != 0)
					{
						file._Structures.Remove(aniList.Offset);
						aniList.Offset = 0;
					}

					file._MigrationStructures.Add(position, new TexAniAssocData());
					texAniAssocData.Offset = position;
					texAniAssocData.Heuristic = PtrHeuristic.Migration;
				}
			}
		}
	}
}
