using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class HInfo : SR1_Structure
    {
        SR1_Primative<int> numHFaces = new SR1_Primative<int>();
        SR1_Pointer<HFace> hfaceList = new SR1_Pointer<HFace>();
        SR1_Primative<int> numHSpheres = new SR1_Primative<int>();
        SR1_Pointer<HSphere> hsphereList = new SR1_Pointer<HSphere>();
        SR1_Primative<int> numHBoxes = new SR1_Primative<int>();
        SR1_Pointer<HBox> hboxList = new SR1_Pointer<HBox>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            numHFaces.Read(reader, this, "numHFaces");
            hfaceList.Read(reader, this, "hfaceList");
            numHSpheres.Read(reader, this, "numHSpheres");
            hsphereList.Read(reader, this, "hsphereList");
            numHBoxes.Read(reader, this, "numHBoxes");
            hboxList.Read(reader, this, "hboxList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new SR1_StructureArray<HFace>(numHFaces.Value).ReadFromPointer(reader, hfaceList);
            new SR1_StructureArray<HSphere>(numHSpheres.Value).ReadFromPointer(reader, hsphereList);
            new SR1_StructureArray<HBox>(numHBoxes.Value).ReadFromPointer(reader, hboxList);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            numHFaces.Write(writer);
            hfaceList.Write(writer);
            numHSpheres.Write(writer);
            hsphereList.Write(writer);
            numHBoxes.Write(writer);
            hboxList.Write(writer);
        }
    }
}
