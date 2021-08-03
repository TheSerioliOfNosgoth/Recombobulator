using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObDraftProperties : PhysObPropertiesBase
    {
        SR1_Primative<short> force = new SR1_Primative<short>();
        SR1_Primative<ushort> radius = new SR1_Primative<ushort>();
        SR1_Primative<ushort> radiusCoef = new SR1_Primative<ushort>();
        SR1_Primative<short> maxVelocity = new SR1_Primative<short>();
        SR1_Primative<uint> height = new SR1_Primative<uint>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            force.Read(reader, this, "force");
            radius.Read(reader, this, "radius");
            radiusCoef.Read(reader, this, "radiusCoef");
            maxVelocity.Read(reader, this, "maxVelocity");
            height.Read(reader, this, "height");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            force.Write(writer);
            radius.Write(writer);
            radiusCoef.Write(writer);
            maxVelocity.Write(writer);
            height.Write(writer);
        }
    }
}
