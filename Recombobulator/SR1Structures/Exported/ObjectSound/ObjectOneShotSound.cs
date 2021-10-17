using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class ObjectOneShotSound : ObjectSound
	{
		SR1_Primative<byte> type = new SR1_Primative<byte>();
		SR1_Primative<byte> numSfxIDs = new SR1_Primative<byte>();
		SR1_Primative<byte> soundInst = new SR1_Primative<byte>();
		SR1_Primative<byte> flags = new SR1_Primative<byte>();
		SR1_Primative<ushort> minVolDistance = new SR1_Primative<ushort>();
		SR1_Primative<short> pitch = new SR1_Primative<short>();
		SR1_Primative<ushort> pitchVariation = new SR1_Primative<ushort>();
		SR1_Primative<byte> maxVolume = new SR1_Primative<byte>();
		SR1_Primative<byte> maxVolVariation = new SR1_Primative<byte>();
		SR1_Primative<byte> initialDelay = new SR1_Primative<byte>();
		SR1_Primative<byte> initialDelayVariation = new SR1_Primative<byte>();
		SR1_PrimativeArray<ushort> sfxIDs = new SR1_PrimativeArray<ushort>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			type.Read(reader, this, "type");
			numSfxIDs.Read(reader, this, "numSfxIDs");
			soundInst.Read(reader, this, "soundInst");
			flags.Read(reader, this, "flags");
			minVolDistance.Read(reader, this, "minVolDistance");
			pitch.Read(reader, this, "pitch");
			pitchVariation.Read(reader, this, "pitchVariation");
			maxVolume.Read(reader, this, "maxVolume");
			maxVolVariation.Read(reader, this, "maxVolVariation");
			initialDelay.Read(reader, this, "initialDelay");
			initialDelayVariation.Read(reader, this, "initialDelayVariation");
			sfxIDs = new SR1_PrimativeArray<ushort>(numSfxIDs.Value);
			sfxIDs.Read(reader, this, "sfxIDs");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			type.Write(writer);
			numSfxIDs.Write(writer);
			soundInst.Write(writer);
			flags.Write(writer);
			minVolDistance.Write(writer);
			pitch.Write(writer);
			pitchVariation.Write(writer);
			maxVolume.Write(writer);
			maxVolVariation.Write(writer);
			initialDelay.Write(writer);
			initialDelayVariation.Write(writer);
			sfxIDs.Write(writer);
		}
	}
}
