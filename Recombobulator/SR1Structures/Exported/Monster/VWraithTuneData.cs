﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VWraithTuneData : MonsterTuneData
	{
		SR1_PrimativeArray<byte> unknown = new SR1_PrimativeArray<byte>(20);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
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