namespace Recombobulator.SR1Structures
{
	class SignalSetSlideAngle : SignalData
	{
		SR1_Primative<int> slideAngle = new SR1_Primative<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			slideAngle.Read(reader, this, "slideAngle");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			slideAngle.Write(writer);
		}
	}
}
