using System;

namespace Recombobulator.SR1Structures
{
	abstract class SR1_StructureSeries : SR1_StructureSeriesBase<SR1_Structure>
	{
	}

	class SR1_StructureSeries<T> : SR1_StructureSeries where T : SR1_Structure, new()
	{
		protected bool _UseBufferLength;
		protected int _BufferLength;

		public SR1_StructureSeries()
		{
		}

		public SR1_StructureSeries(int bufferLength)
		{
			_UseBufferLength = true;
			_BufferLength = bufferLength;
		}

		public void Add(T entry)
		{
			_List.Add(entry);
		}

		public void InsertAt(int index, T entry)
		{
			_List.Insert(index, entry);
		}

		public void RemoveAt(T entry)
		{
			_List.Remove(entry);
		}

		public void RemoveAt(int index)
		{
			_List.RemoveAt(index);
		}

		protected T CreateReplacementObject(SR1_Reader reader, in T original)
		{
			Type type = original.GetType();
			T temp = new T();
			T replacement = new T();
			long oldPosition = reader.BaseStream.Position;

			if (type == typeof(EventBasicObject))
			{
				EventBasicObject tempEBO = new EventBasicObject();
				tempEBO.ReadTemp(reader);
				temp = (T)tempEBO.CreateReplacementObject();
				replacement = (T)tempEBO.CreateReplacementObject();
			}
			else if (type == typeof(ObjectSound))
			{
				ObjectSound tempOS = new ObjectSound();
				tempOS.ReadTemp(reader);
				temp = (T)tempOS.CreateReplacementObject();
				replacement = (T)tempOS.CreateReplacementObject();
			}
			else if (type == typeof(PhysObProperties))
			{
				PhysObProperties tempPOP = new PhysObProperties();
				tempPOP.ReadTemp(reader);
				temp = (T)tempPOP.CreateReplacementObject();
				replacement = (T)tempPOP.CreateReplacementObject();
			}
			else if (type == typeof(VMObject))
			{
				VMObject tempVMO = new VMObject();
				tempVMO.ReadTemp(reader);
				temp = (T)tempVMO.CreateReplacementObject();
				replacement = (T)tempVMO.CreateReplacementObject();
			}

			reader.BaseStream.Position = oldPosition;
			temp.ReadTemp(reader);

			// Don't reset to old position after reading temp.
			// reader.BaseStream.Position will be used to tell whether
			// it fits in the buffer.

			return replacement;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			long endPosition = reader.BaseStream.Position + _BufferLength;
			int i = 0;

			if (_UseBufferLength)
			{
				_List.Clear();
			}

			while (true)
			{
				if (_UseBufferLength &&
					reader.BaseStream.Position >= endPosition)
				{
					break;
				}

				long oldPosition = reader.BaseStream.Position;
				T tempEntry = (_UseBufferLength) ? new T() : (T)_List[i];
				T newEntry = CreateReplacementObject(reader, in tempEntry);

				if (_UseBufferLength &&
					reader.BaseStream.Position > endPosition)
				{
					reader.BaseStream.Position = oldPosition;
					break;
				}

				reader.BaseStream.Position = oldPosition;
				newEntry.Read(reader, this, "[" + i.ToString() + "]");

				if (_UseBufferLength)
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
			string typeName = typeof(T).Name;

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
