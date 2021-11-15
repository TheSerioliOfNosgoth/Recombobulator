using System.Collections.Generic;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
	class ScriptParser
	{
		enum ScriptType : sbyte
		{
			Condition = 0,
			Action = 1,
		}

		enum PCode : short
		{
			AddObjectToStack = 0,
			ModifyObjectToStackWithAttribute = 1,
			DoStackOperationEquals = 2, // Assign
			DoStackMathOperation10 = 3, // CompareEqualNumbersOnStack?
			Execute = 4,
			BeginConditional = 5,
			EndConditional = 6,
			BeginAction = 7,
			EndAction = 8,
			EndOfLine2 = 9,
			Unused2 = 10,
			AddNumberToStack = 11,
			DoStackMathOperation1 = 12, // AddValuesOnStack
			DoStackMathOperation2 = 13, // SubtractValuesOnStack
			DoStackMathOperation3 = 14, // MultiplyValuesOnStack
			DoStackMathOperation4 = 15, // DivideValuesOnStack
			ConvertToNumberAndMultiply = 16, // Copy stack, push 1, multiply, then test for equal.
			ConvertToNumberAndDivide = 17, // Copy stack, push 1, divide, then test for equal.
			DoStackMathOperation6 = 18, // CompareLessValuesOnStack
			DoStackMathOperation8 = 19, // CompareLessOrEqualValuesOnStack
			DoStackMathOperation7 = 20, // CompareGreaterValuesOnStack
			DoStackMathOperation9 = 21, // CompareGreaterOrEqualValuesOnStack
			DoStackMathOperation11 = 22, // CompareEqualValuesOnStack?
			DoStackMathOperation5 = 23, // RemainderValuesOnStack
			Unused3 = 24,
			Unused4 = 25,
			Unused5 = 26,
			AddGameObjectToStack = 27,
			AddPlayerObjectToStack = 28,
			AddSubListObjectToStack = 29,
			SetCurrentActionScriptBits = 30,
			DoStackMathOperation12 = 31, // UnknownMathOp12
		}

		struct PCodeDesc
		{
			public string n;
			public int l;
			public int mathOp;
		};

		static PCodeDesc[] _PCodes = new PCodeDesc[]
		{
			new PCodeDesc() { n = "PushObject", l = 2, mathOp = -1 },
			new PCodeDesc() { n = "ModifyObjectAttribute", l = 2, mathOp = -1 },
			new PCodeDesc() { n = "Assign", l = 1, mathOp = -1 }, // Definately assignment. var0.SetValue(var1)
            new PCodeDesc() { n = "MathOp10", l = 1, mathOp = 10 },
			new PCodeDesc() { n = "Execute", l = 1, mathOp = -1 },
			new PCodeDesc() { n = /*"Begin0"*/"BeginConditional", l = 1, mathOp = -1 },
			new PCodeDesc() { n = /*"End0"*/"EndConditional", l = 1, mathOp = -1 },
			new PCodeDesc() { n = /*"Begin1"*/"BeginAction", l = 1, mathOp = -1 },
			new PCodeDesc() { n = /*"End1"*/"EndAction", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "End2", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "Unused2", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "PushNumber", l = 2, mathOp = -1 },
			new PCodeDesc() { n = "Add", l = 1, mathOp = 1 },
			new PCodeDesc() { n = "Subtract", l = 1, mathOp = 2 },
			new PCodeDesc() { n = "Multiply", l = 1, mathOp = 3 },
			new PCodeDesc() { n = "Divide", l = 1, mathOp = 4 },
			new PCodeDesc() { n = "ConvertToNumberAndMultiply", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "ConvertToNumberAndDivide", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "IsLess", l = 1, mathOp = 6 },
			new PCodeDesc() { n = "IsLessOrEqual", l = 1, mathOp = 7 },
			new PCodeDesc() { n = "IsGreater", l = 1, mathOp = 8 },
			new PCodeDesc() { n = "IsGreaterOrEqual", l = 1, mathOp = 9 },
			new PCodeDesc() { n = "MathOp11", l = 1, mathOp = 11 },
			new PCodeDesc() { n = "Remainder", l = 1, mathOp = 5 },
			new PCodeDesc() { n = "Unused3", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "Unused4", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "Unused5", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "PushGameObject", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "PushPlayerObject", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "PushSubListObject", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "SetCurrentActionScriptBits", l = 1, mathOp = -1 },
			new PCodeDesc() { n = "MathOp12", l = 1, mathOp = 12 },
		};

		enum VarType : short
		{
			None = 0,
			Unknown1 = 1,
			InstanceObject = 2,
			Unknown3 = 3,
			Unknown4 = 4,
			Unknown5 = 5,
			Unknown6 = 6,
			Number = 7,
			Unknown8 = 8,
			Vector3d = 9,
			ShortPointer = 10,
			Unknown11 = 11,
			Unknown12 = 12,
			Unknown13 = 13,
			Rotation3d = 14,
			InstanceSpline = 15,
			EventObject = 16,
			Signal = 17,
			Unknown18 = 18,
			Unknown19 = 19,
			Unknown20 = 20,
			Unknown21 = 21,
			Unknown22 = 22,
			Unknown23 = 23,
			Unknown24 = 24,
			Unknown25 = 25,
			Unknown26 = 26,
			Unknown27 = 27,
			CharPointer = 28,
		}

		static string[] _VarTypes = new string[]
		{
			"None",
			"Unknown1",
			"InstanceObject",
			"Unknown3",
			"Unknown4",
			"Unknown5",
			"Unknown6",
			"Number",
			"Unknown8",
			"Vector3d",
			"ShortPointer",
			"Unknown11",
			"Unknown12",
			"Unknown13",
			"Rotation3d",
			"InstanceSpline",
			"EventObject",
			"SignalObject",
			"Unknown18",
			"Unknown19",
			"Unknown20",
			"Unknown21",
			"Unknown22",
			"Unknown23",
			"Unknown24",
			"Unknown25",
			"Unknown26",
			"Unknown27",
			"CharPointer",
		};

		struct Operation
		{
			public string n;
			public VarType t;
			public int p;
		}

		public void ParseAll(SR1_Reader reader)
		{
			if (reader.Events == null)
			{
				return;
			}

			unsafe
			{
				foreach (Event scriptEvent in reader.Events.events)
				{
					SR1_StructureArray<ScriptPCode> conditionals = scriptEvent.conditionals;
					SR1_StructureArray<ScriptPCode> actions = scriptEvent.actions;
					SR1_PointerArray<ScriptPCode> conditionalPointers = scriptEvent.conditionalPointers;
					SR1_PointerArray<ScriptPCode> actionPointers = scriptEvent.actionPointers;
					reader.LogScript("// Event " + scriptEvent.eventNumber + " \r\n");
					int c = 0;
					int a = 0;

					for (int b = 0; b < scriptEvent.numActions.Value; b++)
					{
						reader.LogScript("\t// Event " + scriptEvent.eventNumber + ", Action " + b.ToString() + " \r\n");

						if (conditionalPointers[c].Offset != 0)
						{
							Parse(reader, scriptEvent, ScriptType.Condition, ((ScriptPCode)conditionals[c]).dataBuf);
							c++;
						}

						if (actionPointers[a].Offset != 0)
						{
							Parse(reader, scriptEvent, ScriptType.Action, ((ScriptPCode)actions[a]).dataBuf);
							a++;
						}
					}
				}
			}
		}

		unsafe void Parse(SR1_Reader reader, Event scriptEvent, ScriptType type, SR1_PrimativeArray<short> scriptData)
		{
			if ((reader.File._ImportFlags & SR1_File.ImportFlags.LogScripts) != 0)
			{
				try
				{
					string script = "";

					short[] opCodes = new short[scriptData.Count];
					int c = 0;
					foreach (short code in scriptData)
					{
						opCodes[c] = code;
						c++;
					}

					List<VarType> stack = new List<VarType>();

					fixed (short* codeStreamStart = opCodes)
					{
						short* codeStream = codeStreamStart;

						c = 0;
						while (c < opCodes.Length)
						{
							PCode opcode = (PCode)(*codeStream - 1);
							int mathOp = _PCodes[(int)opcode].mathOp;
							int additionalBytes = 0;

							script += "\t\t";

							if (mathOp > -1)
							{
								Operation operation = GetCompareOperationFromID(stack[stack.Count - 1], _PCodes[(int)opcode].mathOp, stack, codeStream);

								VarType param0VarType = stack[stack.Count - 2];
								string param0 = "(" + _VarTypes[(int)param0VarType] + ")var" + (stack.Count - 2);
								VarType param1VarType = stack[stack.Count - 1];
								string param1 = "(" + _VarTypes[(int)param1VarType] + ")var" + (stack.Count - 1);
								string result = "(" + _VarTypes[(int)operation.t] + ")";

								script += "object var" + stack.Count + " = " + result + operation.n + "(" + param0 + ", " + param1 + ")";
								stack.Add(operation.t);
							}
							else if (opcode == PCode.AddObjectToStack)
							{
								int param = *(codeStream + 1);
								EventBaseObject scriptEventInstance = (EventBaseObject)scriptEvent.instances[param];

								VarType param0Type = GetVarType_AddObjectToStack(scriptEventInstance, stack, codeStream);
								string param0 = "(" + _VarTypes[(int)param0Type] + ")instances[" + param.ToString() + "]";

								script += "object var" + stack.Count + " = " + param0;
								stack.Add(param0Type);
							}
							else if (opcode == PCode.AddPlayerObjectToStack)
							{
								int param = *(codeStream + 1);

								VarType param0Type = VarType.InstanceObject;
								string param0 = "(" + _VarTypes[(int)param0Type] + ")gameTrackerX.playerInstance";

								script += "object var" + stack.Count + " = " + param0;
								stack.Add(param0Type);
							}
							else if (opcode == PCode.ModifyObjectToStackWithAttribute)
							{
								int param = *(codeStream + 1);
								Operation operation = GetTranformOperationFromID(stack[stack.Count - 1], param, stack, codeStream);

								VarType param0VarType = stack[stack.Count - 1];
								string param0 = "(" + _VarTypes[(int)param0VarType] + ")var" + (stack.Count - 1);
								string result = "(" + _VarTypes[(int)operation.t] + ")";

								if (operation.t != VarType.None)
								{
									script += "var" + (stack.Count - 1) + " = " + result;
								}

								if (param0VarType == VarType.EventObject)
								{
									// SavedEvent and SavedEventSmallVars both inherit from SavedBasic.
									// Hopefully the opcodes don't care where these come from and behave the same as local events.
									if (operation.p == 1)
									{
										string param1 = (*(codeStream + 2)).ToString();
										script += operation.n + "(" + param0 + ", " + param1 + ")";
										additionalBytes++;
									}
									else
									{
										script += operation.n + "(" + param0 + ")";
									}
								}
								else
								{
									script += operation.n + "(" + param0 + ")";
								}

								if (operation.t != VarType.None)
								{
									stack[stack.Count - 1] = operation.t;
								}
							}
							else if (opcode == PCode.AddNumberToStack)
							{
								int param = *(codeStream + 1);

								VarType param0Type = VarType.Number;
								string param0 = "(" + _VarTypes[(int)param0Type] + ")" + param.ToString();
								script += "object var" + stack.Count + " = " + param0;
								stack.Add(VarType.Number);
							}
							else if (opcode == PCode.DoStackOperationEquals)
							{
								VarType param0VarType = stack[stack.Count - 2];
								string param0 = "(" + _VarTypes[(int)param0VarType] + ")var" + (stack.Count - 2);
								VarType param1VarType = stack[stack.Count - 1];
								string param1 = "(" + _VarTypes[(int)param1VarType] + ")var" + (stack.Count - 1);

								script += _PCodes[*codeStream - 1].n + "(" + param0 + ", " + param1 + ")";
							}
							else if (opcode == PCode.Execute)
							{
								script += _PCodes[*codeStream - 1].n + "()";
								stack.Clear();
							}
							else
							{
								script += _PCodes[*codeStream - 1].n + "()";
							}

							script += ";\r\n";

							codeStream += _PCodes[*codeStream - 1].l;
							codeStream += additionalBytes;
							c += _PCodes[opCodes[c] - 1].l;
							c += additionalBytes;

							if (c < opCodes.Length &&
								(opcode == PCode.Execute ||
								opcode == PCode.EndConditional || opcode == PCode.EndAction || opcode == PCode.EndOfLine2))
							{
								script += "\r\n";
							}
						}

						reader.LogScript(script);
					}
				}
				catch
				{
					reader.LogScript("\t\t// Error reading " + type.ToString() + " script.\r\n");
				}
			}
		}

		unsafe VarType GetVarType_AddObjectToStack(EventBaseObject eventObject, List<VarType> stack, short* codeStream)
		{
			int objectId = eventObject.id.Value - 1;
			VarType stackEntryId = VarType.None;

			switch (objectId)
			{
				case 0:
					EventInstanceObject eventInstanceObject = (EventInstanceObject)eventObject;
					if ((eventInstanceObject.flags.Value & 1) == 0)
					{
						// The game checks instance and falls back to the intro.
						// Since the introUniqueId is all I have, use that.
						if (eventInstanceObject.introUniqueID.Value == 0)
						{
							// If the game can't find an instance, it would be 4, otherwise 6.
							break;
						}
						else
						{
							stackEntryId = VarType.InstanceObject;
							break;
						}
					}
					else
					{
						stackEntryId = VarType.Unknown27;
						break;
					}
				case 2:
					stackEntryId = VarType.EventObject;
					break;
				case 5:
					stackEntryId = VarType.Signal;
					break;
				default:
					break;
			}

			return stackEntryId;
		}

		unsafe Operation GetTranformOperationFromID(VarType varType, int item, List<VarType> stack, short* codeStream)
		{
			// These all look like functions to reposition objects in the game world.
			Operation operation = new Operation();
			operation.n = "GetUnknownTransform";
			operation.t = VarType.None;

			switch (varType)
			{
				case VarType.InstanceObject:
					operation = GetInstanceTranformOperationFromID(item, stack, codeStream);
					break;
				case VarType.EventObject:
					operation = GetEventTranformOperationFromID(item, stack, codeStream);
					break;
				case VarType.InstanceSpline:
					operation = GetInstanceSplineTranformOperationFromID(item, stack, codeStream);
					break;
				case VarType.Signal:
					operation = GetSignalTranformOperationFromID(item, stack, codeStream);
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetInstanceTranformOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "GetUnknownInstanceTransform";
			operation.t = VarType.None;

			switch (item)
			{
				case 5:
					operation.n = "GetInstancePosition";
					operation.t = VarType.Vector3d;
					break;
				case 12:
					operation.n = "GetInstanceSpline";
					operation.t = VarType.InstanceSpline;
					break;
				// EVENT_DoInstanceAction
				case 0x4c:
					operation.n = "SetInstanceNextMessage_0x0004000a"; // PositionMessage?
					operation.t = VarType.None;
					break;
				case 0x4e:
					operation.n = "SetInstanceNextMessage_0x0004000e";
					operation.t = VarType.None;
					break;
				case 0x20:
					// Message depends on the previous operand. Should pass in stack.
					// OK, there's no way to know for sure what this is before runtime, so maybe just return item here.
					operation.n = "SetInstanceNextMessage_Unknown";
					operation.t = VarType.None;
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetEventTranformOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "GetUnknownEventTransform";
			operation.t = VarType.None;

			switch (item)
			{
				case 3:
					operation.n = "GetEventVariableAddress";
					operation.t = VarType.ShortPointer;
					operation.p = 1;
					break;
				case 0x1A:
					operation.n = "SetEventVariableTrue";
					operation.t = VarType.ShortPointer;
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetInstanceSplineTranformOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();

			switch (item)
			{
				case 31:
					operation.n = "UpdateInstanceSplineA";
					operation.t = VarType.None;
					break;
				case 34:
					operation.n = "UpdateInstanceSplineB";
					operation.t = VarType.None;
					break;
				case 35:
					operation.n = "UpdateInstanceSplineC";
					operation.t = VarType.None;
					break;
				default:
					operation.n = "UpdateInstanceSplineD";
					operation.t = VarType.None;
					break;
			}

			return operation;
		}

		unsafe Operation GetSignalTranformOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "GetUnknownSignalTransform";
			operation.t = VarType.None;

			switch (item)
			{
				case 0x1a:
					operation.n = "GetSignalAsItSelf";
					operation.t = VarType.Signal;
					break;
				case 0x32:
					operation.n = "GetSignalFlags";
					operation.t = VarType.Number;
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetCompareOperationFromID(VarType varType, int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "UnknownCompare";
			operation.t = VarType.None;

			switch (varType)
			{
				case VarType.Vector3d:
					operation = GetVector3dCompareOperationFromID(item, stack, codeStream);
					break;
				case VarType.Number:
					operation = GetNumberCompareOperationFromID(item, stack, codeStream);
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetVector3dCompareOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "UnknownVector3dCompare";
			operation.t = VarType.None;

			switch (item)
			{
				case 10:
				// Fallthrough
				case 11:
					operation.n = "Vector3dCompareEquals";
					operation.t = VarType.Number;
					break;
				default:
					break;
			}

			return operation;
		}

		unsafe Operation GetNumberCompareOperationFromID(int item, List<VarType> stack, short* codeStream)
		{
			Operation operation = new Operation();
			operation.n = "UnknownNumberCompare";
			operation.t = VarType.None;

			switch (item)
			{
				case 10:
				// Fallthrough
				case 11:
					operation.n = "NumberCompareEquals";
					operation.t = VarType.Number;
					break;
				default:
					break;
			}

			return operation;
		}
	}
}
