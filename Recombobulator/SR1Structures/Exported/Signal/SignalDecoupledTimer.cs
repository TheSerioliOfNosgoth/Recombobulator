namespace Recombobulator.SR1Structures
{
	class SignalDecoupledTimer : SignalData
	{
		SR1_Primative<int> timer = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			timer.Read(reader, this, "timer");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			timer.Write(writer);
		}
	}
}
