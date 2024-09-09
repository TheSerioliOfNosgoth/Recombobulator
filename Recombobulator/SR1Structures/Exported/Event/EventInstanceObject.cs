using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventInstanceObject : EventBaseObject
	{
		// Inherited SR1_Primative<short> id = new SR1_Primative<short>();
		public SR1_Primative<short> flags = new SR1_Primative<short>();
		public SR1_Primative<int> unitID = new SR1_Primative<int>();
		public SR1_Primative<int> introUniqueID = new SR1_Primative<int>();
		public SR1_Pointer<Instance> instance = new SR1_Pointer<Instance>();
		// Sometimes an SFXMarker, but always null in the area files, so probaby doesn't matter.
		public SR1_Pointer<Intro> data = new SR1_Pointer<Intro>();

		Intro Intro = null;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			flags.Read(reader, this, "flags");
			unitID.Read(reader, this, "unitID");
			introUniqueID.Read(reader, this, "introUniqueID");
			instance.Read(reader, this, "instance");
			data.Read(reader, this, "data");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			Level level = (Level)reader.File._Structures[0];
			if (level.introList.Offset != 0)
			{
				SR1_StructureSeries<Intro> intros = (SR1_StructureSeries<Intro>)reader.File._Structures[level.introList.Offset];
				foreach (Intro intro in intros)
				{
					if (intro.UniqueID.Value == introUniqueID.Value)
					{
						Intro = intro;
						break;
					}
				}
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			flags.Write(writer);
			unitID.Write(writer);
			introUniqueID.Write(writer);
			instance.Write(writer);
			data.Write(writer);
        }
		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

            if (unitID.Value == file._Overrides.OldStreamUnitID &&
				file._Overrides.NewStreamUnitID != 0)
            {
                unitID.Value = file._Overrides.NewStreamUnitID;
            }

            if (file._Overrides.NewIntroIDs.TryGetValue(introUniqueID.Value, out int newIntroID))
            {
                introUniqueID.Value = newIntroID;
            }
        }

        public override string ToString()
		{
			string result = base.ToString();

			if (Intro != null)
			{
				result += "{ Intro = 0x" + Intro.Start.ToString("X8") + ", " + Intro.name + "-" + introUniqueID.Value + " }";
			}

			return result;
		}
	}
}
