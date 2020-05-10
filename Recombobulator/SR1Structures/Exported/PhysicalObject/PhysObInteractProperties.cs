using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObInteractProperties : SR1_Structure
    {
        PhysObProperties Properties = new PhysObProperties();
        SR1_Primative<ushort> conditions = new SR1_Primative<ushort>();
        SR1_Primative<ushort> auxConditions = new SR1_Primative<ushort>();
        SR1_Primative<ushort> action = new SR1_Primative<ushort>();
        SR1_Primative<ushort> auxAction = new SR1_Primative<ushort>();
        SR1_Primative<byte> startAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> endAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> razielAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> razielAuxAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> frame = new SR1_Primative<byte>();
        SR1_Primative<byte> startAnimMode = new SR1_Primative<byte>();
        SR1_Primative<ushort> distance = new SR1_Primative<ushort>();
        SR1_Primative<ushort> newType = new SR1_Primative<ushort>();
        SR1_Primative<ushort> newClass = new SR1_Primative<ushort>();
        SR1_Primative<ushort> mode = new SR1_Primative<ushort>();
        SR1_Primative<ushort> engageXYDistance = new SR1_Primative<ushort>();
        SR1_Primative<short> engageZMinDelta = new SR1_Primative<short>();
        SR1_Primative<short> engageZMaxDelta = new SR1_Primative<short>();
        SR1_Primative<byte> engageYCone = new SR1_Primative<byte>();
        SR1_Primative<byte> engageZCone = new SR1_Primative<byte>();
        SR1_Primative<ushort> pad = new SR1_Primative<ushort>();
        SR1_Pointer<PhysObWeaponAttributes> weapon = new SR1_Pointer<PhysObWeaponAttributes>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            conditions.Read(reader, this, "conditions");
            auxConditions.Read(reader, this, "auxConditions");
            action.Read(reader, this, "action");
            auxAction.Read(reader, this, "auxAction");
            startAnim.Read(reader, this, "startAnim");
            endAnim.Read(reader, this, "endAnim");
            razielAnim.Read(reader, this, "razielAnim");
            razielAuxAnim.Read(reader, this, "razielAuxAnim");
            frame.Read(reader, this, "frame");
            startAnimMode.Read(reader, this, "startAnimMode");
            distance.Read(reader, this, "distance");
            newType.Read(reader, this, "newType");
            newClass.Read(reader, this, "newClass");
            mode.Read(reader, this, "mode");
            engageXYDistance.Read(reader, this, "engageXYDistance");
            engageZMinDelta.Read(reader, this, "engageZMinDelta");
            engageZMaxDelta.Read(reader, this, "engageZMaxDelta");
            engageYCone.Read(reader, this, "engageYCone");
            engageZCone.Read(reader, this, "engageZCone");
            pad.Read(reader, this, "pad");
            weapon.Read(reader, this, "weapon");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new PhysObWeaponAttributes().ReadFromPointer(reader, weapon);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            conditions.Write(writer);
            auxConditions.Write(writer);
            action.Write(writer);
            auxAction.Write(writer);
            startAnim.Write(writer);
            endAnim.Write(writer);
            razielAnim.Write(writer);
            razielAuxAnim.Write(writer);
            frame.Write(writer);
            startAnimMode.Write(writer);
            distance.Write(writer);
            newType.Write(writer);
            newClass.Write(writer);
            mode.Write(writer);
            engageXYDistance.Write(writer);
            engageZMinDelta.Write(writer);
            engageZMaxDelta.Write(writer);
            engageYCone.Write(writer);
            engageZCone.Write(writer);
            pad.Write(writer);
            weapon.Write(writer);
        }
    }
}
