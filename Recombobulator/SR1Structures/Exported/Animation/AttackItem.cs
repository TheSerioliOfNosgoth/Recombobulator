using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class AttackItem : SR1_Structure
    {
        SR1_Primative<byte> anim = new SR1_Primative<byte>();
        SR1_Primative<byte> framesIn = new SR1_Primative<byte>();
        SR1_Primative<byte> alphaIn = new SR1_Primative<byte>();
        SR1_Primative<byte> framesOut = new SR1_Primative<byte>();
        SR1_Primative<byte> switchDelay = new SR1_Primative<byte>();
        SR1_Primative<byte> ignoreDelay = new SR1_Primative<byte>();
        SR1_Primative<byte> segmentToGlow = new SR1_Primative<byte>();
        SR1_Primative<byte> startCollisionFrame = new SR1_Primative<byte>();
        SR1_Primative<byte> handsToCollide = new SR1_Primative<byte>();
        SR1_Primative<byte> endCollisionFrame = new SR1_Primative<byte>();
        SR1_Primative<byte> ribbonStartFrame = new SR1_Primative<byte>();
        SR1_Primative<byte> ribbonStartSegment = new SR1_Primative<byte>();
        SR1_Primative<byte> ribbonEndSegment = new SR1_Primative<byte>();
        SR1_Primative<byte> ribbonLifeTime = new SR1_Primative<byte>();
        SR1_Primative<byte> ribbonFaceLifeTime = new SR1_Primative<byte>();
        SR1_Primative<byte> knockBackFrames = new SR1_Primative<byte>();
        SR1_Primative<byte> glowFadeInFrames = new SR1_Primative<byte>();
        SR1_Primative<byte> glowFadeOutFrames = new SR1_Primative<byte>();
        SR1_Primative<ushort> ribbonStartOpacity = new SR1_Primative<ushort>();
        SR1_Primative<uint> ribbonStartColor = new SR1_Primative<uint>();
        SR1_Primative<uint> ribbonEndColor = new SR1_Primative<uint>();
        SR1_Primative<uint> glowColor = new SR1_Primative<uint>();
        SR1_Primative<ushort> knockBackDistance = new SR1_Primative<ushort>();
        SR1_Primative<ushort> hitPowerScale = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            anim.Read(reader, this, "anim");
            framesIn.Read(reader, this, "framesIn");
            alphaIn.Read(reader, this, "alphaIn");
            framesOut.Read(reader, this, "framesOut");
            switchDelay.Read(reader, this, "switchDelay");
            ignoreDelay.Read(reader, this, "ignoreDelay");
            segmentToGlow.Read(reader, this, "segmentToGlow");
            startCollisionFrame.Read(reader, this, "startCollisionFrame");
            handsToCollide.Read(reader, this, "handsToCollide");
            endCollisionFrame.Read(reader, this, "endCollisionFrame");
            ribbonStartFrame.Read(reader, this, "ribbonStartFrame");
            ribbonStartSegment.Read(reader, this, "ribbonStartSegment");
            ribbonEndSegment.Read(reader, this, "ribbonEndSegment");
            ribbonLifeTime.Read(reader, this, "ribbonLifeTime");
            ribbonFaceLifeTime.Read(reader, this, "ribbonFaceLifeTime");
            knockBackFrames.Read(reader, this, "knockBackFrames");
            glowFadeInFrames.Read(reader, this, "glowFadeInFrames");
            glowFadeOutFrames.Read(reader, this, "glowFadeOutFrames");
            ribbonStartOpacity.Read(reader, this, "ribbonStartOpacity");
            ribbonStartColor.Read(reader, this, "ribbonStartColor");
            ribbonEndColor.Read(reader, this, "ribbonEndColor");
            glowColor.Read(reader, this, "glowColor");
            knockBackDistance.Read(reader, this, "knockBackDistance");
            hitPowerScale.Read(reader, this, "hitPowerScale");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            anim.Write(writer);
            framesIn.Write(writer);
            alphaIn.Write(writer);
            framesOut.Write(writer);
            switchDelay.Write(writer);
            ignoreDelay.Write(writer);
            segmentToGlow.Write(writer);
            startCollisionFrame.Write(writer);
            handsToCollide.Write(writer);
            endCollisionFrame.Write(writer);
            ribbonStartFrame.Write(writer);
            ribbonStartSegment.Write(writer);
            ribbonEndSegment.Write(writer);
            ribbonLifeTime.Write(writer);
            ribbonFaceLifeTime.Write(writer);
            knockBackFrames.Write(writer);
            glowFadeInFrames.Write(writer);
            glowFadeOutFrames.Write(writer);
            ribbonStartOpacity.Write(writer);
            ribbonStartColor.Write(writer);
            ribbonEndColor.Write(writer);
            glowColor.Write(writer);
            knockBackDistance.Write(writer);
            hitPowerScale.Write(writer);
        }
    }
}
