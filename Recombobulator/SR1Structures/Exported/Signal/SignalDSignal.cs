namespace Recombobulator.SR1Structures
{
    class SignalDSignal : SignalData
    {
        SR1_Pointer<Intro> intro = new SR1_Pointer<Intro>();
        SR1_Pointer<object> data = new SR1_Pointer<object>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            intro.Read(reader, this, "intro");
            data.Read(reader, this, "data");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            intro.Write(writer);
            data.Write(writer);
        }
    }
}
