using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MonsterAttackList : SR1_Structure
	{
		SR1_PrimativeArray<byte> data = new SR1_PrimativeArray<byte>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			int bufferLength = 0;
			long oldPos = reader.BaseStream.Position;

			while (true)
			{
				bufferLength++;

				byte next = reader.ReadByte();
				if (next == 0xD3)
				{
					break;
				}
			}

			reader.BaseStream.Position = oldPos;

			data = new SR1_PrimativeArray<byte>(bufferLength);
			data.SetPadding(4);
			data.Read(reader, this, "data");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			data.Write(writer);
		}
	}
}
