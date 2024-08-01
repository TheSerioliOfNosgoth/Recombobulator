namespace Recombobulator.SR1Structures
{
	class SignalStreamLevel : SignalData
	{
		public readonly SR1_Primative<int> currentnum = new SR1_Primative<int>();
		public readonly SR1_Primative<int> streamID = new SR1_Primative<int>();
		public readonly SR1_String toname = new SR1_String(16);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			currentnum.Read(reader, this, "currentnum");
			streamID.Read(reader, this, "streamID", SR1_File.Version.Jan23, SR1_File.Version.Next);
			toname.SetReadMax(true).Read(reader, this, "toname");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			currentnum.Write(writer);
			streamID.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			toname.Write(writer);
		}

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if (streamID.Value == file._Overrides.OldStreamUnitID &&
                file._Overrides.NewStreamUnitID != 0)
            {
                streamID.Value = file._Overrides.NewStreamUnitID;
            }
        }
    }
}
