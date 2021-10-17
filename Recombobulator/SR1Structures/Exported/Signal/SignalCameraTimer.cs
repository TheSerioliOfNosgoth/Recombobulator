namespace Recombobulator.SR1Structures
{
	class SignalCameraTimer : SignalData
	{
		SR1_Primative<int> cameraTimer = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			cameraTimer.Read(reader, this, "cameraTimer");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			cameraTimer.Write(writer);
		}
	}
}
