namespace Recombobulator.SR1Structures
{
    class SignalChangeModel : SignalData
    {
        SR1_Pointer<Intro> intro = new SR1_Pointer<Intro>();
        SR1_Primative<int> model = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            intro.Read(reader, this, "intro");
            model.Read(reader, this, "model");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            intro.Write(writer);
            model.Write(writer);
        }
    }
}
