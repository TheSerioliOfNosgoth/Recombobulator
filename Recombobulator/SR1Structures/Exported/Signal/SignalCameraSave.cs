namespace Recombobulator.SR1Structures
{
	class SignalCameraSave : SignalData
	{
		SR1_Primative<int> cameraSave = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			cameraSave.Read(reader, this, "cameraSave");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			cameraSave.Write(writer);
		}
	}
}
