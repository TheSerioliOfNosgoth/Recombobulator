namespace Recombobulator.SR1Structures
{
	class SignalStreamLevel : SignalData
	{
		SR1_Primative<int> currentnum = new SR1_Primative<int>();
		SR1_Primative<int> streamID = new SR1_Primative<int>();
		SR1_PrimativeArray<char> toname = new SR1_PrimativeArray<char>(16);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			currentnum.Read(reader, this, "currentnum");
			streamID.Read(reader, this, "streamID");
			toname.Read(reader, this, "toname");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			currentnum.Write(writer);
			streamID.Write(writer);
			toname.Write(writer);
		}
	}
}
