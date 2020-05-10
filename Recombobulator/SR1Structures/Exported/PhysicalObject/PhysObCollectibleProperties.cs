using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class PhysObCollectibleProperties : SR1_Structure
    {
        PhysObProperties Properties = new PhysObProperties();
        SR1_Primative<short> collectClass = new SR1_Primative<short>();
        SR1_Primative<short> collectValue = new SR1_Primative<short>();
        SR1_Primative<uint> distance = new SR1_Primative<uint>();
        SR1_Primative<byte> idleAnim = new SR1_Primative<byte>();
        SR1_Primative<byte> collectAnim = new SR1_Primative<byte>();
        SR1_Primative<ushort> spatialNode = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Properties.Read(reader, this, "Properties");
            collectClass.Read(reader, this, "collectClass");
            collectValue.Read(reader, this, "collectValue");
            distance.Read(reader, this, "distance");
            idleAnim.Read(reader, this, "idleAnim");
            collectAnim.Read(reader, this, "collectAnim");
            spatialNode.Read(reader, this, "spatialNode");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            Properties.Write(writer);
            collectClass.Write(writer);
            collectValue.Write(writer);
            distance.Write(writer);
            idleAnim.Write(writer);
            collectAnim.Write(writer);
            spatialNode.Write(writer);
        }
    }
}
