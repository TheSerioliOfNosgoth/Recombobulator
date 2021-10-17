using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	abstract class SR1_PointerSeries : SR1_StructureSeriesBase<SR1_PointerBase>
	{
	}

	class SR1_PointerSeries<T> : SR1_PointerSeries where T : SR1_Structure, new()
	{
		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			SR1_Pointer<T> newEntry = null;
			int i = 0;
			do
			{
				newEntry = new SR1_Pointer<T>();
				newEntry.Read(reader, this, "[" + i.ToString() + "]");
				_List.Add(newEntry);
			}
			while (newEntry.Offset != 0);
		}

		public override string GetTypeName(bool includeDimensions)
		{
			string typeName = typeof(T).Name + "*";

			if (_List.Count == 0)
			{
				typeName += "[]";
				return typeName;
			}

			if (includeDimensions)
			{
				typeName += "[";
				typeName += _List.Count;
				typeName += "]";
			}

			return typeName;
		}
	}
}
