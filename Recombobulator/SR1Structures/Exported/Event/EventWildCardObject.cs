using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class EventWildCardObject : SR1_Structure
    {
        SR1_Primative<short> id = new SR1_Primative<short>();
        SR1_Primative<short> spad = new SR1_Primative<short>();
        SR1_Primative<int> unitID = new SR1_Primative<int>();
        SR1_Pointer<SR1_String> objectName = new SR1_Pointer<SR1_String>();
        SR1_String objectNameBuf = new SR1_String(12);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            id.Read(reader, this, "id");
            spad.Read(reader, this, "spad");
            unitID.Read(reader, this, "unitID");
            objectName.Read(reader, this, "objectName");

            if (objectName.Offset != 0) objectNameBuf.SetPadding(4).Read(reader, this, "objectNameBuf");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            id.Write(writer);
            spad.Write(writer);
            unitID.Write(writer);
            objectName.Write(writer);

            if (objectName.Offset != 0) objectNameBuf.Write(writer);
        }
    }
}
