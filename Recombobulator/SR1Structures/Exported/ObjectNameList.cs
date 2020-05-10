using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ObjectNameList : SR1_Structure
    {
        List<SR1_PrimativeArray<char>> _List = new List<SR1_PrimativeArray<char>>();
        SR1_PrimativePointer<char> listStart = new SR1_PrimativePointer<char>();
        SR1_Primative<int> pad = new SR1_Primative<int>();

        public ObjectNameList()
        {
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            while (true)
            {
                byte next = reader.ReadByte();
                reader.BaseStream.Position--;

                if (next == 0xFF)
                {
                    break;
                }

                SR1_PrimativeArray<char> name = new SR1_PrimativeArray<char>(16);
                name.Read(reader, this, "[" + _List.Count.ToString() + "]");
                _List.Add(name);
            }

            pad.Read(reader, this, "pad");
            listStart.Read(reader, this, "listStart");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            for (int i = 0; i < _List.Count; i++)
            {
                _List[i].Write(writer);
            }

            pad.Write(writer);
            listStart.Write(writer);
        }
    }
}
