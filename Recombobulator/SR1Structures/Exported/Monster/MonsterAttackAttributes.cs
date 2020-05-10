using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterAttackAttributes : SR1_Structure
    {
        SR1_Primative<short> attackRange = new SR1_Primative<short>();
        SR1_Primative<short> attackHeight = new SR1_Primative<short>();
        SR1_Primative<short> knockBackDistance = new SR1_Primative<short>();
        SR1_Primative<sbyte> knockBackDuration = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> damage = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sphereSegment = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sphereOnFrame = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sphereOnAnim = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sphereOffFrame = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sphereOffAnim = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> turnFrames = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> numAnims = new SR1_Primative<sbyte>();
        SR1_PrimativeArray<sbyte> attackProbability = new SR1_PrimativeArray<sbyte>(12);
        SR1_PrimativeArray<sbyte> animList = new SR1_PrimativeArray<sbyte>(5);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            attackRange.Read(reader, this, "attackRange");
            attackHeight.Read(reader, this, "attackHeight");
            knockBackDistance.Read(reader, this, "knockBackDistance");
            knockBackDuration.Read(reader, this, "knockBackDuration");
            damage.Read(reader, this, "damage");
            sphereSegment.Read(reader, this, "sphereSegment");
            sphereOnFrame.Read(reader, this, "sphereOnFrame");
            sphereOnAnim.Read(reader, this, "sphereOnAnim");
            sphereOffFrame.Read(reader, this, "sphereOffFrame");
            sphereOffAnim.Read(reader, this, "sphereOffAnim");
            turnFrames.Read(reader, this, "turnFrames");
            numAnims.Read(reader, this, "numAnims");
            attackProbability.Read(reader, this, "attackProbability");
            animList.Read(reader, this, "animList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            attackRange.Write(writer);
            attackHeight.Write(writer);
            knockBackDistance.Write(writer);
            knockBackDuration.Write(writer);
            damage.Write(writer);
            sphereSegment.Write(writer);
            sphereOnFrame.Write(writer);
            sphereOnAnim.Write(writer);
            sphereOffFrame.Write(writer);
            sphereOffAnim.Write(writer);
            turnFrames.Write(writer);
            numAnims.Write(writer);
            attackProbability.Write(writer);
            animList.Write(writer);
        }
    }
}
