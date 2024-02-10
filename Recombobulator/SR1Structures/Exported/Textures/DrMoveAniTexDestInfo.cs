using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class DrMoveAniTexDestInfo : SR1_Structure
	{
		SR1_Primative<short> pixDstX = new SR1_Primative<short>();
		SR1_Primative<short> pixDstY = new SR1_Primative<short>();
		SR1_Primative<short> pixW = new SR1_Primative<short>();
		SR1_Primative<short> pixH = new SR1_Primative<short>();
		SR1_Primative<short> clutDstX = new SR1_Primative<short>();
		SR1_Primative<short> clutDstY = new SR1_Primative<short>();
		SR1_Primative<short> clutW = new SR1_Primative<short>();
		SR1_Primative<short> clutH = new SR1_Primative<short>();
		SR1_Primative<short> tPageDst = new SR1_Primative<short>();
		SR1_Primative<short> pad0 = new SR1_Primative<short>();
		SR1_Primative<short> pad1 = new SR1_Primative<short>();
		SR1_Primative<short> pad2 = new SR1_Primative<short>();
		SR1_Primative<short> pixCurrentX = new SR1_Primative<short>();
		SR1_Primative<short> pixCurrentY = new SR1_Primative<short>();
		SR1_Primative<short> clutCurrentX = new SR1_Primative<short>();
		SR1_Primative<short> clutCurrentY = new SR1_Primative<short>();
		SR1_Primative<short> tPageCurrent = new SR1_Primative<short>();
		SR1_Primative<short> pad3 = new SR1_Primative<short>();
		SR1_Primative<int> numFrames = new SR1_Primative<int>();
		SR1_Primative<int> speed = new SR1_Primative<int>();
		SR1_StructureArray<DrMoveAniTexSrcInfo> frame = new SR1_StructureArray<DrMoveAniTexSrcInfo>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			pixDstX.Read(reader, this, "pixDstX");
			pixDstY.Read(reader, this, "pixDstY");
			pixW.Read(reader, this, "pixW");
			pixH.Read(reader, this, "pixH");

			clutDstX.Read(reader, this, "clutDstX", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutDstY.Read(reader, this, "clutDstY", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutW.Read(reader, this, "clutW", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutH.Read(reader, this, "clutH", SR1_File.Version.First, SR1_File.Version.Retail_PC);

			tPageDst.Read(reader, this, "tPageDst", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad0.Read(reader, this, "pad0", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad1.Read(reader, this, "pad1", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad2.Read(reader, this, "pad2", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			pixCurrentX.Read(reader, this, "pixCurrentX");
			pixCurrentY.Read(reader, this, "pixCurrentY");

			clutCurrentX.Read(reader, this, "clutCurrentX", SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutCurrentY.Read(reader, this, "clutCurrentY", SR1_File.Version.First, SR1_File.Version.Retail_PC);

			tPageCurrent.Read(reader, this, "tPageCurrent", SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad3.Read(reader, this, "pad3", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			numFrames.Read(reader, this, "numFrames");
			speed.Read(reader, this, "speed");

			frame = new SR1_StructureArray<DrMoveAniTexSrcInfo>(numFrames.Value);
			frame.Read(reader, this, "frame");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			pixDstX.Write(writer);
			pixDstY.Write(writer);
			pixW.Write(writer);
			pixH.Write(writer);

			clutDstX.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutDstY.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutW.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutH.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);

			tPageDst.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad0.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad1.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad2.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			pixCurrentX.Write(writer);
			pixCurrentY.Write(writer);

			clutCurrentX.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
			clutCurrentY.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);

			tPageCurrent.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);
			pad3.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

			numFrames.Write(writer);
			speed.Write(writer);
			frame.Write(writer);
		}
	}
}
