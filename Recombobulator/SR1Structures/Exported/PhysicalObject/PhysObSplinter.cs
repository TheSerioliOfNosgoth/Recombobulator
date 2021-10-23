using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PhysObSplinter : SR1_Structure
	{
		SR1_Primative<int> numSplinterData = new SR1_Primative<int>();
		SR1_Pointer<FXSplinter> splinterData = new SR1_Pointer<FXSplinter>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numSplinterData.Read(reader, this, "numSplinterData");
			splinterData.Read(reader, this, "splinterData");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new SR1_StructureArray<FXSplinter>(numSplinterData.Value).ReadFromPointer(reader, splinterData);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numSplinterData.Write(writer);
			splinterData.Write(writer);
		}
	}
}
