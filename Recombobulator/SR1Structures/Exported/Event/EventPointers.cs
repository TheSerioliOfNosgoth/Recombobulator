using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventPointers : SR1_Structure
	{
		public SR1_Primative<int> numPuzzles = new SR1_Primative<int>();
		public SR1_PointerArray<Event> eventInstances = new SR1_PointerArray<Event>(0, false);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numPuzzles.Read(reader, this, "numPuzzles");

			eventInstances = new SR1_PointerArray<Event>(numPuzzles.Value, false);
			eventInstances.Read(reader, this, "eventInstances");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numPuzzles.Write(writer);
			eventInstances.Write(writer);
		}
	}
}
