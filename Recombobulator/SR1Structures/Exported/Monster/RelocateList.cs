using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class RelocateList : SR1_Structure
	{
		SR1_PrimativeArray<int> unknown = new SR1_PrimativeArray<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			int bufferLength = 0;
			long oldPos = reader.BaseStream.Position;

			while (true)
			{
				bufferLength++;

				int next = reader.ReadInt32();
				if (next == -1)
				{
					break;
				}
			}

			reader.BaseStream.Position = oldPos;

			unknown = new SR1_PrimativeArray<int>(bufferLength);
			unknown.Read(reader, this, "unknown");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			unknown.Write(writer);
		}
	}
}