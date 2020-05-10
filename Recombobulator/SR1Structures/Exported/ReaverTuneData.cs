using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ReaverTuneData : SR1_Structure
    {
        SR1_Primative<uint> spectralGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> materialGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> sunlightGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> waterGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> stoneGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> fireGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> spiritGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> soundGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> spectralInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> materialInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> sunlightInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> waterInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> stoneInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> fireInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> spiritInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> soundInnerColor = new SR1_Primative<uint>();
        SR1_Primative<uint> spectralInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> materialInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> sunlightInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> waterInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> stoneInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> fireInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> spiritInnerGlowColor = new SR1_Primative<uint>();
        SR1_Primative<uint> soundInnerGlowColor = new SR1_Primative<uint>();
        SR1_PrimativeArray<uint> icon_colors = new SR1_PrimativeArray<uint>(24);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            spectralGlowColor.Read(reader, this, "spectralGlowColor");
            materialGlowColor.Read(reader, this, "materialGlowColor");
            sunlightGlowColor.Read(reader, this, "sunlightGlowColor");
            waterGlowColor.Read(reader, this, "waterGlowColor");
            stoneGlowColor.Read(reader, this, "stoneGlowColor");
            fireGlowColor.Read(reader, this, "fireGlowColor");
            spiritGlowColor.Read(reader, this, "spiritGlowColor");
            soundGlowColor.Read(reader, this, "soundGlowColor");
            spectralInnerColor.Read(reader, this, "spectralInnerColor");
            materialInnerColor.Read(reader, this, "materialInnerColor");
            sunlightInnerColor.Read(reader, this, "sunlightInnerColor");
            waterInnerColor.Read(reader, this, "waterInnerColor");
            stoneInnerColor.Read(reader, this, "stoneInnerColor");
            fireInnerColor.Read(reader, this, "fireInnerColor");
            spiritInnerColor.Read(reader, this, "spiritInnerColor");
            soundInnerColor.Read(reader, this, "soundInnerColor");
            spectralInnerGlowColor.Read(reader, this, "spectralInnerGlowColor");
            materialInnerGlowColor.Read(reader, this, "materialInnerGlowColor");
            sunlightInnerGlowColor.Read(reader, this, "sunlightInnerGlowColor");
            waterInnerGlowColor.Read(reader, this, "waterInnerGlowColor");
            stoneInnerGlowColor.Read(reader, this, "stoneInnerGlowColor");
            fireInnerGlowColor.Read(reader, this, "fireInnerGlowColor");
            spiritInnerGlowColor.Read(reader, this, "spiritInnerGlowColor");
            soundInnerGlowColor.Read(reader, this, "soundInnerGlowColor");
            icon_colors.Read(reader, this, "icon_colors");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            spectralGlowColor.Write(writer);
            materialGlowColor.Write(writer);
            sunlightGlowColor.Write(writer);
            waterGlowColor.Write(writer);
            stoneGlowColor.Write(writer);
            fireGlowColor.Write(writer);
            spiritGlowColor.Write(writer);
            soundGlowColor.Write(writer);
            spectralInnerColor.Write(writer);
            materialInnerColor.Write(writer);
            sunlightInnerColor.Write(writer);
            waterInnerColor.Write(writer);
            stoneInnerColor.Write(writer);
            fireInnerColor.Write(writer);
            spiritInnerColor.Write(writer);
            soundInnerColor.Write(writer);
            spectralInnerGlowColor.Write(writer);
            materialInnerGlowColor.Write(writer);
            sunlightInnerGlowColor.Write(writer);
            waterInnerGlowColor.Write(writer);
            stoneInnerGlowColor.Write(writer);
            fireInnerGlowColor.Write(writer);
            spiritInnerGlowColor.Write(writer);
            soundInnerGlowColor.Write(writer);
            icon_colors.Write(writer);
        }
    }
}
