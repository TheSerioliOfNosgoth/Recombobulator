namespace Recombobulator.SR1Structures
{
	class SignalCameraKey : SignalData
	{
		SR1_Pointer<CameraKey> cameraKey = new SR1_Pointer<CameraKey>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			cameraKey.Read(reader, this, "cameraKey");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			cameraKey.Write(writer);
		}
	}
}
