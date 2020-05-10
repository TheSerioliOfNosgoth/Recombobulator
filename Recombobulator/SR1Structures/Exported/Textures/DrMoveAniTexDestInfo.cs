using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class DrMoveAniTexDestInfo : SR1_Structure
    {
        SR1_Primative<short> pixDstX = new SR1_Primative<short>();
        SR1_Primative<short> pixDstY = new SR1_Primative<short>();
        SR1_Primative<short> pixW = new SR1_Primative<short>();
        SR1_Primative<short> pixH = new SR1_Primative<short>();
        SR1_Primative<short> clutDstX = new SR1_Primative<short>();
        SR1_Primative<short> clutDstY = new SR1_Primative<short>();
        SR1_Primative<short> clutW = new SR1_Primative<short>();
        SR1_Primative<short> clutH = new SR1_Primative<short>();
        SR1_Primative<short> pixCurrentX = new SR1_Primative<short>();
        SR1_Primative<short> pixCurrentY = new SR1_Primative<short>();
        SR1_Primative<short> clutCurrentX = new SR1_Primative<short>();
        SR1_Primative<short> clutCurrentY = new SR1_Primative<short>();
        SR1_Primative<int> numFrames = new SR1_Primative<int>();
        SR1_Primative<int> speed = new SR1_Primative<int>();
        SR1_StructureArray<DrMoveAniTexSrcInfo> frame = new SR1_StructureArray<DrMoveAniTexSrcInfo>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            pixDstX.Read(reader, this, "pixDstX");
            pixDstY.Read(reader, this, "pixDstY");
            pixW.Read(reader, this, "pixW");
            pixH.Read(reader, this, "pixH");
            clutDstX.Read(reader, this, "clutDstX");
            clutDstY.Read(reader, this, "clutDstY");
            clutW.Read(reader, this, "clutW");
            clutH.Read(reader, this, "clutH");
            pixCurrentX.Read(reader, this, "pixCurrentX");
            pixCurrentY.Read(reader, this, "pixCurrentY");
            clutCurrentX.Read(reader, this, "clutCurrentX");
            clutCurrentY.Read(reader, this, "clutCurrentY");
            numFrames.Read(reader, this, "numFrames");
            speed.Read(reader, this, "speed");

            frame = new SR1_StructureArray<DrMoveAniTexSrcInfo>(numFrames.Value);
            frame.Read(reader, this, "frame");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            pixDstX.Write(writer);
            pixDstY.Write(writer);
            pixW.Write(writer);
            pixH.Write(writer);
            clutDstX.Write(writer);
            clutDstY.Write(writer);
            clutW.Write(writer);
            clutH.Write(writer);
            pixCurrentX.Write(writer);
            pixCurrentY.Write(writer);
            clutCurrentX.Write(writer);
            clutCurrentY.Write(writer);
            numFrames.Write(writer);
            speed.Write(writer);
            frame.Write(writer);
        }
    }
}
