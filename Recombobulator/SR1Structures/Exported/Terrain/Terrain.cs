using System.Collections.Generic;

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
		SR1_PrimativeArray<ushort> _morphNormals = null;
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

		// Added when converting to proto builds.
		BSPTree _envTree = null;
		BSPTree _sunTree = null;
		BSPLeaf _sunLeaf = null;
		List<TFace> _sunFaces = null;
		List<TFace> _sigFaces = null;

		// The first signal referenced by the terrain.
		MultiSignal _terrainSignal = null;

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

		private void CopyBSPRootToLoaf(BSPLeaf leaf)
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

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
			uint position = lastStructure.End;

			base.MigrateVersion(file, targetVersion, migrateFlags);

			Level level = (Level)file._Structures[0];
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
						file._Structures[0].Name == "city10")
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
						file._Structures[0].Name == "city11")
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
						file._Structures[0].Name == "lair33")
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
						file._Structures[0].Name == "retreat6")
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

						//MultiSignal newMultiSignal = new MultiSignal();
						//newMultiSignal.numSignals.Value = 1;
						//newMultiSignal.signalNum.Value = 2;
						//newMultiSignal.flags.Value = 0;
						//Signal newSignal = new Signal();
						//newSignal.id.Value = 18;
						//SignalStreamLevel newSignalStreamLevel = new SignalStreamLevel();
						//newSignalStreamLevel.currentnum.Value = 2;
						//newSignalStreamLevel.streamID.Value = 171;
						//newSignalStreamLevel.toname.SetReadMax(true);
						//newSignalStreamLevel.toname.SetText("city12,1", 16);
						//newSignal.data = newSignalStreamLevel;
						//newMultiSignal.signalList.Add(newSignal);
						//newMultiSignal.pad.Value = 15;
					}

					#endregion
				}

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

					CopyBSPRootToLoaf(_sunLeaf);

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

					CopyBSPRootToLoaf(_sigLeaf);

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
				newMorphColors.SetPadding(4);

				// The morph colors originally applies to a subset of the vertices,
				// but now they apply to every veretex, so copy the regular colors
				// from the vertices, then overwrite any found in the old morph
				// colors.
				var vertices = (SR1_StructureSeries<TVertex>)file._Structures[vertexList.Offset];
				foreach (TVertex vertex in vertices)
				{
					MorphColor morphColor = new MorphColor();
					morphColor.morphColor15.Value = unchecked((short)vertex.rbg15.Value);
					newMorphColors.Add(morphColor);
				}

				var oldMorphColors = (SR1_StructureSeries<MorphColor>)file._Structures[MorphColorList.Offset];
				foreach (MorphColor oldMorphColor in oldMorphColors)
				{
					int vindex = oldMorphColor.vindex.Value;
					if (vindex >= 0)
					{
						MorphColor morphColor = (MorphColor)newMorphColors[vindex];

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

		public override void MigratePointers(SR1_Writer writer, SR1_File.Version sourceVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigratePointers(writer, sourceVersion, migrateFlags);

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
		}
	}
}
