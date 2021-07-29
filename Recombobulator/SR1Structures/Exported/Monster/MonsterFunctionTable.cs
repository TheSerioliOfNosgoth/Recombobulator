using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterFunctionTable : SR1_Structure
    {
        SR1_Primative<uint> initFunc = new SR1_Primative<uint>();
        SR1_Primative<uint> cleanUpFunc = new SR1_Primative<uint>();
        SR1_Primative<uint> damageEffectFunc = new SR1_Primative<uint>();
        SR1_Primative<uint> queryFunc = new SR1_Primative<uint>();
        SR1_Primative<uint> messageFunc = new SR1_Primative<uint>();
        SR1_Primative<uint> stateFuncs = new SR1_Primative<uint>();
        SR1_Primative<uint> versionID = new SR1_Primative<uint>();
        SR1_Primative<uint> localVersionID = new SR1_Primative<uint>();

        SR1_PrimativeArray<byte> asmCode = new SR1_PrimativeArray<byte>(0);
        SR1_StructureArray<MonsterStateChoice> stateChoices = new SR1_StructureArray<MonsterStateChoice>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            initFunc.Read(reader, this, "initFunc");
            cleanUpFunc.Read(reader, this, "cleanUpFunc");
            damageEffectFunc.Read(reader, this, "damageEffectFunc");
            queryFunc.Read(reader, this, "queryFunc");
            messageFunc.Read(reader, this, "messageFunc");
            stateFuncs.Read(reader, this, "stateFuncs");
            versionID.Read(reader, this, "versionID");
            localVersionID.Read(reader, this, "localVersionID");

            int codeLength = (int)((long)(Start + stateFuncs.Value) - localVersionID.End);
            asmCode = new SR1_PrimativeArray<byte>(codeLength);
            asmCode.Read(reader, this, "asmCode");

            int numChoices = 0;
            MonsterStateChoice tempMSC = new MonsterStateChoice();
            do
            {
                numChoices++;
                tempMSC.ReadTemp(reader);
            }
            while (tempMSC.state.Value != -1);

            reader.BaseStream.Position = asmCode.End;

            stateChoices = new SR1_StructureArray<MonsterStateChoice>(numChoices);
            stateChoices.Read(reader, this, "stateChoices");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            initFunc.Write(writer);
            cleanUpFunc.Write(writer);
            damageEffectFunc.Write(writer);
            queryFunc.Write(writer);
            messageFunc.Write(writer);
            stateFuncs.Write(writer);
            versionID.Write(writer);
            localVersionID.Write(writer);

            asmCode.Write(writer);
            stateChoices.Write(writer);
        }
    }
}