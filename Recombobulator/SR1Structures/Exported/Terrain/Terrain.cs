using System;
using System.Collections.Generic;
using System.Linq;

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
		SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>(PtrHeuristic.Start);
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
		SR1_StructureSeries<MorphColor> _morphColors = null;
		SR1_PrimativeSeries<ushort> _morphNormals = null;
		SR1_StructureSeries<BSPTree> _bspTrees = null;
		BSPTree _sigTree = null;
		BSPLeaf _sigLeaf = null;
		SR1_StructureSeries<BSPLeaf> _signalLeaves = null;

		// Exist in proto builds.
		BSPNode _bspRoot = null;
		SR1_StructureSeries<BSPNode> _bspNodes = null;
		SR1_StructureSeries<BSPLeaf> _bspLeaves = null;
		bool HasSignalTree = false;
		bool HasSunlightTree = false;

		// Added when converting proto builds.
		BSPTree _envTree = null;
		BSPTree _sunTree = null;
		BSPLeaf _sunLeaf = null;
		List<TFace> _sunFaces = null;
		List<TFace> _sigFaces = null;

		// Added when converting cathy63 to fit cathy56.
		BSPTree _moddedTree = null;
		BSPLeaf _moddedLeaf = null;
		List<TFace> _moddedFaces = null;

		// The first signal referenced by the terrain.
		MultiSignal _terrainSignal = null;

		// Used to remember where to insert the morph normals, on migrating from Proto1
		// because of weird padding at the end of them.
		uint NormalListEnd = 0;

		struct ObjVector
		{
			public short x;
			public short y;
			public short z;
		}

		struct ObjVertex
		{
			public ObjVector pos;
			public byte r;
			public byte g;
			public byte b;
		}

		struct ObjFace
		{
			public int v0, v1, v2;
			public int uv0, uv1, uv2;
			public int mtl;
		}

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

			#region Signals

			uint signalsOffset = reader.Level.SignalListStart.Offset;
			_multiSignals = (SR1_StructureSeries<MultiSignal>)reader.File._Structures[signalsOffset];

			foreach (MultiSignal mSignal in _multiSignals)
			{
				if (mSignal.Start == signals.Offset)
				{
					_terrainSignal = mSignal;
					break;
				}
			}

			#endregion

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

			#region Intros

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

			#endregion

			#region Morphs

			int numMorphVerticies = GetMorphVertexCount(reader);
			_morphVertices = new SR1_StructureSeries<MorphVertex>();
			_morphVertices.ReadFromPointer(reader, MorphDiffList, numMorphVerticies);

			int numMorphColors = GetMorphColorCount(reader);
			int morphColorPadding = GetMorphColorPadding(reader);
			_morphColors = new SR1_StructureSeries<MorphColor>();
			_morphColors.SetPadding(morphColorPadding);
			_morphColors.ReadFromPointer(reader, MorphColorList, numMorphColors);

			int numMorphNormals = GetMorphNormalCount(reader);
			int morphNormalPadding = GetMorphNormalPadding(reader);
			int morphNormalAlign = GetMorphNormalAlign(reader);
			_morphNormals = new SR1_PrimativeSeries<ushort>();
			_morphNormals.SetPadding(morphNormalPadding);
			_morphNormals.Align = morphNormalAlign;
			_morphNormals.ReadFromPointer(reader, morphNormalIdx, numMorphNormals);

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
				_bspTrees = new SR1_StructureSeries<BSPTree>();
				_bspTrees.SetReadCount(numBSPTrees.Value);
				_bspTrees.ReadFromPointer(reader, BSPTreeArray);

				if (_bspTrees.Count > 0)
				{
					_sigTree = (BSPTree)_bspTrees[numBSPTrees.Value - 1];
					if (_sigTree.ID.Value == -1)
					{
						if (reader.File._Structures.ContainsKey(_sigTree.startLeaves.Offset))
						{
							var nodesOrLeaves = reader.File._Structures[_sigTree.startLeaves.Offset];
							_signalLeaves = nodesOrLeaves as SR1_StructureSeries<BSPLeaf>;
							if (_signalLeaves != null && _signalLeaves.Count > 0)
							{
								_sigLeaf = (BSPLeaf)_signalLeaves[0];
							}
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

		private int GetMorphVertexCount(SR1_Reader reader)
		{
			int numMorphVerticies = 0;

			if (MorphDiffList.Offset != 0)
			{
				MorphVertex morphVertex = new MorphVertex();

				reader.BaseStream.Position = MorphDiffList.Offset;

				do
				{
					morphVertex.ReadTemp(reader);
					numMorphVerticies++;
				}
				while (morphVertex.vindex.Value != -1);
			}

			return numMorphVerticies;
		}

		private int GetMorphColorCount(SR1_Reader reader)
		{
			int numMorphColors = 0;

			if (reader.File._Version >= SR1_File.Version.Jan23)
			{
				return numVertices.Value;
			}

			if (MorphColorList.Offset != 0)
			{
				MorphColor morphColor = new MorphColor();

				reader.BaseStream.Position = MorphColorList.Offset;

				do
				{
					morphColor.ReadTemp(reader);
					numMorphColors++;
				}
				while (morphColor.vindex.Value != -1);
			}

			return numMorphColors;
		}

		private int GetMorphColorPadding(SR1_Reader reader)
		{
			int morphColorPadding = 0;

			if (reader.File._Version >= SR1_File.Version.Apr14)
			{
				morphColorPadding = 4;
			}
			else if (reader.File._Version >= SR1_File.Version.Jan23)
			{
				morphColorPadding = 2;
			}

			return morphColorPadding;
		}

		private int GetMorphNormalCount(SR1_Reader reader)
		{
			if (reader.File._Version >= SR1_File.Version.Jan23)
			{
				return numFaces.Value;
			}

			return 0;
		}

		private int GetMorphNormalPadding(SR1_Reader reader)
		{
			return 4;
		}

		private int GetMorphNormalAlign(SR1_Reader reader)
		{
			return 2;
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
						else
						{
							HasSignalTree = true;
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
					}
				}

				if (face.Texture != null)
				{
					face.Texture.NumReferences++;

					if (reader.File._Version < SR1_File.Version.Jan23)
					{
						if ((face.attr0.Value & 0x0200) != 0)
						{
							face.IsWater = true;
							face.Texture.IsWater = true;
						}
						else if ((face.attr0.Value & 0x0040) != 0)
						{
							face.IsSunlight = true;
							face.Texture.IsSunlight = true;
							HasSunlightTree = true;
						}
					}
					else if ((face.attr.Value & 0x08) != 0)
					{
						face.IsWater = true;
						face.Texture.IsWater = true;
					}
				}
			}
		}

		private void CopyBSPRootToLeaf(BSPLeaf leaf)
		{
			Sphere.Copy(leaf.sphere, _bspRoot.sphere);
			Sphere.Copy(leaf.spectralSphere, _bspRoot.spectralSphere);

			short radius = (short)leaf.sphere.radius.Value;

			leaf.box.minX.Value = (short)(leaf.sphere.position.x.Value - radius);
			leaf.box.minY.Value = (short)(leaf.sphere.position.y.Value - radius);
			leaf.box.minZ.Value = (short)(leaf.sphere.position.z.Value - radius);
			leaf.box.maxX.Value = (short)(leaf.sphere.position.x.Value + radius);
			leaf.box.maxY.Value = (short)(leaf.sphere.position.y.Value + radius);
			leaf.box.maxZ.Value = (short)(leaf.sphere.position.z.Value + radius);

			short spectralRadius = (short)leaf.spectralSphere.radius.Value;

			leaf.spectralBox.minX.Value = (short)(leaf.spectralSphere.position.x.Value - spectralRadius);
			leaf.spectralBox.minY.Value = (short)(leaf.spectralSphere.position.y.Value - spectralRadius);
			leaf.spectralBox.minZ.Value = (short)(leaf.spectralSphere.position.z.Value - spectralRadius);
			leaf.spectralBox.maxX.Value = (short)(leaf.spectralSphere.position.x.Value + spectralRadius);
			leaf.spectralBox.maxY.Value = (short)(leaf.spectralSphere.position.y.Value + spectralRadius);
			leaf.spectralBox.maxZ.Value = (short)(leaf.spectralSphere.position.z.Value + spectralRadius);
		}

		private int DotProduct(ObjVector a, ObjVector b)
		{
			//int aX = a.x;
			//int aY = a.y;
			//int aZ = a.z;
			//int bX = b.x;
			//int bY = b.y;
			//int bZ = b.z;

			return (a.x * b.x + a.y * b.y + a.z * b.z);
		}

		private ObjVector CalculateFaceNormal(ObjVector v0, ObjVector v1, ObjVector v2)
		{
			ObjVector normal = new ObjVector();

			int aX = v1.x - v0.x;
			int aY = v1.y - v0.y;
			int aZ = v1.z - v0.z;

			int bX = v2.x - v0.x;
			int bY = v2.y - v0.y;
			int bZ = v2.z - v0.z;

			int nX = (((aY * bZ) - (aZ * bY)) >> 12);
			int nY = (-((aX * bZ) - (aZ * bX)) >> 12);
			int nZ = (((aX * bY) - (aY * bX)) >> 12);

			int length = Math.Abs(nY);

			if (length < Math.Abs(nX))
			{
				length = Math.Abs(nX);
			}

			if (length < Math.Abs(nZ))
			{
				length = Math.Abs(nZ);
			}

			if (length != 0)
			{
				normal.x = (short)((nX << 12) / length);
				normal.y = (short)((nY << 12) / length);
				normal.z = (short)((nZ << 12) / length);
			}

			/*long x = aY * bZ - aZ * bY;
			long y = aZ * bX - aX * bZ;
			long z = aX * bY - aY * bX;

			long length = Math.Max(Math.Abs(x), Math.Max(Math.Abs(y), Math.Abs(z)));
			if (length != 0)
			{
				normal.x = (short)((x * short.MaxValue) / length);
				normal.y = (short)((y * short.MaxValue) / length);
				normal.z = (short)((z * short.MaxValue) / length);
			}*/

			return normal;
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			Level level = (Level)file._Structures[0];

			SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
			uint endPosition = lastStructure.End;

			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (level.introList.Offset != 0)
			{
				SR1_StructureSeries<Intro> intros = (SR1_StructureSeries<Intro>)file._Structures[level.introList.Offset];
				numIntros.Value = intros.Count;
			}

			if (_portalList != null)
			{
				SR1_StructureList<StreamUnitPortal> portals = _portalList.portals;

				int newNumPortals = 0;
				while (newNumPortals < portals.Count)
				{
					if (((StreamUnitPortal)portals[newNumPortals]).OmitFromMigration)
					{
						portals.RemoveAt(newNumPortals);
					}
					else
					{
						newNumPortals++;
					}
				}

				if (file._Version == SR1_File.Version.Retail_PC &&
					targetVersion == SR1_File.Version.Retail_PC)
				{
					#region FixCity10

					if ((migrateFlags & SR1_File.MigrateFlags.FixCity10) != 0 &&
						level.Name == "city10")
					{
						#region Signal0

						var v196 = (TVertex)_vertices[196];
						var v197 = (TVertex)_vertices[197];
						var v198 = (TVertex)_vertices[198];
						var v199 = (TVertex)_vertices[199];

						v196.vertex.x.Value = -2547;
						v196.vertex.y.Value = 0;
						v196.vertex.z.Value = 224;

						v197.vertex.x.Value = -4467;
						v197.vertex.y.Value = 0;
						v197.vertex.z.Value = 224;

						v198.vertex.x.Value = -4467;
						v198.vertex.y.Value = 0;
						v198.vertex.z.Value = -1921;

						v199.vertex.x.Value = -2547;
						v199.vertex.y.Value = 0;
						v199.vertex.z.Value = -1921;

						#endregion

						#region Signal1

						var v200 = (TVertex)_vertices[200];
						var v201 = (TVertex)_vertices[201];
						var v202 = (TVertex)_vertices[202];
						var v203 = (TVertex)_vertices[203];

						v200.vertex.x.Value = -4467;
						v200.vertex.y.Value = -5120;
						v200.vertex.z.Value = 217;

						v201.vertex.x.Value = -2547;
						v201.vertex.y.Value = -5120;
						v201.vertex.z.Value = 217;

						v202.vertex.x.Value = -4467;
						v202.vertex.y.Value = -5120;
						v202.vertex.z.Value = -642;

						v203.vertex.x.Value = -2547;
						v203.vertex.y.Value = -5120;
						v203.vertex.z.Value = -642;

						#endregion

						#region Signal2

						var v204 = (TVertex)_vertices[204];
						var v205 = (TVertex)_vertices[205];
						var v206 = (TVertex)_vertices[206];
						var v207 = (TVertex)_vertices[207];

						v204.vertex.x.Value = -4467;
						v204.vertex.y.Value = -5120;
						v204.vertex.z.Value = -642;

						v205.vertex.x.Value = -2547;
						v205.vertex.y.Value = -5120;
						v205.vertex.z.Value = -642;

						v206.vertex.x.Value = -2552;
						v206.vertex.y.Value = -10880;
						v206.vertex.z.Value = -646;

						v207.vertex.x.Value = -4472;
						v207.vertex.y.Value = -10880;
						v207.vertex.z.Value = -646;

						#endregion

						#region SigLeaf

						_sigLeaf.sphereNoSq.position.x.Value = -3496;
						_sigLeaf.sphereNoSq.position.y.Value = -5359;
						_sigLeaf.sphereNoSq.position.z.Value = -1261;
						_sigLeaf.sphereNoSq.radius.Value = 5646;
						_sigLeaf.box.minX.Value = -4472;
						_sigLeaf.box.minY.Value = -10880;
						_sigLeaf.box.minZ.Value = -1921;
						_sigLeaf.box.maxX.Value = -2547;
						_sigLeaf.box.maxY.Value = 0;
						_sigLeaf.box.maxZ.Value = 224;

						_sigLeaf.spectralSphereNoSq.position.x.Value = -3496;
						_sigLeaf.spectralSphereNoSq.position.y.Value = -5359;
						_sigLeaf.spectralSphereNoSq.position.z.Value = -1261;
						_sigLeaf.spectralSphereNoSq.radius.Value = 5646;
						_sigLeaf.spectralBox.minX.Value = -4472;
						_sigLeaf.spectralBox.minY.Value = -10880;
						_sigLeaf.spectralBox.minZ.Value = -1921;
						_sigLeaf.spectralBox.maxX.Value = -2547;
						_sigLeaf.spectralBox.maxY.Value = 0;
						_sigLeaf.spectralBox.maxZ.Value = 224;

						#endregion

						#region Portal0

						StreamUnitPortal portal0 = (StreamUnitPortal)_portalList.portals[0];

						portal0.minx.Value = -4472;
						portal0.miny.Value = -10880;
						portal0.minz.Value = -646;
						portal0.maxx.Value = -2547;
						portal0.maxy.Value = -5120;
						portal0.maxz.Value = -642;

						((SVector)portal0.t1[0]).x.Value = -4472;
						((SVector)portal0.t1[0]).y.Value = -10880;
						((SVector)portal0.t1[0]).z.Value = -646;

						((SVector)portal0.t1[1]).x.Value = -4467;
						((SVector)portal0.t1[1]).y.Value = -5120;
						((SVector)portal0.t1[1]).z.Value = -642;

						((SVector)portal0.t1[2]).x.Value = -2547;
						((SVector)portal0.t1[2]).y.Value = -5120;
						((SVector)portal0.t1[2]).z.Value = -642;

						((SVector)portal0.t2[0]).x.Value = -2547;
						((SVector)portal0.t2[0]).y.Value = -5120;
						((SVector)portal0.t2[0]).z.Value = -642;

						((SVector)portal0.t2[1]).x.Value = -2552;
						((SVector)portal0.t2[1]).y.Value = -10880;
						((SVector)portal0.t2[1]).z.Value = -646;

						((SVector)portal0.t2[2]).x.Value = -4472;
						((SVector)portal0.t2[2]).y.Value = -10880;
						((SVector)portal0.t2[2]).z.Value = -646;

						#endregion

						#region Portal1

						StreamUnitPortal portal1 = (StreamUnitPortal)_portalList.portals[1];

						portal1.minx.Value = -4467;
						portal1.miny.Value = -5120;
						portal1.minz.Value = -642;
						portal1.maxx.Value = -2547;
						portal1.maxy.Value = -5120;
						portal1.maxz.Value = 217;

						((SVector)portal1.t1[0]).x.Value = -2547;
						((SVector)portal1.t1[0]).y.Value = -5120;
						((SVector)portal1.t1[0]).z.Value = -642;

						((SVector)portal1.t1[1]).x.Value = -4467;
						((SVector)portal1.t1[1]).y.Value = -5120;
						((SVector)portal1.t1[1]).z.Value = -642;

						((SVector)portal1.t1[2]).x.Value = -2547;
						((SVector)portal1.t1[2]).y.Value = -5120;
						((SVector)portal1.t1[2]).z.Value = 217;

						((SVector)portal1.t2[0]).x.Value = -4467;
						((SVector)portal1.t2[0]).y.Value = -5120;
						((SVector)portal1.t2[0]).z.Value = 217;

						((SVector)portal1.t2[1]).x.Value = -2547;
						((SVector)portal1.t2[1]).y.Value = -5120;
						((SVector)portal1.t2[1]).z.Value = 217;

						((SVector)portal1.t2[2]).x.Value = -4467;
						((SVector)portal1.t2[2]).y.Value = -5120;
						((SVector)portal1.t2[2]).z.Value = -642;

						#endregion

						#region Portal2

						StreamUnitPortal portal2 = (StreamUnitPortal)_portalList.portals[2];

						portal2.minx.Value = -4467;
						portal2.miny.Value = 0;
						portal2.minz.Value = -1921;
						portal2.maxx.Value = -2547;
						portal2.maxy.Value = 0;
						portal2.maxz.Value = 224;

						((SVector)portal2.t1[0]).x.Value = -4467;
						((SVector)portal2.t1[0]).y.Value = 0;
						((SVector)portal2.t1[0]).z.Value = -1921;

						((SVector)portal2.t1[1]).x.Value = -2547;
						((SVector)portal2.t1[1]).y.Value = 0;
						((SVector)portal2.t1[1]).z.Value = -1921;

						((SVector)portal2.t1[2]).x.Value = -2547;
						((SVector)portal2.t1[2]).y.Value = 0;
						((SVector)portal2.t1[2]).z.Value = 224;

						((SVector)portal2.t2[0]).x.Value = -2547;
						((SVector)portal2.t2[0]).y.Value = 0;
						((SVector)portal2.t2[0]).z.Value = 224;

						((SVector)portal2.t2[1]).x.Value = -4467;
						((SVector)portal2.t2[1]).y.Value = 0;
						((SVector)portal2.t2[1]).z.Value = 224;

						((SVector)portal2.t2[2]).x.Value = -4467;
						((SVector)portal2.t2[2]).y.Value = 0;
						((SVector)portal2.t2[2]).z.Value = -1921;

						#endregion
					}

					#endregion

					#region FixCity11

					if ((migrateFlags & SR1_File.MigrateFlags.FixCity11) != 0 &&
						level.Name == "city11")
					{
						StreamUnitPortal newPortal = new StreamUnitPortal();
						newPortal.tolevelname.SetReadMax(true);
						newPortal.tolevelname.SetText("city12,1", 16);
						newPortal.streamID.Value = 171;
						newPortal.MSignalID.Value = 2;

						newPortal.minx.Value = -660;
						newPortal.miny.Value = 2342;
						newPortal.minz.Value = -7144;
						newPortal.maxx.Value = -660;
						newPortal.maxy.Value = 3700;
						newPortal.maxz.Value = -5786;

						((SVector)newPortal.t1[0]).x.Value = -660;
						((SVector)newPortal.t1[0]).y.Value = 2342;
						((SVector)newPortal.t1[0]).z.Value = -5786;

						((SVector)newPortal.t1[1]).x.Value = -660;
						((SVector)newPortal.t1[1]).y.Value = 3700;
						((SVector)newPortal.t1[1]).z.Value = -5786;

						((SVector)newPortal.t1[2]).x.Value = -660;
						((SVector)newPortal.t1[2]).y.Value = 3700;
						((SVector)newPortal.t1[2]).z.Value = -7144;

						((SVector)newPortal.t2[0]).x.Value = -660;
						((SVector)newPortal.t2[0]).y.Value = 3700;
						((SVector)newPortal.t2[0]).z.Value = -7144;

						((SVector)newPortal.t2[1]).x.Value = -660;
						((SVector)newPortal.t2[1]).y.Value = 2342;
						((SVector)newPortal.t2[1]).z.Value = -7144;

						((SVector)newPortal.t2[2]).x.Value = -660;
						((SVector)newPortal.t2[2]).y.Value = 2342;
						((SVector)newPortal.t2[2]).z.Value = -5786;

						portals.Add(newPortal);
						newNumPortals++;

						MultiSignal newMultiSignal = new MultiSignal();
						newMultiSignal.numSignals.Value = 1;
						newMultiSignal.signalNum.Value = 2;
						newMultiSignal.flags.Value = 0;
						Signal newSignal = new Signal();
						newSignal.id.Value = 18;
						SignalStreamLevel newSignalStreamLevel = new SignalStreamLevel();
						newSignalStreamLevel.currentnum.Value = 2;
						newSignalStreamLevel.streamID.Value = 171;
						newSignalStreamLevel.toname.SetReadMax(true);
						newSignalStreamLevel.toname.SetText("city12,1", 16);
						newSignal.data = newSignalStreamLevel;
						newMultiSignal.signalList.Add(newSignal);
						newMultiSignal.pad.Value = 15;

						_multiSignals.InsertAt(0, newMultiSignal);
						_terrainSignal = newMultiSignal;

						signals.Offset = 0;
						signals.Heuristic = PtrHeuristic.Explicit;

						TFace face = (TFace)_faces[1676];
						face.Portal = newPortal;
						face.MultiSignal = newMultiSignal;
						face = (TFace)_faces[1677];
						face.Portal = newPortal;
						face.MultiSignal = newMultiSignal;
					}

					#endregion

					#region FixLair33

					if (//(migrateFlags & SR1_File.MigrateFlags.FixLair33) != 0 &&
						level.Name == "lair33")
					{
						StreamUnitPortal portal1 = (StreamUnitPortal)_portalList.portals[1];
						portal1.tolevelname.SetReadMax(true);
						portal1.tolevelname.SetText("retreat6,75", 16);

						StreamUnitPortal portal2 = (StreamUnitPortal)_portalList.portals[2];
						portal2.tolevelname.SetReadMax(true);
						portal2.tolevelname.SetText("retreat6,77", 16);
					}

					#endregion

					#region FixRetreat6

					if (//(migrateFlags & SR1_File.MigrateFlags.FixRetreat6) != 0 &&
						level.Name == "retreat6")
					{
						StreamUnitPortal newPortal;

						#region Portal1

						newPortal = new StreamUnitPortal();
						newPortal.tolevelname.SetReadMax(true);
						newPortal.tolevelname.SetText("lair33,76", 16);
						newPortal.streamID.Value = -4575;
						newPortal.MSignalID.Value = 77;

						newPortal.minx.Value = 2734;
						newPortal.miny.Value = -3333;
						newPortal.minz.Value = 2779;
						newPortal.maxx.Value = 3208;
						newPortal.maxy.Value = -2829;
						newPortal.maxz.Value = 4705;

						((SVector)newPortal.t1[0]).x.Value = 2734;
						((SVector)newPortal.t1[0]).y.Value = -3144;
						((SVector)newPortal.t1[0]).z.Value = 2785;

						((SVector)newPortal.t1[1]).x.Value = 3208;
						((SVector)newPortal.t1[1]).y.Value = -2988;
						((SVector)newPortal.t1[1]).z.Value = 4705;

						((SVector)newPortal.t1[2]).x.Value = 2833;
						((SVector)newPortal.t1[2]).y.Value = -3333;
						((SVector)newPortal.t1[2]).z.Value = 4705;

						((SVector)newPortal.t2[0]).x.Value = 3208;
						((SVector)newPortal.t2[0]).y.Value = -2988;
						((SVector)newPortal.t2[0]).z.Value = 4705;

						((SVector)newPortal.t2[1]).x.Value = 2734;
						((SVector)newPortal.t2[1]).y.Value = -3144;
						((SVector)newPortal.t2[1]).z.Value = 2785;

						((SVector)newPortal.t2[2]).x.Value = 3027;
						((SVector)newPortal.t2[2]).y.Value = -2829;
						((SVector)newPortal.t2[2]).z.Value = 2779;

						portals.Add(newPortal);
						newNumPortals++;

						#endregion

						#region Portal2

						newPortal = new StreamUnitPortal();
						newPortal.tolevelname.SetReadMax(true);
						newPortal.tolevelname.SetText("lair33,74", 16);
						newPortal.streamID.Value = -4575;
						newPortal.MSignalID.Value = 75;

						newPortal.minx.Value = 4085;
						newPortal.miny.Value = -1257;
						newPortal.minz.Value = 2785;
						newPortal.maxx.Value = 4354;
						newPortal.maxy.Value = -722;
						newPortal.maxz.Value = 4705;

						((SVector)newPortal.t1[0]).x.Value = 4085;
						((SVector)newPortal.t1[0]).y.Value = -1185;
						((SVector)newPortal.t1[0]).z.Value = 2785;

						((SVector)newPortal.t1[1]).x.Value = 4354;
						((SVector)newPortal.t1[1]).y.Value = -766;
						((SVector)newPortal.t1[1]).z.Value = 4705;

						((SVector)newPortal.t1[2]).x.Value = 4234;
						((SVector)newPortal.t1[2]).y.Value = -1257;
						((SVector)newPortal.t1[2]).z.Value = 4705;

						((SVector)newPortal.t2[0]).x.Value = 4201;
						((SVector)newPortal.t2[0]).y.Value = -722;
						((SVector)newPortal.t2[0]).z.Value = 2785;

						((SVector)newPortal.t2[1]).x.Value = 4354;
						((SVector)newPortal.t2[1]).y.Value = -766;
						((SVector)newPortal.t2[1]).z.Value = 4705;

						((SVector)newPortal.t2[2]).x.Value = 4085;
						((SVector)newPortal.t2[2]).y.Value = -1185;
						((SVector)newPortal.t2[2]).z.Value = 2785;

						portals.Add(newPortal);
						newNumPortals++;

						#endregion
					}

					#endregion
				}

				#region FixCathy56

				if (file._Version == SR1_File.Version.Feb16 &&
					targetVersion == SR1_File.Version.Retail_PC &&
					//(migrateFlags & SR1_File.MigrateFlags.FixCathy56) != 0 &&
					level.Name == "cathy56")
				{
					short offsetX = 1400;
					short offsetY = 0;
					short offsetZ = 0;

					#region Portals

					StreamUnitPortal newPortal = new StreamUnitPortal();
					newPortal.tolevelname.SetReadMax(true);
					newPortal.tolevelname.SetText("cathy63,103", 16);
					newPortal.streamID.Value = 413;
					newPortal.MSignalID.Value = 104;

					newPortal.minx.Value = (short)(6476 + offsetX);
					newPortal.miny.Value = (short)(13025 + offsetY);
					newPortal.minz.Value = (short)(15031 + offsetZ);
					newPortal.maxx.Value = (short)(6476 + offsetX);
					newPortal.maxy.Value = (short)(16022 + offsetY);
					newPortal.maxz.Value = (short)(17492 + offsetZ);

					((SVector)newPortal.t1[0]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t1[0]).y.Value = (short)(16022 + offsetY);
					((SVector)newPortal.t1[0]).z.Value = (short)(15031 + offsetZ);

					((SVector)newPortal.t1[1]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t1[1]).y.Value = (short)(13025 + offsetY);
					((SVector)newPortal.t1[1]).z.Value = (short)(17492 + offsetZ);

					((SVector)newPortal.t1[2]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t1[2]).y.Value = (short)(16022 + offsetY);
					((SVector)newPortal.t1[2]).z.Value = (short)(17491 + offsetZ);

					((SVector)newPortal.t2[0]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t2[0]).y.Value = (short)(16022 + offsetY);
					((SVector)newPortal.t2[0]).z.Value = (short)(15031 + offsetZ);

					((SVector)newPortal.t2[1]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t2[1]).y.Value = (short)(13025 + offsetY);
					((SVector)newPortal.t2[1]).z.Value = (short)(15031 + offsetZ);

					((SVector)newPortal.t2[2]).x.Value = (short)(6476 + offsetX);
					((SVector)newPortal.t2[2]).y.Value = (short)(13025 + offsetY);
					((SVector)newPortal.t2[2]).z.Value = (short)(17492 + offsetZ);

					portals.Add(newPortal);
					newNumPortals++;

					#endregion

					#region Signals

					MultiSignal multiSignal2 = (MultiSignal)_multiSignals[2];
					multiSignal2.numSignals.Value = 1;
					multiSignal2.signalNum.Value = 104;
					multiSignal2.flags.Value = 0;
					Signal newSignal = new Signal();
					newSignal.id.Value = 18;
					SignalStreamLevel newSignalStreamLevel = new SignalStreamLevel();
					newSignalStreamLevel.currentnum.Value = 104;
					newSignalStreamLevel.streamID.Value = 413;
					newSignalStreamLevel.toname.SetReadMax(true);
					newSignalStreamLevel.toname.SetText("cathy63,103", 16);
					newSignal.data = newSignalStreamLevel;
					multiSignal2.signalList.Add(newSignal);
					multiSignal2.pad.Value = 15;

					TFace face;

					face = (TFace)_faces[5315];
					face.Portal = newPortal;
					face.MultiSignal = multiSignal2;

					face = (TFace)_faces[5316];
					face.Portal = newPortal;
					face.MultiSignal = multiSignal2;

					Vertex vertex;

					vertex = ((TVertex)_vertices[1042]).vertex;
					vertex.x.Value = (short)(6476 + offsetX);
					vertex.y.Value = (short)(16022 + offsetY);
					vertex.z.Value = (short)(15031 + offsetZ);

					vertex = ((TVertex)_vertices[1841]).vertex;
					vertex.x.Value = (short)(6476 + offsetX);
					vertex.y.Value = (short)(13025 + offsetY);
					vertex.z.Value = (short)(15031 + offsetZ);

					vertex = ((TVertex)_vertices[2701]).vertex;
					vertex.x.Value = (short)(6476 + offsetX);
					vertex.y.Value = (short)(13025 + offsetY);
					vertex.z.Value = (short)(17492 + offsetZ);

					vertex = ((TVertex)_vertices[2702]).vertex;
					vertex.x.Value = (short)(6476 + offsetX);
					vertex.y.Value = (short)(16022 + offsetY);
					vertex.z.Value = (short)(17491 + offsetZ);

					#endregion

					#region Bounds

					Position materialSpherePos = _sigLeaf.sphereNoSq.position;
					materialSpherePos.x.Value = (short)(6476 + offsetX);
					materialSpherePos.y.Value = (short)(14524 + offsetY);
					materialSpherePos.z.Value = (short)(16261 + offsetZ);

					BoundingBox materialBox = _sigLeaf.box;
					materialBox.minX.Value = (short)(6476 + offsetX);
					materialBox.minY.Value = (short)(13025 + offsetY);
					materialBox.minZ.Value = (short)(15031 + offsetZ);
					materialBox.maxX.Value = (short)(6476 + offsetX);
					materialBox.maxY.Value = (short)(16022 + offsetY);
					materialBox.maxZ.Value = (short)(17492 + offsetZ);

					Position spectralSpherePos = _sigLeaf.spectralSphereNoSq.position;
					spectralSpherePos.x.Value = (short)(6476 + offsetX);
					spectralSpherePos.y.Value = (short)(14524 + offsetY);
					spectralSpherePos.z.Value = (short)(16261 + offsetZ);

					BoundingBox spectralBox = _sigLeaf.spectralBox;
					spectralBox.minX.Value = (short)(6476 + offsetX);
					spectralBox.minY.Value = (short)(13025 + offsetY);
					spectralBox.minZ.Value = (short)(15031 + offsetZ);
					spectralBox.maxX.Value = (short)(6476 + offsetX);
					spectralBox.maxY.Value = (short)(16022 + offsetY);
					spectralBox.maxZ.Value = (short)(17492 + offsetZ);

					#endregion
				}

				#endregion

				#region FixCathy63

				if (file._Version == SR1_File.Version.Feb16 &&
					targetVersion == SR1_File.Version.Retail_PC &&
					//(migrateFlags & SR1_File.MigrateFlags.FixCathy63) != 0 &&
					level.Name == "cathy63")
				{
					List<ObjVertex> newVertices = new List<ObjVertex>();
					List<(byte, byte)> newUVs = new List<(byte, byte)>();
					List<ObjFace> newFaces = new List<ObjFace>();
					ushort[] textureIDs = file._Overrides.NewTextureIDs.Values.ToArray();

					#region Parsing

					string resourceName = "Recombobulator.Resources.cathy63mod.obj";
					System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
					using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourceName))
					using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
					{
						string line;
						int currentMtl = 0;
						while (!reader.EndOfStream)
						{
							line = reader.ReadLine();

							if (line.StartsWith("mtllib "))
							{
								continue;
							}

							if (line.StartsWith("o "))
							{
								continue;
							}

							if (line.StartsWith("v "))
							{
								string[] parts = line.Split(new char[] { ' ' }, 7);
								Console.WriteLine(string.Format("v = {0}, {1}, {2}, {3}, {4}, {5}", parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]));

								ObjVertex ov = new ObjVertex();
								ov.pos.x = (short)(1000f * float.Parse(parts[1]));
								ov.pos.y = (short)(1000f * float.Parse(parts[2]));
								ov.pos.z = (short)(1000f * float.Parse(parts[3]));
								ov.r = (byte)(255f * float.Parse(parts[4]));
								ov.g = (byte)(255f * float.Parse(parts[5]));
								ov.b = (byte)(255f * float.Parse(parts[6]));
								newVertices.Add(ov);
								continue;
							}

							if (line.StartsWith("vn "))
							{
								string[] parts = line.Split(new char[] { ' ' }, 4);
								Console.WriteLine(string.Format("vn = {0}, {1}, {2}", parts[1], parts[2], parts[3]));

								continue;
							}

							if (line.StartsWith("vt "))
							{
								string[] parts = line.Split(new char[] { ' ', '_' }, 3);
								Console.WriteLine(string.Format("vt = {0}, {1}", parts[1], parts[2]));

								byte u = (byte)(255f * float.Parse(parts[1]));
								byte v = (byte)(255f - (255f * float.Parse(parts[2])));
								newUVs.Add((u, v));
								continue;
							}

							if (line.StartsWith("s "))
							{
								continue;
							}

							if (line.StartsWith("usemtl "))
							{
								string[] parts = line.Split(new char[] { ' ', '_' }, 3);
								Console.WriteLine(string.Format("usemtl = {0}", parts[2]));

								currentMtl = int.Parse(parts[2]);
								continue;
							}

							if (line.StartsWith("f "))
							{
								string[] parts = line.Split(new char[] { ' ', '/' }, 10);
								Console.WriteLine(string.Format("f = [ {0}, {1}, {2} ], [ {3}, {4}, {5} ], [ {6}, {7}, {8} ]", parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], parts[9]));

								ObjFace of = new ObjFace();
								of.v0 = int.Parse(parts[1]) - 1;
								of.uv0 = int.Parse(parts[2]) - 1;
								of.v1 = int.Parse(parts[4]) - 1;
								of.uv1 = int.Parse(parts[5]) - 1;
								of.v2 = int.Parse(parts[7]) - 1;
								of.uv2 = int.Parse(parts[8]) - 1;
								of.mtl = currentMtl;
								newFaces.Add(of);
								continue;
							}

							if (line.StartsWith("l "))
							{
								string[] parts = line.Split(new char[] { ' ' }, 3);
								Console.WriteLine(string.Format("l = {0}, {1}", parts[1], parts[2]));
								continue;
							}
						}
					}

					Console.WriteLine(string.Format("Vertices: {0}", newVertices.Count));
					Console.WriteLine(string.Format("UVs: {0}", newUVs.Count));
					Console.WriteLine(string.Format("Faces: {0}", newFaces.Count));

					#endregion

					short minX = short.MaxValue;
					short minY = short.MaxValue;
					short minZ = short.MaxValue;
					short maxX = short.MinValue;
					short maxY = short.MinValue;
					short maxZ = short.MinValue;

					foreach (ObjVertex ov in newVertices)
					{
						TVertex v = new TVertex();
						v.vertex.x.Value = (short)ov.pos.x;
						v.vertex.y.Value = (short)-ov.pos.z;
						v.vertex.z.Value = (short)ov.pos.y;
						v.r0.Value = ov.r;
						v.g0.Value = ov.g;
						v.b0.Value = ov.b;
						v.code.Value = 0x34;
						int r = (v.r0.Value >> 3) << 0;
						int g = (v.g0.Value >> 3) << 5;
						int b = (v.b0.Value >> 3) << 10;
						int color = r | g | b;
						v.rbg15.Value = (ushort)color;
						_vertices.Add(v);

						minX = Math.Min(minX, v.vertex.x.Value);
						minY = Math.Min(minY, v.vertex.y.Value);
						minZ = Math.Min(minZ, v.vertex.z.Value);
						maxX = Math.Max(maxX, v.vertex.x.Value);
						maxY = Math.Max(maxY, v.vertex.y.Value);
						maxZ = Math.Max(maxZ, v.vertex.z.Value);

						MorphColor mc = new MorphColor();
						mc.morphColor15.Value = 0;
						_morphColors.Add(mc);
					}
					
					_moddedFaces = new List<TFace>();

					foreach (ObjFace of in newFaces)
					{
						ObjVector v00 = newVertices[of.v0].pos;
						ObjVector v01 = newVertices[of.v1].pos;
						ObjVector v02 = newVertices[of.v2].pos;
						ObjVector on0 = CalculateFaceNormal(v00, v01, v02);
						//short temp = on0.y;
						//on0.y = (short)-on0.z;
						//on0.z = temp;
						//ObjVertex v10 = new ObjVertex { x = v00.x, y = (short)-v00.z, z = v00.y };
						//ObjVertex v11 = new ObjVertex { x = v01.x, y = (short)-v01.z, z = v01.y };
						//ObjVertex v12 = new ObjVertex { x = v02.x, y = (short)-v02.z, z = v02.y };
						//ObjNormal on1 = CalculateFaceNormal(v10, v11, v12);
						ObjVector on1 = new ObjVector { x = on0.x, y = (short)-on0.z, z = on0.y };
						Normal n = new Normal();
						n.x.Value = (short)on1.x;
						n.y.Value = (short)on1.z;
						n.z.Value = (short)on1.y;
						int normalIndex = _normals.Count;
						bool isFlipped = DotProduct(on0, on1) < 0;
						Console.WriteLine("isFlipped = " + isFlipped.ToString());
						//if (isFlipped)
						//{
						//	normalIndex = -normalIndex;
						//}
						if (v00.y == v01.y && v00.y == v02.y)
						{
							Console.WriteLine("isFlat = true");
						}
						else
						{
							Console.WriteLine("isFlat = false");
						}
						_normals.Add(n);
						_morphNormals.Add(unchecked((ushort)normalIndex));

						TextureFT3 t = new TextureFT3();
						if (of.mtl > 0 &&
							of.mtl < textureIDs.Length)
						{
							t.tpage.Value = textureIDs[of.mtl];
						}
						else
						{
							t.tpage.Value = 0;
						}
						t.tpage.Value |= 0x2000; // UseAlphaMask
						t.attr2.Value = 0x0108;
						var uv0 = newUVs[of.uv0];
						var uv1 = newUVs[of.uv1];
						var uv2 = newUVs[of.uv2];
						t.u0.Value = uv0.Item1;
						t.v0.Value = uv0.Item2;
						t.u1.Value = uv1.Item1;
						t.v1.Value = uv1.Item2;
						t.u2.Value = uv2.Item1;
						t.v2.Value = uv2.Item2;
						_textures.Add(t);

						TFace f = new TFace();
						f.face.v0.Value = (ushort)(of.v0 + numVertices.Value);
						f.face.v1.Value = (ushort)(of.v1 + numVertices.Value);
						f.face.v2.Value = (ushort)(of.v2 + numVertices.Value);
						f.normal.Value = unchecked((ushort)normalIndex);
						//if (isFlipped)
						//{
						//	f.attr.Value = 0x20;
						//}
						f.textoff.Value = 0xFFFF;
						f.Texture = t;
						_moddedFaces.Add(f);
						_faces.Add(f);
					}

					numVertices.Value = _vertices.Count;
					numNormals.Value = _normals.Count;
					numFaces.Value = _faces.Count;

					// Create a leaf for the modded section. Only one is needed.
					_moddedLeaf = new BSPLeaf();

					// Leaves need flag 2!
					_moddedLeaf.flags.Value = 2;

					_moddedLeaf.faceList.Offset = 0;
					_moddedLeaf.faceList.Heuristic = PtrHeuristic.Explicit;
					_moddedLeaf.numFaces.Value = (short)_moddedFaces.Count;

					#region Bounds

					short centerX = (short)((minX + maxX) / 2);
					short centerY = (short)((minY + maxY) / 2);
					short centerZ = (short)((minZ + maxZ) / 2);
					int dX = maxX - minX;
					int dY = maxY - minY;
					int dZ = maxZ - minZ;
					ushort radius = (ushort)(Math.Sqrt((dX * dX) + (dY * dY) + (dZ * dZ)) / 2);

					_moddedLeaf.sphereNoSq.radius.Value = radius;
					Position materialSpherePos = _moddedLeaf.sphereNoSq.position;
					materialSpherePos.x.Value = centerX;
					materialSpherePos.y.Value = centerY;
					materialSpherePos.z.Value = centerZ;

					BoundingBox materialBox = _moddedLeaf.box;
					materialBox.minX.Value = minX;
					materialBox.minY.Value = minY;
					materialBox.minZ.Value = minZ;
					materialBox.maxX.Value = maxX;
					materialBox.maxY.Value = maxY;
					materialBox.maxZ.Value = maxZ;

					_moddedLeaf.spectralSphereNoSq.radius.Value = radius;
					Position spectralSpherePos = _moddedLeaf.spectralSphereNoSq.position;
					spectralSpherePos.x.Value = centerX;
					spectralSpherePos.y.Value = centerY;
					spectralSpherePos.z.Value = centerZ;

					BoundingBox spectralBox = _moddedLeaf.spectralBox;
					spectralBox.minX.Value = minX;
					spectralBox.minY.Value = minY;
					spectralBox.minZ.Value = minZ;
					spectralBox.maxX.Value = maxX;
					spectralBox.maxY.Value = maxY;
					spectralBox.maxZ.Value = maxZ;

					#endregion

					// Insert the BSPLeaf just before the signal leaf.
					uint sigLeafPosition = _sigLeaf.Start;
					file._MigrationStructures.Add(sigLeafPosition, _moddedLeaf);

					// Create a tree for the modded section. Only one is needed.
					_moddedTree = new BSPTree();

					// Set the modded BSPTree fields to the new modded leaves.
					_moddedTree.bspRoot.Offset = 0;
					_moddedTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
					_moddedTree.startLeaves.Offset = 0;
					_moddedTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
					_moddedTree.endLeaves.Offset = 0;
					_moddedTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
					_moddedTree.ID.Value = 1;

					// Insert the new BSPTree just before the signal tree.
					_bspTrees.InsertAt(1, _moddedTree);

					numBSPTrees.Value = _bspTrees.Count;

					BSPTree envTree = (BSPTree)_bspTrees[0];
					if (file._Structures.ContainsKey(envTree.startLeaves.Offset))
					{
						SR1_Structure structure = file._Structures[envTree.startLeaves.Offset];
						SR1_StructureSeries<BSPLeaf> envLeaves = structure as SR1_StructureSeries<BSPLeaf>;
						if (envLeaves != null)
						{
							int[] leavesToRemove = { 132, 133, 134, 135, 136, 137, 140, 141, 155 };
							foreach (int leafToRemove in leavesToRemove)
							{
								BSPLeaf leaf = (BSPLeaf)envLeaves[leafToRemove];
								leaf.sphereNoSq.radius.Value = 0;
								leaf.box.minX.Value = leaf.sphereNoSq.position.x.Value;
								leaf.box.minY.Value = leaf.sphereNoSq.position.y.Value;
								leaf.box.minZ.Value = leaf.sphereNoSq.position.z.Value;
								leaf.box.maxX.Value = leaf.sphereNoSq.position.x.Value;
								leaf.box.maxY.Value = leaf.sphereNoSq.position.y.Value;
								leaf.box.maxZ.Value = leaf.sphereNoSq.position.z.Value;
								leaf.spectralSphereNoSq.radius.Value = 0;
								leaf.spectralBox.minX.Value = leaf.spectralSphereNoSq.position.x.Value;
								leaf.spectralBox.minY.Value = leaf.spectralSphereNoSq.position.y.Value;
								leaf.spectralBox.minZ.Value = leaf.spectralSphereNoSq.position.z.Value;
								leaf.spectralBox.maxX.Value = leaf.spectralSphereNoSq.position.x.Value;
								leaf.spectralBox.maxY.Value = leaf.spectralSphereNoSq.position.y.Value;
								leaf.spectralBox.maxZ.Value = leaf.spectralSphereNoSq.position.z.Value;
								leaf.numFaces.Value = 0;
								leaf.faceList.Offset = _faces.End;
								leaf.faceList.Heuristic = PtrHeuristic.End;
							}
						}
					}
				}

				#endregion

				_portalList.numPortals.Value = newNumPortals;
			}

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
				_bspTrees = new SR1_StructureSeries<BSPTree>();

				#region EnvTree

				_envTree = new BSPTree();

				// Set the environment BSPTree fields to the existing root and leaves.
				_envTree.bspRoot.Offset = 0;
				_envTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
				_envTree.startLeaves.Offset = 0;
				_envTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
				_envTree.endLeaves.Offset = 0;
				_envTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
				_envTree.ID.Value = 0;

				_bspTrees.Add(_envTree);

				#endregion

				#region SunTree

				if (HasSunlightTree)
				{
					_sunFaces = new List<TFace>();

					foreach (TFace face in _faces)
					{
						if (face.IsSunlight)
						{
							TFace sunFace = new TFace();

							TFace.Copy(sunFace, face);
							sunFace.textoff.Value = 0xFFFF;
							sunFace.sortPush.Value = 1;

							_sunFaces.Add(sunFace);
						}
					}

					foreach (TFace sunFace in _sunFaces)
					{
						_faces.Add(sunFace);
					}

					// Create a leaf for the sun tree. Only one is needed.
					_sunLeaf = new BSPLeaf();

					CopyBSPRootToLeaf(_sunLeaf);

					// Leaves need flag 2!
					_sunLeaf.flags.Value = 2;

					_sunLeaf.faceList.Offset = 0;
					_sunLeaf.faceList.Heuristic = PtrHeuristic.Explicit;
					_sunLeaf.numFaces.Value = (short)_sunFaces.Count;

					// MigrateVersion is only called on MembersRead, which won't include
					// this new one, so do that here.
					_sunLeaf.MigrateVersion(file, targetVersion, migrateFlags);

					// Insert the BSPLeaf at the end of the leaves list.
					_bspLeaves.Add(_sunLeaf);

					_sunTree = new BSPTree();

					_sunTree.bspRoot.Offset = 0;
					_sunTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
					_sunTree.startLeaves.Offset = 0;
					_sunTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
					_sunTree.endLeaves.Offset = 0;
					_sunTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
					_sunTree.ID.Value = 1;

					// No collision and burn enemies.
					_sunTree.flags.Value = 0x0042;

					_bspTrees.Add(_sunTree);
				}

				#endregion

				#region SigTree

				if (HasSignalTree)
				{
					_sigFaces = new List<TFace>();

					bool needTerrainSignal = (_terrainSignal == null);

					foreach (TFace face in _faces)
					{
						if (face.IsInSignalGroup)
						{
							TFace sigFace = new TFace();

							Face.Copy(sigFace.face, face.face);
							sigFace.attr.Value = 0x40; // 0x44?
							sigFace.normal.Value = face.normal.Value;
							sigFace.textoff.Value = 0xFFFF;

							sigFace.IsInSignalGroup = true;

							sigFace.MultiSignal = face.MultiSignal;
							sigFace.Signal = face.Signal;
							sigFace.Portal = face.Portal;

							bool removeSignal = false;

							removeSignal |= sigFace.MultiSignal != null && sigFace.MultiSignal.OmitFromMigration;
							removeSignal |= sigFace.Signal != null && sigFace.Signal.OmitFromMigration;
							removeSignal |= sigFace.Portal != null && sigFace.Portal.OmitFromMigration;

							if (removeSignal)
							{
								sigFace.attr.Value = 0;

								sigFace.MultiSignal = null;
								sigFace.Signal = null;
								sigFace.Portal = null;
							}

							_sigFaces.Add(sigFace);

							if (needTerrainSignal && _multiSignals != null)
							{
								foreach (MultiSignal mSignal in _multiSignals)
								{
									if (face.MultiSignal != null && face.MultiSignal == mSignal)
									{
										if (_terrainSignal == null || mSignal.Start < _terrainSignal.Start)
										{
											_terrainSignal = mSignal;
										}

										break;
									}
								}
							}
						}
					}

					if (_terrainSignal != null)
					{
						signals.Offset = _terrainSignal.Start;
					}

					foreach (TFace sigFace in _sigFaces)
					{
						_faces.Add(sigFace);
					}

					// Create a leaf for the signal tree. Only one is needed.
					_sigLeaf = new BSPLeaf();

					CopyBSPRootToLeaf(_sigLeaf);

					// Leaves need flag 2!
					_sigLeaf.flags.Value = 2;

					_sigLeaf.faceList.Offset = 0;
					_sigLeaf.faceList.Heuristic = PtrHeuristic.Explicit;
					_sigLeaf.numFaces.Value = (short)_sigFaces.Count;

					// MigrateVersion is only called on MembersRead, which won't include
					// this new one, so do that here.
					_sigLeaf.MigrateVersion(file, targetVersion, migrateFlags);

					// Insert the BSPLeaf at the end of the leaves list.
					_bspLeaves.Add(_sigLeaf);

					_sigTree = new BSPTree();

					// Set the signal BSPTree fields to the new signal leaves.
					_sigTree.bspRoot.Offset = 0;
					_sigTree.bspRoot.Heuristic = PtrHeuristic.Explicit;
					_sigTree.startLeaves.Offset = 0;
					_sigTree.startLeaves.Heuristic = PtrHeuristic.Explicit;
					_sigTree.endLeaves.Offset = 0;
					_sigTree.endLeaves.Heuristic = PtrHeuristic.Explicit;
					_sigTree.ID.Value = -1;

					_bspTrees.Add(_sigTree);
				}

				#endregion

				numFaces.Value = _faces.Count;

				// Insert the BSPTree array where the root used to be.
				file._MigrationStructures.Add(endLeaves.Offset, _bspTrees);

				numBSPTrees.Value = _bspTrees.Count;
				BSPTreeArray.Offset = 0;
				BSPTreeArray.Heuristic = PtrHeuristic.Explicit;

				#endregion

				#region MorphColors

				// Create a new array of morph colors with one per vertex.
				SR1_StructureSeries<MorphColor> newMorphColors = new SR1_StructureSeries<MorphColor>();
				newMorphColors.SetPadding(_morphColors.GetPadding());
				newMorphColors.Align = _morphColors.Align;

				// The morph colors originally applies to a subset of the vertices,
				// but now they apply to every veretex, so copy the regular colors
				// from the vertices, then overwrite any found in the old morph
				// colors.
				foreach (TVertex vertex in _vertices)
				{
					MorphColor newMorphColor = new MorphColor();
					newMorphColor.morphColor15.Value = unchecked((short)vertex.rbg15.Value);
					newMorphColors.Add(newMorphColor);
				}

				foreach (MorphColor morphColor in _morphColors)
				{
					int vindex = morphColor.vindex.Value;
					if (vindex >= 0)
					{
						MorphColor newMorphColor = (MorphColor)newMorphColors[vindex];

						// MigrateVersion happens in Terrain before the MorphColors,
						// so those won't have been converted yet, and the new ones
						// aren't even in the list, so copy them here and migrate
						// them as well.
						MorphColor.Copy(newMorphColor, morphColor);
						newMorphColor.MigrateVersion(file, targetVersion, migrateFlags);
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
				SR1_PrimativeSeries<ushort> newMorphNormals = new SR1_PrimativeSeries<ushort>();
				newMorphNormals.SetPadding(_morphNormals.GetPadding());
				newMorphNormals.Align = _morphNormals.Align;

				// There were no morph normals originally, but they would be stored in
				// the same array, so just copy indices of regular ones from the faces.
				foreach (TFace face in _faces)
				{
					newMorphNormals.Add(face.normal.Value);
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
					if (_textures != null && _drMoveAniTex != null)
					{
						int numAniTextures = _drMoveAniTex.numAniTextures.Value;
						int numTextures = _textures.Count;

						for (int a = 0; a < numAniTextures; a++)
						{
							int refCountIndex = entryList.Count;
							int refCount = 0;
							for (int t = 0; t < numTextures; t++)
							{
								TextureFT3 texture = (TextureFT3)_textures[t];
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

					file._MigrationStructures.Add(endPosition, new TexAniAssocData(entryList));
					texAniAssocData.Offset = endPosition;
					texAniAssocData.Heuristic = PtrHeuristic.Migration;
				}
				else
				{
					if (aniList.Offset != 0)
					{
						file._Structures.Remove(aniList.Offset);
						aniList.Offset = 0;
					}

					file._MigrationStructures.Add(endPosition, new TexAniAssocData());
					texAniAssocData.Offset = endPosition;
					texAniAssocData.Heuristic = PtrHeuristic.Migration;
				}

				#endregion
			}
		}

		public override void MigratePointers(SR1_Writer writer, SR1_File.Version sourceVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigratePointers(writer, sourceVersion, migrateFlags);

			Level level = (Level)writer.File._Structures[0];

			if (sourceVersion == SR1_File.Version.Retail_PC &&
				writer.File._Version == SR1_File.Version.Retail_PC)
			{
				#region FixCity11

				if ((migrateFlags & SR1_File.MigrateFlags.FixCity11) != 0 &&
					writer.File._Structures[0].Name == "city11")
				{
					signals.Offset = _terrainSignal.NewStart;
				}

				#endregion
			}

			if (_faces != null)
			{
				foreach (TFace face in _faces)
				{
					writer.BaseStream.Position = face.textoff.NewStart;

					if (face.IsInSignalGroup)
					{
						if (_terrainSignal != null && face.MultiSignal != null)
						{
							uint offset = face.MultiSignal.NewStart - _terrainSignal.NewStart;
							writer.Write((ushort)offset);
						}
					}
					else
					{
						if (_textures != null && face.Texture != null)
						{
							uint offset = face.Texture.NewStart - _textures[0].NewStart;
							writer.Write((ushort)offset);
						}
					}
				}
			}

			if (sourceVersion < SR1_File.Version.Jan23 &&
				writer.File._Version >= SR1_File.Version.Jan23)
			{
				uint endLeavesOffset = _bspLeaves.NewEnd;

				if (HasSignalTree)
				{
					endLeavesOffset = _sigLeaf.NewStart;
				}

				if (HasSunlightTree)
				{
					endLeavesOffset = _sunLeaf.NewStart;
				}

				BSPTreeArray.Offset = _bspTrees.NewStart;

				_envTree.bspRoot.Offset = _bspRoot.NewStart;
				_envTree.startLeaves.Offset = _bspLeaves.NewStart;
				_envTree.endLeaves.Offset = endLeavesOffset;

				if (HasSunlightTree)
				{
					_sunTree.bspRoot.Offset = _sunLeaf.NewStart;
					_sunTree.startLeaves.Offset = _sunLeaf.NewStart;
					_sunTree.endLeaves.Offset = _sunLeaf.NewEnd;

					if (_sunFaces.Count > 0)
					{
						_sunLeaf.faceList.Offset = _sunFaces[0].NewStart;
					}
					else
					{
						_sunLeaf.faceList.Offset = _faces.NewEnd;
					}
				}

				if (HasSignalTree)
				{
					_sigTree.bspRoot.Offset = _sigLeaf.NewStart;
					_sigTree.startLeaves.Offset = _sigLeaf.NewStart;
					_sigTree.endLeaves.Offset = _sigLeaf.NewEnd;

					if (_sigFaces.Count > 0)
					{
						_sigLeaf.faceList.Offset = _sigFaces[0].NewStart;
					}
					else
					{
						_sigLeaf.faceList.Offset = _faces.NewEnd;
					}
				}
			}

			if (sourceVersion == SR1_File.Version.Feb16 &&
				writer.File._Version == SR1_File.Version.Retail_PC &&
				//(migrateFlags & SR1_File.MigrateFlags.FixCathy63) != 0 &&
				level.Name == "cathy63")
			{
				_moddedTree.bspRoot.Offset = _moddedLeaf.NewStart;
				_moddedTree.startLeaves.Offset = _moddedLeaf.NewStart;
				_moddedTree.endLeaves.Offset = _moddedLeaf.NewEnd;

				if (_moddedFaces.Count > 0)
				{
					_moddedLeaf.faceList.Offset = _moddedFaces[0].NewStart;
				}
				else
				{
					_moddedLeaf.faceList.Offset = _faces.NewEnd;
				}
			}
		}
	}
}
