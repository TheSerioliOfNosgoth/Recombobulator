﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMMoveVertex : VMVertex
	{
		SR1_Pointer<TVertex> tv = new SR1_Pointer<TVertex>();
		SR1_Primative<short> tvIdx = new SR1_Primative<short>();
		Position basePos = new Position();
		SR1_Primative<short> offset = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			tv.Read(reader, this, "tv", SR1_File.Version.First, SR1_File.Version.Jan23);
			tvIdx.Read(reader, this, "tvIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			basePos.Read(reader, this, "basePos");
			offset.Read(reader, this, "offset");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			tv.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			tvIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			basePos.Write(writer);
			offset.Write(writer);
		}
	}
}
