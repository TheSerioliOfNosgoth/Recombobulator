using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMColorObject : VMObject
	{
		public readonly SR1_Primative<short> bspIdx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> materialIdx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> spectralIdx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> currentIdx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> timeScale = new SR1_Primative<short>();
		public readonly SR1_Primative<int> timer = new SR1_Primative<int>();
		public readonly Position position = new Position();
		public readonly SR1_Primative<short> radius = new SR1_Primative<short>();
		public readonly SR1_Primative<int> radiusSquared = new SR1_Primative<int>();
		public readonly SR1_Primative<int> numVMOffsets = new SR1_Primative<int>();
		public readonly SR1_Pointer<VMMoveOffset> vmoffsetList = new SR1_Pointer<VMMoveOffset>();
		public readonly SR1_Primative<int> numVMOffsetTables = new SR1_Primative<int>();
		public readonly SR1_PointerArrayPointer<VMColorOffsetTable> vmoffsetTableList = new SR1_PointerArrayPointer<VMColorOffsetTable>();
		public readonly SR1_Pointer<VMColorOffsetTable> curVMOffsetTable = new SR1_Pointer<VMColorOffsetTable>();
		public readonly SR1_Primative<int> numVMVertices = new SR1_Primative<int>();
		public readonly SR1_Pointer<VMColorVertex> vmvertexList = new SR1_Pointer<VMColorVertex>();
		public readonly SR1_Primative<int> numVMInterpolated = new SR1_Primative<int>();
		public readonly SR1_Pointer<VMInterpolated> vminterpolatedList = new SR1_Pointer<VMInterpolated>();
		public readonly SR1_Pointer<SR1_String> name = new SR1_Pointer<SR1_String>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			base.ReadMembers(reader, parent);

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
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			base.WriteMembers(writer);

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

		public override string ToString()
		{
			string result = base.ToString();

			if (vmoffsetList.Start != 0)
			{
				result += " { offsets = 0x" + vmoffsetList.ToString() + " }";
			}

			if (vmoffsetTableList.Start != 0)
			{
				result += " { offsetTables = 0x" + vmoffsetTableList.ToString() + " }";
			}

			if (vmvertexList.Start != 0)
			{
				result += " { vertices = 0x" + vmvertexList.ToString() + " }";
			}

			if (vminterpolatedList.Start != 0)
			{
				result += " { interps = 0x" + vminterpolatedList.ToString() + " }";
			}

			return result;
		}
	}
}
