﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Intro : SR1_Structure
	{
		public readonly SR1_String name = new SR1_String(16);
		public readonly SR1_Primative<int> intronum = new SR1_Primative<int>();
		public readonly SR1_Pointer<Object> sr1object = new SR1_Pointer<Object>();
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
			sr1object.Read(reader, this, "object", SR1_File.Version.First, SR1_File.Version.Jan23);
			UniqueID.Read(reader, this, "UniqueID", SR1_File.Version.Jan23, SR1_File.Version.Next);
			link.Read(reader, this, "link", SR1_File.Version.First, SR1_File.Version.Feb16);
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
			spectralPosition.Read(reader, this, "spectralPosition", SR1_File.Version.Jan23, SR1_File.Version.Next);
			spad.Read(reader, this, "spad", SR1_File.Version.Jan23, SR1_File.Version.Next);

			if (!reader.File._IntroNames.Contains(name.ToString()))
			{
				reader.File._IntroNames.Add(name.ToString());
			}

			if (reader.File._Version < SR1_File.Version.Jan23)
			{
				// Just use the count as the ID so there's something to remap.
				int uniqueID = reader.File._IntroIDs.Count;
				reader.File._IntroIDs.Add(uniqueID);
			}
			else
			{
				int uniqueID = UniqueID.Value;
				if (!reader.File._IntroIDs.Contains(uniqueID))
				{
					reader.File._IntroIDs.Add(uniqueID);
				}
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if (data.Offset != 0 && !reader.File._Structures.ContainsKey(data.Offset))
			{
				reader.BaseStream.Position = (long)data.Offset;
				INICommand tempCMD = new INICommand();
				do
				{
					tempCMD.ReadTemp(reader);
				}
				while (tempCMD.command.Value != 0);

				int bufferLength = (int)reader.BaseStream.Position - (int)data.Offset;
				SR1_StructureSeries<INICommand> commands = new SR1_StructureSeries<INICommand>();
				commands.SetReadLength(bufferLength);

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
			sr1object.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			UniqueID.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			link.Write(writer, SR1_File.Version.First, SR1_File.Version.Feb16);
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
			spectralPosition.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			spad.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version != targetVersion)
			{
				//if (name.ToString() == "raziel")
				//{
					// Raziel always has this ID.
					//UniqueID.Value = 2560;
				//}
				if (file._Overrides.NewIntroIDs.TryGetValue(UniqueID.Value, out int newIntroID))
				{
					UniqueID.Value = newIntroID;
				}

				if (file._Overrides.NewObjectNames.ContainsKey(name.ToString()))
				{
					name.SetText(file._Overrides.NewObjectNames[name.ToString()], 16);
				}
			}

			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Jan23)
			{
				spectralPosition.x.Value = position.x.Value;
				spectralPosition.y.Value = position.y.Value;
				spectralPosition.z.Value = position.z.Value;
				
				// Remove the commands until I know they are the same as retail.
				if (data.Offset != 0)
				{
					file._Structures.Remove(data.Offset);
					data.Offset = 0;
				}
			}
		}

		public override string ToString()
		{
			return "{ name = \"" + name.ToString().Trim('\0') + "-" + UniqueID.ToString() + "\", data = " + data.ToString() + " }";
		}
	}
}