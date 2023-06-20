using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class StreamUnitPortal : SR1_Structure
	{
		public readonly SR1_String tolevelname = new SR1_String(16);
		public readonly SR1_Primative<int> MSignalID = new SR1_Primative<int>();
		public readonly SR1_Primative<int> streamID = new SR1_Primative<int>();
		public readonly SR1_Primative<short> minx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> miny = new SR1_Primative<short>();
		public readonly SR1_Primative<short> minz = new SR1_Primative<short>();
		public readonly SR1_Primative<short> flags = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxx = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxy = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxz = new SR1_Primative<short>();
		public readonly SR1_Primative<short> pad2 = new SR1_Primative<short>();
		public readonly SR1_Pointer<StreamUnit> toStreamUnit = new SR1_Pointer<StreamUnit>();
		public SR1_StructureArray<SVector> t1 = new SR1_StructureArray<SVector>(3);
		public SR1_StructureArray<SVector> t2 = new SR1_StructureArray<SVector>(3);

		public bool OmitFromMigration { get; set; } = false;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			tolevelname.SetReadMax(true).Read(reader, this, "tolevelname");
			MSignalID.Read(reader, this, "MSignalID");
			streamID.Read(reader, this, "streamID");
			minx.Read(reader, this, "minx");
			miny.Read(reader, this, "miny");
			minz.Read(reader, this, "minz");
			flags.Read(reader, this, "flags");
			maxx.Read(reader, this, "maxx");
			maxy.Read(reader, this, "maxy");
			maxz.Read(reader, this, "maxz");
			pad2.Read(reader, this, "pad2");
			toStreamUnit.Read(reader, this, "toStreamUnit");
			t1.Read(reader, this, "t1");
			t2.Read(reader, this, "t2");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			tolevelname.Write(writer);
			MSignalID.Write(writer);
			streamID.Write(writer);
			minx.Write(writer);
			miny.Write(writer);
			minz.Write(writer);
			flags.Write(writer);
			maxx.Write(writer);
			maxy.Write(writer);
			maxz.Write(writer);
			pad2.Write(writer);
			toStreamUnit.Write(writer);
			t1.Write(writer);
			t2.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if ((migrateFlags & SR1_File.MigrateFlags.RemovePortals) != 0)
			{
				OmitFromMigration = true;
			}

			if (file._Version < SR1_File.Version.May12 && targetVersion >= SR1_File.Version.May12)
			{
				if (file._Structures[0].Name == "undrct15" && (MSignalID.Value == 3 || MSignalID.Value == 4))
				{
					SVector left = new SVector();
					SVector right = new SVector();

					left.x.Value = ((SVector)t1[1]).x.Value;
					left.y.Value = ((SVector)t1[1]).y.Value;
					left.z.Value = ((SVector)t1[1]).z.Value;
					right.x.Value = ((SVector)t1[2]).x.Value;
					right.y.Value = ((SVector)t1[2]).y.Value;
					right.z.Value = ((SVector)t1[2]).z.Value;
					((SVector)t1[1]).x.Value = right.x.Value;
					((SVector)t1[1]).y.Value = right.y.Value;
					((SVector)t1[1]).z.Value = right.z.Value;
					((SVector)t1[2]).x.Value = left.x.Value;
					((SVector)t1[2]).y.Value = left.y.Value;
					((SVector)t1[2]).z.Value = left.z.Value;

					left.x.Value = ((SVector)t2[1]).x.Value;
					left.y.Value = ((SVector)t2[1]).y.Value;
					left.z.Value = ((SVector)t2[1]).z.Value;
					right.x.Value = ((SVector)t2[2]).x.Value;
					right.y.Value = ((SVector)t2[2]).y.Value;
					right.z.Value = ((SVector)t2[2]).z.Value;
					((SVector)t2[1]).x.Value = right.x.Value;
					((SVector)t2[1]).y.Value = right.y.Value;
					((SVector)t2[1]).z.Value = right.z.Value;
					((SVector)t2[2]).x.Value = left.x.Value;
					((SVector)t2[2]).y.Value = left.y.Value;
					((SVector)t2[2]).z.Value = left.z.Value;
				}
			}
		}
	}
}
