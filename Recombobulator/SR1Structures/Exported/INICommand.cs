using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class INICommand : SR1_Structure
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

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion)
        {
            base.MigrateVersion(file, targetVersion);

            if (file._Version != targetVersion)
            {
                if (command.Value == 0x1A && parameter.Length >= 1 &&
                    file._IntroIDs != null && file._NewIntroIDs != null)
                {
                    for (int i = 0; i < file._IntroIDs.Count; i++)
                    {
                        if (file._IntroIDs[i] == parameter[i])
                        {
                            parameter[i] = file._NewIntroIDs[i];
                            break;
                        }
                    }
                }
            }
        }
    }
}
