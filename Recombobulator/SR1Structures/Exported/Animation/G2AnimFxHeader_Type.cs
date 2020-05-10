using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class G2AnimFxHeader_Type : SR1_Structure
    {
        public readonly SR1_Primative<byte> sizeAndSection = new SR1_Primative<byte>();
        public readonly SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
        public readonly SR1_Primative<ushort> keyframeID = new SR1_Primative<ushort>();

        SR1_PrimativeArray<byte> data = new SR1_PrimativeArray<byte>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sizeAndSection.Read(reader, this, "sizeAndSection");
            type.Read(reader, this, "type");

            if (type.Value != -1)
            {
                keyframeID.Read(reader, this, "keyframeID");

                long next = Start + ((sizeAndSection.Value & 0xF0) >> 2);
                int length = (int)(next - reader.BaseStream.Position);
                data = new SR1_PrimativeArray<byte>(length);
                data.Read(reader, this, "data");
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sizeAndSection.Write(writer);
            type.Write(writer);

            if (type.Value != -1)
            {
                keyframeID.Write(writer);
                data.Write(writer);
            }
        }
    }
}
