using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class ScriptPCode : SR1_Structure
    {
        enum PCode : short
        {
            AddObjectToStack = 0,
            ModifyObjectToStackWithAttribute = 1,
            DoStackOperationEquals = 2,
            DoStackMathOperation10 = 3, // UnknownMathOp11
            ExecuteCode = 4,
            Unused0 = 5,
            EndOfLine0 = 6,
            Unused1 = 7,
            EndOfLine1 = 8,
            EndOfLine2 = 9,
            Unused2 = 10,
            AddNumberToStack = 11,
            DoStackMathOperation1 = 12, // AddNumbersOnStack
            DoStackMathOperation2 = 13, // SubtractNumbersOnStack
            DoStackMathOperation3 = 14, // MultiplyNumbersOnStack
            DoStackMathOperation4 = 15, // DivideNumbersOnStack
            ConvertToNumberAndMultiply = 16, // Copy stack, push 1, multiply, then test for equal.
            ConvertToNumberAndDivide = 17, // Copy stack, push 1, divide, then test for equal.
            DoStackMathOperation6 = 18, // CompareLessumbersOnStack
            DoStackMathOperation8 = 19, // CompareLessOrEqualNumbersOnStack
            DoStackMathOperation7 = 20, // CompareGreaterNumbersOnStack
            DoStackMathOperation9 = 21, // CompareGreaterOrEqualNumbersOnStack
            DoStackMathOperation11 = 22, // UnknownMathOp11
            DoStackMathOperation5 = 23, // RemainderNumbersOnStack
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
        };

        static PCodeDesc[] _PCodes = new PCodeDesc[]
        {
            new PCodeDesc() { n = "PushObject", l = 2 },
            new PCodeDesc() { n = "ModifyObjectAttribute", l = 2 },
            new PCodeDesc() { n = "IsEqual", l = 1 },
            new PCodeDesc() { n = "MathOp11", l = 1 },
            new PCodeDesc() { n = "Execute", l = 1 },
            new PCodeDesc() { n = "Unused0", l = 1 },
            new PCodeDesc() { n = "return", l = 1 },
            new PCodeDesc() { n = "Unused1", l = 1 },
            new PCodeDesc() { n = "return;", l = 1 },
            new PCodeDesc() { n = "return", l = 1 },
            new PCodeDesc() { n = "Unused2", l = 1 },
            new PCodeDesc() { n = "PushNumber", l = 2 },
            new PCodeDesc() { n = "Add", l = 1 },
            new PCodeDesc() { n = "Subtract", l = 1 },
            new PCodeDesc() { n = "Multiply", l = 1 },
            new PCodeDesc() { n = "Divide", l = 1 },
            new PCodeDesc() { n = "ConvertToNumberAndMultiply", l = 1 },
            new PCodeDesc() { n = "ConvertToNumberAndDivide", l = 1 },
            new PCodeDesc() { n = "IsLess", l = 1 },
            new PCodeDesc() { n = "IsLessOrEqual", l = 1 },
            new PCodeDesc() { n = "IsGreater", l = 1 },
            new PCodeDesc() { n = "IsGreaterOrEqual", l = 1 },
            new PCodeDesc() { n = "MathOp11", l = 1 },
            new PCodeDesc() { n = "Remainder", l = 1 },
            new PCodeDesc() { n = "Unused3", l = 1 },
            new PCodeDesc() { n = "Unused4", l = 1 },
            new PCodeDesc() { n = "Unused5", l = 1 },
            new PCodeDesc() { n = "PushGameObject", l = 1 },
            new PCodeDesc() { n = "PushPlayerObject", l = 1 },
            new PCodeDesc() { n = "PushSubListObject", l = 1 },
            new PCodeDesc() { n = "SetCurrentActionScriptBits", l = 1 },
            new PCodeDesc() { n = "MathOp11", l = 1 },
        };

        SR1_Primative<ushort> sizeOfPcodeStream = new SR1_Primative<ushort>();
        SR1_Primative<ushort> conditionBits = new SR1_Primative<ushort>();
        SR1_PrimativePointer<short> data = new SR1_PrimativePointer<short>();

        SR1_PrimativeArray<short> dataBuf = new SR1_PrimativeArray<short>(0);

        string debugString = "";

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sizeOfPcodeStream.Read(reader, this, "sizeOfPcodeStream");
            conditionBits.Read(reader, this, "conditionBits");
            data.Read(reader, this, "data");

            if (data.Offset != 0)
            {
                dataBuf = new SR1_PrimativeArray<short>(sizeOfPcodeStream.Value);
                dataBuf.SetPadding(4).Read(reader, this, "dataBuf");

                bool debugScripting = false;
                if (debugScripting)
                {
                    try
                    {

                        short[] opCodes = new short[dataBuf.List.Count];
                        int c = 0;
                        foreach (short code in dataBuf.List)
                        {
                            opCodes[c] = code;
                            c++;
                        }

                        c = 0;
                        while (c < opCodes.Length)
                        {
                            PCode opcode = (PCode)(opCodes[c] - 1);

                            debugString += _PCodes[opCodes[c] - 1].n;

                            if (opcode == PCode.AddObjectToStack ||
                                opcode == PCode.ModifyObjectToStackWithAttribute ||
                                opcode == PCode.AddNumberToStack)
                            {
                                debugString += ("(" + opCodes[c + 1].ToString() + ")");
                            }

                            debugString += ";\r\n";

                            if (opcode == PCode.ExecuteCode ||
                                opcode == PCode.EndOfLine0 ||
                                opcode == PCode.EndOfLine1 ||
                                opcode == PCode.EndOfLine2)
                            {
                                debugString += "\r\n";
                            }

                            c += _PCodes[opCodes[c] - 1].l;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sizeOfPcodeStream.Write(writer);
            conditionBits.Write(writer);
            data.Write(writer);

            if (data.Offset != 0) dataBuf.Write(writer);
        }
    }
}
