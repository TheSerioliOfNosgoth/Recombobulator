using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class G2AnimKeylist_Type : SR1_Structure
    {
        struct G2AnimSegKeyflagInfo_Type
        {
            public long stream;
            public uint flags;
            public int bitCount;
        }

        struct SectionData
        {
            public int numSections;
            public int numSegments;
            public int sectionA;
            public int sectionB;
            public int sectionC;
        }

        class Channel
        {
            public SR1_PrimativePointer<byte> chanData = new SR1_PrimativePointer<byte>();
            public SR1_PrimativeArray<byte> chanDataBuf = new SR1_PrimativeArray<byte>();
            int section;

            public Channel(int section)
            {
                this.section = section;
            }

            public uint ReadChanData(SR1_Reader reader, SR1_Structure parent)
            {
                chanData.Read(reader, parent, "chanData" + section);
                return chanData.End;
            }

            public void ReadChanDataBuf(SR1_Reader reader, SR1_Structure parent, int keyCount, int firstSeg, int lastSeg, int numSegments, long segKeyList)
            {
                int chanLength = FooBar(reader, segKeyList, (int)chanData.Offset, keyCount, firstSeg, lastSeg, numSegments);
                chanDataBuf = new SR1_PrimativeArray<byte>(chanLength);
                chanDataBuf.Read(reader, parent, "chanData" + section + "Buf");
            }
        }

        public readonly SR1_Primative<byte> sectionCount = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> s0TailTime = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> s1TailTime = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> s2TailTime = new SR1_Primative<byte>();
        public readonly SR1_Primative<ushort> keyCount = new SR1_Primative<ushort>();

        public readonly SR1_Primative<short> timePerKey = new SR1_Primative<short>();
        public readonly SR1_Primative<ushort> pad00 = new SR1_Primative<ushort>();
        public readonly SR1_Primative<short> pad01 = new SR1_Primative<short>();
        public readonly SR1_Primative<ushort> pad10 = new SR1_Primative<ushort>();
        public readonly SR1_Primative<short> pad11 = new SR1_Primative<short>();
        public readonly SR1_Pointer<G2AnimFxHeader_Type> fxList = new SR1_Pointer<G2AnimFxHeader_Type>();

        SectionData sectionData = new SectionData();
        Channel[] channels = { new Channel(0), new Channel(1), new Channel(2) };

        SR1_PrimativeArray<byte> chanDataflags = new SR1_PrimativeArray<byte>();
        SR1_PrimativeArray<byte> pad = new SR1_PrimativeArray<byte>(0);

        SR1_String objectName = new SR1_String(12);

        int padOverrideLength = -1;

        public void OverridePadLength(int length)
        {
            padOverrideLength = length;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sectionCount.Read(reader, this, "sectionCount");
            s0TailTime.Read(reader, this, "s0TailTime");
            s1TailTime.Read(reader, this, "s1TailTime");
            s2TailTime.Read(reader, this, "s2TailTime");
            keyCount.Read(reader, this, "keyCount");

            if (sectionCount.Value == 255)
            {
                objectName.SetPadding(4);
                objectName.Read(reader, this, "objectName");
            }
            else
            {
                ReadSectionData(reader);

                timePerKey.Read(reader, this, "timePerKey");
                pad00.Read(reader, this, "pad00");
                pad01.Read(reader, this, "pad01");
                pad10.Read(reader, this, "pad10");
                pad11.Read(reader, this, "pad11");
                fxList.Read(reader, this, "fxList");

                uint mod;
                long segKeyList = 0;

                if (sectionData.numSections > 0) segKeyList = channels[0].ReadChanData(reader, this);
                if (sectionData.numSections > 1) segKeyList = channels[1].ReadChanData(reader, this);
                if (sectionData.numSections > 2) segKeyList = channels[2].ReadChanData(reader, this);

                int flagBitOffset = 8;
                int segIndex = ((sectionData.numSegments * 3) + 7) & unchecked((int)0xfffffff8);
                byte wombatFlags = reader.ReadByte();
                reader.BaseStream.Position--;

                if ((wombatFlags & 0x01) != 0) flagBitOffset += segIndex;
                if ((wombatFlags & 0x02) != 0) flagBitOffset += segIndex;
                if ((wombatFlags & 0x04) != 0) flagBitOffset += segIndex;

                int nextOffset = 0;
                mod = (uint)flagBitOffset % 16; // Need 16 bits to pad the buffer to 2 bytes.
                if (mod > 0)
                {
                    flagBitOffset += (int)(16 - mod); // Need 16 bits to pad the buffer to 2 bytes.
                }
                nextOffset += flagBitOffset / 8;

                chanDataflags = new SR1_PrimativeArray<byte>(nextOffset);
                chanDataflags.Read(reader, this, "chanDataFlags");

                if (sectionData.numSections > 0)
                {
                    channels[0].ReadChanDataBuf(reader, this, keyCount.Value, 0, sectionData.sectionA, sectionData.numSegments, segKeyList);
                }

                if (sectionData.numSections > 1)
                {
                    channels[1].ReadChanDataBuf(reader, this, keyCount.Value, sectionData.sectionA + 1, sectionData.sectionB, sectionData.numSegments, segKeyList);
                }

                if (sectionData.numSections > 2)
                {
                    channels[2].ReadChanDataBuf(reader, this, keyCount.Value, sectionData.sectionB + 1, sectionData.sectionC, sectionData.numSegments, segKeyList);
                }

                if (padOverrideLength >= 0)
                {
                    pad = new SR1_PrimativeArray<byte>(padOverrideLength);
                }
                else
                {
                    mod = (uint)(reader.BaseStream.Position) % 4;
                    if (mod > 0)
                    {
                        uint padding = 4 - mod;
                        pad = new SR1_PrimativeArray<byte>((int)padding);
                    }
                }
                pad.Read(reader, this, "pad");

                //G2EmulationSetStartAndEndSegment(&Raziel.State, 0, 0, 0xd);
                //G2EmulationSetStartAndEndSegment(&Raziel.State, 1, 0xe, 0x31);
                //G2EmulationSetStartAndEndSegment(&Raziel.State, 2, 0x32, 0x41);
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            if (fxList.Offset != 0 && !reader.AnimFXDictionary.ContainsKey(fxList.Offset))
            {
                reader.AnimFXDictionary.Add(fxList.Offset, fxList);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sectionCount.Write(writer);
            s0TailTime.Write(writer);
            s1TailTime.Write(writer);
            s2TailTime.Write(writer);
            keyCount.Write(writer);

            if (sectionCount.Value == 255)
            {
                objectName.Write(writer);
            }
            else
            {
                timePerKey.Write(writer);
                pad00.Write(writer);
                pad01.Write(writer);
                pad10.Write(writer);
                pad11.Write(writer);
                fxList.Write(writer);

                if (sectionData.numSections > 0) channels[0].chanData.Write(writer);
                if (sectionData.numSections > 1) channels[1].chanData.Write(writer);
                if (sectionData.numSections > 2) channels[2].chanData.Write(writer);

                chanDataflags.Write(writer);

                if (sectionData.numSections > 0) channels[0].chanDataBuf.Write(writer);
                if (sectionData.numSections > 1) channels[1].chanDataBuf.Write(writer);
                if (sectionData.numSections > 2) channels[2].chanDataBuf.Write(writer);

                pad.Write(writer);
            }
        }
        public override string ToString()
        {
            if (sectionCount.Value == 255)
            {
                return "{ " + objectName + " }";
            }

            string description = "{ sectionCount = " + sectionData.numSections;
            if (sectionData.numSections > 0) description += ", chanData0 = " + channels[0].chanData;
            if (sectionData.numSections > 1) description += ", chanData1 = " + channels[1].chanData;
            if (sectionData.numSections > 2) description += ", chanData2 = " + channels[2].chanData;
            description += ", fxList = " + fxList + " }";
            return description;
        }

        static int FooBar(SR1_Reader reader, long segKeyList, int chanData, int keyCount, int firstSeg, int lastSeg, int totalSegments)
        {
            long oldPos = reader.BaseStream.Position;
            reader.BaseStream.Position = segKeyList;

            int oldChanData = chanData;

            int flagBitOffset = (firstSeg * 3) + 8;
            int segIndex = ((totalSegments * 3) + 7) & unchecked((int)0xfffffff8);
            G2AnimSegKeyflagInfo_Type[] kfInfos = new G2AnimSegKeyflagInfo_Type[3];

            byte wombatFlags = reader.ReadByte();
            reader.BaseStream.Position = segKeyList;

            if ((wombatFlags & 0x01) != 0)
            {
                Wombat(reader, segKeyList, flagBitOffset, ref kfInfos[0]);
                flagBitOffset += segIndex;
            }
            if ((wombatFlags & 0x02) != 0)
            {
                Wombat(reader, segKeyList, flagBitOffset, ref kfInfos[1]);
                flagBitOffset += segIndex;
            }
            if ((wombatFlags & 0x04) != 0)
            {
                Wombat(reader, segKeyList, flagBitOffset, ref kfInfos[2]);
                flagBitOffset += segIndex;
            }

            flagBitOffset = 0;
            for (int i = firstSeg; i <= lastSeg; i++)
            {
                uint kangaroo0 = Kangaroo(reader, ref kfInfos[0]);
                uint kangaroo1 = Kangaroo(reader, ref kfInfos[1]);
                uint kangaroo2 = Kangaroo(reader, ref kfInfos[2]);

                uint kangarooAll = kangaroo0 | (kangaroo1 << 4) | (kangaroo2 << 8);

                while (kangarooAll != 0)
                {
                    int currentChanData = chanData;

                    if ((kangaroo0 & 1) != 0)
                    {
                        reader.BaseStream.Position = chanData + 1;
                        byte channelType = reader.ReadByte();
                        reader.BaseStream.Position--;
                        channelType &= 0xE0;
                        if (channelType == 0xE0)
                        {
                            channelType = 0;
                        }
                        if (channelType == 0)
                        {
                            currentChanData = chanData + (keyCount * 2);
                        }
                        else
                        {
                            currentChanData = chanData + 4; // Short pointer, so add double. (Quadruple, based on no$psx???)
                            if (channelType != 0x20)
                            {
                                if (flagBitOffset == 0)
                                {
                                    flagBitOffset = 8;
                                }

                                if (channelType == 0x40)
                                {
                                    // _G2Anim_InitializeChannel_AdaptiveDelta
                                    chanData += (((keyCount + 3) >> 2) + 2) * 2; // Short pointer, so add double.
                                }
                                else if (channelType == 0x60)
                                {
                                    // _G2Anim_InitializeChannel_Linear
                                    long oldPos0 = reader.BaseStream.Position;

                                    reader.BaseStream.Position = chanData;
                                    short sVar4 = reader.ReadInt16();
                                    chanData += ((sVar4 & 0xfff) + 1) * 2; // Short pointer, so add double.

                                    reader.BaseStream.Position = oldPos0;
                                }

                                flagBitOffset--;
                                currentChanData = chanData;
                            }
                        }
                    }

                    chanData = currentChanData;
                    kangarooAll >>= 1;
                    kangaroo0 = kangarooAll;
                }
            }

            reader.BaseStream.Position = oldPos;

            return chanData - oldChanData;
        }

        static void Wombat(SR1_Reader reader, long segKeyKist, int flagBitOffset, ref G2AnimSegKeyflagInfo_Type kfInfo)
        {
            long oldPos = reader.BaseStream.Position;

            kfInfo.stream = segKeyKist + ((flagBitOffset >> 5) << 2);
            reader.BaseStream.Position = kfInfo.stream;
            uint flags = reader.ReadUInt32();
            kfInfo.bitCount = 0x20 - (flagBitOffset & 0x1f);
            kfInfo.flags = flags >> (flagBitOffset & 0x1f);

            reader.BaseStream.Position = oldPos;
        }

        static uint Kangaroo(SR1_Reader reader, ref G2AnimSegKeyflagInfo_Type kfInfo)
        {
            long oldPos = reader.BaseStream.Position;

            uint flags = 0;
            if (kfInfo.stream != 0)
            {
                flags = kfInfo.flags & 7;
                kfInfo.flags >>= 3;
                kfInfo.bitCount -= 3;
                if (kfInfo.bitCount < 1)
                {
                    kfInfo.stream += 4;
                    reader.BaseStream.Position = kfInfo.stream;
                    kfInfo.flags = reader.ReadUInt32();
                    if (kfInfo.bitCount < 0)
                    {
                        flags |= kfInfo.flags << (kfInfo.bitCount + 3 & 0x1f) & 7;
                        kfInfo.flags >>= (-kfInfo.bitCount & 0x1f);
                    }
                    kfInfo.bitCount += 0x20;
                }
            }

            reader.BaseStream.Position = oldPos;

            return flags;
        }

        void ReadSectionData(SR1_Reader reader)
        {
            // Generic and Physical get it the numSections from the Model.
            // Monster gets the numSections from the MonsterSubAttributes if present, otherwise it gets it from the Model.
            // Raziel has the numSections hardcoded, but they're the same as the highest of sectionA, sectionB, and sectionC + 1.
            // For kain and vlgra, try getting it from the MonsterSubAttributes.

            string scriptName = reader.ObjectName.ToString();

            // Kain has one weird animation that doesn't match the rest. Checking for the three seems hacky though.
            if (scriptName == "paths___")
            {
                sectionData.sectionA = 4;// reader.Object.sectionA.Value;
                sectionData.sectionB = reader.Object.sectionB.Value;
                sectionData.sectionC = reader.Object.sectionC.Value;
                sectionData.numSegments = sectionData.sectionA + 1;
                sectionData.numSections = 1;
            }
            else if (//reader.Model == null || 
                (sectionCount.Value == 3 && scriptName == "kain____") ||
                (sectionCount.Value == 3 && scriptName == "moebius_") ||
                (sectionCount.Value == 3 && scriptName == "bellchn_") ||
                (sectionCount.Value == 3 && scriptName == "vlgr____") ||
                (sectionCount.Value == 3 && scriptName == "human___"))
            {
                sectionData.sectionA = reader.Object.sectionA.Value;
                sectionData.sectionB = reader.Object.sectionB.Value;
                sectionData.sectionC = reader.Object.sectionC.Value;
                sectionData.numSegments = sectionData.sectionC + 1;
                sectionData.numSections = 3;
            }
            else if (scriptName == "raziel__" || scriptName == "firhost_" ||
                scriptName == "hostfor_" || scriptName == "glfhost_" ||
                scriptName == "undhost_" || scriptName == "tranhst_" ||
                scriptName == "pilxhst_" || scriptName == "pilhost_" ||
                scriptName == "cathost_" || scriptName == "tmbhost_" ||
                scriptName == "boshost_" || scriptName == "ronhost_" ||
                scriptName == "cronhstr")
            {
                sectionData.numSections = sectionCount.Value;
                sectionData.sectionA = reader.Object.sectionA.Value;
                sectionData.sectionB = reader.Object.sectionB.Value;
                sectionData.sectionC = reader.Object.sectionC.Value;

                if (sectionData.numSections > 0) sectionData.numSegments = sectionData.sectionA + 1;
                if (sectionData.numSections > 1) sectionData.numSegments = sectionData.sectionB + 1;
                if (sectionData.numSections > 2) sectionData.numSegments = sectionData.sectionC + 1;
            }
            else if (scriptName == "sknhost_" || scriptName == "pilhostk" || scriptName == "cronhstk")
            {
                sectionData.numSections = sectionCount.Value;
                sectionData.sectionA = 49;
                sectionData.sectionB = -1;
                sectionData.sectionC = -1;
                sectionData.numSegments = sectionData.sectionA + 1;
            }
            else if (scriptName == "walhostb")
            {
                sectionData.numSections = 1;
                sectionData.sectionA = 40; // or 41
                sectionData.sectionB = -1;
                sectionData.sectionC = -1;
                sectionData.numSegments = sectionData.sectionA + 1;
            }
            else if (scriptName == "eaggot__" || scriptName == "eaggots_" ||
                scriptName == "saggot__" || scriptName == "saggots_")
            {
                sectionData.numSections = 1;
                sectionData.sectionA = 4;
                sectionData.sectionB = -1;
                sectionData.sectionC = -1;
                sectionData.numSegments = sectionData.sectionA + 1;
            }
            else if (scriptName == "pshblkz_")
            {
                sectionData.numSections = 1;
                sectionData.sectionA = 1;
                sectionData.sectionB = -1;
                sectionData.sectionC = -1;
                sectionData.numSegments = sectionData.sectionA + 1;
            }
            else if ((reader.Object.oflags2.Value & 0x00040000) != 0)
            {
                // InitPhysicalObject
                //if ((reader.PhysObProperties.Type.Value & 0x00008000) != 0)
                if (reader.PhysObProperties != null)
                {
                    sectionData.numSections = sectionCount.Value;

                    //if (numSections == 1)
                    //{
                    sectionData.sectionA = reader.Model.numSegments.Value - 1;
                    sectionData.sectionB = 0;
                    sectionData.sectionC = 0;
                    sectionData.numSegments = sectionData.sectionA + 1;
                    //}

                    // Better to check for Raziel anims in PhysobAttributes/InteractibleAttributes than looking for three.
                    if (sectionData.numSections == 3)
                    {
                        sectionData.sectionA = reader.Object.sectionA.Value;
                        sectionData.sectionB = reader.Object.sectionB.Value;
                        sectionData.sectionC = reader.Object.sectionC.Value;
                        sectionData.numSegments = sectionData.sectionC + 1;
                    }
                }
            }
            else if ((reader.Object.oflags2.Value & 0x00080000) != 0)
            {
                // MON_AnimInit
                // instance->extraData is a MonsterVars.
                // MonsterVars->subAttr (MonsterVars + 0x168) is a MonsterSubAttributes.
                // MonsterSubAttributes->numSections is the number of sections.
                // MonsterSubAttributes->sectionEnd are sectionA, sectionB, and sectionC.
                sectionData.numSections = reader.MonsterSubAttributes.numSections.Value;

                if (sectionData.numSections > 0)
                {
                    if (reader.MonsterSubAttributes.sectionEnd[0] != 0)
                    {
                        sectionData.sectionA = reader.MonsterSubAttributes.sectionEnd[0];
                    }
                    else
                    {
                        sectionData.sectionA = reader.Model.numSegments.Value - 1;
                    }

                    sectionData.numSegments = sectionData.sectionA + 1;
                }

                if (sectionData.numSections > 1)
                {
                    if (reader.MonsterSubAttributes.sectionEnd[1] != 0)
                    {
                        sectionData.sectionB = reader.MonsterSubAttributes.sectionEnd[1];
                    }
                    else
                    {
                        sectionData.sectionB = reader.Model.numSegments.Value - 1;
                    }

                    sectionData.numSegments = sectionData.sectionB + 1;
                }

                if (sectionData.numSections > 2)
                {
                    if (reader.MonsterSubAttributes.sectionEnd[2] != 0)
                    {
                        sectionData.sectionC = reader.MonsterSubAttributes.sectionEnd[2];
                    }
                    else
                    {
                        sectionData.sectionC = reader.Model.numSegments.Value - 1;
                    }

                    sectionData.numSegments = sectionData.sectionC + 1;
                }
            }
            else
            {
                // GenericInit
                if ((reader.Object.oflags2.Value & 0x40000000) == 0)
                {
                    sectionData.numSections = 1;
                    sectionData.sectionA = reader.Model.numSegments.Value - 1;
                    sectionData.sectionB = 0;
                    sectionData.sectionC = 0;

                    sectionData.numSegments = sectionData.sectionA + 1;
                }
            }
        }
    }
}
