namespace Recombobulator.SR1Structures
{
	class SignalStartAnitex : SignalData
	{
		SR1_Primative<int> misc = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			misc.Read(reader, this, "misc");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			misc.Write(writer);
		}
	}
}
