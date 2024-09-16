using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMColorVertex : VMVertex
	{
		public readonly SR1_Pointer<TVertex> tv = new SR1_Pointer<TVertex>();
		public readonly SR1_Primative<short> r = new SR1_Primative<short>();
		public readonly SR1_Primative<short> g = new SR1_Primative<short>();
		public readonly SR1_Primative<short> b = new SR1_Primative<short>();
		public readonly SR1_Primative<short> tvIdx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> offset = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			tv.Read(reader, this, "tv", SR1_File.Version.First, SR1_File.Version.Jan23);
			r.Read(reader, this, "r", SR1_File.Version.First, SR1_File.Version.Jan23);
			g.Read(reader, this, "g", SR1_File.Version.First, SR1_File.Version.Jan23);
			b.Read(reader, this, "b", SR1_File.Version.First, SR1_File.Version.Jan23);
			tvIdx.Read(reader, this, "tvIdx", SR1_File.Version.Jan23, SR1_File.Version.Next);
			offset.Read(reader, this, "offset");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			tv.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			r.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			g.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			b.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			tvIdx.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			offset.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Jan23)
			{
				tvIdx.Value = (short)VertexIndex;
			}
		}
	}
}
