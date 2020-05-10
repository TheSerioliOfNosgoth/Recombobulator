using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Event : SR1_Structure
    {
        SR1_Primative<short> eventNumber = new SR1_Primative<short>();
        SR1_Primative<short> numInstances = new SR1_Primative<short>();
        SR1_Primative<byte> numActions = new SR1_Primative<byte>();
        SR1_Primative<byte> processingPuppetShow = new SR1_Primative<byte>();
        SR1_PrimativeArray<short> eventVariables = new SR1_PrimativeArray<short>(5);
        SR1_PointerArrayPointer<EventBasicObject> instanceList = new SR1_PointerArrayPointer<EventBasicObject>();
        SR1_PointerArrayPointer<ScriptPCode> conditionalList = new SR1_PointerArrayPointer<ScriptPCode>();
        SR1_PointerArrayPointer<ScriptPCode> actionList = new SR1_PointerArrayPointer<ScriptPCode>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            eventNumber.Read(reader, this, "eventNumber");
            numInstances.Read(reader, this, "numInstances");
            numActions.Read(reader, this, "numActions");
            processingPuppetShow.Read(reader, this, "processingPuppetShow");
            eventVariables.Read(reader, this, "eventVariables");
            instanceList.Read(reader, this, "instanceList");
            conditionalList.Read(reader, this, "conditionalList");
            actionList.Read(reader, this, "actionList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            if (numInstances.Value > 0)
            {
                new SR1_PointerArray<EventBasicObject>(numInstances.Value, true).ReadFromPointer(reader, instanceList);
            }

            if (numActions.Value > 0)
            {
                new SR1_PointerArray<ScriptPCode>(numActions.Value, true).ReadFromPointer(reader, conditionalList);
                new SR1_PointerArray<ScriptPCode>(numActions.Value, true).ReadFromPointer(reader, actionList);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            eventNumber.Write(writer);
            numInstances.Write(writer);
            numActions.Write(writer);
            processingPuppetShow.Write(writer);
            eventVariables.Write(writer);
            instanceList.Write(writer);
            conditionalList.Write(writer);
            actionList.Write(writer);
        }
    }
}
