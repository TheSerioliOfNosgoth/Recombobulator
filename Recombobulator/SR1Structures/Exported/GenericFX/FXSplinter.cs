using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class FXSplinter : SR1_Structure
    {
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_Primative<short> soundFx = new SR1_Primative<short>();
        SR1_Primative<short> chunkVelXY = new SR1_Primative<short>();
        SR1_Primative<short> chunkVelZ = new SR1_Primative<short>();
        SR1_Primative<short> chunkVelRng = new SR1_Primative<short>();
        SR1_Primative<short> triVelRng = new SR1_Primative<short>();
        SR1_Primative<short> lifetime = new SR1_Primative<short>();
        SR1_Primative<short> faceLimit = new SR1_Primative<short>();
        SR1_Primative<short> rotRateRng = new SR1_Primative<short>();
        SR1_Primative<short> gravityZ = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            flags.Read(reader, this, "flags");
            soundFx.Read(reader, this, "soundFx");
            chunkVelXY.Read(reader, this, "chunkVelXY");
            chunkVelZ.Read(reader, this, "chunkVelZ");
            chunkVelRng.Read(reader, this, "chunkVelRng");
            triVelRng.Read(reader, this, "triVelRng");
            lifetime.Read(reader, this, "lifetime");
            faceLimit.Read(reader, this, "faceLimit");
            rotRateRng.Read(reader, this, "rotRateRng");
            gravityZ.Read(reader, this, "gravityZ");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            flags.Write(writer);
            soundFx.Write(writer);
            chunkVelXY.Write(writer);
            chunkVelZ.Write(writer);
            chunkVelRng.Write(writer);
            triVelRng.Write(writer);
            lifetime.Write(writer);
            faceLimit.Write(writer);
            rotRateRng.Write(writer);
            gravityZ.Write(writer);
        }
    }
}
