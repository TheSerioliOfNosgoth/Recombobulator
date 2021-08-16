using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class NodeType : SR1_Structure
    {
        SR1_Pointer<NodeType> prev = new SR1_Pointer<NodeType>();
        SR1_Pointer<NodeType> next = new SR1_Pointer<NodeType>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            prev.Read(reader, this, "prev");
            next.Read(reader, this, "next");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            prev.Write(writer);
            next.Write(writer);
        }
    }
}
