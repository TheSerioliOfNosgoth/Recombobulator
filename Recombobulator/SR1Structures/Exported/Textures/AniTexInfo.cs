using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class AniTexInfo : SR1_Structure
    {
        SR1_Pointer<TextureMT3> texture = new SR1_Pointer<TextureMT3>();
        SR1_Primative<int> numFrames = new SR1_Primative<int>();
        SR1_Primative<int> speed = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            texture.Read(reader, this, "texture");
            numFrames.Read(reader, this, "numFrames");
            speed.Read(reader, this, "speed");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            texture.Write(writer);
            numFrames.Write(writer);
            speed.Write(writer);
        }
    }
}
