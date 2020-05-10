using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterAttackList : SR1_Structure
    {
        SR1_PrimativeArray<byte> unknown0 = new SR1_PrimativeArray<byte>(3);
        SR1_Primative<byte> unknown1 = new SR1_Primative<byte>();
        SR1_PrimativeArray<byte> data = new SR1_PrimativeArray<byte>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            unknown0.Read(reader, this, "unknown0");
            unknown1.Read(reader, this, "unknown1");

            int bufferLength = 0;
            long oldPos = reader.BaseStream.Position;

            while (true)
            {
                bufferLength++;

                byte next = reader.ReadByte();
                if (next == 0xD3)
                {
                    break;
                }
            }

            reader.BaseStream.Position = oldPos;

            data = new SR1_PrimativeArray<byte>(bufferLength);
            data.SetPadding(4);
            data.Read(reader, this, "data");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            unknown0.Write(writer);
            unknown1.Write(writer);
            data.Write(writer);
        }
    }
}
