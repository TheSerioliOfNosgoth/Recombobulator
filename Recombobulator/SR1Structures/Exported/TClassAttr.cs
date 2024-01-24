using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class TClassAttr : SR1_Structure
	{
		SR1_Primative<short> flags = new SR1_Primative<short>();
		SR1_Primative<ushort> sound = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			flags.Read(reader, this, "flags");
			sound.Read(reader, this, "sound");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags.Write(writer);
			sound.Write(writer);
		}
	}
}
