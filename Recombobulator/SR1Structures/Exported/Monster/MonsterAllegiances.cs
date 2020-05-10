using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterAllegiances : SR1_Structure
    {
        SR1_Primative<uint> enemies = new SR1_Primative<uint>();
        SR1_Primative<uint> allies = new SR1_Primative<uint>();
        SR1_Primative<uint> food = new SR1_Primative<uint>();
        SR1_Primative<uint> gods = new SR1_Primative<uint>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            enemies.Read(reader, this, "enemies");
            allies.Read(reader, this, "allies");
            food.Read(reader, this, "food");
            gods.Read(reader, this, "gods");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            enemies.Write(writer);
            allies.Write(writer);
            food.Write(writer);
            gods.Write(writer);
        }
    }
}
