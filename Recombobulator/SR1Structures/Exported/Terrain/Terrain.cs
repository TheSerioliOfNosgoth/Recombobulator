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

		SR1_StructureSeries<MultiSignal> _multiSignals = null;
		SR1_StructureSeries<TVertex> _vertices = null;
		SR1_StructureSeries<TFace> _faces = null;
		SR1_StructureSeries<Normal> _normals = null;
		DrMoveAniTex _drMoveAniTex = null;
		StreamUnitPortalList _portalList = null;
		SR1_StructureSeries<TextureFT3> _textures = null;
		SR1_StructureSeries<MorphVertex> _morphVertices = null;
		SR1_StructureArray<MorphColor> _morphColors = null;
		SR1_PrimativeArray<ushort> _morphNormals = null;
		SR1_StructureSeries<BSPLeaf> _signalLeaves = null;

		// Exist in proto builds.
		BSPNode _bspRoot = null;
		SR1_StructureSeries<BSPNode> _bspNodes = null;
		SR1_StructureSeries<BSPLeaf> _bspLeaves = null;

		// Added when converting to proto builds.
		SR1_StructureArray<BSPTree> _bspTrees = null;
		BSPTree _envTree = null;
		BSPTree _sigTree = null;
		BSPLeaf _sigLeaf = null;
		List<TFace> _sigFaces = null;

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
			_multiSignals = null;
			_vertices = null;
			_faces = null;
			_normals = null;
			_drMoveAniTex = null;
			_portalList = null;
			_textures = null;
			_morphVertices = null;
			_morphColors = null;
			_morphNormals = null;
			_signalLeaves = null;

			_bspRoot = null;
			_bspNodes = null;
			_bspLeaves = null;
			_bspTrees = null;
			_envTree = null;
			_sigTree = null;
			_sigLeaf = null;
			_sigFaces = null;

			uint signalsOffset = reader.Level.SignalListStart.Offset;
			_multiSignals = (SR1_StructureSeries<MultiSignal>)reader.File._Structures[signalsOffset];

			#region Geometry

			_vertices = new SR1_StructureSeries<TVertex>();
			_vertices.ReadFromPointer(reader, vertexList, numVertices.Value);

			_faces = new SR1_StructureSeries<TFace>();
			_faces.ReadFromPointer(reader, faceList, numFaces.Value);

			_normals = new SR1_StructureSeries<Normal>();
			_normals.SetPadding(4);
			_normals.ReadFromPointer(reader, normalList, numNormals.Value);
			if (numNormals.Value > 0)
			{
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
				new SR1_StructureSeries<BSPNode>().ReadFromPointer(reader, sbspRoot, sbspStartLeaves);
			}

			_portalList = new StreamUnitPortalList();
			_portalList.ReadFromPointer(reader, StreamUnits);

			_textures = new SR1_StructureSeries<TextureFT3>();
			_textures.ReadFromPointer(reader, StartTextureList, EndTextureList);

			#region Morphs

			if (reader.File._Version <= SR1_File.Version.May12)
			{
				new SR1_StructureSeries<SBSPLeaf>().ReadFromPointer(reader, sbspStartLeaves, sbspEndLeaves);

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

			_morphVertices = new SR1_StructureSeries<MorphVertex>();
			_morphVertices.ReadFromPointer(reader, MorphDiffList, MorphColorList);

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

			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				_bspNodes = new SR1_StructureSeries<BSPNode>();
				_bspNodes.ReadFromPointer(reader, bspRoot, startLeaves);
				_bspNodes.Align = 4;

				_bspRoot = (BSPNode)_bspNodes[0];

				_bspLeaves = new SR1_StructureSeries<BSPLeaf>();
				_bspLeaves.ReadFromPointer(reader, startLeaves, endLeaves);

				// _signalLeaves is the entire hierarchy in proto builds.
				// MapSignalFaces will use the attr of the faces to figure out
				// which ones have signals instead of the tree ID.
				_signalLeaves = _bspLeaves;
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
						if (reader.File._Structures.ContainsKey(tree.startLeaves.Offset))
						{
							var nodesOrLeaves = reader.File._Structures[tree.startLeaves.Offset];
							_signalLeaves = nodesOrLeaves as SR1_StructureSeries<BSPLeaf>;
						}
					}
				}
			}

			#endregion

			#region MapFaces

			MapSignalFaces(reader);
			MapTextureFaces(reader);

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
						new SR1_StructureSeries<BSPLeaf>().SetReadCount(1).Read(reader, null, "");
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

		private void MapSignalFaces(SR1_Reader reader)
		{
			if (_multiSignals == null || _multiSignals.Count == 0 ||
				_faces == null || _faces.Count == 0 ||
				_signalLeaves == null || _signalLeaves.Count == 0)
			{
				return;
			}

			foreach (BSPLeaf leaf in _signalLeaves)
			{
				int faceSize = (reader.File._Version >= SR1_File.Version.Jan23) ? 12 : 16;
				int faceIndex = (int)(leaf.faceList.Offset - _faces.Start) / faceSize;
				short numFaces = leaf.numFaces.Value;

				for (short f = 0; f < numFaces; f++)
				{
					TFace face = (TFace)_faces[faceIndex + f];

					face.IsInSignalGroup = true;
					if (reader.File._Version < SR1_File.Version.Jan23)
					{
						face.IsInSignalGroup = ((face.attr0.Value & 0x4000) != 0);
						if (!face.IsInSignalGroup)
						{
							continue;
						}
					}

					foreach (MultiSignal mSignal in _multiSignals)
					{
						uint signalOffset = face.texture.Offset;
						if (reader.File._Version >= SR1_File.Version.Jan23)
						{
							// Note to self, don't try to use _multiSignals.Start here.
							// signals.Offset is not the first one in the array!
							signalOffset = signals.Offset + face.textoff.Value;
						}

						if (mSignal.Start == signalOffset)
						{
							face.MultiSignal = mSignal;
							if (mSignal.numSignals.Value > 0)
							{
								face.Signal = (Signal)mSignal.signalList[0];
							}
							break;
						}
					}

					if (face.MultiSignal != null && _portalList != null)
					{
						foreach (StreamUnitPortal portal in _portalList.portals)
						{
							if (portal.MSignalID.Value == face.MultiSignal.signalNum.Value)
							{
								face.Portal = portal;
								break;
							}
						}
					}
				}
			}
		}

		private void MapTextureFaces(SR1_Reader reader)
		{
			if (_faces == null || _faces.Count <= 0 ||
				_textures == null || _textures.Count <= 0)
			{
				return;
			}

			foreach (TFace face in _faces)
			{
				if (!face.IsInSignalGroup)
				{
					int textureOffset = face.textoff.Value;
					if (reader.File._Version < SR1_File.Version.Jan23)
					{
						textureOffset = (int)(face.texture.Offset - _textures.Start);
					}

					int textureSize = (reader.File._Version >= SR1_File.Version.Apr14) ? 12 : 16;
					int textureIndex = textureOffset / textureSize;

					if (textureIndex >= 0 && textureIndex < _textures.Count)
					{
						face.Texture = (TextureFT3)_textures[textureIndex];
						face.TextureIndex = textureIndex;
					}
				}

				if (face.Texture != null)
				{
					face.Texture.NumReferences++;

					if (reader.File._Version >= SR1_File.Version.Jan23 &&
						(face.attr.Value & 0x08) != 0)
					{
						face.Texture.HasTranslucentPolygon = true;
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
				#region BSPTrees

				// Create a new array of BSPTres.
				_bspTrees = new SR1_StructureArray<BSPTree>(2);

				#region EnvTree

				_envTree = (BSPTree)_bspTrees[0];

				// Set the environment BSPTree fields to the existing root and leaves.
				_envTree.bspRoot.Offset = 0;
				_envTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
				_envTree.startLeaves.Offset = 0;
				_envTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
				_envTree.endLeaves.Offset = 0;
				_envTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
				_envTree.ID.Value = 0;

				#endregion

				#region SigTree

				_sigFaces = new List<TFace>();

				foreach(TFace face in _faces)
				{
					if (face.IsInSignalGroup)
					{
						TFace sigFace = new TFace();
						TFace.Copy(sigFace, face);
						sigFace.MigrateVersion(file, targetVersion, migrateFlags);
						sigFace.attr.Value = 0; // 0x44;
						sigFace.textoff.Value = 0; // face.textoff.Value;

						// TODO - Rmove this once terrain.signals has a valid pointer.
						sigFace.IsInSignalGroup = false;

						_sigFaces.Add(sigFace);
					}
				}

				foreach (TFace sigFace in _sigFaces)
				{
					_faces.Add(sigFace);
				}

				numFaces.Value = _faces.Count;

				// Create a leaf for the signal tree. Only one is needed.
				_sigLeaf = new BSPLeaf();

				// Leaves need flag 2!
				_sigLeaf.flags.Value = 2;

				Sphere.Copy(_sigLeaf.sphere, _bspRoot.sphere);
				Sphere.Copy(_sigLeaf.spectralSphere, _bspRoot.spectralSphere);

				_sigLeaf.faceList.Offset = 0;
				_sigLeaf.faceList.Heuristic = PtrHeuristic.Explicit;
				_sigLeaf.numFaces.Value = 0; // (short)_sigFaces.Count;

				short radius = (short)_sigLeaf.sphere.radius.Value;

				_sigLeaf.box.minX.Value = (short)(_sigLeaf.sphere.position.x.Value - radius);
				_sigLeaf.box.minY.Value = (short)(_sigLeaf.sphere.position.y.Value - radius);
				_sigLeaf.box.minZ.Value = (short)(_sigLeaf.sphere.position.z.Value - radius);
				_sigLeaf.box.maxX.Value = (short)(_sigLeaf.sphere.position.x.Value + radius);
				_sigLeaf.box.maxY.Value = (short)(_sigLeaf.sphere.position.y.Value + radius);
				_sigLeaf.box.maxZ.Value = (short)(_sigLeaf.sphere.position.z.Value + radius);

				short spectralRadius = (short)_sigLeaf.spectralSphere.radius.Value;

				_sigLeaf.spectralBox.minX.Value = (short)(_sigLeaf.spectralSphere.position.x.Value - spectralRadius);
				_sigLeaf.spectralBox.minY.Value = (short)(_sigLeaf.spectralSphere.position.y.Value - spectralRadius);
				_sigLeaf.spectralBox.minZ.Value = (short)(_sigLeaf.spectralSphere.position.z.Value - spectralRadius);
				_sigLeaf.spectralBox.maxX.Value = (short)(_sigLeaf.spectralSphere.position.x.Value + spectralRadius);
				_sigLeaf.spectralBox.maxY.Value = (short)(_sigLeaf.spectralSphere.position.y.Value + spectralRadius);
				_sigLeaf.spectralBox.maxZ.Value = (short)(_sigLeaf.spectralSphere.position.z.Value + spectralRadius);

				// MigrateVersion is only called on MembersReal, which won't include
				// this new one, so do that here.
				_sigLeaf.MigrateVersion(file, targetVersion, migrateFlags);

				// Insert the BSPLeaf at the end of the leaves list.
				_bspLeaves.Add(_sigLeaf);

				_sigTree = (BSPTree)_bspTrees[1];

				// Set the signal BSPTree fields to the new signal leaves.
				_sigTree.bspRoot.Offset = 0;
				_sigTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
				_sigTree.startLeaves.Offset = 0;
				_sigTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
				_sigTree.endLeaves.Offset = 0;
				_sigTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
				_sigTree.ID.Value = -1;

				#endregion

				// Insert the BSPTree array where the root used to be.
				file._MigrationStructures.Add(endLeaves.Offset, _bspTrees);

				numBSPTrees.Value = 2;
				BSPTreeArray.Offset = 0;
				BSPTreeArray.Heuristic = PtrHeuristic.Explicit;

				#endregion

				#region MorphColors

				// Create a new array of morph colors with one per vertex.
				SR1_StructureArray<MorphColor> newMorphColors = new SR1_StructureArray<MorphColor>(numVertices.Value);
				newMorphColors.SetPadding(4);

				// The morph colors originally applies to a subset of the vertices,
				// but now they apply to every veretex, so copy the regular colors
				// from the vertices, then overwrite any found in the old morph
				// colors.
				var vertices = (SR1_StructureSeries<TVertex>)file._Structures[vertexList.Offset];
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
				foreach (TFace face in _faces)
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
				#region AniTex

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

				#endregion
			}
		}

		public override void MigratePointers(SR1_File file, SR1_File.Version sourceVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigratePointers(file, sourceVersion, migrateFlags);

			if (sourceVersion < SR1_File.Version.Jan23 && file._Version >= SR1_File.Version.Jan23)
			{
				BSPTreeArray.Offset = _bspTrees.NewStart;

				_envTree.bspRoot.Offset = _bspRoot.NewStart;
				_envTree.startLeaves.Offset = _bspLeaves.NewStart;
				_envTree.endLeaves.Offset = _sigLeaf.NewStart;

				_sigTree.bspRoot.Offset = _sigLeaf.NewStart;
				_sigTree.startLeaves.Offset = _sigLeaf.NewStart;
				_sigTree.endLeaves.Offset = _sigLeaf.NewEnd;

				if (_sigFaces.Count > 0)
				{
					_sigLeaf.faceList.Offset = 0; // _sigFaces[0].NewStart;
				}
				else
				{
					_sigLeaf.faceList.Offset = _faces.NewEnd;
				}
			}
		}
	}
}
