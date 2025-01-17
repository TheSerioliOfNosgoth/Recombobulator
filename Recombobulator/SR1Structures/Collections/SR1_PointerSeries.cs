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
		protected bool _NullTerminated = true;
		protected bool _UseReadCount;
		protected bool _UseReadLength;
		protected int _ReadCount;
		protected int _ReadLength;

		public SR1_Structure SetNullTerminated()
		{
			_NullTerminated = true;
			_UseReadCount = false;
			_UseReadLength = false;
			_ReadCount = 0;
			_ReadLength = 0;

			return this;
		}

		public SR1_Structure SetReadQueue()
		{
			_NullTerminated = false;
			_UseReadCount = false;
			_UseReadLength = false;
			_ReadCount = 0;
			_ReadLength = 0;

			return this;
		}

		public SR1_Structure SetReadCount(int count)
		{
			_NullTerminated = false;
			_UseReadCount = true;
			_UseReadLength = false;
			_ReadCount = count;
			_ReadLength = 0;

			return this;
		}

		public SR1_Structure SetReadLength(int length)
		{
			_NullTerminated = false;
			_UseReadCount = false;
			_UseReadLength = true;
			_ReadCount = 0;
			_ReadLength = length;

			return this;
		}

		public SR1_Structure ReadFromPointer(SR1_Reader reader, SR1_PointerBase startPointer, int count)
		{
			if (startPointer != null && startPointer.Offset != 0 &&
				count > 0)
			{
				SetReadCount(count);

				if (_ReadCount > 0 &&
					startPointer.PrepareToReadReference(reader))
				{
					Read(reader, null, "");
				}
			}

			return this;
		}

		public SR1_Structure ReadFromPointer(SR1_Reader reader, SR1_PointerBase startPointer, uint endOffset)
		{
			if (startPointer != null && startPointer.Offset != 0 &&
				endOffset != 0)
			{
				SetReadLength((int)(endOffset - startPointer.Offset));

				if (_ReadLength > 0 &&
					startPointer.PrepareToReadReference(reader))
				{
					Read(reader, null, "");
				}
			}

			return this;
		}

		public SR1_Structure ReadFromPointer(SR1_Reader reader, SR1_PointerBase startPointer, SR1_PointerBase endPointer)
		{
			if (endPointer != null)
			{
				ReadFromPointer(reader, startPointer, endPointer.Offset);
			}

			return this;
		}

		public void Add<U>(SR1_Pointer<U> entry) where U : T, new()
		{
			_List.Add(entry);
		}

		public void Add<U>(SR1_Pointer<U>[] entries) where U : T, new()
		{
			_List.AddRange(entries);
		}

		public void InsertAt<U>(int index, SR1_Pointer<U> entry) where U : T, new()
		{
			_List.Insert(index, entry);
		}

		public void Remove<U>(SR1_Pointer<U> entry) where U : T, new()
		{
			_List.Remove(entry);
		}

		public void RemoveAt(int index)
		{
			_List.RemoveAt(index);
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			long endPosition = reader.BaseStream.Position + _ReadLength;
			int i = 0;

			bool readPreset = (_NullTerminated || _UseReadCount || _UseReadLength);
			if (readPreset)
			{
				_List.Clear();
			}

			if (_NullTerminated)
			{
				SR1_Pointer<T> newEntry;
				do
				{
					newEntry = new SR1_Pointer<T>();
					newEntry.Read(reader, this, "[" + i.ToString() + "]");
					_List.Add(newEntry);
				}
				while (newEntry.Offset != 0);

				return;
			}

			while (true)
			{
				if (_UseReadCount && i >= _ReadCount)
				{
					break;
				}

				if (_UseReadLength &&
					reader.BaseStream.Position >= endPosition)
				{
					break;
				}

				if (!readPreset && i >= _List.Count)
				{
					break;
				}

				long oldPosition = reader.BaseStream.Position;
				SR1_PointerBase newEntry = (readPreset) ? new SR1_Pointer<T>() : _List[i];

				if (_UseReadLength &&
					reader.BaseStream.Position > endPosition)
				{
					reader.BaseStream.Position = oldPosition;
					break;
				}

				reader.BaseStream.Position = oldPosition;
				newEntry.Read(reader, this, "[" + i.ToString() + "]");

				if (readPreset)
				{
					_List.Add(newEntry);
				}
				else
				{
					_List[i] = newEntry;
				}

				i++;
			}
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
