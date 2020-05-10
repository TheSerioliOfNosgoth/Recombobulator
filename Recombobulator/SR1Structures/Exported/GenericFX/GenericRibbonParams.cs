using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericRibbonParams : SR1_Structure
    {
        SR1_Primative<sbyte> id = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> StartOnInit = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> startSegment = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> endSegment = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
        SR1_Primative<short> ribbonLifeTime = new SR1_Primative<short>();
        SR1_Primative<short> faceLifeTime = new SR1_Primative<short>();
        SR1_Primative<short> startFadeValue = new SR1_Primative<short>();
        SR1_Primative<int> startColor = new SR1_Primative<int>();
        SR1_Primative<int> endColor = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            id.Read(reader, this, "id");
            StartOnInit.Read(reader, this, "StartOnInit");
            startSegment.Read(reader, this, "startSegment");
            endSegment.Read(reader, this, "endSegment");
            type.Read(reader, this, "type");
            use_child.Read(reader, this, "use_child");
            ribbonLifeTime.Read(reader, this, "ribbonLifeTime");
            faceLifeTime.Read(reader, this, "faceLifeTime");
            startFadeValue.Read(reader, this, "startFadeValue");
            startColor.Read(reader, this, "startColor");
            endColor.Read(reader, this, "endColor");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            id.Write(writer);
            StartOnInit.Write(writer);
            startSegment.Write(writer);
            endSegment.Write(writer);
            type.Write(writer);
            use_child.Write(writer);
            ribbonLifeTime.Write(writer);
            faceLifeTime.Write(writer);
            startFadeValue.Write(writer);
            startColor.Write(writer);
            endColor.Write(writer);
        }
    }
}
