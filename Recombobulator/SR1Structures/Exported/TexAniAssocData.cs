using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class TexAniAssocData : SR1_Structure
	{
		SR1_Primative<int> numEntries = new SR1_Primative<int>();
		SR1_PrimativeArray<int> entries = new SR1_PrimativeArray<int>(0);

		public TexAniAssocData()
		{
		}

		public TexAniAssocData(List<int> entryList)
		{
			if (entryList != null)
			{
				numEntries = new SR1_Primative<int>();
				numEntries.Value = entryList.Count;

				entries = new SR1_PrimativeArray<int>(entryList.Count);
				for (int i = 0; i < entryList.Count; i++)
				{
					entries[i] = entryList[i];
				}
			}
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numEntries.Read(reader, this, "numEntries");

			entries = new SR1_PrimativeArray<int>(numEntries.Value);
			entries.Read(reader, this, "entries");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numEntries.Write(writer);
			entries.Write(writer);
		}
	}
}
