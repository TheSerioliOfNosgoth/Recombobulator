namespace Recombobulator.SR1Structures
{
	class SignalCameraUnlock : SignalData
	{
		SR1_Primative<int> cameraUnlock = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			cameraUnlock.Read(reader, this, "cameraUnlock");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			cameraUnlock.Write(writer);
		}
	}
}
