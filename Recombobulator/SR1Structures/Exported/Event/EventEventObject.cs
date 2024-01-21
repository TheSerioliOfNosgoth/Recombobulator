using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class EventEventObject : EventBaseObject
	{
		// Inherited SR1_Primative<short> id = new SR1_Primative<short>();
		SR1_Primative<short> eventNumber = new SR1_Primative<short>();
		SR1_Primative<int> unitID = new SR1_Primative<int>();
		SR1_Pointer<Event> _event = new SR1_Pointer<Event>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			eventNumber.Read(reader, this, "eventNumber");
			unitID.Read(reader, this, "unitID");
			_event.Read(reader, this, "_event");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			eventNumber.Write(writer);
			unitID.Write(writer);
			_event.Write(writer);
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
