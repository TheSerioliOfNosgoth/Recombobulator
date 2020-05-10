namespace Recombobulator.SR1Structures
{
    class SignalCameraTilt : SignalData
    {
        SR1_Primative<int> cameraTilt = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraTilt.Read(reader, this, "cameraTilt");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraTilt.Write(writer);
        }
    }
}
