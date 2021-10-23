namespace Recombobulator.SR1Structures
{
	class SignalDeathZ : SignalData
	{
		SR1_Primative<Intro> deathZ = new SR1_Primative<Intro>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			deathZ.Read(reader, this, "deathZ");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			deathZ.Write(writer);
		}
	}
}
