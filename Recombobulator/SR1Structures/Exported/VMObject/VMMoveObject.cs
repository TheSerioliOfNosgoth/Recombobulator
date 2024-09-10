using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMMoveObject : VMObject
	{
		SR1_Primative<int> flags0 = new SR1_Primative<int>();
		SR1_Primative<ushort> flags = new SR1_Primative<ushort>();
		SR1_Primative<short> bspIdx = new SR1_Primative<short>();
		SR1_Primative<short> materialIdx = new SR1_Primative<short>();
		SR1_Primative<short> spectralIdx = new SR1_Primative<short>();
		SR1_Primative<short> currentIdx = new SR1_Primative<short>();
		SR1_Primative<short> timeScale = new SR1_Primative<short>();
		SR1_Primative<int> timer = new SR1_Primative<int>();
		Position position = new Position();
		SR1_Primative<short> radius = new SR1_Primative<short>();
		SR1_Primative<int> radiusSquared = new SR1_Primative<int>();
		SR1_Primative<int> numVMOffsets = new SR1_Primative<int>();
		SR1_Pointer<VMMoveOffset> vmoffsetList = new SR1_Pointer<VMMoveOffset>();
		SR1_Primative<int> numVMOffsetTables = new SR1_Primative<int>();
		SR1_PointerArrayPointer<VMMoveOffsetTable> vmoffsetTableList = new SR1_PointerArrayPointer<VMMoveOffsetTable>();
		SR1_Pointer<VMMoveOffsetTable> curVMOffsetTable = new SR1_Pointer<VMMoveOffsetTable>();
		SR1_Primative<int> numVMVertices = new SR1_Primative<int>();
		SR1_Pointer<VMMoveVertex> vmvertexList = new SR1_Pointer<VMMoveVertex>();
		SR1_Primative<int> numVMInterpolated = new SR1_Primative<int>();
		SR1_Pointer<VMInterpolated> vminterpolatedList = new SR1_Pointer<VMInterpolated>();
		SR1_Pointer<SR1_String> name = new SR1_Pointer<SR1_String>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			flags0.Read(reader, this, "flags", SR1_File.Version.First, SR1_File.Version.Jan23);
			flags.Read(reader, this, "flags", SR1_File.Version.Jan23, SR1_File.Version.Next);
			bspIdx.Read(reader, this, "bspIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			materialIdx.Read(reader, this, "materialIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			spectralIdx.Read(reader, this, "spectralIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			currentIdx.Read(reader, this, "currentIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			timeScale.Read(reader, this, "timeScale", SR1_File.Version.Jan23, SR1_File.Version.Next);
			timer.Read(reader, this, "timer");
			position.Read(reader, this, "position");
			radius.Read(reader, this, "radius");
			radiusSquared.Read(reader, this, "radiusSquared");
			numVMOffsets.Read(reader, this, "numVMOffsets", SR1_File.Version.First, SR1_File.Version.Jan23);
			vmoffsetList.Read(reader, this, "vmoffsetlist", SR1_File.Version.First, SR1_File.Version.Jan23);
			numVMOffsetTables.Read(reader, this, "numVMOffsetTables", SR1_File.Version.Jan23, SR1_File.Version.Next);
			vmoffsetTableList.Read(reader, this, "vmoffsetTableList", SR1_File.Version.Jan23, SR1_File.Version.Next);
			curVMOffsetTable.Read(reader, this, "curVMOffsetTable", SR1_File.Version.Jan23, SR1_File.Version.Next);
			numVMVertices.Read(reader, this, "numVMVertices");
			vmvertexList.Read(reader, this, "vmvertexList");
			numVMInterpolated.Read(reader, this, "numVMInterpolated");
			vminterpolatedList.Read(reader, this, "vminterpolatedList");
			name.Read(reader, this, "name", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				uint end = 0;

				if (numVMOffsets.Value > 0)
				{
					var offsets = new SR1_StructureSeries<VMMoveOffset>();
					offsets.SetReadCount(numVMOffsets.Value);
					offsets.ReadFromPointer(reader, vmoffsetList);
					end = Math.Max(end, offsets.End);
				}

				if (numVMVertices.Value > 0)
				{
					var vertices = new SR1_StructureSeries<VMMoveVertex>();
					vertices.SetReadCount(numVMVertices.Value);
					vertices.ReadFromPointer(reader, vmvertexList);
					end = Math.Max(end, vertices.End);
				}

				if (numVMInterpolated.Value > 0)
				{
					var interps = new SR1_StructureSeries<VMInterpolated>();
					interps.SetReadCount(numVMInterpolated.Value);
					interps.ReadFromPointer(reader, vminterpolatedList);
					end = Math.Max(end, interps.End);
				}

				if ((end % 4) != 0)
				{
					reader.BaseStream.Position = end;
					new SR1_Primative<ushort>().Read(reader, null, "");
				}
			}
			else
			{
				if (numVMOffsetTables.Value > 0)
				{
					var offsetTables = new SR1_PointerSeries<VMMoveOffsetTable>();
					offsetTables.SetReadCount(numVMOffsetTables.Value);
				}

				if (numVMVertices.Value > 0)
				{
					var vertices = new SR1_StructureSeries<VMMoveVertex>();
					vertices.SetReadCount(numVMVertices.Value);
				}

				if (numVMInterpolated.Value > 0)
				{
					var interps = new SR1_StructureSeries<VMInterpolated>();
					interps.SetReadCount(numVMInterpolated.Value);
				}

				// vmObjectData.VMObjectNames.Add(new SR1_String(12));
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			flags.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			bspIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			materialIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			spectralIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			currentIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			timeScale.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			timer.Write(writer);
			position.Write(writer);
			radius.Write(writer);
			radiusSquared.Write(writer);
			numVMOffsets.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			vmoffsetList.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			numVMOffsetTables.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			vmoffsetTableList.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			curVMOffsetTable.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			numVMVertices.Write(writer);
			vmvertexList.Write(writer);
			numVMInterpolated.Write(writer);
			vminterpolatedList.Write(writer);
			name.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}
	}
}
