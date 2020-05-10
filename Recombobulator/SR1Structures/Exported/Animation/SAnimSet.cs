using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SAnimSet : SR1_Structure
    {
        SR1_PointerSeries<SAnim> animSet = new SR1_PointerSeries<SAnim>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            animSet.Read(reader, this, "animSet");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            foreach (SR1_PointerBase pointer in animSet.List)
            {
                if (pointer.Offset != 0 && !reader.SAnimDictionary.ContainsKey(pointer.Offset))
                {
                    reader.SAnimDictionary.Add(pointer.Offset, pointer);
                }
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            animSet.Write(writer);
        }
    }
}
