using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Intro : SR1_Structure
    {
        SR1_PrimativeArray<char> name = new SR1_PrimativeArray<char>(16);
        SR1_Primative<int> intronum = new SR1_Primative<int>();
        SR1_Primative<int> UniqueID = new SR1_Primative<int>();
        Rotation rotation = new Rotation();
        Position position = new Position();
        SR1_Primative<short> maxRad = new SR1_Primative<short>();
        SR1_Primative<int> maxRadSq = new SR1_Primative<int>();
        SR1_Primative<int> flags = new SR1_Primative<int>();
        SR1_Pointer<INICommand> data = new SR1_Pointer<INICommand>();
        SR1_Pointer<Instance> instance = new SR1_Pointer<Instance>();
        SR1_Pointer<MultiSpline> multiSpline = new SR1_Pointer<MultiSpline>();
        // Sometimes an SFXMarker, but always null in the area files, so probaby doesn't matter.
        SR1_PrimativePointer<object> dsignal = new SR1_PrimativePointer<object>();
        SR1_Primative<short> specturalLightGroup = new SR1_Primative<short>();
        SR1_Primative<short> meshColor = new SR1_Primative<short>();
        Position spectralPosition = new Position();
        SR1_Primative<short> spad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            name.Read(reader, this, "name");
            intronum.Read(reader, this, "intronum");
            UniqueID.Read(reader, this, "UniqueID");
            rotation.Read(reader, this, "rotation");
            position.Read(reader, this, "position");
            maxRad.Read(reader, this, "maxRad");
            maxRadSq.Read(reader, this, "maxRadSq");
            flags.Read(reader, this, "flags");
            data.Read(reader, this, "data");
            instance.Read(reader, this, "instance");
            multiSpline.Read(reader, this, "multiSpline");
            dsignal.Read(reader, this, "dsignal");
            specturalLightGroup.Read(reader, this, "specturalLightGroup");
            meshColor.Read(reader, this, "meshColor");
            spectralPosition.Read(reader, this, "spectralPosition");
            spad.Read(reader, this, "spad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            if (data.Offset != 0x00000000 && !reader.File._Structures.ContainsKey(data.Offset))
            {
                reader.BaseStream.Position = (long)data.Offset;
                INICommand tempCMD = new INICommand();
                do
                {
                    tempCMD.ReadTemp(reader);
                }
                while (tempCMD.command.Value != 0);

                int bufferLength = (int)reader.BaseStream.Position - (int)data.Offset;
                SR1_StructureSeries<INICommand> commands = new SR1_StructureSeries<INICommand>(bufferLength);

                reader.BaseStream.Position = (long)data.Offset;
                commands.Read(reader, null, "");
            }

            new MultiSpline().ReadFromPointer(reader, multiSpline);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            name.Write(writer);
            intronum.Write(writer);
            UniqueID.Write(writer);
            rotation.Write(writer);
            position.Write(writer);
            maxRad.Write(writer);
            maxRadSq.Write(writer);
            flags.Write(writer);
            data.Write(writer);
            instance.Write(writer);
            multiSpline.Write(writer);
            dsignal.Write(writer);
            specturalLightGroup.Write(writer);
            meshColor.Write(writer);
            spectralPosition.Write(writer);
            spad.Write(writer);
        }

        public override string ToString()
        {
            return "{ " + name.ToString().Trim('\0') + " }";
        }
    }
}