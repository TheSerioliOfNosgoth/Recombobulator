namespace Recombobulator.SR1Structures
{
    class SignalHideObject : SignalData
    {
        SR1_Pointer<Intro> intro = new SR1_Pointer<Intro>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            intro.Read(reader, this, "intro");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            intro.Write(writer);
        }
    }
}
