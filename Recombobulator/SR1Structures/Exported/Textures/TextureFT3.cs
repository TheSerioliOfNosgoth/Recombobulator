﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class TextureFT3 : SR1_Structure
	{
		public readonly SR1_Primative<byte> u0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> v0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<ushort> clut = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<byte> u1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> v1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<ushort> tpage = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<ushort> attr2 = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<byte> u2 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> v2 = new SR1_Primative<byte>();
		public readonly SR1_Primative<ushort> attr = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<int> color = new SR1_Primative<int>().ShowAsHex(true);

		public int NumReferences = 0;
		public int AniTexIndex = -1;
		public bool HasTranslucentPolygon = false;

		bool IsPSX = false;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version < SR1_File.Version.Retail_PC)
			{
				IsPSX = true;
			}

			u0.Read(reader, this, "u0");
			v0.Read(reader, this, "v0");

			clut.Read(reader, this, "clut", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			tpage.Read(reader, this, "tpage", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u1.Read(reader, this, "u1");
			v1.Read(reader, this, "v1");

			tpage.Read(reader, this, "tpage", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			attr2.Read(reader, this, "attr2", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u2.Read(reader, this, "u2");
			v2.Read(reader, this, "v2");
			attr.Read(reader, this, "attr");

			color.Read(reader, this, "color", SR1_File.Version.Feb04, SR1_File.Version.Apr14);

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
			attr2.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			u2.Write(writer);
			v2.Write(writer);
			attr.Write(writer);

			color.Write(writer, SR1_File.Version.Feb04, SR1_File.Version.Apr14);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				ushort tPage = (ushort)(tpage.Value & (0x001F | 0x0010 | 0x0800));
				if (file._Overrides.NewTextureIDs.ContainsKey(tPage))
				{
					tpage.Value = file._Overrides.NewTextureIDs[tPage];
				}
				else
				{
					tpage.Value = 0;
				}
				attr2.Value = 0x0108;

				if (file._Version < SR1_File.Version.Apr14)
				{
					if ((migrateFlags & SR1_File.MigrateFlags.ForceWaterTranslucent) != 0 && HasTranslucentPolygon)
					{
						tpage.Value |= 0x4000;
						attr2.Value |= 0x0060;
					}

					if ((attr.Value & 0x0010) != 0)
					{
						tpage.Value |= 0x2000; // UseAlphaMask
					}

					// Alpha builds:
					// - UseAlphaMask is in TextureFT3.attr // See bridges and chains in undrct 1.
					// - Water is in TFace.attr. Looks like 0x08. // See undrct 1, 20, 21.
					// - Doublesided is in TFace.attr. Looks like 0x10. // See chains in undrct 1, 20, 21.
					// - Phasethrough is in TextureFT3.attr. Looks like 0x1000. // pillars 8 and city 9.
					// - Climbable walls. Looks like 0x0200. // See city 2.
					// - Burn in sunlight and set on fire are in BSPTree.flags. // See train 9.

					attr.Value &= unchecked((ushort)0x1000);
					//attr.Value |= 0x0001; // DoubleSided?
				}
				else
				{
					if ((migrateFlags & SR1_File.MigrateFlags.ForceWaterTranslucent) != 0 && HasTranslucentPolygon)
					{
						tpage.Value |= 0x4000;
						attr2.Value |= 0x0060;
					}

					if ((attr.Value & 0x0040) != 0)
					{
						if (file._Structures[0].Name == "movie1")
						{
							tpage.Value |= 0x4000;
							attr2.Value |= 0x0060;
						}
						else
						{
							tpage.Value |= 0x2000; // UseAlphaMask
						}
					}

					attr.Value &= unchecked((ushort)0x1000);
					//attr.Value |= 0x0001; // DoubleSided?
				}
			}
		}

		public override string ToString()
		{
			string result = base.ToString();
			ushort maskedTPage;
			if (IsPSX)
			{
				maskedTPage = (ushort)(tpage.Value & (0x001F | 0x0010 | 0x0800));
			}
			else
			{
				maskedTPage = (ushort)(tpage.Value & 0x7FF);
			}
			result += "{ tpage = " + tpage + ", maskedTPage = 0x" + maskedTPage.ToString("X4") + ", clut = " + clut;
			result += ", attr = " + attr.ToString() + ", attr2 = " + attr2.ToString();
			result += ", aniTexIndex = " + AniTexIndex.ToString() + ", NumReferences = " + NumReferences.ToString() + " }";
			return result;
		}
	}
}
