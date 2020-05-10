using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class TVertex : SR1_Structure
    {
        Vertex vertex = new Vertex();
        SR1_Primative<ushort> rbg15 = new SR1_Primative<ushort>();
        SR1_Primative<byte> r0 = new SR1_Primative<byte>();
        SR1_Primative<byte> g0 = new SR1_Primative<byte>();
        SR1_Primative<byte> b0 = new SR1_Primative<byte>();
        SR1_Primative<byte> code = new SR1_Primative<byte>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            vertex.Read(reader, this, "vertex");
            rbg15.Read(reader, this, "rbg15");
            r0.Read(reader, this, "r0");
            g0.Read(reader, this, "g0");
            b0.Read(reader, this, "b0");
            code.Read(reader, this, "code");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            vertex.Write(writer);
            rbg15.Write(writer);
            r0.Write(writer);
            g0.Write(writer);
            b0.Write(writer);
            code.Write(writer);
        }
    }
}
