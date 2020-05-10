namespace Recombobulator.SR1Structures
{
    class SignalFogNear : SignalData
    {
        SR1_Primative<int> fogNear = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            fogNear.Read(reader, this, "fogNear");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            fogNear.Write(writer);
        }
    }
}
