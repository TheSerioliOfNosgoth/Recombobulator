﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class TextureMT3 : SR1_Structure
	{
		SR1_Primative<byte> u0 = new SR1_Primative<byte>();
		SR1_Primative<byte> v0 = new SR1_Primative<byte>();
		SR1_Primative<ushort> clut = new SR1_Primative<ushort>().ShowAsHex(true);
		SR1_Primative<byte> u1 = new SR1_Primative<byte>();
		SR1_Primative<byte> v1 = new SR1_Primative<byte>();
		SR1_Primative<ushort> tpage = new SR1_Primative<ushort>().ShowAsHex(true);
		SR1_Primative<ushort> pad = new SR1_Primative<ushort>();
		SR1_Primative<byte> u2 = new SR1_Primative<byte>();
		SR1_Primative<byte> v2 = new SR1_Primative<byte>();
		SR1_Primative<sbyte> pad1 = new SR1_Primative<sbyte>();
		SR1_Primative<sbyte> sortPush = new SR1_Primative<sbyte>();
		SR1_Primative<int> color = new SR1_Primative<int>();

		public int NumReferences = 0;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			u0.Read(reader, this, "u0");
			v0.Read(reader, this, "v0");

			clut.Read(reader, this, "clut", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			tpage.Read(reader, this, "tpage", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u1.Read(reader, this, "u1");
			v1.Read(reader, this, "v1");

			tpage.Read(reader, this, "tpage", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			pad.Read(reader, this, "pad", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u2.Read(reader, this, "u2");
			v2.Read(reader, this, "v2");

			pad1.Read(reader, this, "pad1");
			sortPush.Read(reader, this, "sortPush");

			color.Read(reader, this, "color");

			ushort textureID;
			if (reader.File._Version == SR1_File.Version.Retail_PC)
			{
				textureID = (ushort)((uint)tpage.Value & 0x000007FF);
			}
			else
			{
				textureID = (ushort)((uint)tpage.Value & 0x00000007);
			}

			if (!reader.File._TextureIDs.Contains(textureID))
			{
				reader.File._TextureIDs.Add(textureID);
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			u0.Write(writer);
			v0.Write(writer);

			clut.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			tpage.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u1.Write(writer);
			v1.Write(writer);

			tpage.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			pad.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u2.Write(writer);
			v2.Write(writer);

			pad1.Write(writer);
			sortPush.Write(writer);

			color.Write(writer);
		}

		public static void Copy(TextureMT3 to, TextureMT3 from)
		{
			to.u0.Value = from.u0.Value;
			to.v0.Value = from.v0.Value;
			to.clut.Value = from.clut.Value;
			to.u1.Value = from.u1.Value;
			to.v1.Value = from.v1.Value;
			to.tpage.Value = from.tpage.Value;
			to.pad.Value = from.pad.Value;
			to.u2.Value = from.u2.Value;
			to.v2.Value = from.v2.Value;
			to.pad1.Value = from.pad1.Value;
			to.sortPush.Value = from.sortPush.Value;
			to.color.Value = from.color.Value;
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				bool isTranslucent = false;

				if ((migrateFlags & SR1_File.MigrateFlags.ApplyTranslucency) != 0 &&
					(tpage.Value & 0x0020) != 0)
				{
					isTranslucent = true;
				}

				ushort tPage = (ushort)(tpage.Value & (0x001F | 0x0010 | 0x0800));
				if (file._Overrides.NewTextureIDs.ContainsKey(tPage))
				{
					tpage.Value = file._Overrides.NewTextureIDs[tPage];
				}
				else
				{
					tpage.Value = 0;
				}

				if (isTranslucent)
				{
					tpage.Value |= 0x4000;
				}
				else
				{
					tpage.Value |= 0x2000; // UseAlphaMask
				}

				pad.Value = 264;
			}
		}

		public override string ToString()
		{
			string result = base.ToString();
			result += "{ tpage = " + tpage + ", clut = " + clut + ", NumReferences = " + NumReferences.ToString() + " }";
			return result;
		}
	}
}
