using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class G2AnimFXList : SR1_Structure
	{
		List<G2AnimFxHeader_Type> _List = new List<G2AnimFxHeader_Type>();

		public G2AnimFXList()
		{
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			while (true)
			{
				G2AnimFxHeader_Type fxHeader = new G2AnimFxHeader_Type();
				fxHeader.Read(reader, this, "[" + _List.Count.ToString() + "]");
				_List.Add(fxHeader);

				if (fxHeader.type.Value == -1 ||
					(reader.Object != null && fxHeader.End >= reader.Object.AnimKeyListStart))
				{
					break;
				}
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			for (int i = 0; i < _List.Count; i++)
			{
				_List[i].Write(writer);
			}
		}
	}
}
