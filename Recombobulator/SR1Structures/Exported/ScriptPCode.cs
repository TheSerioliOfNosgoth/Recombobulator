using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class ScriptPCode : SR1_Structure
	{
		public SR1_Primative<ushort> sizeOfPcodeStream = new SR1_Primative<ushort>();
		public SR1_Primative<ushort> conditionBits = new SR1_Primative<ushort>();
		public SR1_PrimativePointer<short> data = new SR1_PrimativePointer<short>();

		public SR1_PrimativeArray<short> dataBuf = new SR1_PrimativeArray<short>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			sizeOfPcodeStream.Read(reader, this, "sizeOfPcodeStream");
			conditionBits.Read(reader, this, "conditionBits");
			data.Read(reader, this, "data");

			if (data.Offset != 0)
			{
				dataBuf = new SR1_PrimativeArray<short>(sizeOfPcodeStream.Value);
				dataBuf.SetPadding(4).Read(reader, this, "dataBuf");
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			sizeOfPcodeStream.Write(writer);
			conditionBits.Write(writer);
			data.Write(writer);

			if (data.Offset != 0) dataBuf.Write(writer);
		}
	}
}
