﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class MonsterSubAttributes : SR1_Structure
	{
		public readonly SR1_PrimativePointer<sbyte> animList = new SR1_PrimativePointer<sbyte>();
		public readonly SR1_Pointer<MonsterSenses> senses = new SR1_Pointer<MonsterSenses>();
		public readonly SR1_Pointer<MonsterCombatAttributes> combatAttributes = new SR1_Pointer<MonsterCombatAttributes>();
		public readonly SR1_Pointer<MonsterAllegiances> allegiances = new SR1_Pointer<MonsterAllegiances>();
		public readonly SR1_PrimativePointer<sbyte> behaviorList = new SR1_PrimativePointer<sbyte>();
		public readonly SR1_Primative<short> scale = new SR1_Primative<short>();
		public readonly SR1_Primative<short> fallDistance = new SR1_Primative<short>();
		public readonly SR1_Primative<short> defAmbushRange = new SR1_Primative<short>();
		public readonly SR1_Primative<short> fleeRange = new SR1_Primative<short>();
		public readonly SR1_Primative<short> speedPivotTurn = new SR1_Primative<short>();
		public readonly SR1_Primative<short> speedWalkTurn = new SR1_Primative<short>();
		public readonly SR1_Primative<short> speedRunTurn = new SR1_Primative<short>();
		public readonly SR1_Primative<short> speedFleeTurn = new SR1_Primative<short>();
		public readonly SR1_Primative<short> forgetTime = new SR1_Primative<short>();
		public readonly SR1_Primative<byte> modelNum = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> physAbility = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> stunnable = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> grabable = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> numSections = new SR1_Primative<byte>();
		public readonly SR1_PrimativeArray<byte> sectionEnd = new SR1_PrimativeArray<byte>(3);
		public readonly SR1_Primative<short> defPlayerAttitude = new SR1_Primative<short>();
		public readonly SR1_Primative<short> defWanderRange = new SR1_Primative<short>();
		public readonly SR1_Primative<short> defGuardRange = new SR1_Primative<short>();
		public readonly SR1_Primative<byte> defInitialBehavior = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> defTriggeredBehavior = new SR1_Primative<byte>();
		public readonly SR1_Primative<sbyte> defActiveBehavior = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<byte> defSpectral = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> sunVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> fireVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> waterVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> impaleVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> soundVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> weaponVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> missileVuln = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseRed = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseGreen = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseBlue = new SR1_Primative<byte>();
		public readonly SR1_Primative<short> minSpikeRange = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxSpikeRange = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxSpikeAngle = new SR1_Primative<short>();
		public readonly SR1_Primative<short> minSpikeHorzSpeed = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxSpikeHorzSpeed = new SR1_Primative<short>();
		public readonly SR1_Primative<short> maxSpikeVertSpeed = new SR1_Primative<short>();
		public readonly SR1_Primative<short> upOnGroundOffset = new SR1_Primative<short>();
		public readonly SR1_Primative<short> downOnGroundOffset = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			reader.MonsterSubAttributes = this;

			animList.Read(reader, this, "animList");
			senses.Read(reader, this, "senses");
			combatAttributes.Read(reader, this, "combatAttributes");
			allegiances.Read(reader, this, "allegiances");
			behaviorList.Read(reader, this, "behaviorList");
			scale.Read(reader, this, "scale");
			fallDistance.Read(reader, this, "fallDistance");
			defAmbushRange.Read(reader, this, "defAmbushRange");
			fleeRange.Read(reader, this, "fleeRange");
			speedPivotTurn.Read(reader, this, "speedPivotTurn");
			speedWalkTurn.Read(reader, this, "speedWalkTurn");
			speedRunTurn.Read(reader, this, "speedRunTurn");
			speedFleeTurn.Read(reader, this, "speedFleeTurn");
			forgetTime.Read(reader, this, "forgetTime");
			modelNum.Read(reader, this, "modelNum");
			physAbility.Read(reader, this, "physAbility");
			stunnable.Read(reader, this, "stunnable");
			grabable.Read(reader, this, "grabable", SR1_File.Version.Feb16, SR1_File.Version.Next);
			numSections.Read(reader, this, "numSections");
			sectionEnd.Read(reader, this, "sectionEnd");
			defPlayerAttitude.Read(reader, this, "defPlayerAttitude");
			defWanderRange.Read(reader, this, "defWanderRange");
			defGuardRange.Read(reader, this, "defGuardRange");
			defInitialBehavior.Read(reader, this, "defInitialBehavior");
			defTriggeredBehavior.Read(reader, this, "defTriggeredBehavior");
			defActiveBehavior.Read(reader, this, "defActiveBehavior");
			defSpectral.Read(reader, this, "defSpectral");
			sunVuln.Read(reader, this, "sunVuln");
			fireVuln.Read(reader, this, "fireVuln");
			waterVuln.Read(reader, this, "waterVuln");
			impaleVuln.Read(reader, this, "impaleVuln");
			soundVuln.Read(reader, this, "soundVuln");
			weaponVuln.Read(reader, this, "weaponVuln");
			missileVuln.Read(reader, this, "missileVuln");
			bruiseRed.Read(reader, this, "bruiseRed", SR1_File.Version.Jun01, SR1_File.Version.Next);
			bruiseGreen.Read(reader, this, "bruiseGreen", SR1_File.Version.Jun01, SR1_File.Version.Next);
			bruiseBlue.Read(reader, this, "bruiseBlue", SR1_File.Version.Jun01, SR1_File.Version.Next);
			minSpikeRange.Read(reader, this, "minSpikeRange");
			maxSpikeRange.Read(reader, this, "maxSpikeRange");
			maxSpikeAngle.Read(reader, this, "maxSpikeAngle");
			minSpikeHorzSpeed.Read(reader, this, "minSpikeHorzSpeed");
			maxSpikeHorzSpeed.Read(reader, this, "maxSpikeHorzSpeed");
			maxSpikeVertSpeed.Read(reader, this, "maxSpikeVertSpeed");
			upOnGroundOffset.Read(reader, this, "upOnGroundOffset", SR1_File.Version.May12, SR1_File.Version.Next);
			downOnGroundOffset.Read(reader, this, "downOnGroundOffset", SR1_File.Version.May12, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new MonsterSenses().ReadFromPointer(reader, senses);
			new MonsterAllegiances().ReadFromPointer(reader, allegiances);

			// The 46 comes from the MonsterAnim enum.
			new SR1_PrimativeArray<sbyte>(46).SetPadding(4).ReadFromPointer(reader, animList);

			// The 18 is a wild guess based on the space left.
			new SR1_PrimativeArray<sbyte>(18).SetPadding(4).ReadFromPointer(reader, behaviorList);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			animList.Write(writer);
			senses.Write(writer);
			combatAttributes.Write(writer);
			allegiances.Write(writer);
			behaviorList.Write(writer);
			scale.Write(writer);
			fallDistance.Write(writer);
			defAmbushRange.Write(writer);
			fleeRange.Write(writer);
			speedPivotTurn.Write(writer);
			speedWalkTurn.Write(writer);
			speedRunTurn.Write(writer);
			speedFleeTurn.Write(writer);
			forgetTime.Write(writer);
			modelNum.Write(writer);
			physAbility.Write(writer);
			stunnable.Write(writer);
			grabable.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Next);
			numSections.Write(writer);
			sectionEnd.Write(writer);
			defPlayerAttitude.Write(writer);
			defWanderRange.Write(writer);
			defGuardRange.Write(writer);
			defInitialBehavior.Write(writer);
			defTriggeredBehavior.Write(writer);
			defActiveBehavior.Write(writer);
			defSpectral.Write(writer);
			sunVuln.Write(writer);
			fireVuln.Write(writer);
			waterVuln.Write(writer);
			impaleVuln.Write(writer);
			soundVuln.Write(writer);
			weaponVuln.Write(writer);
			missileVuln.Write(writer);
			bruiseRed.Write(writer, SR1_File.Version.Jun01, SR1_File.Version.Next);
			bruiseGreen.Write(writer, SR1_File.Version.Jun01, SR1_File.Version.Next);
			bruiseBlue.Write(writer, SR1_File.Version.Jun01, SR1_File.Version.Next);
			minSpikeRange.Write(writer);
			maxSpikeRange.Write(writer);
			maxSpikeAngle.Write(writer);
			minSpikeHorzSpeed.Write(writer);
			maxSpikeHorzSpeed.Write(writer);
			maxSpikeVertSpeed.Write(writer);
			upOnGroundOffset.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			downOnGroundOffset.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
		}
	}
}
