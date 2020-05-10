namespace Recombobulator.SR1Structures
{
    class SignalCameraAdjust : SignalData
    {
        SR1_Primative<int> cameraAdjust = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            cameraAdjust.Read(reader, this, "cameraAdjust");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            cameraAdjust.Write(writer);
        }
    }
}
