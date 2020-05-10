namespace Recombobulator.SR1Structures
{
    class SignalCameraMode : SignalData
    {
        SR1_Primative<int> cameraMode = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraMode.Read(reader, this, "cameraMode");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraMode.Write(writer);
        }
    }
}
