using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMMoveObject : VMObject
	{
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
			flags.Read(reader, this, "flags");
			bspIdx.Read(reader, this, "bspIdx");
			materialIdx.Read(reader, this, "materialIdx");
			spectralIdx.Read(reader, this, "spectralIdx");
			currentIdx.Read(reader, this, "currentIdx");
			timeScale.Read(reader, this, "timeScale");
			timer.Read(reader, this, "timer");
			position.Read(reader, this, "position");
			radius.Read(reader, this, "radius");
			radiusSquared.Read(reader, this, "radiusSquared");
			numVMOffsetTables.Read(reader, this, "numVMOffsetTables");
			vmoffsetTableList.Read(reader, this, "vmoffsetTableList");
			curVMOffsetTable.Read(reader, this, "curVMOffsetTable");
			numVMVertices.Read(reader, this, "numVMVertices");
			vmvertexList.Read(reader, this, "vmvertexList");
			numVMInterpolated.Read(reader, this, "numVMInterpolated");
			vminterpolatedList.Read(reader, this, "vminterpolatedList");
			name.Read(reader, this, "name");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			VMObjectList vmObjectData = (VMObjectList)parent.Parent;
			if (numVMOffsetTables.Value > 0) vmObjectData.VMOffsetTableLists.Add(new SR1_PointerArray<VMMoveOffsetTable>(numVMOffsetTables.Value, false));
			if (numVMVertices.Value > 0) vmObjectData.VMVertexLists.Add(new SR1_StructureArray<VMMoveVertex>(numVMVertices.Value));
			if (numVMInterpolated.Value > 0) vmObjectData.VMInterpolatedLists.Add(new SR1_StructureArray<VMInterpolated>(numVMInterpolated.Value));
			vmObjectData.VMObjectNames.Add(new SR1_String(12));
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			flags.Write(writer);
			bspIdx.Write(writer);
			materialIdx.Write(writer);
			spectralIdx.Write(writer);
			currentIdx.Write(writer);
			timeScale.Write(writer);
			timer.Write(writer);
			position.Write(writer);
			radius.Write(writer);
			radiusSquared.Write(writer);
			numVMOffsetTables.Write(writer);
			vmoffsetTableList.Write(writer);
			curVMOffsetTable.Write(writer);
			numVMVertices.Write(writer);
			vmvertexList.Write(writer);
			numVMInterpolated.Write(writer);
			vminterpolatedList.Write(writer);
			name.Write(writer);
		}
	}
}
