using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class CinemaFnTableT : SR1_Structure
	{
		SR1_Primative<uint> play = new SR1_Primative<uint>();
		SR1_Primative<uint> versionID = new SR1_Primative<uint>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			play.Read(reader, this, "play");
			versionID.Read(reader, this, "versionID");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			play.Write(writer);
			versionID.Write(writer);
		}
	}
}
