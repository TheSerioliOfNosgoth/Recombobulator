using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_StructureArrayBase<T> : SR1_Structure, IReadOnlyList<T> where T : SR1_Structure
	{
		protected int[] _dimensions = null;
		protected T[] _array = null;

		public T this[int i] { get { return _array[i]; } set { _array[i] = value; } }
		public int Count { get { return _array == null ? 0 : _array.Length; } }

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)_array).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _array.GetEnumerator();
		}

		protected abstract T CreateElement();

		public SR1_StructureArrayBase(params int[] dimensions)
		{
			_dimensions = new int[dimensions.Length];
			dimensions.CopyTo(_dimensions, 0);

			int arrayLength = 1;
			foreach (int d in dimensions)
			{
				arrayLength *= d;
			}

			_array = new T[arrayLength];

			for (int i = 0; i < _array.Length; i++)
			{
				_array[i] = CreateElement();
			}
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			for (int i = 0; i < _array.Length; i++)
			{
				Type type = _array[i].GetType();
				if (type == typeof(SR1Structures.EventBasicObject))
				{
					long oldPosition = reader.BaseStream.Position;
					SR1Structures.EventBasicObject tempEBO = new SR1Structures.EventBasicObject();
					tempEBO.ReadTemp(reader);
					reader.BaseStream.Position = oldPosition;

					_array[i] = (T)tempEBO.CreateReplacementObject();
				}
				else if (type == typeof(SR1Structures.ObjectSound))
				{
					long oldPosition = reader.BaseStream.Position;
					SR1Structures.ObjectSound tempOS = new SR1Structures.ObjectSound();
					tempOS.ReadTemp(reader);
					reader.BaseStream.Position = oldPosition;

					_array[i] = (T)tempOS.CreateReplacementObject();
				}
				else if (type == typeof(SR1Structures.PhysObProperties))
				{
					long oldPosition = reader.BaseStream.Position;
					SR1Structures.PhysObProperties tempPOP = new SR1Structures.PhysObProperties();
					tempPOP.ReadTemp(reader);
					reader.BaseStream.Position = oldPosition;

					_array[i] = (T)tempPOP.CreateReplacementObject();
				}
				else if (type == typeof(SR1Structures.VMObject))
				{
					long oldPosition = reader.BaseStream.Position;
					SR1Structures.VMObject tempVMO = new SR1Structures.VMObject();
					tempVMO.ReadTemp(reader);
					reader.BaseStream.Position = oldPosition;

					_array[i] = (T)tempVMO.CreateReplacementObject();
				}


				string elementName = "";
				int index = i;
				int d = _dimensions.Length;
				while (d > 0)
				{
					d--;
					int subIndex = index % _dimensions[d];
					index /= _dimensions[d];
					string indexName = "";
					indexName += "[";
					indexName += subIndex.ToString();
					indexName += "]";
					indexName += elementName;
					elementName = indexName;
				}

				_array[i].Read(reader, this, elementName);
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			for (int i = 0; i < _array.Length; i++)
			{
				_array[i].Write(writer);
			}
		}

		public override string ToString()
		{
			if (_array.Length == 0)
			{
				return "{ }";
			}

			return "{...}";
		}
	}
}
