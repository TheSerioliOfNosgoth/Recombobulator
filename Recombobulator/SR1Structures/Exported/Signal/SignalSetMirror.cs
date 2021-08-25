namespace Recombobulator.SR1Structures
{
    class SignalSetMirror : SignalData
    {
        SR1_Pointer<Mirror> mirror = new SR1_Pointer<Mirror>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            mirror.Read(reader, this, "mirror");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new Mirror().ReadFromPointer(reader, mirror);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            mirror.Write(writer);
        }
    }
}
