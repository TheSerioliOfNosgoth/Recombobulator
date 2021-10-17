using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class ThrowItem : SR1_Structure
	{
		SR1_Primative<byte> prepAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> prepAnimFramesIn = new SR1_Primative<byte>();
		SR1_Primative<byte> prepAnimAlphaIn = new SR1_Primative<byte>();
		SR1_Primative<byte> coilAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> coilAnimFramesIn = new SR1_Primative<byte>();
		SR1_Primative<byte> coilAnimAlphaIn = new SR1_Primative<byte>();
		SR1_Primative<byte> throwAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> throwAnimFramesInFactor = new SR1_Primative<byte>();
		SR1_Primative<byte> throwFrameToLaunch = new SR1_Primative<byte>();
		SR1_Primative<byte> framesOut = new SR1_Primative<byte>();
		SR1_Primative<ushort> launchPointX = new SR1_Primative<ushort>();
		SR1_Primative<ushort> launchPointY = new SR1_Primative<ushort>();
		SR1_Primative<ushort> launchPointZ = new SR1_Primative<ushort>();
		SR1_Primative<ushort> maxXRotation = new SR1_Primative<ushort>();
		SR1_Primative<ushort> minXRotation = new SR1_Primative<ushort>();
		SR1_Primative<ushort> maxZRotation = new SR1_Primative<ushort>();
		SR1_Primative<ushort> minZRotation = new SR1_Primative<ushort>();
		SR1_Primative<ushort> velocity = new SR1_Primative<ushort>();
		SR1_Primative<ushort> gravity = new SR1_Primative<ushort>();
		SR1_Primative<ushort> coilRot = new SR1_Primative<ushort>();
		SR1_Primative<ushort> pad = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			prepAnim.Read(reader, this, "prepAnim");
			prepAnimFramesIn.Read(reader, this, "prepAnimFramesIn");
			prepAnimAlphaIn.Read(reader, this, "prepAnimAlphaIn");
			coilAnim.Read(reader, this, "coilAnim");
			coilAnimFramesIn.Read(reader, this, "coilAnimFramesIn");
			coilAnimAlphaIn.Read(reader, this, "coilAnimAlphaIn");
			throwAnim.Read(reader, this, "throwAnim");
			throwAnimFramesInFactor.Read(reader, this, "throwAnimFramesInFactor");
			throwFrameToLaunch.Read(reader, this, "throwFrameToLaunch");
			framesOut.Read(reader, this, "framesOut");
			launchPointX.Read(reader, this, "launchPointX");
			launchPointY.Read(reader, this, "launchPointY");
			launchPointZ.Read(reader, this, "launchPointZ");
			maxXRotation.Read(reader, this, "maxXRotation");
			minXRotation.Read(reader, this, "minXRotation");
			maxZRotation.Read(reader, this, "maxZRotation");
			minZRotation.Read(reader, this, "minZRotation");
			velocity.Read(reader, this, "velocity");
			gravity.Read(reader, this, "gravity");
			coilRot.Read(reader, this, "coilRot");
			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			prepAnim.Write(writer);
			prepAnimFramesIn.Write(writer);
			prepAnimAlphaIn.Write(writer);
			coilAnim.Write(writer);
			coilAnimFramesIn.Write(writer);
			coilAnimAlphaIn.Write(writer);
			throwAnim.Write(writer);
			throwAnimFramesInFactor.Write(writer);
			throwFrameToLaunch.Write(writer);
			framesOut.Write(writer);
			launchPointX.Write(writer);
			launchPointY.Write(writer);
			launchPointZ.Write(writer);
			maxXRotation.Write(writer);
			minXRotation.Write(writer);
			maxZRotation.Write(writer);
			minZRotation.Write(writer);
			velocity.Write(writer);
			gravity.Write(writer);
			coilRot.Write(writer);
			pad.Write(writer);
		}
	}
}
