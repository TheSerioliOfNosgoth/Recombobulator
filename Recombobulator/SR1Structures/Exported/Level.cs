using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Level : SR1_Structure
	{
		// Use      //.*\r?\n     to search and remove comments.

		public readonly SR1_Pointer<Terrain> terrain = new SR1_Pointer<Terrain>();
		public readonly SR1_Pointer<LightList> lightList = new SR1_Pointer<LightList>();
		public readonly SR1_Primative<int> numVMObjects = new SR1_Primative<int>();
		public readonly SR1_Pointer<VMObject> vmobjectList = new SR1_Pointer<VMObject>();
		public readonly SR1_Primative<int> numSpotLights = new SR1_Primative<int>();
		public readonly SR1_Pointer<SpotLight> spotLightList = new SR1_Pointer<SpotLight>();
		public readonly SR1_Primative<int> numPointLights = new SR1_Primative<int>();
		public readonly SR1_Pointer<PointLight> pointLightList = new SR1_Pointer<PointLight>();
		public readonly SR1_Primative<int> numSpotSpecturalLights = new SR1_Primative<int>();
		public readonly SR1_Pointer<SpotLight> spotSpecturalLightList = new SR1_Pointer<SpotLight>();
		public readonly SR1_Primative<int> numPointSpecturalLights = new SR1_Primative<int>();
		public readonly SR1_Pointer<PointLight> pointSpecturalLightList = new SR1_Pointer<PointLight>();
		public readonly SR1_Primative<int> numBGObjects = new SR1_Primative<int>();
		public readonly SR1_Pointer<BGObject> bgObjectList = new SR1_Pointer<BGObject>();
		public readonly SR1_Primative<int> waterZLevel = new SR1_Primative<int>();
		public readonly SR1_Primative<byte> backColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> backColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> backColorB = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> cpad1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> specturalColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> specturalColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> specturalColorB = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> cpad2 = new SR1_Primative<byte>();
		public readonly SR1_Primative<ushort> fogFar = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> fogNear = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> spectralFogFar = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> spectralFogNear = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> waterFogFar = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> waterFogNear = new SR1_Primative<ushort>();
		public readonly SR1_Primative<int> depthQBlendStart = new SR1_Primative<int>();
		public readonly SR1_Pointer<LightList> spectrallightList = new SR1_Pointer<LightList>();
		public readonly SR1_PrimativePointer<short> depthQPTable = new SR1_PrimativePointer<short>();
		public readonly SR1_Primative<int> numCameras = new SR1_Primative<int>();
		public readonly SR1_Pointer<CameraKey> cameraList = new SR1_Pointer<CameraKey>(); // void in sym, but really a CameraKey
		public readonly SR1_Primative<int> bspPlaneError = new SR1_Primative<int>();
		public readonly SR1_Primative<int> numVGroups = new SR1_Primative<int>();
		public readonly SR1_Pointer<VGroup> vGroupList = new SR1_Pointer<VGroup>();
		public readonly SR1_Primative<int> deathZ = new SR1_Primative<int>();
		public readonly SR1_Primative<int> flags = new SR1_Primative<int>();
		public readonly SR1_Pointer<MultiSignal> startSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Primative<int> numIntros = new SR1_Primative<int>();
		public readonly SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>();
		public readonly SR1_Pointer<DrMoveAniTex> bgAniList = new SR1_Pointer<DrMoveAniTex>();
		public readonly SR1_Primative<int> numHotSpots = new SR1_Primative<int>();
		public readonly SR1_Pointer<HotSpot> hotSpotList = new SR1_Pointer<HotSpot>();
		public readonly SR1_Pointer<ObjectNameList> objectNameList = new SR1_Pointer<ObjectNameList>();
		public readonly SR1_Primative<int> depthQFogStart = new SR1_Primative<int>();
		public readonly SR1_Primative<int> morphLastStep = new SR1_Primative<int>();
		public readonly SR1_PrimativePointer<char> worldName = new SR1_PrimativePointer<char>();
		public readonly VramSize vramSize = new VramSize();
		public readonly SR1_Primative<int> numberBrotherUnits = new SR1_Primative<int>();
		public readonly SR1_PrimativePointer<char> brotherUnitsNames = new SR1_PrimativePointer<char>();
		public readonly SR1_Primative<ushort> holdFogFar = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> holdFogNear = new SR1_Primative<ushort>();
		public readonly SR1_Primative<int> numberEnemyUnits = new SR1_Primative<int>();
		public readonly SR1_PrimativePointer<char> enemyUnitsNames = new SR1_PrimativePointer<char>();
		public readonly SR1_PrimativePointer<int> timesSignalList = new SR1_PrimativePointer<int>();
		public readonly SR1_Pointer<MultiSignal> spectralSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> materialSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> startUnitLoadedSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> startUnitMainSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> startGoingIntoWaterSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> startGoingOutOfWaterSignal = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Primative<int> unitFlags = new SR1_Primative<int>();
		public readonly SR1_Pointer<MultiSignal> SignalListStart = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Pointer<MultiSignal> SignalListEnd = new SR1_Pointer<MultiSignal>();
		public readonly SR1_Primative<int> numBSPLeaves = new SR1_Primative<int>();
		public readonly SR1_Pointer<EventPointers> PuzzleInstances = new SR1_Pointer<EventPointers>();
		public readonly SR1_Primative<int> NumberOfPlanMarkers = new SR1_Primative<int>();
		public readonly SR1_Pointer<PlanMkr> PlanMarkerList = new SR1_Pointer<PlanMkr>();
		public readonly SR1_Primative<int> NumberOfSFXMarkers = new SR1_Primative<int>();
		public readonly SR1_Pointer<SFXMkr> SFXMarkerList = new SR1_Pointer<SFXMkr>();
		public readonly SR1_Primative<uint> versionNumber = new SR1_Primative<uint>().ShowAsHex(true);
		public readonly SR1_PrimativePointer<char> dynamicMusicName = new SR1_PrimativePointer<char>();
		public readonly SR1_Primative<int> streamUnitID = new SR1_Primative<int>();
		public readonly SR1_StructureArray<TClassAttr> tClassAttr = new SR1_StructureArray<TClassAttr>(16);
		public readonly SR1_PrimativeArray<short> TODLighting = new SR1_PrimativeArray<short>(4, 3);
		public readonly SR1_PrimativeArray<byte> TODbackColor = new SR1_PrimativeArray<byte>(4, 3);
		public readonly SR1_PrimativeArray<ushort> TODfogNearFar = new SR1_PrimativeArray<ushort>(4, 2);
		public readonly SR1_Primative<short> TODRedScale = new SR1_Primative<short>();
		public readonly SR1_Primative<short> TODGrnScale = new SR1_Primative<short>();
		public readonly SR1_Primative<short> TODBluScale = new SR1_Primative<short>();
		public readonly SR1_Primative<short> TODfogNear = new SR1_Primative<short>();
		public readonly SR1_Primative<short> TODfogFar = new SR1_Primative<short>();
		public readonly SR1_Primative<short> short_pad = new SR1_Primative<short>();
		public readonly SR1_Primative<byte> TODbackColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> TODbackColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> TODbackColorB = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> cpad3 = new SR1_Primative<byte>();
		public readonly SR1_Pointer<LightGroup> razielLightGroup = new SR1_Pointer<LightGroup>();
		public readonly SR1_Pointer<LightGroup> razielSpectralLightGroup = new SR1_Pointer<LightGroup>();

		SR1_String worldNameString = new SR1_String(12);
		LightList lightListStruct0 = new LightList();
		LightList lightListStruct1 = new LightList();
		SR1_PrimativeArray<byte> push6Padding = new SR1_PrimativeArray<byte>(8);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			reader.Level = this;

			terrain.Read(reader, this, "terrain");
			lightList.Read(reader, this, "lightList");
			numVMObjects.Read(reader, this, "numVMObjects");
			vmobjectList.Read(reader, this, "vmobjectList");
			numSpotLights.Read(reader, this, "numSpotLights");
			spotLightList.Read(reader, this, "spotLightList");
			numPointLights.Read(reader, this, "numPointLights");
			pointLightList.Read(reader, this, "pointLightList");
			numSpotSpecturalLights.Read(reader, this, "numSpotSpecturalLights");
			spotSpecturalLightList.Read(reader, this, "spotSpecturalLightList");
			numPointSpecturalLights.Read(reader, this, "numPointSpecturalLights");
			pointSpecturalLightList.Read(reader, this, "pointSpecturalLightList");
			numBGObjects.Read(reader, this, "numBGObjects");
			bgObjectList.Read(reader, this, "bgObjectList");
			waterZLevel.Read(reader, this, "waterZLevel");
			backColorR.Read(reader, this, "backColorR");
			backColorG.Read(reader, this, "backColorG");
			backColorB.Read(reader, this, "backColorB");
			cpad1.Read(reader, this, "cpad1");
			specturalColorR.Read(reader, this, "specturalColorR");
			specturalColorG.Read(reader, this, "specturalColorG");
			specturalColorB.Read(reader, this, "specturalColorB");
			cpad2.Read(reader, this, "cpad2");
			fogFar.Read(reader, this, "fogFar");
			fogNear.Read(reader, this, "fogNear");
			spectralFogFar.Read(reader, this, "spectralFogFar");
			spectralFogNear.Read(reader, this, "spectralFogNear");
			waterFogFar.Read(reader, this, "waterFogFar", SR1_File.Version.May12, SR1_File.Version.Next);
			waterFogNear.Read(reader, this, "waterFogNear", SR1_File.Version.May12, SR1_File.Version.Next);
			depthQBlendStart.Read(reader, this, "depthQBlendStart");
			spectrallightList.Read(reader, this, "spectrallightList", SR1_File.Version.May12, SR1_File.Version.Next);
			depthQPTable.Read(reader, this, "depthQPTable", SR1_File.Version.Feb16, SR1_File.Version.May12);
			numCameras.Read(reader, this, "numCameras");
			cameraList.Read(reader, this, "cameraList");
			bspPlaneError.Read(reader, this, "bspPlaneError");
			numVGroups.Read(reader, this, "numVGroups");
			vGroupList.Read(reader, this, "vGroupList");
			deathZ.Read(reader, this, "deathZ");
			flags.Read(reader, this, "flags");
			startSignal.Read(reader, this, "startSignal");
			numIntros.Read(reader, this, "numIntros");
			introList.Read(reader, this, "introList");
			bgAniList.Read(reader, this, "bgAniList");
			numHotSpots.Read(reader, this, "numHotSpots");
			hotSpotList.Read(reader, this, "hotSpotList");
			objectNameList.Read(reader, this, "objectNameList");
			depthQFogStart.Read(reader, this, "depthQFogStart");
			morphLastStep.Read(reader, this, "morphLastStep");
			worldName.Read(reader, this, "worldName");
			vramSize.Read(reader, this, "vramSize", SR1_File.Version.May12, SR1_File.Version.Next);
			numberBrotherUnits.Read(reader, this, "numberBrotherUnits", SR1_File.Version.Feb16, SR1_File.Version.May12);
			brotherUnitsNames.Read(reader, this, "brotherUnitsNames", SR1_File.Version.Feb16, SR1_File.Version.May12);
			holdFogFar.Read(reader, this, "holdFogFar");
			holdFogNear.Read(reader, this, "holdFogNear");
			numberEnemyUnits.Read(reader, this, "numberEnemyUnits");
			enemyUnitsNames.Read(reader, this, "enemyUnitsNames");
			timesSignalList.Read(reader, this, "timesSignalList");
			spectralSignal.Read(reader, this, "spectralSignal");
			materialSignal.Read(reader, this, "materialSignal");
			startUnitLoadedSignal.Read(reader, this, "startUnitLoadedSignal");
			startUnitMainSignal.Read(reader, this, "startUnitMainSignal");
			startGoingIntoWaterSignal.Read(reader, this, "startGoingIntoWaterSignal");
			startGoingOutOfWaterSignal.Read(reader, this, "startGoingOutOfWaterSignal");
			unitFlags.Read(reader, this, "unitFlags");
			SignalListStart.Read(reader, this, "SignalListStart");
			SignalListEnd.Read(reader, this, "SignalListEnd");
			numBSPLeaves.Read(reader, this, "numBSPLeaves");
			PuzzleInstances.Read(reader, this, "PuzzleInstances");
			NumberOfPlanMarkers.Read(reader, this, "NumberOfPlanMarkers");
			PlanMarkerList.Read(reader, this, "PlanMarkerList");
			NumberOfSFXMarkers.Read(reader, this, "NumberOfSFXMarkers", SR1_File.Version.May12, SR1_File.Version.Next);
			SFXMarkerList.Read(reader, this, "SFXMarkerList", SR1_File.Version.May12, SR1_File.Version.Next);
			versionNumber.Read(reader, this, "versionNumber");
			dynamicMusicName.Read(reader, this, "dynamicMusicName");
			streamUnitID.Read(reader, this, "streamUnitID");
			tClassAttr.Read(reader, this, "tClassAttr");
			TODLighting.Read(reader, this, "TODLighting");
			TODbackColor.Read(reader, this, "TODbackColor");
			TODfogNearFar.Read(reader, this, "TODfogNearFar");
			TODRedScale.Read(reader, this, "TODRedScale");
			TODGrnScale.Read(reader, this, "TODGrnScale");
			TODBluScale.Read(reader, this, "TODBluScale");
			TODfogNear.Read(reader, this, "TODfogNear");
			TODfogFar.Read(reader, this, "TODfogFar");
			short_pad.Read(reader, this, "short_pad");
			TODbackColorR.Read(reader, this, "TODbackColorR");
			TODbackColorG.Read(reader, this, "TODbackColorG");
			TODbackColorB.Read(reader, this, "TODbackColorB");
			cpad3.Read(reader, this, "cpad3");
			razielLightGroup.Read(reader, this, "razielLightGroup", SR1_File.Version.May12, SR1_File.Version.Next);
			razielSpectralLightGroup.Read(reader, this, "razielSpectralLightGroup", SR1_File.Version.May12, SR1_File.Version.Next);

			reader.PlanMarkerList = PlanMarkerList;
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			worldNameString.SetPadding(4).ReadFromPointer(reader, worldName);
			Name = worldNameString.ToString();

			SR1_Structure terrainStruct = new Terrain().ReadFromPointer(reader, terrain);

			lightListStruct0.ReadFromPointer(reader, lightList);
			if (reader.File._Version == SR1_File.Version.Feb16 &&
				lightListStruct0.End != 0 && lightListStruct0.End != depthQPTable.Offset)
			{
				if (Name == "push6")
				{
					push6Padding.ReadOrphan(reader, lightListStruct0.End);
				}
				else
				{
					// Got lucky with depthQPTable always being there. Could use a better solution.
					lightListStruct1.SetSkipAmbient(true);
					lightListStruct1.ReadOrphan(reader, lightListStruct0.End);
				}
			}

			new VMObjectList(numVMObjects.Value).ReadFromPointer(reader, vmobjectList);
			if (reader.File._Version <= SR1_File.Version.May12)
			{
				new SR1_StructureArray<SpotLight>(numSpotLights.Value).ReadFromPointer(reader, spotLightList);
			}
			new LightList().ReadFromPointer(reader, spectrallightList);

			if (reader.File._Version == SR1_File.Version.Feb16)
			{
				// depthQPTable is length 3464 discovered in function LIGHT_CalcDQPTable
				/*int iVar6 = -((int)(fogFar.Value * fogNear.Value) / (int)(fogFar.Value - fogNear.Value));
                if (0x9ffe < iVar6)
                {
                    iVar6 = 0x9ffe;
                }
                if (iVar6 < -0x9ffe)
                {
                    iVar6 = -0x9ffe;
                }
                int iVar8 = (int)((uint)fogFar.Value << 0xc) / (int)((uint)fogFar.Value - (uint)fogNear.Value);
                int iVar9 = (iVar6 * -0x1000) / iVar8;
                int iVar10 = (iVar6 * 0x1000) / (0x1000 - iVar8);
                int iVar11 = 0x3620;
                int iVar12 = iVar11 / 4;*/
				new SR1_PrimativeArray<short>(3464).ReadFromPointer(reader, depthQPTable);
			}

			new SR1_StructureArray<CameraKey>(numCameras.Value).ReadFromPointer(reader, cameraList);
			new SR1_StructureArray<VGroup>(numVGroups.Value).ReadFromPointer(reader, vGroupList);
			new SR1_StructureArray<Intro>(numIntros.Value).ReadFromPointer(reader, introList);
			SR1_Structure objectNameListStruct = new ObjectNameList().ReadFromPointer(reader, objectNameList);
			new SR1_StructureSeries<MultiSignal>((int)(SignalListEnd.Offset - SignalListStart.Offset)).ReadFromPointer(reader, SignalListStart);
			new SR1_StructureArray<PlanMkr>(NumberOfPlanMarkers.Value).SetPadding(4).ReadFromPointer(reader, PlanMarkerList);
			new SR1_StructureArray<SFXMkr>(NumberOfSFXMarkers.Value).ReadFromPointer(reader, SFXMarkerList);
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				new SR1_String(4).SetPadding(4).ReadFromPointer(reader, dynamicMusicName);
			}
			else if (reader.File._Version == SR1_File.Version.Feb16 &&
				dynamicMusicName.Offset != 0 && dynamicMusicName.Offset < cpad3.End)
			{
				uint adjustLength = cpad3.End - dynamicMusicName.Offset;
				new SR1_String(12 - (int)adjustLength).SetPadding(4).ReadOrphan(reader, cpad3.End);
			}
			else
			{
				new SR1_String(12).SetPadding(4).ReadFromPointer(reader, dynamicMusicName);
			}
			new LightGroup().ReadFromPointer(reader, razielLightGroup);
			new LightGroup().ReadFromPointer(reader, razielSpectralLightGroup);

			Events events = null;
			if (PuzzleInstances.Offset != 0)
			{
				reader.BaseStream.Position = PuzzleInstances.Offset;
				EventPointers tempEventPointers = new EventPointers();
				tempEventPointers.ReadTemp(reader);
				uint firstEvent = uint.MaxValue;
				foreach (SR1_PointerBase eventInstance in tempEventPointers.eventInstances)
				{
					if (eventInstance.Offset != 0 && eventInstance.Offset < firstEvent)
					{
						firstEvent = eventInstance.Offset;
					}
				}

				if (firstEvent != 0 && firstEvent != uint.MaxValue)
				{
					reader.BaseStream.Position = firstEvent;
					events = new Events(tempEventPointers.numPuzzles.Value);
					events.Read(reader, null, "");
					reader.Events = events;
				}
			}

			if (terrainStruct.End != 0)
			{
				foreach (SR1_Pointer<SFXFileData> pointer in reader.SFXDictionary.Values)
				{
					SR1_Structure sfxFileDataList = new SFXFileDataList().ReadFromPointer(reader, pointer);
					if (!reader.File._Structures.ContainsKey(sfxFileDataList.End))
					{
						reader.BaseStream.Position = sfxFileDataList.End;
						new SR1_PrimativeArray<byte>((int)(terrainStruct.Start - sfxFileDataList.End)).Read(reader, null, "");
					}
					break;
				}
			}

			// 8 mystery bytes after events. Mirror?
			if (events != null && !reader.File._Structures.ContainsKey(events.End))
			{
				reader.BaseStream.Position = events.End;
				new Mirror().Read(reader, null, "");
			}

			reader.BaseStream.Position = objectNameListStruct.Start - 1;
			while (!reader.File._Structures.ContainsKey((uint)reader.BaseStream.Position))
			{
				reader.BaseStream.Position--;
			}
			if (!reader.File._Structures.ContainsKey(reader.File._Structures[(uint)reader.BaseStream.Position].End))
			{
				reader.BaseStream.Position = reader.File._Structures[(uint)reader.BaseStream.Position].End;
				int length = (int)objectNameListStruct.Start - (int)reader.BaseStream.Position;
				new SR1_PrimativeArray<byte>(length).Read(reader, null, "");
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			terrain.Write(writer);
			lightList.Write(writer);
			numVMObjects.Write(writer);
			vmobjectList.Write(writer);
			numSpotLights.Write(writer);
			spotLightList.Write(writer);
			numPointLights.Write(writer);
			pointLightList.Write(writer);
			numSpotSpecturalLights.Write(writer);
			spotSpecturalLightList.Write(writer);
			numPointSpecturalLights.Write(writer);
			pointSpecturalLightList.Write(writer);
			numBGObjects.Write(writer);
			bgObjectList.Write(writer);
			waterZLevel.Write(writer);
			backColorR.Write(writer);
			backColorG.Write(writer);
			backColorB.Write(writer);
			cpad1.Write(writer);
			specturalColorR.Write(writer);
			specturalColorG.Write(writer);
			specturalColorB.Write(writer);
			cpad2.Write(writer);
			fogFar.Write(writer);
			fogNear.Write(writer);
			spectralFogFar.Write(writer);
			spectralFogNear.Write(writer);
			waterFogFar.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			waterFogNear.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			depthQBlendStart.Write(writer);
			spectrallightList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			depthQPTable.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.May12);
			numCameras.Write(writer);
			cameraList.Write(writer);
			bspPlaneError.Write(writer);
			numVGroups.Write(writer);
			vGroupList.Write(writer);
			deathZ.Write(writer);
			flags.Write(writer);
			startSignal.Write(writer);
			numIntros.Write(writer);
			introList.Write(writer);
			bgAniList.Write(writer);
			numHotSpots.Write(writer);
			hotSpotList.Write(writer);
			objectNameList.Write(writer);
			depthQFogStart.Write(writer);
			morphLastStep.Write(writer);
			worldName.Write(writer);
			vramSize.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			numberBrotherUnits.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.May12);
			brotherUnitsNames.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.May12);
			holdFogFar.Write(writer);
			holdFogNear.Write(writer);
			numberEnemyUnits.Write(writer);
			enemyUnitsNames.Write(writer);
			timesSignalList.Write(writer);
			spectralSignal.Write(writer);
			materialSignal.Write(writer);
			startUnitLoadedSignal.Write(writer);
			startUnitMainSignal.Write(writer);
			startGoingIntoWaterSignal.Write(writer);
			startGoingOutOfWaterSignal.Write(writer);
			unitFlags.Write(writer);
			SignalListStart.Write(writer);
			SignalListEnd.Write(writer);
			numBSPLeaves.Write(writer);
			PuzzleInstances.Write(writer);
			NumberOfPlanMarkers.Write(writer);
			PlanMarkerList.Write(writer);
			NumberOfSFXMarkers.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			SFXMarkerList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			versionNumber.Write(writer);
			dynamicMusicName.Write(writer);
			streamUnitID.Write(writer);
			tClassAttr.Write(writer);
			TODLighting.Write(writer);
			TODbackColor.Write(writer);
			TODfogNearFar.Write(writer);
			TODRedScale.Write(writer);
			TODGrnScale.Write(writer);
			TODBluScale.Write(writer);
			TODfogNear.Write(writer);
			TODfogFar.Write(writer);
			short_pad.Write(writer);
			TODbackColorR.Write(writer);
			TODbackColorG.Write(writer);
			TODbackColorB.Write(writer);
			cpad3.Write(writer);
			razielLightGroup.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			razielSpectralLightGroup.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version != targetVersion)
			{
				if (file._Overrides.NewName != null)
				{
					worldNameString.SetText(file._Overrides.NewName, 12);
				}

				if (file._Overrides.NewStreamUnitID != 0)
				{
					streamUnitID.Value = file._Overrides.NewStreamUnitID;
				}
			}

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				versionNumber.Value = SR1_File.RETAIL_VERSION;

				fogFar.Value = 20000;
				fogNear.Value = 19999;
				spectralFogFar.Value = 20000;
				spectralFogNear.Value = 19999;
				waterFogFar.Value = 20000;
				waterFogNear.Value = 19999;
				vramSize.x.Value = -1;
				vramSize.y.Value = -1;
				vramSize.w.Value = -1;
				vramSize.h.Value = -1;
				holdFogFar.Value = 20000;
				holdFogNear.Value = 19999;
				TODfogNearFar[0] = 19999;
				TODfogNearFar[1] = 20000;
				TODfogNearFar[2] = 19999;
				TODfogNearFar[3] = 20000;
				TODfogNearFar[4] = 19999;
				TODfogNearFar[5] = 20000;
				TODfogNearFar[6] = 19999;
				TODfogNearFar[7] = 20000;

				if (SFXMarkerList.Offset != 0)
				{
					file._Structures.Remove(SFXMarkerList.Offset);
					NumberOfSFXMarkers.Value = 0;
					SFXMarkerList.Offset = 0;
				}

				if (lightListStruct1.Start != 0)
				{
					file._Structures.Remove(lightListStruct1.Start);
				}

				if (push6Padding.Start != 0)
				{
					file._Structures.Remove(push6Padding.Start);
				}

				if (depthQPTable.Offset != 0)
				{
					file._Structures.Remove(depthQPTable.Offset);
				}

				if (bgAniList.Offset != 0)
				{
					file._Structures.Remove(bgAniList.Offset);
					bgAniList.Offset = 0;
				}

				if ((migrateFlags & SR1_File.MigrateFlags.RemoveEvents) != 0)
				{
					if (PuzzleInstances.Offset != 0)
					{
						file._Structures.Remove(PuzzleInstances.Offset);
						PuzzleInstances.Offset = 0;
					}
				}

				if ((migrateFlags & SR1_File.MigrateFlags.RemoveVertexMorphs) != 0)
				{
					numVMObjects.Value = 0;
					if (vmobjectList.Offset != 0)
					{
						file._Structures.Remove(vmobjectList.Offset);
						vmobjectList.Offset = 0;
					}
				}
			}
		}
	}
}
