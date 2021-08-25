namespace Recombobulator.SR1Structures
{
    class SignalStopVertexMorph : SignalData
    {
        SR1_Pointer<VMObject> vmobject = new SR1_Pointer<VMObject>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            vmobject.Read(reader, this, "vmobject");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            vmobject.Write(writer);
        }
    }
}
