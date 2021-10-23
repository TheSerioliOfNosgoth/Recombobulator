using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventInstanceObject : EventBaseObject
	{
		// Inherited SR1_Primative<short> id = new SR1_Primative<short>();
		public SR1_Primative<short> flags = new SR1_Primative<short>();
		public SR1_Primative<int> unitID = new SR1_Primative<int>();
		public SR1_Primative<int> introUniqueID = new SR1_Primative<int>();
		public SR1_Pointer<Instance> instance = new SR1_Pointer<Instance>();
		// Sometimes an SFXMarker, but always null in the area files, so probaby doesn't matter.
		public SR1_Pointer<Intro> data = new SR1_Pointer<Intro>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			flags.Read(reader, this, "flags");
			unitID.Read(reader, this, "unitID");
			introUniqueID.Read(reader, this, "introUniqueID");
			instance.Read(reader, this, "instance");
			data.Read(reader, this, "data");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			flags.Write(writer);
			unitID.Write(writer);
			introUniqueID.Write(writer);
			instance.Write(writer);
			data.Write(writer);
		}
	}
}
