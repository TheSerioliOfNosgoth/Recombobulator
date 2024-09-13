using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class VMObject : SR1_Structure
	{
		public readonly SR1_Primative<int> flags0 = new SR1_Primative<int>();
		public readonly SR1_Primative<ushort> flags = new SR1_Primative<ushort>();

		public bool IsColorObject = false;

		public object CreateReplacementObject()
		{
			if (IsColorObject)
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
			flags0.Read(reader, this, "flags", SR1_File.Version.First, SR1_File.Version.Jan23);
			flags.Read(reader, this, "flags", SR1_File.Version.Jan23, SR1_File.Version.Next);

			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				IsColorObject = (flags0.Value & 8) == 0;
			}
			else
			{
				IsColorObject = (flags.Value & 8) == 0;
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			flags.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}
	}
}
