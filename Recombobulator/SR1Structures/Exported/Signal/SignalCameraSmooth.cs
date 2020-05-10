namespace Recombobulator.SR1Structures
{
    class SignalCameraSmooth : SignalData
    {
        SR1_Primative<int> cameraSmooth = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraSmooth.Read(reader, this, "cameraSmooth");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraSmooth.Write(writer);
        }
    }
}
