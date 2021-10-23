using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SoundInstance : SR1_Structure
	{
		SR1_Primative<byte> channel = new SR1_Primative<byte>();
		SR1_Primative<byte> state = new SR1_Primative<byte>();
		SR1_Primative<byte> delay = new SR1_Primative<byte>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			channel.Read(reader, this, "channel");
			state.Read(reader, this, "state");
			delay.Read(reader, this, "delay");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			channel.Write(writer);
			state.Write(writer);
			delay.Write(writer);
		}
	}
}
