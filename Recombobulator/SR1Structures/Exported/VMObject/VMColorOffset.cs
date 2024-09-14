using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VMColorOffset : VMOffset
	{
		SR1_Primative<short> dr0 = new SR1_Primative<short>();
		SR1_Primative<short> dg0 = new SR1_Primative<short>();
		SR1_Primative<short> db0 = new SR1_Primative<short>();
		SR1_Primative<sbyte> dr = new SR1_Primative<sbyte>();
		SR1_Primative<sbyte> dg = new SR1_Primative<sbyte>();
		SR1_Primative<sbyte> db = new SR1_Primative<sbyte>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			dr0.Read(reader, this, "dr", SR1_File.Version.First, SR1_File.Version.Jan23);
			dg0.Read(reader, this, "dg", SR1_File.Version.First, SR1_File.Version.Jan23);
			db0.Read(reader, this, "db", SR1_File.Version.First, SR1_File.Version.Jan23);
			dr.Read(reader, this, "dr", SR1_File.Version.Jan23, SR1_File.Version.Next);
			dg.Read(reader, this, "dg", SR1_File.Version.Jan23, SR1_File.Version.Next);
			db.Read(reader, this, "db", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			dr0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			dg0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			db0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			dr.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			dg.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			db.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Jan23)
			{
				dr.Value = (sbyte)dr0.Value;
				dg.Value = (sbyte)dg0.Value;
				db.Value = (sbyte)db0.Value;
			}
		}
	}
}
