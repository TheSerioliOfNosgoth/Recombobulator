using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventTGroupObject : EventBaseObject
	{
		// Inherited SR1_Primative<short> id = new SR1_Primative<short>();
		SR1_Primative<short> tgroupNumber = new SR1_Primative<short>();
		SR1_Primative<int> unitID = new SR1_Primative<int>();
		SR1_Pointer<BSPTree> bspTree = new SR1_Pointer<BSPTree>();
		SR1_Pointer<StreamUnit> stream = new SR1_Pointer<StreamUnit>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			tgroupNumber.Read(reader, this, "tgroupNumber");
			unitID.Read(reader, this, "unitID");
			bspTree.Read(reader, this, "bspTree");
			stream.Read(reader, this, "stream");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			tgroupNumber.Write(writer);
			unitID.Write(writer);
			bspTree.Write(writer);
			stream.Write(writer);
		}
	}
}
