﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class SBSPLeaf : SR1_Structure
	{
		Sphere sphere = new Sphere();
		Sphere_noSq sphereNoSq = new Sphere_noSq();
		SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>();
		SR1_Primative<short> numIntros = new SR1_Primative<short>();
		SR1_Primative<short> flags = new SR1_Primative<short>();
		SR1_Pointer<CDLight> lightList = new SR1_Pointer<CDLight>();
		SR1_Primative<short> numLights = new SR1_Primative<short>();
		SR1_Primative<short> pad = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			sphere.Read(reader, this, "sphere", SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Read(reader, this, "sphere", SR1_File.Version.Jan23, SR1_File.Version.Next);
			introList.Read(reader, this, "introList");
			numIntros.Read(reader, this, "numIntros");
			flags.Read(reader, this, "flags");
			lightList.Read(reader, this, "lightList");
			numLights.Read(reader, this, "numLights");
			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			//if (numIntros.Value > 0 && introList.Offset != 0 && !reader.IntroListDictionary.ContainsKey(introList.Offset))
			//{
			//    reader.IntroListDictionary.Add(introList.Offset, new SR1_PointerArray<Intro>(numIntros.Value, false));
			//}
			new SR1_PointerArray<Intro>(numIntros.Value, false).ReadFromPointer(reader, introList);
			new SR1_PointerArray<CDLight>(numLights.Value, false).ReadFromPointer(reader, lightList);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			sphere.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			sphereNoSq.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			introList.Write(writer);
			numIntros.Write(writer);
			flags.Write(writer);
			lightList.Write(writer);
			numLights.Write(writer);
			pad.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version <= SR1_File.Version.May12 && targetVersion >= SR1_File.Version.Jun01)
			{
				if (introList.Offset != 0)
				{
					file._Structures.Remove(introList.Offset);
				}

				if (lightList.Offset != 0)
				{
					file._Structures.Remove(lightList.Offset);
				}
			}
		}
	}
}
