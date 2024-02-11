using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class VMObject : SR1_Structure
	{
		SR1_Primative<ushort> flags = new SR1_Primative<ushort>();

		public object CreateReplacementObject()
		{
			if ((flags.Value & 8) == 0)
			{
				return new VMColorObject();
			}
			else
			{
				return new VMMoveObject();
			}
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			flags.Read(reader, this, "flags");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags.Write(writer);
		}
	}
}
