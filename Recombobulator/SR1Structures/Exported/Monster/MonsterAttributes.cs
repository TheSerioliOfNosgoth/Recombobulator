using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MonsterAttributes : SR1_Structure
	{
		public readonly SR1_Primative<uint> magicnum = new SR1_Primative<uint>().ShowAsHex(true);
		public readonly SR1_Pointer<MonsterTuneData> tunData = new SR1_Pointer<MonsterTuneData>();
		public readonly SR1_PrimativePointer<sbyte> auxAnimList = new SR1_PrimativePointer<sbyte>();
		public readonly SR1_PrimativePointer<sbyte> ambientAnimList = new SR1_PrimativePointer<sbyte>();
		public readonly SR1_Primative<int> whatAmI = new SR1_Primative<int>().ShowAsHex(true);
		public readonly SR1_Primative<sbyte> numAuxAnims = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numAmbientAnims = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<byte> defaultAge = new SR1_Primative<byte>();
		public readonly SR1_Primative<sbyte> pupateObject = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<byte> damageFXSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> headSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> neckSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> leftShoulderSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> rightShoulderSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> waistSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> leftKneeSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> rightKneeSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> leftFootSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> rightFootSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> spineSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> leftWeaponSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> rightWeaponSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> grabSegment = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bloodImpaleFrame = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bloodConeFrame = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseRed = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseGreen = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> bruiseBlue = new SR1_Primative<byte>();
		public readonly SR1_Primative<sbyte> numSubAttributes = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numCombatAttributes = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numAttackAttributes = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numMissiles = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numAnims = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numIdles = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numBehaviors = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<sbyte> numShatters = new SR1_Primative<sbyte>();
		public readonly SR1_PrimativeArray<byte> pad = new SR1_PrimativeArray<byte>(0);
		public readonly SR1_PointerArrayPointer<MonsterSubAttributes> subAttributesList = new SR1_PointerArrayPointer<MonsterSubAttributes>();
		public readonly SR1_PointerArrayPointer<MonsterCombatAttributes> combatAttributesList = new SR1_PointerArrayPointer<MonsterCombatAttributes>();
		public readonly SR1_Pointer<MonsterAttackAttributes> attackAttributesList = new SR1_Pointer<MonsterAttackAttributes>();
		public readonly SR1_Pointer<MonsterMissile> missileList = new SR1_Pointer<MonsterMissile>();
		public readonly SR1_Pointer<MonsterAnim> animList = new SR1_Pointer<MonsterAnim>();
		public readonly SR1_Pointer<MonsterIdle> idleList = new SR1_Pointer<MonsterIdle>();
		public readonly SR1_Pointer<MonsterBehavior> behaviorList = new SR1_Pointer<MonsterBehavior>();
		public readonly SR1_Pointer<FXSplinter> shatterList = new SR1_Pointer<FXSplinter>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			magicnum.Read(reader, this, "magicnum");
			tunData.Read(reader, this, "tunData");
			auxAnimList.Read(reader, this, "auxAnimList");
			ambientAnimList.Read(reader, this, "ambientAnimList");
			whatAmI.Read(reader, this, "whatAmI");
			numAuxAnims.Read(reader, this, "numAuxAnims");
			numAmbientAnims.Read(reader, this, "numAmbientAnims");
			defaultAge.Read(reader, this, "defaultAge");
			pupateObject.Read(reader, this, "pupateObject");
			damageFXSegment.Read(reader, this, "damageFXSegment");
			headSegment.Read(reader, this, "headSegment");
			neckSegment.Read(reader, this, "neckSegment");
			leftShoulderSegment.Read(reader, this, "leftShoulderSegment");
			rightShoulderSegment.Read(reader, this, "rightShoulderSegment");
			waistSegment.Read(reader, this, "waistSegment");
			leftKneeSegment.Read(reader, this, "leftKneeSegment");
			rightKneeSegment.Read(reader, this, "rightKneeSegment");
			leftFootSegment.Read(reader, this, "leftFootSegment");
			rightFootSegment.Read(reader, this, "rightFootSegment");
			spineSegment.Read(reader, this, "spineSegment");
			leftWeaponSegment.Read(reader, this, "leftWeaponSegment");
			rightWeaponSegment.Read(reader, this, "rightWeaponSegment");
			grabSegment.Read(reader, this, "grabSegment", SR1_File.Version.Feb16, SR1_File.Version.Next);
			bloodImpaleFrame.Read(reader, this, "bloodImpaleFrame");
			bloodConeFrame.Read(reader, this, "bloodConeFrame");
			bruiseRed.Read(reader, this, "bruiseRed", SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			bruiseGreen.Read(reader, this, "bruiseGreen", SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			bruiseBlue.Read(reader, this, "bruiseBlue", SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			numSubAttributes.Read(reader, this, "numSubAttributes");
			numCombatAttributes.Read(reader, this, "numCombatAttributes");
			numAttackAttributes.Read(reader, this, "numAttackAttributes");
			numMissiles.Read(reader, this, "numMissiles");
			numAnims.Read(reader, this, "numAnims");
			numIdles.Read(reader, this, "numIdles");
			numBehaviors.Read(reader, this, "numBehaviors");
			numShatters.Read(reader, this, "numShatters", SR1_File.Version.May12, SR1_File.Version.Next);
			pad.SetPadding(4).Read(reader, this, "pad");
			subAttributesList.Read(reader, this, "subAttributesList");
			combatAttributesList.Read(reader, this, "combatAttributesList");
			attackAttributesList.Read(reader, this, "attackAttributesList");
			missileList.Read(reader, this, "missileList");
			animList.Read(reader, this, "animList");
			idleList.Read(reader, this, "idleList");
			behaviorList.Read(reader, this, "behaviorList");
			shatterList.Read(reader, this, "shatterList", SR1_File.Version.May12, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			SR1_Structure temp = null;

			temp = new SR1_PrimativeArray<sbyte>(numAuxAnims.Value).ReadFromPointer(reader, auxAnimList);

			// Unknown byte plus padding. Always 0xD1
			if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
			{
				reader.BaseStream.Position = temp.End;
				new SR1_Primative<byte>().SetPadding(4).Read(reader, null, "");
			}

			temp = new SR1_PrimativeArray<sbyte>(numAmbientAnims.Value).ReadFromPointer(reader, ambientAnimList);

			// Unknown byte plus padding. Always 0xD1
			if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
			{
				reader.BaseStream.Position = temp.End;
				new SR1_Primative<byte>().SetPadding(4).Read(reader, null, "");
			}

			temp = new SR1_PointerArray<MonsterSubAttributes>(numSubAttributes.Value, true, 4).ReadFromPointer(reader, subAttributesList);

			// Unknown byte plus padding. Always 0xD1
			if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
			{
				reader.BaseStream.Position = temp.End;
				new SR1_Primative<byte>().SetPadding(4).Read(reader, null, "");
			}

			temp = new SR1_PointerArray<MonsterCombatAttributes>(numCombatAttributes.Value, true).ReadFromPointer(reader, combatAttributesList);

			// Unknown byte plus padding. Always 0xD1
			if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
			{
				reader.BaseStream.Position = temp.End;
				new SR1_Primative<byte>().SetPadding(4).Read(reader, null, "");
			}

			new SR1_StructureArray<MonsterAttackAttributes>(numAttackAttributes.Value).ReadFromPointer(reader, attackAttributesList);
			new SR1_StructureArray<MonsterMissile>(numMissiles.Value).ReadFromPointer(reader, missileList);
			new SR1_StructureArray<MonsterAnim>(numAnims.Value).ReadFromPointer(reader, animList);
			new SR1_StructureArray<MonsterIdle>(numIdles.Value).ReadFromPointer(reader, idleList);

			int realNumBehaviors = (numBehaviors.Value > 0) ? (numBehaviors.Value - 1) : 0;
			if (reader.File._Version >= SR1_File.Version.Feb16)
			{
				if (reader.Object.Name == "wrshp___")
				{
					realNumBehaviors -= 1;
				}
				else if (reader.Object.Name == "soul____")
				{
					realNumBehaviors = 1;
				}
			}
			new SR1_StructureArray<MonsterBehavior>(realNumBehaviors).ReadFromPointer(reader, behaviorList);

			new SR1_StructureArray<FXSplinter>(numShatters.Value).ReadFromPointer(reader, shatterList);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			magicnum.Write(writer);
			tunData.Write(writer);
			auxAnimList.Write(writer);
			ambientAnimList.Write(writer);
			whatAmI.Write(writer);
			numAuxAnims.Write(writer);
			numAmbientAnims.Write(writer);
			defaultAge.Write(writer);
			pupateObject.Write(writer);
			damageFXSegment.Write(writer);
			headSegment.Write(writer);
			neckSegment.Write(writer);
			leftShoulderSegment.Write(writer);
			rightShoulderSegment.Write(writer);
			waistSegment.Write(writer);
			leftKneeSegment.Write(writer);
			rightKneeSegment.Write(writer);
			leftFootSegment.Write(writer);
			rightFootSegment.Write(writer);
			spineSegment.Write(writer);
			leftWeaponSegment.Write(writer);
			rightWeaponSegment.Write(writer);
			grabSegment.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Next);
			bloodImpaleFrame.Write(writer);
			bloodConeFrame.Write(writer);
			bruiseRed.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			bruiseGreen.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			bruiseBlue.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Jun01);
			numSubAttributes.Write(writer);
			numCombatAttributes.Write(writer);
			numAttackAttributes.Write(writer);
			numMissiles.Write(writer);
			numAnims.Write(writer);
			numIdles.Write(writer);
			numBehaviors.Write(writer);
			numShatters.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			pad.Write(writer);
			subAttributesList.Write(writer);
			combatAttributesList.Write(writer);
			attackAttributesList.Write(writer);
			missileList.Write(writer);
			animList.Write(writer);
			idleList.Write(writer);
			behaviorList.Write(writer);
			shatterList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				magicnum.Value = 0xACE00065;

				// For the priestess.
				if (file._Structures[0].Name == "priests_")
                {
					// Not needed?
					//if (tunData.Offset != 0)
					//{
					//	file._Structures.Remove(tunData.Offset);
					//	tunData.Offset = 0;
					//}

					//whatAmI.Value = 66; // Not needed?

                    numBehaviors.Value = 2;

                    MonsterBehavior newBehaviors = new MonsterBehavior();
                    newBehaviors.alertness.Value = 3;
                    newBehaviors.idleFreq.Value = 0;
                    newBehaviors.numIdles.Value = 0;
                    newBehaviors.idleList[0] = -1;
                    newBehaviors.idleList[1] = -1;
                    newBehaviors.idleList[2] = -1;
                    newBehaviors.idleList[3] = -1;
                    newBehaviors.idleList[4] = -1;

					SR1_Structure lastStructure = file._Structures.Values[file._Structures.Count - 1];
					uint position = lastStructure.End;

					file._MigrationStructures.Add(position, newBehaviors);
					behaviorList.Offset = position;
                }
			}
		}
	}
}
