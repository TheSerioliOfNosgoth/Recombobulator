﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class BSPLeaf : SR1_Structure
	{
		public readonly Sphere sphere = new Sphere();
		public readonly Sphere_noSq sphereNoSq = new Sphere_noSq();
		public readonly SR1_Pointer<TFace> faceList = new SR1_Pointer<TFace>();
		public readonly SR1_Primative<short> numFaces = new SR1_Primative<short>();
		public readonly SR1_Primative<short> flags = new SR1_Primative<short>();
		public readonly BoundingBox box = new BoundingBox();
		public readonly BoundingBox spectralBox = new BoundingBox();
		public readonly Sphere spectralSphere = new Sphere();
		public readonly Sphere_noSq spectralSphereNoSq = new Sphere_noSq();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			sphere.Read(reader, this, "sphere", SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Read(reader, this, "sphere", SR1_File.Version.Jan23, SR1_File.Version.Next);
			faceList.Read(reader, this, "faceList");
			numFaces.Read(reader, this, "numFaces");
			flags.Read(reader, this, "flags");
			box.Read(reader, this, "box");
			spectralBox.Read(reader, this, "spectralBox");
			spectralSphere.Read(reader, this, "spectralSphere", SR1_File.Version.First, SR1_File.Version.Jan23);
			spectralSphereNoSq.Read(reader, this, "spectralSphere", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			sphere.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			faceList.Write(writer);
			numFaces.Write(writer);
			flags.Write(writer);
			box.Write(writer);
			spectralBox.Write(writer);
			spectralSphere.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			spectralSphereNoSq.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}
	}
}
