using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class EventBasicObject : EventBaseObject
    {
        // Inherited SR1_Primative<short> id = new SR1_Primative<short>();
        SR1_Primative<short> pad = new SR1_Primative<short>();

        public object CreateReplacementObject()
        {
            switch (id.Value)
            {
                case 1:
                    return new EventInstanceObject();
                case 2:
                    return new EventWildCardObject();
                case 3:
                    return new EventEventObject();
                case 4:
                    return new EventTGroupObject();
                case 5:
                    return new EventWildCardObject();
                case 6:
                    return new EventWildCardObject();
                case 7:
                    return new EventVMOObject();
                default:
                    return new EventBasicObject();
            }
        }

        // Based on EVENT_AddObjectToStack:

        // 1(really 0) = EventInstanceObject (size 0x14)
        // 2(really 1) = EventBasicObject (size 0x02)
        // 3(really 2) = EventEventObject (size 0x0C)
        // 4(really 3) = EventTGroupObject (size 0x10)
        // 5(really 4) = ? (size 0x0C)? Chrono1 seems to confirm.
        // 6(really 5) = EventWildCardObject (size 0x0C)
        // 7(really 6) = Suspected union with EventTGroupObject, TGroupObject_alt_definition_0002 (offset=8) and AreaObject_alt_definition_0002(offset=12)
        // ^^sunrm1 has examples of 7.

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            id.Read(reader, this, "id");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            id.Write(writer);
            pad.Write(writer);
        }
    }
}
