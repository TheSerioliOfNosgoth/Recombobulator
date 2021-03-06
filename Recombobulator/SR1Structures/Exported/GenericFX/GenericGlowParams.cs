﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericGlowParams : SR1_Structure
    {
        SR1_Primative<sbyte> StartOnInit = new SR1_Primative<sbyte>();
        SR1_Primative<byte> segment = new SR1_Primative<byte>();
        SR1_Primative<byte> segmentEnd = new SR1_Primative<byte>();
        SR1_Primative<sbyte> numSegments = new SR1_Primative<sbyte>();
        SR1_Primative<byte> color_num = new SR1_Primative<byte>();
        SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
        SR1_Primative<byte> numColors = new SR1_Primative<byte>();
        SR1_Primative<sbyte> id = new SR1_Primative<sbyte>();
        SR1_Primative<int> atuColorCycleRate = new SR1_Primative<int>();
        SR1_Primative<short> width = new SR1_Primative<short>();
        SR1_Primative<short> height = new SR1_Primative<short>();
        SR1_Primative<int> lifetime = new SR1_Primative<int>();
        SR1_Primative<short> fadein_time = new SR1_Primative<short>();
        SR1_Primative<short> fadeout_time = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            StartOnInit.Read(reader, this, "StartOnInit");
            segment.Read(reader, this, "segment");
            segmentEnd.Read(reader, this, "segmentEnd");
            numSegments.Read(reader, this, "numSegments");
            color_num.Read(reader, this, "color_num");
            use_child.Read(reader, this, "use_child");
            numColors.Read(reader, this, "numColors");
            id.Read(reader, this, "id");
            atuColorCycleRate.Read(reader, this, "atuColorCycleRate");
            width.Read(reader, this, "width");
            height.Read(reader, this, "height");
            lifetime.Read(reader, this, "lifetime");
            fadein_time.Read(reader, this, "fadein_time");
            fadeout_time.Read(reader, this, "fadeout_time");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            StartOnInit.Write(writer);
            segment.Write(writer);
            segmentEnd.Write(writer);
            numSegments.Write(writer);
            color_num.Write(writer);
            use_child.Write(writer);
            numColors.Write(writer);
            id.Write(writer);
            atuColorCycleRate.Write(writer);
            width.Write(writer);
            height.Write(writer);
            lifetime.Write(writer);
            fadein_time.Write(writer);
            fadeout_time.Write(writer);
        }
    }
}
