using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObWeaponAttributes : SR1_Structure
    {
        SR1_Primative<int> Damage = new SR1_Primative<int>();
        SR1_Primative<int> AltDamage = new SR1_Primative<int>();
        SR1_Primative<short> knockBackDistance = new SR1_Primative<short>();
        SR1_Primative<sbyte> knockBackFrames = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> dropSound = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> Class = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> ThrowSphere = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> LeftHandSphere = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> RightHandSphere = new SR1_Primative<sbyte>();
        SR1_Pointer<PhysObLight> Light = new SR1_Pointer<PhysObLight>();
        PhysObSplinter splinter = new PhysObSplinter();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Damage.Read(reader, this, "Damage");
            AltDamage.Read(reader, this, "AltDamage");
            knockBackDistance.Read(reader, this, "knockBackDistance");
            knockBackFrames.Read(reader, this, "knockBackFrames");
            dropSound.Read(reader, this, "dropSound");
            Class.Read(reader, this, "Class");
            ThrowSphere.Read(reader, this, "ThrowSphere");
            LeftHandSphere.Read(reader, this, "LeftHandSphere");
            RightHandSphere.Read(reader, this, "RightHandSphere");
            Light.Read(reader, this, "Light");
            splinter.Read(reader, this, "splinter");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new PhysObLight().ReadFromPointer(reader, Light);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Damage.Write(writer);
            AltDamage.Write(writer);
            knockBackDistance.Write(writer);
            knockBackFrames.Write(writer);
            dropSound.Write(writer);
            Class.Write(writer);
            ThrowSphere.Write(writer);
            LeftHandSphere.Write(writer);
            RightHandSphere.Write(writer);
            Light.Write(writer);
            splinter.Write(writer);
        }
    }
}
