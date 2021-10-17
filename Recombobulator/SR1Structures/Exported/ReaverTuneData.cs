using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class ReaverTuneData : SR1_Structure
	{
		SR1_StructureArray<ReaverWaveData> wave = new SR1_StructureArray<ReaverWaveData>(5);
		SR1_Primative<short> waveRate = new SR1_Primative<short>();
		SR1_Primative<short> glowStartSegment = new SR1_Primative<short>();
		SR1_Primative<short> glowNumberOfSegments = new SR1_Primative<short>();
		SR1_Primative<short> glowWidth = new SR1_Primative<short>();
		SR1_Primative<uint> spectralGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> materialGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> sunlightGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> waterGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> stoneGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> fireGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> spiritGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> soundGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> spectralInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> materialInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> sunlightInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> waterInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> stoneInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> fireInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> spiritInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> soundInnerColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> spectralInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> materialInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> sunlightInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> waterInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> stoneInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> fireInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> spiritInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> soundInnerGlowColor = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_PrimativeArray<uint> icon_colors = new SR1_PrimativeArray<uint>(24).ShowAsHex(true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			wave.Read(reader, this, "wave", SR1_File.Version.First, SR1_File.Version.May12);
			waveRate.Read(reader, this, "waveRate", SR1_File.Version.First, SR1_File.Version.May12);
			glowStartSegment.Read(reader, this, "glowStartSegment", SR1_File.Version.First, SR1_File.Version.May12);
			glowNumberOfSegments.Read(reader, this, "glowNumberOfSegments", SR1_File.Version.First, SR1_File.Version.May12);
			glowWidth.Read(reader, this, "glowWidth", SR1_File.Version.First, SR1_File.Version.May12);
			spectralGlowColor.Read(reader, this, "spectralGlowColor");
			materialGlowColor.Read(reader, this, "materialGlowColor");
			sunlightGlowColor.Read(reader, this, "sunlightGlowColor");
			waterGlowColor.Read(reader, this, "waterGlowColor");
			stoneGlowColor.Read(reader, this, "stoneGlowColor");
			fireGlowColor.Read(reader, this, "fireGlowColor");
			spiritGlowColor.Read(reader, this, "spiritGlowColor");
			soundGlowColor.Read(reader, this, "soundGlowColor");
			spectralInnerColor.Read(reader, this, "spectralInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			materialInnerColor.Read(reader, this, "materialInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			sunlightInnerColor.Read(reader, this, "sunlightInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			waterInnerColor.Read(reader, this, "waterInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			stoneInnerColor.Read(reader, this, "stoneInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			fireInnerColor.Read(reader, this, "fireInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			spiritInnerColor.Read(reader, this, "spiritInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			soundInnerColor.Read(reader, this, "soundInnerColor", SR1_File.Version.May12, SR1_File.Version.Next);
			spectralInnerGlowColor.Read(reader, this, "spectralInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			materialInnerGlowColor.Read(reader, this, "materialInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			sunlightInnerGlowColor.Read(reader, this, "sunlightInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			waterInnerGlowColor.Read(reader, this, "waterInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			stoneInnerGlowColor.Read(reader, this, "stoneInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			fireInnerGlowColor.Read(reader, this, "fireInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			spiritInnerGlowColor.Read(reader, this, "spiritInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			soundInnerGlowColor.Read(reader, this, "soundInnerGlowColor", SR1_File.Version.May12, SR1_File.Version.Next);
			// Icon colors aren't used.
			// icon_colors.Read(reader, this, "icon_colors");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			wave.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			waveRate.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			glowStartSegment.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			glowNumberOfSegments.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			glowWidth.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			spectralGlowColor.Write(writer);
			materialGlowColor.Write(writer);
			sunlightGlowColor.Write(writer);
			waterGlowColor.Write(writer);
			stoneGlowColor.Write(writer);
			fireGlowColor.Write(writer);
			spiritGlowColor.Write(writer);
			soundGlowColor.Write(writer);
			spectralInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			materialInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			sunlightInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			waterInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			stoneInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			fireInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			spiritInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			soundInnerColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			spectralInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			materialInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			sunlightInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			waterInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			stoneInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			fireInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			spiritInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			soundInnerGlowColor.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			// Icon colors aren't used.
			// icon_colors.Write(writer);
		}
	}
}
