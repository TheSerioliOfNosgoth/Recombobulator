using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class TFace : SR1_Structure
    {
        Face face = new Face();
        SR1_Primative<byte> attr = new SR1_Primative<byte>();
        SR1_Primative<sbyte> sortPush = new SR1_Primative<sbyte>();
        SR1_Primative<ushort> normal = new SR1_Primative<ushort>();
        SR1_Primative<ushort> textoff = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            face.Read(reader, this, "face");
            attr.Read(reader, this, "attr");
            sortPush.Read(reader, this, "sortPush");
            normal.Read(reader, this, "normal");
            textoff.Read(reader, this, "textoff");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            face.Write(writer);
            attr.Write(writer);
            sortPush.Write(writer);
            normal.Write(writer);
            textoff.Write(writer);
        }
    }
}
