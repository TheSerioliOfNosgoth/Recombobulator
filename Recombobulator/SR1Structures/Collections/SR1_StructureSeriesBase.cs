using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	abstract class SR1_StructureSeriesBase<T> : SR1_Structure where T : SR1_Structure
	{
		protected List<T> _List = new List<T>();

		public T this[int i] { get { return _List[i]; } set { _List[i] = value; } }
		public int Count { get { return _List.Count; } }

		public IReadOnlyList<T> List { get { return _List; } }

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

		public override string ToString()
		{
			if (_List.Count == 0)
			{
				return "{ }";
			}

			return "{...}";
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
