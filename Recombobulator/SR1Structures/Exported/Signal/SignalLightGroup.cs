namespace Recombobulator.SR1Structures
{
    class SignalLightGroup : SignalData
    {
        SR1_Primative<int> lightGroup = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            lightGroup.Read(reader, this, "lightGroup");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            lightGroup.Write(writer);
        }
    }
}
