using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Segment : SR1_Structure
    {
        SR1_Primative<int> flags = new SR1_Primative<int>();
        SR1_Primative<short> firstTri = new SR1_Primative<short>();
        SR1_Primative<short> lastTri = new SR1_Primative<short>();
        SR1_Primative<short> firstVertex = new SR1_Primative<short>();
        SR1_Primative<short> lastVertex = new SR1_Primative<short>();
        SR1_Primative<short> px = new SR1_Primative<short>();
        SR1_Primative<short> py = new SR1_Primative<short>();
        SR1_Primative<short> pz = new SR1_Primative<short>();
        SR1_Primative<short> parent = new SR1_Primative<short>();
        SR1_Pointer<HInfo> hInfo = new SR1_Pointer<HInfo>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            flags.Read(reader, this, "flags");
            firstTri.Read(reader, this, "firstTri");
            lastTri.Read(reader, this, "lastTri");
            firstVertex.Read(reader, this, "firstVertex");
            lastVertex.Read(reader, this, "lastVertex");
            px.Read(reader, this, "px");
            py.Read(reader, this, "py");
            pz.Read(reader, this, "pz");
            this.parent.Read(reader, this, "parent");
            hInfo.Read(reader, this, "hInfo");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new HInfo().ReadFromPointer(reader, hInfo);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            flags.Write(writer);
            firstTri.Write(writer);
            lastTri.Write(writer);
            firstVertex.Write(writer);
            lastVertex.Write(writer);
            px.Write(writer);
            py.Write(writer);
            pz.Write(writer);
            parent.Write(writer);
            hInfo.Write(writer);
        }
    }
}
