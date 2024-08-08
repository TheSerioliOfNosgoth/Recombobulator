using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GenericFXObject : SR1_Structure
	{
        public readonly SR1_Pointer<GenericParticleParams> ParticleList = new SR1_Pointer<GenericParticleParams>();
        public readonly SR1_Pointer<GenericRibbonParams> RibbonList = new SR1_Pointer<GenericRibbonParams>();
        public readonly SR1_Pointer<GenericGlowParams> GlowList = new SR1_Pointer<GenericGlowParams>();
        public readonly SR1_Pointer<GenericLightningParams> LightningList = new SR1_Pointer<GenericLightningParams>();
        public readonly SR1_Pointer<GenericBlastRingParams> BlastList = new SR1_Pointer<GenericBlastRingParams>();
        public readonly SR1_Pointer<GenericFlashParams> FlashList = new SR1_Pointer<GenericFlashParams>();
        public readonly SR1_PrimativePointer<int> ColorList = new SR1_PrimativePointer<int>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			ParticleList.Read(reader, this, "ParticleList");
			RibbonList.Read(reader, this, "RibbonList");
			GlowList.Read(reader, this, "GlowList");
			LightningList.Read(reader, this, "LightningList", SR1_File.Version.May12, SR1_File.Version.Next);
			BlastList.Read(reader, this, "BlastList", SR1_File.Version.May12, SR1_File.Version.Next);
			FlashList.Read(reader, this, "FlashList", SR1_File.Version.May12, SR1_File.Version.Next);
			ColorList.Read(reader, this, "ColorList");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if (reader.File._Version >= SR1_File.Version.May12)
			{
				new SR1_StructureSeries<GenericParticleParams>().ReadFromPointer(reader, ParticleList, RibbonList);
				new SR1_StructureSeries<GenericRibbonParams>().ReadFromPointer(reader, RibbonList, GlowList);
				new SR1_StructureSeries<GenericGlowParams>().ReadFromPointer(reader, GlowList, LightningList);
				new SR1_StructureSeries<GenericLightningParams>().ReadFromPointer(reader, LightningList, BlastList);
				new SR1_StructureSeries<GenericBlastRingParams>().ReadFromPointer(reader, BlastList, ColorList);
				new SR1_PrimativeArray<uint>((int)(FlashList.Offset - ColorList.Offset) >> 2).ReadFromPointer(reader, ColorList);
				new SR1_StructureSeries<GenericFlashParams>().ReadFromPointer(reader, FlashList, (uint)reader.BaseStream.Length);
			}
			else
			{
				new SR1_StructureSeries<GenericParticleParams>().ReadFromPointer(reader, ParticleList, RibbonList);
				new SR1_StructureSeries<GenericRibbonParams>().ReadFromPointer(reader, RibbonList, GlowList);
				new SR1_StructureSeries<GenericGlowParams>().ReadFromPointer(reader, GlowList, ColorList);
				new SR1_PrimativeArray<uint>((int)(reader.BaseStream.Length - ColorList.Offset) >> 2).ReadFromPointer(reader, ColorList);
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			ParticleList.Write(writer);
			RibbonList.Write(writer);
			GlowList.Write(writer);
			LightningList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			BlastList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			FlashList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			ColorList.Write(writer);
		}
	}
}
