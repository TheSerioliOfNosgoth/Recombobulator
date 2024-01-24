using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class SFXMkr : SR1_Structure
	{
		SR1_Pointer<SFXFileData> soundData = new SR1_Pointer<SFXFileData>();
		SR1_Primative<int> uniqueID = new SR1_Primative<int>();
		SR1_StructureArray<SoundInstance> sfxTbl = new SR1_StructureArray<SoundInstance>(4);
		Position pos = new Position();
		SR1_Primative<short> pad = new SR1_Primative<short>();
		SR1_Primative<int> livesInOnePlace = new SR1_Primative<int>();
		SR1_Primative<int> inSpectral = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			soundData.Read(reader, this, "soundData");
			uniqueID.Read(reader, this, "uniqueID");
			sfxTbl.Read(reader, this, "sfxTbl");
			pos.Read(reader, this, "pos");
			pad.Read(reader, this, "pad");
			livesInOnePlace.Read(reader, this, "livesInOnePlace");
			inSpectral.Read(reader, this, "inSpectral");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if (soundData.Offset != 0 && !reader.SFXDictionary.ContainsKey(soundData.Offset))
			{
				reader.SFXDictionary.Add(soundData.Offset, soundData);
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			soundData.Write(writer);
			uniqueID.Write(writer);
			sfxTbl.Write(writer);
			pos.Write(writer);
			pad.Write(writer);
			livesInOnePlace.Write(writer);
			inSpectral.Write(writer);
		}
	}
}
