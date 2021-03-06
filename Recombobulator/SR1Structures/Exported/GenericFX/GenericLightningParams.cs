﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericLightningParams : SR1_Structure
    {
        SR1_Primative<sbyte> type = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
        SR1_Primative<short> lifeTime = new SR1_Primative<short>();
        SR1_Primative<short> deg = new SR1_Primative<short>();
        SR1_Primative<short> deg_inc = new SR1_Primative<short>();
        Position start_offset = new Position();
        SR1_Primative<sbyte> startSeg = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> endSeg = new SR1_Primative<sbyte>();
        Position end_offset = new Position();
        SR1_Primative<sbyte> matrixSeg = new SR1_Primative<sbyte>();
        SR1_Primative<byte> segs = new SR1_Primative<byte>();
        SR1_Primative<short> width = new SR1_Primative<short>();
        SR1_Primative<short> small_width = new SR1_Primative<short>();
        SR1_Primative<short> sine_size = new SR1_Primative<short>();
        SR1_Primative<short> variation = new SR1_Primative<short>();
        SR1_Primative<int> color = new SR1_Primative<int>();
        SR1_Primative<int> glow_color = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
            use_child.Read(reader, this, "use_child");
            lifeTime.Read(reader, this, "lifeTime");
            deg.Read(reader, this, "deg");
            deg_inc.Read(reader, this, "deg_inc");
            start_offset.Read(reader, this, "start_offset");
            startSeg.Read(reader, this, "startSeg");
            endSeg.Read(reader, this, "endSeg");
            end_offset.Read(reader, this, "end_offset");
            matrixSeg.Read(reader, this, "matrixSeg");
            segs.Read(reader, this, "segs");
            width.Read(reader, this, "width");
            small_width.Read(reader, this, "small_width");
            sine_size.Read(reader, this, "sine_size");
            variation.Read(reader, this, "variation");
            color.Read(reader, this, "color");
            glow_color.Read(reader, this, "glow_color");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
            use_child.Write(writer);
            lifeTime.Write(writer);
            deg.Write(writer);
            deg_inc.Write(writer);
            start_offset.Write(writer);
            startSeg.Write(writer);
            endSeg.Write(writer);
            end_offset.Write(writer);
            matrixSeg.Write(writer);
            segs.Write(writer);
            width.Write(writer);
            small_width.Write(writer);
            sine_size.Write(writer);
            variation.Write(writer);
            color.Write(writer);
            glow_color.Write(writer);
        }
    }
}
