namespace Recombobulator.SR1Structures
{
	class SignalDepricated : SignalData
	{
		SR1_PrimativeArray<int> buf = new SR1_PrimativeArray<int>(0);

		public SignalDepricated(int length)
		{
			buf = new SR1_PrimativeArray<int>(length);
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			buf.Read(reader, this, "buf");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			buf.Write(writer);
		}
	}
}
