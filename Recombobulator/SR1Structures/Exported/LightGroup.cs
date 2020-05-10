using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class LightGroup : SR1_Structure
    {
        Matrix lightMatrix = new Matrix();
        Matrix colorMatrix = new Matrix();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            lightMatrix.Read(reader, this, "lightMatrix");
            colorMatrix.Read(reader, this, "colorMatrix");

            reader.BaseStream.Position += 4;
            End = (uint)reader.BaseStream.Position;
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            lightMatrix.Write(writer);
            colorMatrix.Write(writer);
            writer.Write(0x00000000u);
        }
    }
}
