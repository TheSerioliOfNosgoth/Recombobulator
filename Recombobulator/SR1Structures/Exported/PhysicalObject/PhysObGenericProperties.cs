using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObGenericProperties : PhysObPropertiesBase
    {
        SR1_PrimativeArray<byte> data = new SR1_PrimativeArray<byte>(0);

        public PhysObGenericProperties(int length)
            : base()
        {
            data = new SR1_PrimativeArray<byte>(length);
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            data.Read(reader, this, "data");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            data.Write(writer);
        }
    }
}
