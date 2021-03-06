﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericFXObject : SR1_Structure
    {
        SR1_Pointer<GenericParticleParams> ParticleList = new SR1_Pointer<GenericParticleParams>();
        SR1_Pointer<GenericRibbonParams> RibbonList = new SR1_Pointer<GenericRibbonParams>();
        SR1_Pointer<GenericGlowParams> GlowList = new SR1_Pointer<GenericGlowParams>();
        SR1_Pointer<GenericLightningParams> LightningList = new SR1_Pointer<GenericLightningParams>();
        SR1_Pointer<GenericBlastRingParams> BlastList = new SR1_Pointer<GenericBlastRingParams>();
        SR1_Pointer<GenericFlashParams> FlashList = new SR1_Pointer<GenericFlashParams>();
        SR1_PrimativePointer<int> ColorList = new SR1_PrimativePointer<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            ParticleList.Read(reader, this, "ParticleList");
            RibbonList.Read(reader, this, "RibbonList");
            GlowList.Read(reader, this, "GlowList");
            LightningList.Read(reader, this, "LightningList");
            BlastList.Read(reader, this, "BlastList");
            FlashList.Read(reader, this, "FlashList");
            ColorList.Read(reader, this, "ColorList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new SR1_StructureSeries<GenericParticleParams>((int)(RibbonList.Offset - ParticleList.Offset)).ReadFromPointer(reader, ParticleList);
            new SR1_StructureSeries<GenericRibbonParams>((int)(GlowList.Offset - RibbonList.Offset)).ReadFromPointer(reader, RibbonList);
            new SR1_StructureSeries<GenericGlowParams>((int)(LightningList.Offset - GlowList.Offset)).ReadFromPointer(reader, GlowList);
            new SR1_StructureSeries<GenericLightningParams>((int)(BlastList.Offset - LightningList.Offset)).ReadFromPointer(reader, LightningList);
            new SR1_StructureSeries<GenericBlastRingParams>((int)(ColorList.Offset - BlastList.Offset)).ReadFromPointer(reader, BlastList);
            new SR1_PrimativeArray<uint>((int)(FlashList.Offset - ColorList.Offset) >> 2).ReadFromPointer(reader, ColorList);
            new SR1_StructureSeries<GenericFlashParams>((int)(reader.BaseStream.Length - FlashList.Offset)).ReadFromPointer(reader, FlashList);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            ParticleList.Write(writer);
            RibbonList.Write(writer);
            GlowList.Write(writer);
            LightningList.Write(writer);
            BlastList.Write(writer);
            FlashList.Write(writer);
            ColorList.Write(writer);
        }
    }
}
