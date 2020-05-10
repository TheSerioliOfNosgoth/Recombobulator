namespace Recombobulator.SR1Structures
{
    class SignalFogFar : SignalData
    {
        SR1_Primative<int> fogFar = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            fogFar.Read(reader, this, "fogFar");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            fogFar.Write(writer);
        }
    }
}
