using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterCombatAttributes : SR1_Structure
    {
        SR1_Primative<short> stunTime = new SR1_Primative<short>();
        SR1_Primative<short> damageTime = new SR1_Primative<short>();
        SR1_Primative<short> recovery = new SR1_Primative<short>();
        SR1_Primative<short> surpriseTime = new SR1_Primative<short>();
        SR1_Primative<short> grabTime = new SR1_Primative<short>();
        SR1_Primative<short> combatRange = new SR1_Primative<short>();
        SR1_Primative<short> surpriseRange = new SR1_Primative<short>();
        SR1_Primative<short> allyRange = new SR1_Primative<short>();
        SR1_Primative<short> enemyAttackRange = new SR1_Primative<short>();
        SR1_Primative<short> enemyRunAttackRange = new SR1_Primative<short>();
        SR1_Primative<short> preferredCombatRange = new SR1_Primative<short>();
        SR1_Primative<short> suckPower = new SR1_Primative<short>();
        SR1_Primative<short> suckRange = new SR1_Primative<short>();
        SR1_Primative<short> suckTime = new SR1_Primative<short>();
        SR1_Primative<sbyte> hitPoints = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> numAttacks = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> missileAttack = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> ambushAttack = new SR1_Primative<sbyte>();
        SR1_PrimativeArray<sbyte> evadeProbability = new SR1_PrimativeArray<sbyte>(12);
        MonsterAttackList attackList = new MonsterAttackList();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            stunTime.Read(reader, this, "stunTime");
            damageTime.Read(reader, this, "damageTime");
            recovery.Read(reader, this, "recovery");
            surpriseTime.Read(reader, this, "surpriseTime");
            grabTime.Read(reader, this, "grabTime");
            combatRange.Read(reader, this, "combatRange");
            surpriseRange.Read(reader, this, "surpriseRange");
            allyRange.Read(reader, this, "allyRange");
            enemyAttackRange.Read(reader, this, "enemyAttackRange");
            SR1_Structure temp = reader.File._Structures[reader.Object.data.Offset];
            enemyRunAttackRange.Read(reader, this, "enemyRunAttackRange", SR1_File.Version.Retail, SR1_File.Version.Next);
            preferredCombatRange.Read(reader, this, "preferredCombatRange", SR1_File.Version.Retail, SR1_File.Version.Next);
            suckPower.Read(reader, this, "suckPower");
            suckRange.Read(reader, this, "suckRange");
            suckTime.Read(reader, this, "suckTime");
            hitPoints.Read(reader, this, "hitPoints");
            numAttacks.Read(reader, this, "numAttacks");
            missileAttack.Read(reader, this, "missileAttack");
            ambushAttack.Read(reader, this, "ambushAttack");
            evadeProbability.Read(reader, this, "evadeProbability");
            attackList.Read(reader, this, "attackList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            stunTime.Write(writer);
            damageTime.Write(writer);
            recovery.Write(writer);
            surpriseTime.Write(writer);
            grabTime.Write(writer);
            combatRange.Write(writer);
            surpriseRange.Write(writer);
            allyRange.Write(writer);
            enemyAttackRange.Write(writer);
            enemyRunAttackRange.Write(writer, SR1_File.Version.Retail, SR1_File.Version.Next);
            preferredCombatRange.Write(writer, SR1_File.Version.Retail, SR1_File.Version.Next);
            suckPower.Write(writer);
            suckRange.Write(writer);
            suckTime.Write(writer);
            hitPoints.Write(writer);
            numAttacks.Write(writer);
            missileAttack.Write(writer);
            ambushAttack.Write(writer);
            evadeProbability.Write(writer);
            attackList.Write(writer);
        }
    }
}
