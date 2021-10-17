using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SFXFileDataList : SR1_Structure
	{
		public readonly SR1_StructureList<SFXFileData> data = new SR1_StructureList<SFXFileData>();

		public SFXFileDataList()
		{
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			foreach (KeyValuePair<uint, SR1_Pointer<SFXFileData>> pointer in reader.SFXDictionary)
			{
				data.Add(new SFXFileData());
			}

			data.Read(reader, this, "data");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{

		}

		public override void WriteMembers(SR1_Writer writer)
		{
			data.Write(writer);
		}
	}
}
