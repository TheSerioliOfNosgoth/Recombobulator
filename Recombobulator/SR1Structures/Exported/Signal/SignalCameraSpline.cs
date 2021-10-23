namespace Recombobulator.SR1Structures
{
	class SignalCameraSpline : SignalData
	{
		SR1_Primative<int> index = new SR1_Primative<int>();
		SR1_Pointer<Intro> intro = new SR1_Pointer<Intro>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			index.Read(reader, this, "index");
			intro.Read(reader, this, "intro");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			index.Write(writer);
			intro.Write(writer);
		}
	}
}
