using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventVMOObject : EventBaseObject
	{
		// Inherited SR1_Primative<short> id = new SR1_Primative<short>();
		SR1_Primative<short> spad = new SR1_Primative<short>();
		SR1_Primative<int> unitID = new SR1_Primative<int>();
		SR1_PrimativeArray<char> vmoObjectName = new SR1_PrimativeArray<char>(16);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			spad.Read(reader, this, "spad");
			unitID.Read(reader, this, "unitID");
			vmoObjectName.Read(reader, this, "vmoObjectName");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			spad.Write(writer);
			unitID.Write(writer);
			vmoObjectName.Write(writer);
		}

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if (unitID.Value == file._Overrides.OldStreamUnitID &&
                file._Overrides.NewStreamUnitID != 0)
            {
                unitID.Value = file._Overrides.NewStreamUnitID;
            }
        }
    }
}
