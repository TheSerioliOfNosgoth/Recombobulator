using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterAnim : SR1_Structure
    {
        SR1_Primative<ushort> velocity = new SR1_Primative<ushort>();
        SR1_Primative<ushort> distance = new SR1_Primative<ushort>();
        SR1_Primative<ushort> playSpeed = new SR1_Primative<ushort>();
        SR1_Primative<byte> interpFrames = new SR1_Primative<byte>();
        SR1_Primative<byte> startFrame = new SR1_Primative<byte>();
        SR1_Primative<byte> interpOut = new SR1_Primative<byte>();
        SR1_Primative<byte> alphaTable = new SR1_Primative<byte>();
        SR1_Primative<byte> alphaTableOut = new SR1_Primative<byte>();
        SR1_Primative<byte> controllingSection = new SR1_Primative<byte>();
        SR1_PrimativeArray<sbyte> index = new SR1_PrimativeArray<sbyte>(3);
        SR1_Primative<sbyte> pad = new SR1_Primative<sbyte>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            velocity.Read(reader, this, "velocity");
            distance.Read(reader, this, "distance");
            playSpeed.Read(reader, this, "playSpeed");
            interpFrames.Read(reader, this, "interpFrames");
            startFrame.Read(reader, this, "startFrame");
            interpOut.Read(reader, this, "interpOut");
            alphaTable.Read(reader, this, "alphaTable");
            alphaTableOut.Read(reader, this, "alphaTableOut");
            controllingSection.Read(reader, this, "controllingSection");
            index.Read(reader, this, "index");

            if (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                pad.Read(reader, this, "pad");
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            velocity.Write(writer);
            distance.Write(writer);
            playSpeed.Write(writer);
            interpFrames.Write(writer);
            startFrame.Write(writer);
            interpOut.Write(writer);
            alphaTable.Write(writer);
            alphaTableOut.Write(writer);
            controllingSection.Write(writer);
            index.Write(writer);

            if (MembersRead.Contains(pad))
            {
                pad.Write(writer);
            }
        }
    }
}
