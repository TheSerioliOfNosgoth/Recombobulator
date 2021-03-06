﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class DrMoveAniTexSrcInfo : SR1_Structure
    {
        SR1_Primative<short> pixSrcX = new SR1_Primative<short>();
        SR1_Primative<short> pixSrcY = new SR1_Primative<short>();
        SR1_Primative<short> clutSrcX = new SR1_Primative<short>();
        SR1_Primative<short> clutSrcY = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            pixSrcX.Read(reader, this, "pixSrcX");
            pixSrcY.Read(reader, this, "pixSrcY");
            clutSrcX.Read(reader, this, "clutSrcX");
            clutSrcY.Read(reader, this, "clutSrcY");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            pixSrcX.Write(writer);
            pixSrcY.Write(writer);
            clutSrcX.Write(writer);
            clutSrcY.Write(writer);
        }
    }
}
