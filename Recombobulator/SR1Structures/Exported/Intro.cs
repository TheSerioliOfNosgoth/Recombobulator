using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Intro : SR1_Structure
	{
		public readonly SR1_String name = new SR1_String(16);
		public readonly SR1_Primative<int> intronum = new SR1_Primative<int>();
		public readonly SR1_Primative<int> UniqueID = new SR1_Primative<int>();
		public readonly SR1_Pointer<Intro> link = new SR1_Pointer<Intro>();
		public readonly Rotation rotation = new Rotation();
		public readonly Position position = new Position();
		public readonly SR1_Primative<short> maxRad = new SR1_Primative<short>();
		public readonly SR1_Primative<int> maxRadSq = new SR1_Primative<int>();
		public readonly SR1_Primative<int> flags = new SR1_Primative<int>();
		public readonly SR1_Pointer<INICommand> data = new SR1_Pointer<INICommand>();
		public readonly SR1_Pointer<Instance> instance = new SR1_Pointer<Instance>();
		public readonly SR1_Pointer<MultiSpline> multiSpline = new SR1_Pointer<MultiSpline>();
		// Sometimes an SFXMarker, but always null in the area files, so probaby doesn't matter.
		public readonly SR1_PrimativePointer<object> dsignal = new SR1_PrimativePointer<object>();
		public readonly SR1_Primative<short> specturalLightGroup = new SR1_Primative<short>();
		public readonly SR1_Primative<short> meshColor = new SR1_Primative<short>();
		public readonly Position spectralPosition = new Position();
		public readonly SR1_Primative<short> spad = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			name.SetReadMax(true).Read(reader, this, "name");
			intronum.Read(reader, this, "intronum");
			UniqueID.Read(reader, this, "UniqueID");
			link.Read(reader, this, "link", SR1_File.Version.Feb04, SR1_File.Version.Feb16);
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

			if (!reader.File._IntroNames.Contains(name.ToString()))
			{
				reader.File._IntroNames.Add(name.ToString());
			}

			int uniqueID = UniqueID.Value;
			if (!reader.File._IntroIDs.Contains(uniqueID))
			{
				reader.File._IntroIDs.Add(uniqueID);
			}
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

				if (reader.Level.Name == "push10")
				{
					new SR1_Primative<int>().Read(reader, null, "");
				}
			}

            if (multiSpline.Offset != 0 && !reader.MultiSplineDictionary.ContainsKey(multiSpline.Offset))
            {
                reader.MultiSplineDictionary.Add(multiSpline.Offset, multiSpline);
            }
        }

		public override void WriteMembers(SR1_Writer writer)
		{
			name.Write(writer);
			intronum.Write(writer);
			UniqueID.Write(writer);
			link.Write(writer, SR1_File.Version.Feb04, SR1_File.Version.Feb16);
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

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version != targetVersion)
			{
				if (file._Overrides.NewIntroIDs.TryGetValue(UniqueID.Value, out int newIntroID))
				{
					UniqueID.Value = newIntroID;
				}

				if (file._Overrides.NewObjectNames.ContainsKey(name.ToString()))
				{
					name.SetText(file._Overrides.NewObjectNames[name.ToString()], 16);
				}
			}
		}

		public override string ToString()
		{
			return "{ name = \"" + name.ToString().Trim('\0') + "-" + UniqueID.ToString() + "\", data = " + data.ToString() + " }";
		}
	}
}