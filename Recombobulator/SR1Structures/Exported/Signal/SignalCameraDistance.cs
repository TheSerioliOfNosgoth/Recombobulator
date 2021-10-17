namespace Recombobulator.SR1Structures
{
	class SignalCameraDistance : SignalData
	{
		SR1_Primative<int> cameraDistance = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			cameraDistance.Read(reader, this, "cameraDistance");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			cameraDistance.Write(writer);
		}
	}
}
