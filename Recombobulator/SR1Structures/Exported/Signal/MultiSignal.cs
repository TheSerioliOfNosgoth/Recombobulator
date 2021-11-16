using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MultiSignal : SR1_Structure
	{
		SR1_Primative<int> numSignals = new SR1_Primative<int>();
		SR1_Primative<short> signalNum = new SR1_Primative<short>();
		SR1_Primative<short> flags = new SR1_Primative<short>();
		SR1_StructureList<Signal> signalList = new SR1_StructureList<Signal>();
		SR1_Primative<int> pad = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numSignals.Read(reader, this, "numSignals");
			signalNum.Read(reader, this, "signalNum");
			flags.Read(reader, this, "flags");

			for (int i = 0; i < numSignals.Value; i++)
			{
				signalList.Add(new Signal());
			}
			signalList.Read(reader, this, "signalList");

			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numSignals.Write(writer);
			signalNum.Write(writer);
			flags.Write(writer);
			signalList.Write(writer);
			pad.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			int newNumSignals = 0;
			while (newNumSignals < signalList.Count)
			{
				if (((Signal)signalList[newNumSignals]).OmitFromMigration)
				{
					signalList.RemoveAt(newNumSignals);
				}
				else
				{
					newNumSignals++;
				}
			}

			numSignals.Value = newNumSignals;

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				pad.Value = 15;
			}
		}
	}
}
