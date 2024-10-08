﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Model : SR1_Structure
	{
		public readonly SR1_Primative<int> numVertices = new SR1_Primative<int>();
		public readonly SR1_Pointer<MVertex> vertexList = new SR1_Pointer<MVertex>();
		public readonly SR1_Primative<int> numNormals = new SR1_Primative<int>();
		public readonly SR1_Pointer<SVectorNoPad> normalList = new SR1_Pointer<SVectorNoPad>();
		public readonly SR1_Primative<int> numFaces = new SR1_Primative<int>();
		public readonly SR1_Pointer<MFace> faceList = new SR1_Pointer<MFace>();
		public readonly SR1_Primative<int> numSegments = new SR1_Primative<int>();
		public readonly SR1_Pointer<Segment> segmentList = new SR1_Pointer<Segment>();
		public readonly SR1_Pointer<AniTex> aniTextures = new SR1_Pointer<AniTex>();
		public readonly SR1_Primative<short> maxRad = new SR1_Primative<short>();
		public readonly SR1_Primative<short> pad = new SR1_Primative<short>();
		public readonly SR1_Primative<int> maxRadSq = new SR1_Primative<int>();
		public readonly SR1_Pointer<MultiSpline> multiSpline = new SR1_Pointer<MultiSpline>();
		public readonly SR1_Pointer<TextureMT3> startTextures = new SR1_Pointer<TextureMT3>(PtrHeuristic.Start);
		public readonly SR1_Pointer<TextureMT3> endTextures = new SR1_Pointer<TextureMT3>(PtrHeuristic.End);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.Model == null)
			{
				reader.Model = this;
			}

			numVertices.Read(reader, this, "numVertices");
			vertexList.Read(reader, this, "vertexList");
			numNormals.Read(reader, this, "numNormals");
			normalList.Read(reader, this, "normalList");
			numFaces.Read(reader, this, "numFaces");
			faceList.Read(reader, this, "faceList");
			numSegments.Read(reader, this, "numSegments");
			segmentList.Read(reader, this, "segmentList");
			aniTextures.Read(reader, this, "aniTextures");
			maxRad.Read(reader, this, "maxRad");
			pad.Read(reader, this, "pad");
			maxRadSq.Read(reader, this, "maxRadSq");
			multiSpline.Read(reader, this, "multiSpline");
			startTextures.Read(reader, this, "startTextures");
			endTextures.Read(reader, this, "endTextures");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new SR1_StructureArray<MVertex>(numVertices.Value).ReadFromPointer(reader, vertexList);
			SR1_Structure normalsStruct = new SR1_StructureArray<SVectorNoPad>(numNormals.Value).ReadFromPointer(reader, normalList);
			new SR1_StructureArray<Segment>(numSegments.Value).ReadFromPointer(reader, segmentList);
			new AniTex().ReadFromPointer(reader, aniTextures);

			// This needs to be at the end so that other structures can be checked for first.
			// The padding was causing issues when it was at the very end of the file.
			if (normalsStruct.End != startTextures.Offset &&
				normalsStruct.End != reader.BaseStream.Length && !reader.File._Structures.ContainsKey(normalsStruct.End))
			{
				new SR1_PrimativeArray<byte>(0).SetPadding(4).ReadOrphan(reader, normalsStruct.End);
			}

			SR1_StructureArray<MFace> faces = new SR1_StructureArray<MFace>(numFaces.Value);
			faces.ReadFromPointer(reader, faceList);
			SR1_StructureSeries<TextureMT3> textures = new SR1_StructureSeries<TextureMT3>();
			textures.ReadFromPointer(reader, startTextures, endTextures);

			foreach (MFace face in faces)
			{
				uint texturePosition = face.texture.Offset;
				if (texturePosition != 0)
				{
					uint textureSize = 16;
					if (texturePosition >= startTextures.Offset && texturePosition <= (endTextures.Offset - textureSize))
					{
						uint textureOffset = texturePosition - startTextures.Offset;
						int textureIndex = (int)(textureOffset / textureSize);

						if (textures != null && textureIndex < textures.Count)
						{
							face.Texture = (TextureMT3)textures[textureIndex];
							face.Texture.NumReferences++;
						}
					}
				}
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numVertices.Write(writer);
			vertexList.Write(writer);
			numNormals.Write(writer);
			normalList.Write(writer);
			numFaces.Write(writer);
			faceList.Write(writer);
			numSegments.Write(writer);
			segmentList.Write(writer);
			aniTextures.Write(writer);
			maxRad.Write(writer);
			pad.Write(writer);
			maxRadSq.Write(writer);
			multiSpline.Write(writer);
			startTextures.Write(writer);
			endTextures.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				if (aniTextures.Offset != 0)
				{
					file._Structures.Remove(aniTextures.Offset);
					aniTextures.Offset = 0;
				}
			}
		}
	}
}
