namespace Recombobulator.SR1Structures
{
    class Events : SR1_Structure
    {
        public SR1_StructureArray<Event> events = new SR1_StructureArray<Event>(0);
        public EventPointers eventPointers = new EventPointers();

        public Events(int numEvents)
            : base()
        {
            events = new SR1_StructureArray<Event>(numEvents);
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            events.Read(reader, this, "events");
            eventPointers.Read(reader, this, "eventPointers");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            events.Write(writer);
            eventPointers.Write(writer);
        }
    }
}
