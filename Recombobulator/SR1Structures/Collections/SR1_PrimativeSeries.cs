using System;
using System.Collections;
using System.Collections.Generic;

namespace Recombobulator.SR1Structures
{
	public class SR1_PrimativeSeries<T> : SR1_PrimativeBase, IReadOnlyList<T>
	{
		protected List<T> _List = new List<T>();
		protected bool _UseReadCount;
		protected bool _UseReadLength;
		protected int _ReadCount;
		protected int _ReadLength;

		public T this[int i] { get { return _List[i]; } set { _List[i] = value; } }
		public int Count { get { return _List.Count; } }
		public override bool IsArray() { return true; }

		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)_List).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _List.GetEnumerator();
		}

		public SR1_PrimativeSeries()
		{
		}

		public SR1_Structure SetReadCount(int count)
		{
			_UseReadCount = true;
			_UseReadLength = false;
			_ReadCount = count;
			_ReadLength = 0;

			return this;
		}

		public SR1_Structure SetReadLength(int length)
		{
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

		public void Add(T entry)
		{
			_List.Add(entry);
		}

		public void InsertAt(int index, T entry)
		{
			_List.Insert(index, entry);
		}

		public void Remove(T entry)
		{
			_List.Remove(entry);
		}

		public void RemoveAt(int index)
		{
			_List.RemoveAt(index);
		}

		public SR1_PrimativeSeries<T> ShowAsHex(bool hex)
		{
			_showAsHex = hex;
			return this;
		}

		// May not be safe. The idea behind this was that pointers could reference
		// primatives inside arrays by calculating their offsets, which might not
		// work on a resizable structure.
		//protected override void AddToRead(SR1_Reader reader)
		//{
		//	if (Start != End && !reader.File.IsReadingTempStruct)
		//	{
		//		reader.File._PrimsRead.Add(this);
		//	}
		//}

		// May not be safe. The idea behind this was that pointers could reference
		// primatives inside arrays by calculating their offsets, which might not
		// work on a resizable structure.
		//protected override void AddToWritten(SR1_Writer writer)
		//{
		//	if (Start != End && !writer.File.IsWritingMigStruct)
		//	{
		//		writer.File._PrimsWritten.Add(this);
		//	}
		//}

		public override TreeList.Node CreateNode()
		{
			string offsetString = " (0x" + Start.ToString("X8") + "-" + "0x" + End.ToString("X8") + ")";
			string typeString = GetTypeName(true);
			string nameString = Name;
			string valueString = ToString();
			TreeList.Node node = new TreeList.Node(new string[] { offsetString, typeString, nameString, valueString });

			TreeList.Node[] elementNodes = new TreeList.Node[_List.Count];
			uint size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(_List[0]);
			uint start = Start;
			uint end = Start + size;
			for (int i = 0; i < _List.Count; i++)
			{
				offsetString = " (0x" + start.ToString("X8") + "-" + "0x" + end.ToString("X8") + ")";
				typeString = GetTypeName(false);
				nameString = "[" + i.ToString() + "]";
				valueString = _List[i].ToString();

				elementNodes[i] = new TreeList.Node(new string[] { offsetString, typeString, nameString, valueString });

				start += size;
				end += size;
			}

			node.Nodes.AddRange(elementNodes);

			return node;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			long endPosition = reader.BaseStream.Position + _ReadLength;
			int i = 0;

			if (_UseReadCount || _UseReadLength)
			{
				_List.Clear();
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

				bool readPreset = (_UseReadCount || _UseReadLength);
				long oldPosition = reader.BaseStream.Position;
				T newEntry = (T)ReadPrimativeType<T>(reader);

				if (_UseReadLength &&
					reader.BaseStream.Position > endPosition)
				{
					reader.BaseStream.Position = oldPosition;
					break;
				}

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

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			// Primatives don't contain references.
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			for (int i = 0; i < _List.Count; i++)
			{
				WritePrimativeType<T>(writer, _List[i]);
			}
		}

		public override string ToString()
		{
			if (typeof(T) == typeof(char))
			{
				string result = "";
				foreach (object o in _List)
				{
					if ((char)o == '\0')
					{
						break;
					}

					result += (char)o;
				}

				return result;
			}
			else
			{
				string result = "{ ";
				foreach (object o in _List)
				{
					if (_showAsHex)
					{
						result += GetPrimativeAsHex<T>((T)o);
					}
					else
					{
						result += o.ToString();
					}
					result += ", ";
				}
				result = result.Trim(',', ' ');
				result += " }";

				return result;
			}
		}

		public override string GetTypeName(bool includeDimensions)
		{
			string typeName = GetPrimativeTypeName(Type.GetTypeCode(typeof(T)));

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
