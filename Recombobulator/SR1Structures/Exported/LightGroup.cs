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
            lightMatrix.SetPadding(4).Read(reader, this, "lightMatrix");
            colorMatrix.SetPadding(4).Read(reader, this, "colorMatrix");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            lightMatrix.Write(writer);
            colorMatrix.Write(writer);
        }
    }
}
