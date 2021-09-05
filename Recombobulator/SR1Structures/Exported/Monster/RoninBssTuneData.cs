using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class RoninBssTuneData : MonsterTuneData
    {
        SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(48);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            if (reader.File._Version >= SR1_File.Version.Jun01)
            {
                unknown = new SR1_PrimativeArray<byte>(48);
            }
            else if (reader.File._Version >= SR1_File.Version.May12)
            {
                unknown = new SR1_PrimativeArray<byte>(44);
            }
            else
            {
                unknown = new SR1_PrimativeArray<byte>(20);
            }

            unknown.Read(reader, this, "unknown");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            unknown.Write(writer);
        }
    }
}