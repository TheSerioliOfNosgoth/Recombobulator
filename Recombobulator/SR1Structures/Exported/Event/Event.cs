using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Event : SR1_Structure
	{
		public SR1_Primative<short> eventNumber = new SR1_Primative<short>();
		public SR1_Primative<short> numInstances = new SR1_Primative<short>();
		public SR1_Primative<byte> numActions = new SR1_Primative<byte>();
		public SR1_Primative<byte> processingPuppetShow = new SR1_Primative<byte>();
		public SR1_PrimativeArray<short> eventVariables = new SR1_PrimativeArray<short>(5);
		public SR1_PointerArrayPointer<EventBasicObject> instanceList = new SR1_PointerArrayPointer<EventBasicObject>();
		public SR1_PointerArrayPointer<ScriptPCode> conditionalList = new SR1_PointerArrayPointer<ScriptPCode>();
		public SR1_PointerArrayPointer<ScriptPCode> actionList = new SR1_PointerArrayPointer<ScriptPCode>();

		public SR1_PointerArray<EventBasicObject> instancePointers = new SR1_PointerArray<EventBasicObject>(0, false);
		public SR1_PointerArray<ScriptPCode> conditionalPointers = new SR1_PointerArray<ScriptPCode>(0, false);
		public SR1_PointerArray<ScriptPCode> actionPointers = new SR1_PointerArray<ScriptPCode>(0, false);

		public SR1_StructureArray<EventBasicObject> instances = new SR1_StructureArray<EventBasicObject>(0);
		public SR1_StructureArray<ScriptPCode> conditionals = new SR1_StructureArray<ScriptPCode>(0);
		public SR1_StructureArray<ScriptPCode> actions = new SR1_StructureArray<ScriptPCode>(0);

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

			if (instanceList.Offset != 0 && numInstances.Value > 0)
			{
				instancePointers = new SR1_PointerArray<EventBasicObject>(numInstances.Value, false);
				instancePointers.Read(reader, this, "instancePonters");
			}

			int tempNumConditions = 0;
			int tempNumActions = 0;

			if (numActions.Value > 0)
			{
				if (conditionalList.Offset != 0)
				{
					conditionalPointers = new SR1_PointerArray<ScriptPCode>(numActions.Value, false);
					conditionalPointers.Read(reader, this, "conditionalPointers");
					foreach (SR1_PointerBase pointer in conditionalPointers)
					{
						if (pointer.Offset != 0)
						{
							tempNumConditions++;
						}
					}
				}

				if (actionList.Offset != 0)
				{
					actionPointers = new SR1_PointerArray<ScriptPCode>(numActions.Value, false);
					actionPointers.Read(reader, this, "actionPointers");
					foreach (SR1_PointerBase pointer in actionPointers)
					{
						if (pointer.Offset != 0)
						{
							tempNumActions++;
						}
					}
				}
			}

			if (instanceList.Offset != 0 && numInstances.Value > 0)
			{
				instances = new SR1_StructureArray<EventBasicObject>(numInstances.Value);
				instances.Read(reader, this, "instances");
			}

			if (tempNumConditions > 0)
			{
				conditionals = new SR1_StructureArray<ScriptPCode>(tempNumConditions);
				conditionals.Read(reader, this, "conditionals");
			}

			if (tempNumActions > 0)
			{
				actions = new SR1_StructureArray<ScriptPCode>(tempNumActions);
				actions.Read(reader, this, "actions");
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
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

			instancePointers.Write(writer);
			conditionalPointers.Write(writer);
			actionPointers.Write(writer);
			instances.Write(writer);
			conditionals.Write(writer);
			actions.Write(writer);
		}
	}
}
