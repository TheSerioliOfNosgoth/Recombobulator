using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class BSPNode : SR1_Structure
    {
        Sphere_noSq sphere = new Sphere_noSq();
        SR1_Primative<short> a = new SR1_Primative<short>();
        SR1_Primative<short> b = new SR1_Primative<short>();
        SR1_Primative<short> c = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_Primative<int> d = new SR1_Primative<int>();
        SR1_Pointer<BSPNode> front = new SR1_Pointer<BSPNode>();
        SR1_Pointer<BSPNode> back = new SR1_Pointer<BSPNode>();
        Sphere_noSq spectralSphere = new Sphere_noSq();
        SR1_Primative<short> front_spectral_error = new SR1_Primative<short>();
        SR1_Primative<short> back_spectral_error = new SR1_Primative<short>();
        SR1_Primative<short> front_material_error = new SR1_Primative<short>();
        SR1_Primative<short> back_material_error = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sphere.Read(reader, this, "sphere");
            a.Read(reader, this, "a");
            b.Read(reader, this, "b");
            c.Read(reader, this, "c");
            flags.Read(reader, this, "flags");
            d.Read(reader, this, "d");
            front.Read(reader, this, "front");
            back.Read(reader, this, "back");
            spectralSphere.Read(reader, this, "spectralSphere");
            front_spectral_error.Read(reader, this, "front_spectral_error");
            back_spectral_error.Read(reader, this, "back_spectral_error");
            front_material_error.Read(reader, this, "front_material_error");
            back_material_error.Read(reader, this, "back_material_error");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sphere.Write(writer);
            a.Write(writer);
            b.Write(writer);
            c.Write(writer);
            flags.Write(writer);
            d.Write(writer);
            front.Write(writer);
            back.Write(writer);
            spectralSphere.Write(writer);
            front_spectral_error.Write(writer);
            back_spectral_error.Write(writer);
            front_material_error.Write(writer);
            back_material_error.Write(writer);
        }
    }
}
