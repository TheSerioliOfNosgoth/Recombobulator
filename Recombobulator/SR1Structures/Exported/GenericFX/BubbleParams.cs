using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class BubbleParams : SR1_Structure
    {
        SR1_Primative<short> DisperseFrames = new SR1_Primative<short>();
        SR1_Primative<short> KillScale = new SR1_Primative<short>();
        SR1_Primative<short> MinSplashSize = new SR1_Primative<short>();
        SR1_Primative<short> MaxSpeed = new SR1_Primative<short>();
        SR1_Primative<short> MaxSpeedRange = new SR1_Primative<short>();
        SR1_Primative<short> ScaleRate = new SR1_Primative<short>();
        SR1_Primative<short> ScaleRateRange = new SR1_Primative<short>();
        SR1_Primative<short> StartScale = new SR1_Primative<short>();
        SR1_Primative<short> StartScaleRange = new SR1_Primative<short>();
        SR1_Primative<short> UniqueBubbles = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            DisperseFrames.Read(reader, this, "DisperseFrames");
            KillScale.Read(reader, this, "KillScale");
            MinSplashSize.Read(reader, this, "MinSplashSize");
            MaxSpeed.Read(reader, this, "MaxSpeed");
            MaxSpeedRange.Read(reader, this, "MaxSpeedRange");
            ScaleRate.Read(reader, this, "ScaleRate");
            ScaleRateRange.Read(reader, this, "ScaleRateRange");
            StartScale.Read(reader, this, "StartScale");
            StartScaleRange.Read(reader, this, "StartScaleRange");
            UniqueBubbles.Read(reader, this, "UniqueBubbles");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            DisperseFrames.Write(writer);
            KillScale.Write(writer);
            MinSplashSize.Write(writer);
            MaxSpeed.Write(writer);
            MaxSpeedRange.Write(writer);
            ScaleRate.Write(writer);
            ScaleRateRange.Write(writer);
            StartScale.Write(writer);
            StartScaleRange.Write(writer);
            UniqueBubbles.Write(writer);
        }
    }
}
