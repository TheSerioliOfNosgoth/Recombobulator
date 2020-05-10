using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class BoundingBox : SR1_Structure
    {
        SR1_Primative<short> minX = new SR1_Primative<short>();
        SR1_Primative<short> minY = new SR1_Primative<short>();
        SR1_Primative<short> minZ = new SR1_Primative<short>();
        SR1_Primative<short> maxX = new SR1_Primative<short>();
        SR1_Primative<short> maxY = new SR1_Primative<short>();
        SR1_Primative<short> maxZ = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            minX.Read(reader, this, "minX");
            minY.Read(reader, this, "minY");
            minZ.Read(reader, this, "minZ");
            maxX.Read(reader, this, "maxX");
            maxY.Read(reader, this, "maxY");
            maxZ.Read(reader, this, "maxZ");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            minX.Write(writer);
            minY.Write(writer);
            minZ.Write(writer);
            maxX.Write(writer);
            maxY.Write(writer);
            maxZ.Write(writer);
        }
    }
}
