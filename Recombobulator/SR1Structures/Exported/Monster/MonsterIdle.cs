using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterIdle : SR1_Structure
    {
        SR1_Primative<sbyte> anim = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> alertMod = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> probability = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> pad = new SR1_Primative<sbyte>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            anim.Read(reader, this, "anim");
            alertMod.Read(reader, this, "alertMod");
            probability.Read(reader, this, "probability");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            anim.Write(writer);
            alertMod.Write(writer);
            probability.Write(writer);
            pad.Write(writer);
        }
    }
}
