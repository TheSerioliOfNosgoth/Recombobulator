using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class IdleList : SR1_Structure
	{
		SR1_PointerSeries<IdleSet> animList = new SR1_PointerSeries<IdleSet>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			animList.Read(reader, this, "animList");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			foreach (SR1_PointerBase pointer in animList.List)
			{
				if (pointer.Offset != 0)
				{
					reader.IdleAnimSetDictionary.Add(pointer.Offset, pointer);
				}
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			animList.Write(writer);
		}
	}
}
