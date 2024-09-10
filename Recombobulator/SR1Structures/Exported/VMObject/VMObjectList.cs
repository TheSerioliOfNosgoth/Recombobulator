using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMObjectList : SR1_Structure
	{
		int _NumObjects;

		public readonly SR1_StructureSeries<VMObject> VMObjects = new SR1_StructureSeries<VMObject>();
		public readonly SR1_StructureList<SR1_PointerSeries<VMOffsetTable>> VMOffsetTableLists = new SR1_StructureList<SR1_PointerSeries<VMOffsetTable>>();
		public readonly SR1_StructureList<SR1_StructureSeries<VMVertex>> VMVertexLists = new SR1_StructureList<SR1_StructureSeries<VMVertex>>();
		public readonly SR1_StructureList<SR1_StructureSeries<VMInterpolated>> VMInterpolatedLists = new SR1_StructureList<SR1_StructureSeries<VMInterpolated>>();
		private SR1_PrimativeArray<byte> pad0 = new SR1_PrimativeArray<byte>(0);
		public readonly SR1_StructureSeries<SR1_String> VMObjectNames = new SR1_StructureSeries<SR1_String>();
		private SR1_PrimativeArray<byte> pad1 = new SR1_PrimativeArray<byte>(0);
		public readonly SR1_StructureSeries<VMOffsetTable> VMOffsetTables = new SR1_StructureSeries<VMOffsetTable>();

		public VMObjectList(int numObjects)
		{
			_NumObjects = numObjects;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			VMObjects.SetReadCount(_NumObjects);
			VMObjects.Read(reader, this, "VMObjects");
			VMOffsetTableLists.Read(reader, this, "VMOffsetTableLists");
			VMVertexLists.Read(reader, this, "VMVertexLists");
			VMInterpolatedLists.Read(reader, this, "VMInterpolatedLists");

			uint mod = VMInterpolatedLists.End % 4;
			if (mod > 0)
			{
				uint padding = 4 - mod;
				pad0 = new SR1_PrimativeArray<byte>((int)padding);
			}
			pad0.Read(reader, this, "pad0");

			VMObjectNames.Read(reader, this, "VMObjectNames");

			mod = VMObjectNames.End % 4;
			if (mod > 0)
			{
				uint padding = 4 - mod;
				pad1 = new SR1_PrimativeArray<byte>((int)padding);
			}
			pad1.Read(reader, this, "pad1");

			SortedDictionary<uint, SR1_PointerBase> dictionary = new SortedDictionary<uint, SR1_PointerBase>();
			foreach (SR1_StructureSeries tableList in VMOffsetTableLists)
			{
				foreach (SR1_PointerBase tablePointer in tableList)
				{
					if (tablePointer.Offset != 0 && !dictionary.ContainsKey(tablePointer.Offset))
					{
						dictionary.Add(tablePointer.Offset, tablePointer);
					}
				}
			}

			foreach (SR1_PointerBase tablePointer in dictionary.Values)
			{
				VMOffsetTable vmOffsetTable = (VMOffsetTable)tablePointer.CreateObject(tablePointer, reader);
				VMOffsetTables.Add(vmOffsetTable);
			}

			VMOffsetTables.Read(reader, this, "VMOffsetTables");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{

		}

		public override void WriteMembers(SR1_Writer writer)
		{
			VMObjects.Write(writer);
			VMOffsetTableLists.Write(writer);
			VMVertexLists.Write(writer);
			VMInterpolatedLists.Write(writer);
			pad0.Write(writer);
			VMObjectNames.Write(writer);
			pad1.Write(writer);
			VMOffsetTables.Write(writer);
		}
	}
}
