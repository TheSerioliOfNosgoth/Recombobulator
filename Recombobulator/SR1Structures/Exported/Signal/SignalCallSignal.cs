namespace Recombobulator.SR1Structures
{
	class SignalCallSignal : SignalData
	{
		// Suspected only.
		SR1_Pointer<MultiSignal> callSignal = new SR1_Pointer<MultiSignal>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			callSignal.Read(reader, this, "callSignal");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			callSignal.Write(writer);
		}
	}
}
