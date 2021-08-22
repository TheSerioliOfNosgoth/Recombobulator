using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class WalBosTuneData : MonsterTuneData
    {
        WalBossAttributes attributes = new WalBossAttributes();
        SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            attributes.Read(reader, this, "attributes");

            if (reader.File._Version >= SR1_File.Version.Jul14)
            {
                unknown = new SR1_PrimativeArray<byte>(40);
            }
            else
            {
                unknown = new SR1_PrimativeArray<byte>(24);
            }
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