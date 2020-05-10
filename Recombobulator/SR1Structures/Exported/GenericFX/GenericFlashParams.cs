using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericFlashParams : SR1_Structure
    {
        SR1_Primative<short> type = new SR1_Primative<short>();
        SR1_Primative<short> timeToColor = new SR1_Primative<short>();
        SR1_Primative<int> color = new SR1_Primative<int>();
        SR1_Primative<short> timeAtColor = new SR1_Primative<short>();
        SR1_Primative<short> timeFromColor = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
            timeToColor.Read(reader, this, "timeToColor");
            color.Read(reader, this, "color");
            timeAtColor.Read(reader, this, "timeAtColor");
            timeFromColor.Read(reader, this, "timeFromColor");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
            timeToColor.Write(writer);
            color.Write(writer);
            timeAtColor.Write(writer);
            timeFromColor.Write(writer);
        }
    }
}
