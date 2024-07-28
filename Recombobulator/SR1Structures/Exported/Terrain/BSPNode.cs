using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class BSPNode : SR1_Structure
	{
		Sphere sphere = new Sphere();
		Sphere_noSq sphereNoSq = new Sphere_noSq();
		public readonly SR1_Primative<short> a = new SR1_Primative<short>();
		public readonly SR1_Primative<short> b = new SR1_Primative<short>();
		public readonly SR1_Primative<short> c = new SR1_Primative<short>();
		public readonly SR1_Primative<short> flags = new SR1_Primative<short>();
		public readonly SR1_Primative<int> d = new SR1_Primative<int>();
		public readonly SR1_Pointer<BSPNode> front = new SR1_Pointer<BSPNode>();
		public readonly SR1_Pointer<BSPNode> back = new SR1_Pointer<BSPNode>();
		public readonly Sphere spectralSphere = new Sphere();
		public readonly Sphere_noSq spectralSphereNoSq = new Sphere_noSq();
		public readonly SR1_Primative<short> front_spectral_error = new SR1_Primative<short>();
		public readonly SR1_Primative<short> back_spectral_error = new SR1_Primative<short>();
		public readonly SR1_Primative<short> front_material_error = new SR1_Primative<short>();
		public readonly SR1_Primative<short> back_material_error = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			sphere.Read(reader, this, "sphere", SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Read(reader, this, "sphere", SR1_File.Version.Jan23, SR1_File.Version.Next);
			a.Read(reader, this, "a");
			b.Read(reader, this, "b");
			c.Read(reader, this, "c");
			flags.Read(reader, this, "flags");
			d.Read(reader, this, "d");
			front.Read(reader, this, "front");
			back.Read(reader, this, "back");
			spectralSphere.Read(reader, this, "spectralSphere", SR1_File.Version.First, SR1_File.Version.Jan23);
			spectralSphereNoSq.Read(reader, this, "spectralSphere", SR1_File.Version.Jan23, SR1_File.Version.Next);
			front_spectral_error.Read(reader, this, "front_spectral_error", SR1_File.Version.Jan23, SR1_File.Version.Next);
			back_spectral_error.Read(reader, this, "back_spectral_error", SR1_File.Version.Jan23, SR1_File.Version.Next);
			front_material_error.Read(reader, this, "front_material_error", SR1_File.Version.Jan23, SR1_File.Version.Next);
			back_material_error.Read(reader, this, "back_material_error", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			sphere.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			a.Write(writer);
			b.Write(writer);
			c.Write(writer);
			flags.Write(writer);
			d.Write(writer);
			front.Write(writer);
			back.Write(writer);
			spectralSphere.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			spectralSphereNoSq.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			front_spectral_error.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			back_spectral_error.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			front_material_error.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			back_material_error.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}
	}
}
