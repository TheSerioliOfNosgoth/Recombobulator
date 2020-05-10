using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SAnim : SR1_Structure
    {
        public readonly SR1_Pointer<VAnim> anim = new SR1_Pointer<VAnim>();
        public readonly SR1_Pointer<SAnim> nextAnim = new SR1_Pointer<SAnim>();
        public readonly SR1_Primative<short> mode = new SR1_Primative<short>();
        public readonly SR1_Primative<short> data = new SR1_Primative<short>();
        public readonly SR1_Primative<short> speedAdjust = new SR1_Primative<short>();
        public readonly SR1_Primative<short> pad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            anim.Read(reader, this, "anim");
            nextAnim.Read(reader, this, "nextAnim");
            mode.Read(reader, this, "mode");
            data.Read(reader, this, "data");
            speedAdjust.Read(reader, this, "speedAdjust");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            anim.Write(writer);
            nextAnim.Write(writer);
            mode.Write(writer);
            data.Write(writer);
            speedAdjust.Write(writer);
            pad.Write(writer);
        }
    }
}
