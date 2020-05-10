using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SplineRotKey : SR1_Structure
    {
        SR1_Primative<short> count = new SR1_Primative<short>();
        G2Quat_Type q = new G2Quat_Type();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            count.Read(reader, this, "count");
            q.Read(reader, this, "q");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            count.Write(writer);
            q.Write(writer);
        }
    }
}
