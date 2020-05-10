namespace Recombobulator.SR1Structures
{
    class SignalCameraShake : SignalData
    {
        SR1_Primative<int> time = new SR1_Primative<int>();
        SR1_Primative<int> scale = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            time.Read(reader, this, "time");
            scale.Read(reader, this, "scale");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            time.Write(writer);
            scale.Write(writer);
        }
    }
}
