namespace Recombobulator.SR1Structures
{
    class SignalGoToFrame : SignalData
    {
        SR1_Pointer<Intro> intro = new SR1_Pointer<Intro>();
        SR1_Primative<int> frame = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            intro.Read(reader, this, "intro");
            frame.Read(reader, this, "frame");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            intro.Write(writer);
            frame.Write(writer);
        }
    }
}
