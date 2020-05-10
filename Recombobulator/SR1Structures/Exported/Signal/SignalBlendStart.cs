namespace Recombobulator.SR1Structures
{
    class SignalBlendStart : SignalData
    {
        SR1_Primative<int> blendStart = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            blendStart.Read(reader, this, "blendStart");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
		    blendStart.Write(writer);
        }
    }
}
