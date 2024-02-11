using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class INICommand : SR1_Structure
	{
		public readonly INICommandType command = new INICommandType();
		SR1_Primative<short> numParameters = new SR1_Primative<short>();
		SR1_PrimativeArray<int> parameter = new SR1_PrimativeArray<int>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			command.Read(reader, this, "command");
			numParameters.Read(reader, this, "numParameters");

			parameter = new SR1_PrimativeArray<int>(numParameters.Value);
			parameter.Read(reader, this, "parameter");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			command.Write(writer);
			numParameters.Write(writer);
			parameter.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version != targetVersion)
			{
				if (command.Value == 0x1A && parameter.Count >= 1)
				{
                    if (file._Overrides.NewIntroIDs.TryGetValue(parameter[0], out int newIntroID))
                    {
                        parameter[0] = newIntroID;
                    }
				}
			}
		}
	}
}
