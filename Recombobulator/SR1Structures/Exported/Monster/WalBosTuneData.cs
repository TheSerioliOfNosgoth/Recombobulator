using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class WalBosTuneData : MonsterTuneData
    {
        WalBossAttributes attributes = new WalBossAttributes();
        SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(40);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            attributes.Read(reader, this, "attributes");
            unknown.Read(reader, this, "unknown");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            attributes.Write(writer);
            unknown.Write(writer);
        }
    }
}