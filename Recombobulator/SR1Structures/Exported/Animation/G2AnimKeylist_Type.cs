﻿using System.Collections.Generic;
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
        public readonly SR1_PrimativePointer<byte> chanData0 = new SR1_PrimativePointer<byte>();
        public readonly SR1_PrimativePointer<byte> chanData1 = new SR1_PrimativePointer<byte>();
        public readonly SR1_PrimativePointer<byte> chanData2 = new SR1_PrimativePointer<byte>();
        SR1_PrimativeArray<byte> chanDataflags = new SR1_PrimativeArray<byte>();
        SR1_PrimativeArray<byte> chanData0Buf = new SR1_PrimativeArray<byte>();
        SR1_PrimativeArray<byte> chanData1Buf = new SR1_PrimativeArray<byte>();
        SR1_PrimativeArray<byte> chanData2Buf = new SR1_PrimativeArray<byte>();
        SR1_PrimativeArray<byte> pad = new SR1_PrimativeArray<byte>(0);

        SR1_String objectName = new SR1_String(12);

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
                timePerKey.Read(reader, this, "timePerKey");
                pad00.Read(reader, this, "pad00");
                pad01.Read(reader, this, "pad01");
                pad10.Read(reader, this, "pad10");
                pad11.Read(reader, this, "pad11");
                fxList.Read(reader, this, "fxList");

                uint mod;

                // For kain and vlgra, try getting it from the MonsterSubAttributes.
                int sectionA = reader.Object.sectionA.Value;
                int sectionB = reader.Object.sectionB.Value;
                int sectionC = reader.Object.sectionC.Value;
                int numSegments = 0;
                long segKeyList = 0;

                if (sectionA >= 0)
                {
                    numSegments = sectionA + 1;
                }
                if (sectionB >= 0)
                {
                    numSegments = sectionB + 1;
                }
                if (sectionC >= 0)
                {
                    numSegments = sectionC + 1;
                }

                if (sectionCount.Value >= 1)
                {
                    chanData0.Read(reader, this, "chanData0");
                    segKeyList = chanData0.End;
                }

                if (sectionCount.Value >= 2)
                {
                    chanData1.Read(reader, this, "chanData1");
                    segKeyList = chanData1.End;
                }

                if (sectionCount.Value >= 3)
                {
                    chanData2.Read(reader, this, "chanData2");
                    segKeyList = chanData2.End;
                }

                int flagBitOffset = 8;
                int segIndex = ((numSegments * 3) + 7) & unchecked((int)0xfffffff8);
                byte wombatFlags = reader.ReadByte();
                reader.BaseStream.Position--;

                if ((wombatFlags & 0x01) != 0)
                {
                    flagBitOffset += segIndex;
                }
                if ((wombatFlags & 0x02) != 0)
                {
                    flagBitOffset += segIndex;
                }
                if ((wombatFlags & 0x04) != 0)
                {
                    flagBitOffset += segIndex;
                }

                int nextOffset = 0;
                mod = (uint)flagBitOffset % 16; // Need 16 bits to pad the buffer to 2 bytes.
                if (mod > 0)
                {
                    flagBitOffset += (int)(16 - mod); // Need 16 bits to pad the buffer to 2 bytes.
                }
                nextOffset += flagBitOffset / 8;

                chanDataflags = new SR1_PrimativeArray<byte>(nextOffset);
                chanDataflags.Read(reader, this, "chanDataFlags");

                if (sectionCount.Value >= 1 && chanData0.Offset != 0 && sectionA >= 0)
                {
                    int chanLength = FooBar(reader, segKeyList, (int)chanData0.Offset, keyCount.Value, 0, sectionA, numSegments);
                    chanData0Buf = new SR1_PrimativeArray<byte>(chanLength);
                    chanData0Buf.Read(reader, this, "chanData0Buf");
                }

                if (sectionCount.Value >= 2 && chanData1.Offset != 0 && sectionB >= 0)
                {
                    int chanLength = FooBar(reader, segKeyList, (int)chanData1.Offset, keyCount.Value, sectionA + 1, sectionB, numSegments);
                    chanData1Buf = new SR1_PrimativeArray<byte>(chanLength);
                    chanData1Buf.Read(reader, this, "chanData1Buf");
                }

                if (sectionCount.Value >= 3 && chanData2.Offset != 0 & sectionC >= 0)
                {
                    int chanLength = FooBar(reader, segKeyList, (int)chanData2.Offset, keyCount.Value, sectionB + 1, sectionC, numSegments);
                    chanData2Buf = new SR1_PrimativeArray<byte>(chanLength);
                    chanData2Buf.Read(reader, this, "chanData2Buf");
                }

                mod = (uint)(reader.BaseStream.Position) % 4;
                if (mod > 0)
                {
                    uint padding = 4 - mod;
                    pad = new SR1_PrimativeArray<byte>((int)padding);
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

                if (sectionCount.Value >= 1)
                {
                    chanData0.Write(writer);
                }

                if (sectionCount.Value >= 2)
                {
                    chanData1.Write(writer);
                }

                if (sectionCount.Value >= 3)
                {
                    chanData2.Write(writer);
                }

                chanDataflags.Write(writer);

                if (sectionCount.Value >= 1 && chanData0.Offset != 0)
                {
                    chanData0Buf.Write(writer);
                }

                if (sectionCount.Value >= 2 && chanData1.Offset != 0)
                {
                    chanData1Buf.Write(writer);
                }

                if (sectionCount.Value >= 3 && chanData2.Offset != 0)
                {
                    chanData2Buf.Write(writer);
                }

                pad.Write(writer);
            }
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
    }
}
