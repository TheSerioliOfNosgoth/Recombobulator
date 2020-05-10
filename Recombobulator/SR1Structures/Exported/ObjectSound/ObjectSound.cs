using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ObjectSound : SR1_Structure
    {
        SR1_Primative<byte> type = new SR1_Primative<byte>();

        public object CreateReplacementObject()
        {
            if (type.Value == 0)
            {
                return new ObjectPeriodicSound();
            }
            else if (type.Value == 1)
            {
                return new ObjectEventSound();
            }
            else if (type.Value < 5)
            {
                return new ObjectOneShotSound();
            }

            return new ObjectDummySound();
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
        }
    }
}
