using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class BSPLeaf : SR1_Structure
    {
        Sphere_noSq sphere = new Sphere_noSq();
        SR1_Pointer<TFace> faceList = new SR1_Pointer<TFace>();
        SR1_Primative<short> numFaces = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        BoundingBox box = new BoundingBox();
        BoundingBox spectralBox = new BoundingBox();
        Sphere_noSq spectralSphere = new Sphere_noSq();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sphere.Read(reader, this, "sphere");
            faceList.Read(reader, this, "faceList");
            numFaces.Read(reader, this, "numFaces");
            flags.Read(reader, this, "flags");
            box.Read(reader, this, "box");
            spectralBox.Read(reader, this, "spectralBox");
            spectralSphere.Read(reader, this, "spectralSphere");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sphere.Write(writer);
            faceList.Write(writer);
            numFaces.Write(writer);
            flags.Write(writer);
            box.Write(writer);
            spectralBox.Write(writer);
            spectralSphere.Write(writer);
        }
    }
}
