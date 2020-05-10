namespace Recombobulator.SR1Structures
{
    class SignalCameraRestore : SignalData
    {
        SR1_Primative<int> cameraRestore = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraRestore.Read(reader, this, "cameraRestore");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraRestore.Write(writer);
        }
    }
}
