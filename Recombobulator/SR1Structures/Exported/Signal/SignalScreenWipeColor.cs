namespace Recombobulator.SR1Structures
{
	class SignalScreenWipeColor : SignalData
	{
		SR1_Primative<byte> r = new SR1_Primative<byte>();
		SR1_Primative<byte> g = new SR1_Primative<byte>();
		SR1_Primative<byte> b = new SR1_Primative<byte>();
		SR1_Primative<byte> pad = new SR1_Primative<byte>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			r.Read(reader, this, "r");
			g.Read(reader, this, "g");
			b.Read(reader, this, "b");
			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			r.Write(writer);
			g.Write(writer);
			b.Write(writer);
			pad.Write(writer);
		}
	}
}
