using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class TVertex : SR1_Structure
	{
		public readonly Vertex vertex = new Vertex();
		public readonly SR1_Primative<ushort> unknown0 = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<ushort> rbg15 = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<byte> r0 = new SR1_Primative<byte>().ShowAsHex(true);
		public readonly SR1_Primative<byte> g0 = new SR1_Primative<byte>().ShowAsHex(true);
		public readonly SR1_Primative<byte> b0 = new SR1_Primative<byte>().ShowAsHex(true);
		public readonly SR1_Primative<byte> code = new SR1_Primative<byte>().ShowAsHex(true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			vertex.Read(reader, this, "vertex");
			unknown0.Read(reader, this, "unknown0", SR1_File.Version.First, SR1_File.Version.Jan23);
			rbg15.Read(reader, this, "rbg15", SR1_File.Version.Jan23, SR1_File.Version.Next);
			r0.Read(reader, this, "r0");
			g0.Read(reader, this, "g0");
			b0.Read(reader, this, "b0");
			code.Read(reader, this, "code");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			vertex.Write(writer);
			unknown0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			rbg15.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			r0.Write(writer);
			g0.Write(writer);
			b0.Write(writer);
			code.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Jan23)
			{
				int r = (r0.Value >> 3) << 0;
				int g = (g0.Value >> 3) << 5;
				int b = (b0.Value >> 3) << 10;
				int color = r | g | b;

				rbg15.Value = (ushort)color;
			}
		}
	}
}
