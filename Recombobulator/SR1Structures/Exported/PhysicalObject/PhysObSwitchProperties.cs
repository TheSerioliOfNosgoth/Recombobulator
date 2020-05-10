using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObSwitchProperties : SR1_Structure
    {
        PhysObProperties Properties = new PhysObProperties();
        SR1_Primative<short> Distance = new SR1_Primative<short>();
        SR1_Primative<short> Class = new SR1_Primative<short>();
        SR1_Primative<byte> onAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> offAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> failedOnAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> enableAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> shutAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> onWalkTimer = new SR1_Primative<byte>();
        SR1_Primative<byte> offWalkTimer = new SR1_Primative<byte>();
        SR1_Primative<byte> walkMode = new SR1_Primative<byte>();
        SR1_Primative<byte> razielOnAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> razielOffAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> razielFailedOnAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> razielEnableAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> startMode = new SR1_Primative<byte>();
        SR1_Primative<byte> startAnim = new SR1_Primative<byte>();
        SR1_Primative<ushort> engageXYDistance = new SR1_Primative<ushort>();
        SR1_Primative<sbyte> engageZMinDelta = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> engageZMaxDelta = new SR1_Primative<sbyte>();
        SR1_Primative<byte> engageYCone = new SR1_Primative<byte>();
        SR1_Primative<byte> engageZCone = new SR1_Primative<byte>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            Distance.Read(reader, this, "Distance");
            Class.Read(reader, this, "Class");
            onAnim.Read(reader, this, "onAnim");
            offAnim.Read(reader, this, "offAnim");
            failedOnAnim.Read(reader, this, "failedOnAnim");
            enableAnim.Read(reader, this, "enableAnim");
            shutAnim.Read(reader, this, "shutAnim");
            onWalkTimer.Read(reader, this, "onWalkTimer");
            offWalkTimer.Read(reader, this, "offWalkTimer");
            walkMode.Read(reader, this, "walkMode");
            razielOnAnim.Read(reader, this, "razielOnAnim");
            razielOffAnim.Read(reader, this, "razielOffAnim");
            razielFailedOnAnim.Read(reader, this, "razielFailedOnAnim");
            razielEnableAnim.Read(reader, this, "razielEnableAnim");
            startMode.Read(reader, this, "startMode");
            startAnim.Read(reader, this, "startAnim");
            engageXYDistance.Read(reader, this, "engageXYDistance");
            engageZMinDelta.Read(reader, this, "engageZMinDelta");
            engageZMaxDelta.Read(reader, this, "engageZMaxDelta");
            engageYCone.Read(reader, this, "engageYCone");
            engageZCone.Read(reader, this, "engageZCone");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            Distance.Write(writer);
            Class.Write(writer);
            onAnim.Write(writer);
            offAnim.Write(writer);
            failedOnAnim.Write(writer);
            enableAnim.Write(writer);
            shutAnim.Write(writer);
            onWalkTimer.Write(writer);
            offWalkTimer.Write(writer);
            walkMode.Write(writer);
            razielOnAnim.Write(writer);
            razielOffAnim.Write(writer);
            razielFailedOnAnim.Write(writer);
            razielEnableAnim.Write(writer);
            startMode.Write(writer);
            startAnim.Write(writer);
            engageXYDistance.Write(writer);
            engageZMinDelta.Write(writer);
            engageZMaxDelta.Write(writer);
            engageYCone.Write(writer);
            engageZCone.Write(writer);
        }
    }
}
