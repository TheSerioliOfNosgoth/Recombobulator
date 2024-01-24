using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class StreamUnitPortalList : SR1_Structure
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

			/*Level level = (Level)file._Structures[0];
			if (level.Name == "city12")
			{
				StreamUnitPortal newPortalA = new StreamUnitPortal();
				newPortalA.tolevelname.SetReadMax(true);
				newPortalA.tolevelname.SetText("city11,2", 16);
				newPortalA.streamID.Value = 154;
				newPortalA.MSignalID.Value = 1;
				newPortalA.minx.Value = 2686;
				newPortalA.miny.Value = 2086;
				newPortalA.minz.Value = -10878;
				newPortalA.maxx.Value = 3672;
				newPortalA.maxy.Value = 3611;
				newPortalA.maxz.Value = -9147;
				((SVector)newPortalA.t1[0]).SetValues(2686, 2086, -10696); // Face 149
				((SVector)newPortalA.t1[1]).SetValues(3672, 3611, -9329); // Face 150
				((SVector)newPortalA.t1[2]).SetValues(3152, 2086, -9147); // Face 151
				((SVector)newPortalA.t2[0]).SetValues(2686, 2086, -10696); // Face 149
				((SVector)newPortalA.t2[1]).SetValues(3206, 3611, -10878); // Face 152
				((SVector)newPortalA.t2[2]).SetValues(3672, 3611, -9329); // Face 150
				portals.Add(newPortalA);
				newNumPortals++;

				StreamUnitPortal newPortalB = new StreamUnitPortal();
				newPortalB.tolevelname.SetReadMax(true);
				newPortalB.tolevelname.SetText("city16,97", 16);
				newPortalB.streamID.Value = 215;
				newPortalB.MSignalID.Value = 98;
				newPortalB.minx.Value = 10879;
				newPortalB.miny.Value = -1396;
				newPortalB.minz.Value = -14072;
				newPortalB.maxx.Value = 10880;
				newPortalB.maxy.Value = 1163;
				newPortalB.maxz.Value = -12792;
				((SVector)newPortalB.t1[0]).SetValues(10880, 1163, -12792); // Face 153
				((SVector)newPortalB.t1[1]).SetValues(10880, 1163, -14072); // Face 136
				((SVector)newPortalB.t1[2]).SetValues(10879, -1396, -14072); // Face 148
				((SVector)newPortalB.t2[0]).SetValues(10879, -1396, -14072); // Face 148
				((SVector)newPortalB.t2[1]).SetValues(10879, -1396, -12792); // Face 154
				((SVector)newPortalB.t2[2]).SetValues(10880, 1163, -12792); // Face 153
				portals.Add(newPortalB);
				newNumPortals++;
			}*/

			numPortals.Value = newNumPortals;
		}
	}
}
