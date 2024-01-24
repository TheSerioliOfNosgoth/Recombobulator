using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_PointerArray : SR1_StructureArrayBase<SR1_PointerBase>
	{
		public SR1_PointerArray(params int[] dimensions)
			: base(dimensions)
		{
		}
	}

	public class SR1_PointerArray<T> : SR1_PointerArray where T : SR1_Structure, new()
	{
		private bool _ShouldCreate = false;
		private int _EntryPadding = 0;

		protected override SR1_PointerBase CreateElement()
		{
			return new SR1_Pointer<T>();
		}

		public SR1_PointerArray(int arrayLength, bool shouldCreate, int entryPadding = 0)
			: base(arrayLength)
		{
			_ShouldCreate = shouldCreate;
			_EntryPadding = entryPadding;
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if (_ShouldCreate)
			{
				for (int i = 0; i < _array.Length; i++)
				{
					if (_array[i].PrepareToReadReference(reader))
					{
						SR1_Structure structure = (SR1_Structure)_array[i].CreateObject(this, reader);
						structure.SetPadding(_EntryPadding);
						structure.Read(reader, null, "");
					}
				}
			}
		}

		public override string GetTypeName(bool includeDimensions)
		{
			string typeName = typeof(T).Name + "*";

			if (_array.Length == 0)
			{
				typeName += "[]";
				return typeName;
			}

			if (includeDimensions)
			{
				int rank = _dimensions.Length;
				for (int r = 0; r < rank; r++)
				{
					typeName += "[";
					typeName += _dimensions[r].ToString();
					typeName += "]";
				}
			}

			return typeName;
		}
	}
}
