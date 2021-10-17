using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SignalData : SR1_Structure
	{
		// No members. This is just a base class.

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
		}
	}
}