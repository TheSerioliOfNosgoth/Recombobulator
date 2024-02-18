using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class DrMoveAniTexSrcInfo : SR1_Structure
	{
		SR1_Primative<short> pixSrcX = new SR1_Primative<short>();
		SR1_Primative<short> pixSrcY = new SR1_Primative<short>();
		SR1_Primative<short> clutSrcX = new SR1_Primative<short>();
		SR1_Primative<short> clutSrcY = new SR1_Primative<short>();
		SR1_Primative<short> tPageSrc = new SR1_Primative<short>();
		SR1_Primative<short> pad = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			pixSrcX.Read(reader, this, "pixSrcX");
			pixSrcY.Read(reader, this, "pixSrcY");
			clutSrcX.Read(reader, this, "clutSrcX", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutSrcY.Read(reader, this, "clutSrcY", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			tPageSrc.Read(reader, this, "tPageSrc", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad.Read(reader, this, "pad", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			pixSrcX.Write(writer);
			pixSrcY.Write(writer);
			clutSrcX.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutSrcY.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			tPageSrc.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			/*if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				int tPageX = (pixSrcX.Value & 0x07c0) >> 6;
				int tPageY = (pixSrcY.Value & 0x0100) >> 4 | (pixSrcY.Value & 0x0200) << 2;
				ushort newTPageDst = (ushort)(tPageX | tPageY);

				short newPixDstX = (short)((pixSrcX.Value & 0x7F) << 2);
				short newPixDstY = (short)((pixSrcY.Value & 0xFF));

				pixSrcX.Value = newPixDstX;
				pixSrcY.Value = newPixDstY;

				if (file._Overrides.NewTextureIDs.ContainsKey(newTPageDst))
				{
					tPageSrc.Value = unchecked((short)file._Overrides.NewTextureIDs[newTPageDst]);
				}
				else
				{
					tPageSrc.Value = -1;
				}
			}*/
		}
	}
}
