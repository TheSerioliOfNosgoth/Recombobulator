using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class RazielData : SR1_Structure
    {
        SR1_Primative<uint> version = new SR1_Primative<uint>();
        SR1_Primative<uint> nonBurningRibbonStartColor = new SR1_Primative<uint>();
        SR1_Primative<uint> nonBurningRibbonEndColor = new SR1_Primative<uint>();
        SR1_PointerArrayPointer<Idle> idleList = new SR1_PointerArrayPointer<Idle>();
        SR1_PointerArrayPointer<AttackItem> attackList = new SR1_PointerArrayPointer<AttackItem>();
        SR1_Pointer<ThrowItem> throwList = new SR1_Pointer<ThrowItem>();
        SR1_Pointer<VAnim> virtualAnimations = new SR1_Pointer<VAnim>();
        SR1_Pointer<VAnim> virtualAnimSingle = new SR1_Pointer<VAnim>();
        SR1_Pointer<SAnim> stringAnimations = new SR1_Pointer<SAnim>();
        SR1_Primative<short> throwFadeValue = new SR1_Primative<short>();
        SR1_Primative<short> throwFadeInRate = new SR1_Primative<short>();
        SR1_Primative<int> throwFadeOutRate = new SR1_Primative<int>();
        SR1_Primative<int> throwManualDistance = new SR1_Primative<int>();
        SR1_Primative<short> healthMaterialRate = new SR1_Primative<short>();
        SR1_Primative<short> healthSpectralRate = new SR1_Primative<short>();
        SR1_Primative<short> healthInvinciblePostHit = new SR1_Primative<short>();
        SR1_Primative<short> healthInvinciblePostShunt = new SR1_Primative<short>();
        SR1_Primative<short> forceMinPitch = new SR1_Primative<short>();
        SR1_Primative<short> forceMaxPitch = new SR1_Primative<short>();
        SR1_Primative<short> forceMinVolume = new SR1_Primative<short>();
        SR1_Primative<short> forceMaxVolume = new SR1_Primative<short>();
        SR1_Primative<uint> forceRampTime = new SR1_Primative<uint>();
        SR1_Primative<int> SwimPhysicsFallDamping = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsWaterFrequency = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsWaterAmplitude = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderDeceleration = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderKickVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderKickAccel = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderKickDecel = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsUnderStealthAdjust = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsCoilVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsCoilDecelerationIn = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsCoilDecelerationOut = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsShotVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsShotAccelerationIn = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsShotAccelerationOut = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfAccelerationIn = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfAccelerationOut = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfKickVelocity = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfKickAccel = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfMinRotation = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfMaxRotation = new SR1_Primative<int>();
        SR1_Primative<int> SwimPhysicsSurfKickDecel = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            version.Read(reader, this, "version");
            nonBurningRibbonStartColor.Read(reader, this, "nonBurningRibbonStartColor");
            nonBurningRibbonEndColor.Read(reader, this, "nonBurningRibbonEndColor");
            idleList.Read(reader, this, "idleList");
            attackList.Read(reader, this, "attackList");
            throwList.Read(reader, this, "throwList");
            virtualAnimations.Read(reader, this, "virtualAnimations");
            virtualAnimSingle.Read(reader, this, "virtualAnimSingle");
            stringAnimations.Read(reader, this, "stringAnimations");
            throwFadeValue.Read(reader, this, "throwFadeValue");
            throwFadeInRate.Read(reader, this, "throwFadeInRate");
            throwFadeOutRate.Read(reader, this, "throwFadeOutRate");
            throwManualDistance.Read(reader, this, "throwManualDistance");
            healthMaterialRate.Read(reader, this, "healthMaterialRate");
            healthSpectralRate.Read(reader, this, "healthSpectralRate");
            healthInvinciblePostHit.Read(reader, this, "healthInvinciblePostHit");
            healthInvinciblePostShunt.Read(reader, this, "healthInvinciblePostShunt");
            forceMinPitch.Read(reader, this, "forceMinPitch");
            forceMaxPitch.Read(reader, this, "forceMaxPitch");
            forceMinVolume.Read(reader, this, "forceMinVolume");
            forceMaxVolume.Read(reader, this, "forceMaxVolume");
            forceRampTime.Read(reader, this, "forceRampTime");
            SwimPhysicsFallDamping.Read(reader, this, "SwimPhysicsFallDamping");
            SwimPhysicsWaterFrequency.Read(reader, this, "SwimPhysicsWaterFrequency");
            SwimPhysicsWaterAmplitude.Read(reader, this, "SwimPhysicsWaterAmplitude");
            SwimPhysicsUnderDeceleration.Read(reader, this, "SwimPhysicsUnderDeceleration");
            SwimPhysicsUnderKickVelocity.Read(reader, this, "SwimPhysicsUnderKickVelocity");
            SwimPhysicsUnderKickAccel.Read(reader, this, "SwimPhysicsUnderKickAccel");
            SwimPhysicsUnderVelocity.Read(reader, this, "SwimPhysicsUnderVelocity");
            SwimPhysicsUnderKickDecel.Read(reader, this, "SwimPhysicsUnderKickDecel");
            SwimPhysicsUnderStealthAdjust.Read(reader, this, "SwimPhysicsUnderStealthAdjust");
            SwimPhysicsCoilVelocity.Read(reader, this, "SwimPhysicsCoilVelocity");
            SwimPhysicsCoilDecelerationIn.Read(reader, this, "SwimPhysicsCoilDecelerationIn");
            SwimPhysicsCoilDecelerationOut.Read(reader, this, "SwimPhysicsCoilDecelerationOut");
            SwimPhysicsShotVelocity.Read(reader, this, "SwimPhysicsShotVelocity");
            SwimPhysicsShotAccelerationIn.Read(reader, this, "SwimPhysicsShotAccelerationIn");
            SwimPhysicsShotAccelerationOut.Read(reader, this, "SwimPhysicsShotAccelerationOut");
            SwimPhysicsSurfVelocity.Read(reader, this, "SwimPhysicsSurfVelocity");
            SwimPhysicsSurfAccelerationIn.Read(reader, this, "SwimPhysicsSurfAccelerationIn");
            SwimPhysicsSurfAccelerationOut.Read(reader, this, "SwimPhysicsSurfAccelerationOut");
            SwimPhysicsSurfKickVelocity.Read(reader, this, "SwimPhysicsSurfKickVelocity");
            SwimPhysicsSurfKickAccel.Read(reader, this, "SwimPhysicsSurfKickAccel");
            SwimPhysicsSurfMinRotation.Read(reader, this, "SwimPhysicsSurfMinRotation");
            SwimPhysicsSurfMaxRotation.Read(reader, this, "SwimPhysicsSurfMaxRotation");
            SwimPhysicsSurfKickDecel.Read(reader, this, "SwimPhysicsSurfKickDecel");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            new IdleList().ReadFromPointer(reader, idleList);
            new AttackItemList().ReadFromPointer(reader, attackList);
            new ThrowItemSet().ReadFromPointer(reader, throwList);
            new SAnimSet().ReadFromPointer(reader, stringAnimations);

            if (reader.IdleAnimSetDictionary.Count > 0)
            {
                SR1_StructureArray<IdleSet> idleAnimSets = new SR1_StructureArray<IdleSet>(reader.IdleAnimSetDictionary.Count);
                idleAnimSets.ReadFromPointer(reader, reader.IdleAnimSetDictionary.Values[0]);
            }

            if (reader.IdleAnimDictionary.Count > 0)
            {
                SR1_StructureArray<Idle> idleAnims = new SR1_StructureArray<Idle>(reader.IdleAnimDictionary.Count);
                idleAnims.ReadFromPointer(reader, reader.IdleAnimDictionary.Values[0]);
            }

            if (reader.AttackAnimSetDictionary.Count > 0)
            {
                SR1_StructureArray<AttackItemSet> attackAnimSets = new SR1_StructureArray<AttackItemSet>(reader.AttackAnimSetDictionary.Count);
                attackAnimSets.ReadFromPointer(reader, reader.AttackAnimSetDictionary.Values[0]);
            }

            if (reader.AttackAnimDictionary.Count > 0)
            {
                SR1_StructureArray<AttackItem> attackAnims = new SR1_StructureArray<AttackItem>(reader.AttackAnimDictionary.Count);
                attackAnims.ReadFromPointer(reader, reader.AttackAnimDictionary.Values[0]);
            }

            if (reader.ThrowAnimDictionary.Count > 0)
            {
                SR1_StructureArray<ThrowItem> throwAnims = new SR1_StructureArray<ThrowItem>(reader.ThrowAnimDictionary.Count);
                throwAnims.ReadFromPointer(reader, reader.ThrowAnimDictionary.Values[0]);
            }

            new SR1_StructureSeries<VAnim>((int)(virtualAnimSingle.Offset - virtualAnimations.Offset)).ReadFromPointer(reader, virtualAnimations);

            if (reader.SAnimDictionary.Count > 0)
            {
                new SR1_StructureSeries<SAnim>((int)(reader.BaseStream.Length - reader.SAnimDictionary.Values[0].Offset)).ReadFromPointer(reader, reader.SAnimDictionary.Values[0]);
                new SR1_StructureSeries<VAnim>((int)(reader.SAnimDictionary.Values[0].Offset - virtualAnimSingle.Offset)).ReadFromPointer(reader, virtualAnimSingle);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            version.Write(writer);
            nonBurningRibbonStartColor.Write(writer);
            nonBurningRibbonEndColor.Write(writer);
            idleList.Write(writer);
            attackList.Write(writer);
            throwList.Write(writer);
            virtualAnimations.Write(writer);
            virtualAnimSingle.Write(writer);
            stringAnimations.Write(writer);
            throwFadeValue.Write(writer);
            throwFadeInRate.Write(writer);
            throwFadeOutRate.Write(writer);
            throwManualDistance.Write(writer);
            healthMaterialRate.Write(writer);
            healthSpectralRate.Write(writer);
            healthInvinciblePostHit.Write(writer);
            healthInvinciblePostShunt.Write(writer);
            forceMinPitch.Write(writer);
            forceMaxPitch.Write(writer);
            forceMinVolume.Write(writer);
            forceMaxVolume.Write(writer);
            forceRampTime.Write(writer);
            SwimPhysicsFallDamping.Write(writer);
            SwimPhysicsWaterFrequency.Write(writer);
            SwimPhysicsWaterAmplitude.Write(writer);
            SwimPhysicsUnderDeceleration.Write(writer);
            SwimPhysicsUnderKickVelocity.Write(writer);
            SwimPhysicsUnderKickAccel.Write(writer);
            SwimPhysicsUnderVelocity.Write(writer);
            SwimPhysicsUnderKickDecel.Write(writer);
            SwimPhysicsUnderStealthAdjust.Write(writer);
            SwimPhysicsCoilVelocity.Write(writer);
            SwimPhysicsCoilDecelerationIn.Write(writer);
            SwimPhysicsCoilDecelerationOut.Write(writer);
            SwimPhysicsShotVelocity.Write(writer);
            SwimPhysicsShotAccelerationIn.Write(writer);
            SwimPhysicsShotAccelerationOut.Write(writer);
            SwimPhysicsSurfVelocity.Write(writer);
            SwimPhysicsSurfAccelerationIn.Write(writer);
            SwimPhysicsSurfAccelerationOut.Write(writer);
            SwimPhysicsSurfKickVelocity.Write(writer);
            SwimPhysicsSurfKickAccel.Write(writer);
            SwimPhysicsSurfMinRotation.Write(writer);
            SwimPhysicsSurfMaxRotation.Write(writer);
            SwimPhysicsSurfKickDecel.Write(writer);
        }
    }
}
