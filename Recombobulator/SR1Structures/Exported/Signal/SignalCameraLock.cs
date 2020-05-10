namespace Recombobulator.SR1Structures
{
    class SignalCameraLock : SignalData
    {
        SR1_Primative<int> cameraLock = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraLock.Read(reader, this, "cameraLock");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraLock.Write(writer);
        }
    }
}
