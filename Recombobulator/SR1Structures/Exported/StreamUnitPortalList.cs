using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class StreamUnitPortalList : SR1_Structure
	{
		public readonly SR1_Primative<int> numPortals = new SR1_Primative<int>();
		public readonly SR1_StructureList<StreamUnitPortal> portals = new SR1_StructureList<StreamUnitPortal>();
		public readonly SR1_Primative<int> pad = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numPortals.Read(reader, this, "numPortals");

			for (int i = 0; i < numPortals.Value; i++)
			{
				portals.Add(new StreamUnitPortal());
			}
			portals.Read(reader, this, "portals");

			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numPortals.Write(writer);
			portals.Write(writer);
			pad.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			int newNumPortals = 0;
			while (newNumPortals < portals.Count)
			{
				if (((StreamUnitPortal)portals[newNumPortals]).OmitFromMigration)
				{
					portals.RemoveAt(newNumPortals);
				}
				else
				{
					newNumPortals++;
				}
			}

			numPortals.Value = newNumPortals;
		}
	}
}
