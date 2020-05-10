using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class WalBossAttackDeltas : SR1_Structure
    {
        SR1_Primative<short> plusDelta = new SR1_Primative<short>();
        SR1_Primative<short> minusDelta = new SR1_Primative<short>();
        SR1_Primative<short> validAtHitPoint = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            plusDelta.Read(reader, this, "plusDelta");
            minusDelta.Read(reader, this, "minusDelta");
            validAtHitPoint.Read(reader, this, "validAtHitPoint");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            plusDelta.Write(writer);
            minusDelta.Write(writer);
            validAtHitPoint.Write(writer);
        }
    }
}
