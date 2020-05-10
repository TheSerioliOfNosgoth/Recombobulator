namespace Recombobulator.SR1Structures
{
    class SignalScreenWipe : SignalData
    {
        SR1_Primative<short> type = new SR1_Primative<short>();
        SR1_Primative<short> time = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
            time.Read(reader, this, "time");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
            time.Write(writer);
        }
    }
}
