using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericBlastRingParams : SR1_Structure
    {
        SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
        SR1_Primative<short> lifeTime = new SR1_Primative<short>();
        Position offset = new Position();
        SR1_Primative<short> matrixSeg = new SR1_Primative<short>();
        SR1_Primative<sbyte> segment = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> sortInWorld = new SR1_Primative<sbyte>();
        SR1_Primative<short> radius = new SR1_Primative<short>();
        SR1_Primative<short> size1 = new SR1_Primative<short>();
        SR1_Primative<short> size2 = new SR1_Primative<short>();
        SR1_Primative<short> endRadius = new SR1_Primative<short>();
        SR1_Primative<short> colorchange_radius = new SR1_Primative<short>();
        SR1_Primative<int> vel = new SR1_Primative<int>();
        SR1_Primative<int> accl = new SR1_Primative<int>();
        SR1_Primative<short> height1 = new SR1_Primative<short>();
        SR1_Primative<short> height2 = new SR1_Primative<short>();
        SR1_Primative<short> height3 = new SR1_Primative<short>();
        SR1_Primative<short> predator_offset = new SR1_Primative<short>();
        SR1_Primative<int> startColor = new SR1_Primative<int>();
        SR1_Primative<int> endColor = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
            use_child.Read(reader, this, "use_child");
            lifeTime.Read(reader, this, "lifeTime");
            offset.Read(reader, this, "offset");
            matrixSeg.Read(reader, this, "matrixSeg");
            segment.Read(reader, this, "segment");
            sortInWorld.Read(reader, this, "sortInWorld");
            radius.Read(reader, this, "radius");
            size1.Read(reader, this, "size1");
            size2.Read(reader, this, "size2");
            endRadius.Read(reader, this, "endRadius");
            colorchange_radius.Read(reader, this, "colorchange_radius");
            vel.Read(reader, this, "vel");
            accl.Read(reader, this, "accl");
            height1.Read(reader, this, "height1");
            height2.Read(reader, this, "height2");
            height3.Read(reader, this, "height3");
            predator_offset.Read(reader, this, "predator_offset");
            startColor.Read(reader, this, "startColor");
            endColor.Read(reader, this, "endColor");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
            use_child.Write(writer);
            lifeTime.Write(writer);
            offset.Write(writer);
            matrixSeg.Write(writer);
            segment.Write(writer);
            sortInWorld.Write(writer);
            radius.Write(writer);
            size1.Write(writer);
            size2.Write(writer);
            endRadius.Write(writer);
            colorchange_radius.Write(writer);
            vel.Write(writer);
            accl.Write(writer);
            height1.Write(writer);
            height2.Write(writer);
            height3.Write(writer);
            predator_offset.Write(writer);
            startColor.Write(writer);
            endColor.Write(writer);
        }
    }
}
