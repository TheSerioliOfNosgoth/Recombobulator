namespace Recombobulator.SR1Structures
{
    class SignalMiscValue : SignalData
    {
        SR1_Primative<int> index = new SR1_Primative<int>();
        SR1_Primative<int> value = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            index.Read(reader, this, "index");
            value.Read(reader, this, "value");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            index.Write(writer);
            value.Write(writer);
        }
    }
}
