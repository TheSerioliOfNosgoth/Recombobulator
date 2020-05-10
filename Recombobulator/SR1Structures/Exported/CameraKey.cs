using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class CameraKey : SR1_Structure
    {
        SR1_Primative<short> x = new SR1_Primative<short>();
        SR1_Primative<short> y = new SR1_Primative<short>();
        SR1_Primative<short> z = new SR1_Primative<short>();
        SR1_Primative<short> id = new SR1_Primative<short>();
        SR1_Primative<short> rx = new SR1_Primative<short>();
        SR1_Primative<short> ry = new SR1_Primative<short>();
        SR1_Primative<short> rz = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_Primative<short> tx = new SR1_Primative<short>();
        SR1_Primative<short> ty = new SR1_Primative<short>();
        SR1_Primative<short> tz = new SR1_Primative<short>();
        SR1_Primative<short> pad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            x.Read(reader, this, "x");
            y.Read(reader, this, "y");
            z.Read(reader, this, "z");
            id.Read(reader, this, "id");
            rx.Read(reader, this, "rx");
            ry.Read(reader, this, "ry");
            rz.Read(reader, this, "rz");
            flags.Read(reader, this, "flags");
            tx.Read(reader, this, "tx");
            ty.Read(reader, this, "ty");
            tz.Read(reader, this, "tz");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            x.Write(writer);
            y.Write(writer);
            z.Write(writer);
            id.Write(writer);
            rx.Write(writer);
            ry.Write(writer);
            rz.Write(writer);
            flags.Write(writer);
            tx.Write(writer);
            ty.Write(writer);
            tz.Write(writer);
            pad.Write(writer);
        }
    }
}
