using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class AttackItemSet : SR1_Structure
    {
        SR1_PointerSeries<AttackItem> animSet = new SR1_PointerSeries<AttackItem>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            animSet.Read(reader, this, "animSet");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            foreach (SR1_PointerBase pointer in animSet.List)
            {
                if (pointer.Offset != 0 && !reader.AttackAnimDictionary.ContainsKey(pointer.Offset))
                {
                    reader.AttackAnimDictionary.Add(pointer.Offset, pointer);
                }
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            animSet.Write(writer);
        }
    }
}
