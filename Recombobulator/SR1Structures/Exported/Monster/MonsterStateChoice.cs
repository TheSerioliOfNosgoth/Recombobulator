using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterStateChoice : SR1_Structure
    {
        public readonly SR1_Primative<int> state = new SR1_Primative<int>();
        public readonly MonsterState functions = new MonsterState();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            state.Read(reader, this, "state");
            functions.Read(reader, this, "functions");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            state.Write(writer);
            functions.Write(writer);
        }
    }
}
