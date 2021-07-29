using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MCardMTableT : SR1_Structure
    {
        SR1_Primative<uint> data_size = new SR1_Primative<uint>();
        SR1_Primative<uint> initialize = new SR1_Primative<uint>();
        SR1_Primative<uint> terminate = new SR1_Primative<uint>();
        SR1_Primative<uint> begin = new SR1_Primative<uint>();
        SR1_Primative<uint> end = new SR1_Primative<uint>();
        SR1_Primative<uint> set_buffer = new SR1_Primative<uint>();
        SR1_Primative<uint> main = new SR1_Primative<uint>();
        SR1_Primative<uint> pause = new SR1_Primative<uint>();
        SR1_Primative<uint> versionID = new SR1_Primative<uint>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            data_size.Read(reader, this, "data_size");
            initialize.Read(reader, this, "initialize");
            terminate.Read(reader, this, "terminate");
            begin.Read(reader, this, "begin");
            end.Read(reader, this, "end");
            set_buffer.Read(reader, this, "set_buffer");
            main.Read(reader, this, "main");
            pause.Read(reader, this, "pause");
            versionID.Read(reader, this, "versionID");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            data_size.Write(writer);
            initialize.Write(writer);
            terminate.Write(writer);
            begin.Write(writer);
            end.Write(writer);
            set_buffer.Write(writer);
            main.Write(writer);
            pause.Write(writer);
            versionID.Write(writer);
        }
    }
}
