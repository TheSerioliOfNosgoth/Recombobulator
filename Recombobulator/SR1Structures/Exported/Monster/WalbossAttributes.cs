using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class WalBossAttributes : SR1_Structure
	{
		SR1_Primative<byte> segmentToAttach = new SR1_Primative<byte>();
		SR1_Primative<byte> frameToBirth = new SR1_Primative<byte>();
		SR1_Primative<byte> frameToExpel = new SR1_Primative<byte>();
		SR1_Primative<byte> maxEggHits = new SR1_Primative<byte>();
		SR1_Primative<short> maxAngle = new SR1_Primative<short>();
		SR1_Primative<short> timeForHitAgony = new SR1_Primative<short>();
		SR1_Primative<short> segmentForBodyTwist = new SR1_Primative<short>();
		SR1_Primative<short> timeToThornPrep = new SR1_Primative<short>();
		SR1_Primative<short> timeToThornPoke = new SR1_Primative<short>();
		SR1_Primative<short> timeToEggThrob = new SR1_Primative<short>();
		SR1_Primative<short> timeToEggExplode = new SR1_Primative<short>();
		SR1_Primative<short> razielStunTime = new SR1_Primative<short>();
		SR1_Primative<short> eggIgniteDist = new SR1_Primative<short>();
		SR1_Primative<short> junk = new SR1_Primative<short>();
		WalBossAttackDeltas attackDeltas = new WalBossAttackDeltas();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			segmentToAttach.Read(reader, this, "segmentToAttach");
			frameToBirth.Read(reader, this, "frameToBirth");
			frameToExpel.Read(reader, this, "frameToExpel");
			maxEggHits.Read(reader, this, "maxEggHits");
			maxAngle.Read(reader, this, "maxAngle");
			timeForHitAgony.Read(reader, this, "timeForHitAgony");
			segmentForBodyTwist.Read(reader, this, "segmentForBodyTwist");
			timeToThornPrep.Read(reader, this, "timeToThornPrep");
			timeToThornPoke.Read(reader, this, "timeToThornPoke");
			timeToEggThrob.Read(reader, this, "timeToEggThrob");
			timeToEggExplode.Read(reader, this, "timeToEggExplode");
			razielStunTime.Read(reader, this, "razielStunTime");
			eggIgniteDist.Read(reader, this, "eggIgniteDist");
			junk.Read(reader, this, "junk");
			attackDeltas.Read(reader, this, "attackDeltas");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			segmentToAttach.Write(writer);
			frameToBirth.Write(writer);
			frameToExpel.Write(writer);
			maxEggHits.Write(writer);
			maxAngle.Write(writer);
			timeForHitAgony.Write(writer);
			segmentForBodyTwist.Write(writer);
			timeToThornPrep.Write(writer);
			timeToThornPoke.Write(writer);
			timeToEggThrob.Write(writer);
			timeToEggExplode.Write(writer);
			razielStunTime.Write(writer);
			eggIgniteDist.Write(writer);
			junk.Write(writer);
			attackDeltas.Write(writer);
		}
	}
}
