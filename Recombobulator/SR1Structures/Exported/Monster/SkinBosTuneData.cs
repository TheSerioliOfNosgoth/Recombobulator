using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SkinBosTuneData : MonsterTuneData
	{
		SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(32);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version >= SR1_File.Version.Jul14)
			{
				unknown = new SR1_PrimativeArray<byte>(32);
			}
			else
			{
				unknown = new SR1_PrimativeArray<byte>(28);
			}
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