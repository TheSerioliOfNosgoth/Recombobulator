using CDC;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class Level : SR1_Structure
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
		public readonly SVector gexStartPosition = new SVector();
		public readonly SR1_Primative<int> collectableCount0 = new SR1_Primative<int>();
		public readonly SR1_Primative<int> collectableCount1 = new SR1_Primative<int>();
		public readonly SR1_Primative<int> collectableCount2 = new SR1_Primative<int>();
		public readonly SR1_Primative<byte> farColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> farColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> farColorB = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> cpad0 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> backColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> backColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> backColorB = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> cpad1 = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> objectFarColorR = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> objectFarColorG = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> objectFarColorB = new SR1_Primative<byte>();
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
		public readonly SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>(PtrHeuristic.Start);
		public readonly SR1_Pointer<DrMoveAniTex> bgAniList = new SR1_Pointer<DrMoveAniTex>();
		public readonly SR1_Primative<int> numHotSpots = new SR1_Primative<int>();
		public readonly SR1_Pointer<HotSpot> hotSpotList = new SR1_Pointer<HotSpot>();
		public readonly SR1_Pointer<ObjectNameList> objectNameList = new SR1_Pointer<ObjectNameList>();
		public readonly SR1_Primative<int> vramBlock = new SR1_Primative<int>();
		public readonly SR1_Primative<int> vramPad = new SR1_Primative<int>();
		public readonly SR1_Primative<int> depthQFogStart = new SR1_Primative<int>();
		public readonly SR1_Primative<int> morphLastStep = new SR1_Primative<int>();
		public readonly SR1_PrimativePointer<char> worldName = new SR1_PrimativePointer<char>();
		public readonly VramSize vramSize = new VramSize();
		public readonly SR1_Primative<int> numberBrotherUnits = new SR1_Primative<int>();
		public readonly SR1_PrimativePointer<char> brotherUnitsNames = new SR1_PrimativePointer<char>();
		public readonly SR1_PrimativePointer<object> unknown0 = new SR1_PrimativePointer<object>();
		public readonly SR1_PrimativePointer<object> currentCollectable = new SR1_PrimativePointer<object>();
		public readonly SR1_PrimativePointer<object> unknown1 = new SR1_PrimativePointer<object>();
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
		public readonly SR1_Pointer<MultiSignal> SignalListStart = new SR1_Pointer<MultiSignal>(PtrHeuristic.Start);
		public readonly SR1_Pointer<MultiSignal> SignalListEnd = new SR1_Pointer<MultiSignal>(PtrHeuristic.End);
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
		Events events = new Events(0);
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
			waterZLevel.Read(reader, this, "waterZLevel", SR1_File.Version.Jan23, SR1_File.Version.Next);
			gexStartPosition.Read(reader, this, "gexStartPosition", SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount0.Read(reader, this, "collectableCount0", SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount1.Read(reader, this, "collectableCount1", SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount2.Read(reader, this, "collectableCount2", SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorR.Read(reader, this, "farColorR", SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorG.Read(reader, this, "farColorG", SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorB.Read(reader, this, "farColorB", SR1_File.Version.First, SR1_File.Version.Jan23);
			cpad0.Read(reader, this, "cpad0", SR1_File.Version.First, SR1_File.Version.Jan23);
			backColorR.Read(reader, this, "backColorR");
			backColorG.Read(reader, this, "backColorG");
			backColorB.Read(reader, this, "backColorB");
			cpad1.Read(reader, this, "cpad1");
			objectFarColorR.Read(reader, this, "objectFarColorR", SR1_File.Version.First, SR1_File.Version.Jan23);
			objectFarColorG.Read(reader, this, "objectFarColorG", SR1_File.Version.First, SR1_File.Version.Jan23);
			objectFarColorB.Read(reader, this, "objectFarColorB", SR1_File.Version.First, SR1_File.Version.Jan23);
			specturalColorR.Read(reader, this, "specturalColorR", SR1_File.Version.Jan23, SR1_File.Version.Next);
			specturalColorG.Read(reader, this, "specturalColorG", SR1_File.Version.Jan23, SR1_File.Version.Next);
			specturalColorB.Read(reader, this, "specturalColorB", SR1_File.Version.Jan23, SR1_File.Version.Next);
			cpad2.Read(reader, this, "cpad2");
			fogFar.Read(reader, this, "fogFar");
			fogNear.Read(reader, this, "fogNear");
			spectralFogFar.Read(reader, this, "spectralFogFar", SR1_File.Version.Feb16, SR1_File.Version.Next);
			spectralFogNear.Read(reader, this, "spectralFogNear", SR1_File.Version.Feb16, SR1_File.Version.Next);
			waterFogFar.Read(reader, this, "waterFogFar", SR1_File.Version.Apr14, SR1_File.Version.Next);
			waterFogNear.Read(reader, this, "waterFogNear", SR1_File.Version.Apr14, SR1_File.Version.Next);
			depthQBlendStart.Read(reader, this, "depthQBlendStart");
			spectrallightList.Read(reader, this, "spectrallightList", SR1_File.Version.Apr14, SR1_File.Version.Next);
			depthQPTable.Read(reader, this, "depthQPTable", SR1_File.Version.First, SR1_File.Version.Apr14);
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
			vramBlock.Read(reader, this, "vramBlock", SR1_File.Version.First, SR1_File.Version.Jan23);
			vramPad.Read(reader, this, "vramPad", SR1_File.Version.First, SR1_File.Version.Jan23);
			depthQFogStart.Read(reader, this, "depthQFogStart", SR1_File.Version.Jan23, SR1_File.Version.Next);
			morphLastStep.Read(reader, this, "morphLastStep", SR1_File.Version.Jan23, SR1_File.Version.Next);
			worldName.Read(reader, this, "worldName");
			vramSize.Read(reader, this, "vramSize", SR1_File.Version.Apr14, SR1_File.Version.Next);
			numberBrotherUnits.Read(reader, this, "numberBrotherUnits", SR1_File.Version.First, SR1_File.Version.Apr14);
			brotherUnitsNames.Read(reader, this, "brotherUnitsNames", SR1_File.Version.First, SR1_File.Version.Apr14);
			unknown0.Read(reader, this, "unknown0", SR1_File.Version.First, SR1_File.Version.Jan23);
			currentCollectable.Read(reader, this, "currentCollectable", SR1_File.Version.First, SR1_File.Version.Jan23);
			unknown1.Read(reader, this, "unknown1", SR1_File.Version.First, SR1_File.Version.Jan23);
			holdFogFar.Read(reader, this, "holdFogFar", SR1_File.Version.Jan23, SR1_File.Version.Next);
			holdFogNear.Read(reader, this, "holdFogNear", SR1_File.Version.Jan23, SR1_File.Version.Next);
			numberEnemyUnits.Read(reader, this, "numberEnemyUnits", SR1_File.Version.Jan23, SR1_File.Version.Next);
			enemyUnitsNames.Read(reader, this, "enemyUnitsNames", SR1_File.Version.Jan23, SR1_File.Version.Next);
			timesSignalList.Read(reader, this, "timesSignalList");
			spectralSignal.Read(reader, this, "spectralSignal");
			materialSignal.Read(reader, this, "materialSignal");
			startUnitLoadedSignal.Read(reader, this, "startUnitLoadedSignal");
			startUnitMainSignal.Read(reader, this, "startUnitMainSignal");
			startGoingIntoWaterSignal.Read(reader, this, "startGoingIntoWaterSignal", SR1_File.Version.Jan23, SR1_File.Version.Next);
			startGoingOutOfWaterSignal.Read(reader, this, "startGoingOutOfWaterSignal", SR1_File.Version.Jan23, SR1_File.Version.Next);
			unitFlags.Read(reader, this, "unitFlags");
			SignalListStart.Read(reader, this, "SignalListStart");
			SignalListEnd.Read(reader, this, "SignalListEnd");
			numBSPLeaves.Read(reader, this, "numBSPLeaves");
			PuzzleInstances.Read(reader, this, "PuzzleInstances", SR1_File.Version.Jan23, SR1_File.Version.Next);
			NumberOfPlanMarkers.Read(reader, this, "NumberOfPlanMarkers", SR1_File.Version.Jan23, SR1_File.Version.Next);
			PlanMarkerList.Read(reader, this, "PlanMarkerList", SR1_File.Version.Jan23, SR1_File.Version.Next);
			NumberOfSFXMarkers.Read(reader, this, "NumberOfSFXMarkers", SR1_File.Version.May12, SR1_File.Version.Next);
			SFXMarkerList.Read(reader, this, "SFXMarkerList", SR1_File.Version.May12, SR1_File.Version.Next);
			versionNumber.Read(reader, this, "versionNumber", SR1_File.Version.Jan23, SR1_File.Version.Next);
			dynamicMusicName.Read(reader, this, "dynamicMusicName", SR1_File.Version.Jan23, SR1_File.Version.Next);
			streamUnitID.Read(reader, this, "streamUnitID", SR1_File.Version.Jan23, SR1_File.Version.Next);
			tClassAttr.Read(reader, this, "tClassAttr", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODLighting.Read(reader, this, "TODLighting", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColor.Read(reader, this, "TODbackColor", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogNearFar.Read(reader, this, "TODfogNearFar", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODRedScale.Read(reader, this, "TODRedScale", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODGrnScale.Read(reader, this, "TODGrnScale", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODBluScale.Read(reader, this, "TODBluScale", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogNear.Read(reader, this, "TODfogNear", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogFar.Read(reader, this, "TODfogFar", SR1_File.Version.Jan23, SR1_File.Version.Next);
			short_pad.Read(reader, this, "short_pad", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorR.Read(reader, this, "TODbackColorR", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorG.Read(reader, this, "TODbackColorG", SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorB.Read(reader, this, "TODbackColorB", SR1_File.Version.Jan23, SR1_File.Version.Next);
			cpad3.Read(reader, this, "cpad3", SR1_File.Version.Jan23, SR1_File.Version.Next);
			razielLightGroup.Read(reader, this, "razielLightGroup", SR1_File.Version.May12, SR1_File.Version.Next);
			razielSpectralLightGroup.Read(reader, this, "razielSpectralLightGroup", SR1_File.Version.May12, SR1_File.Version.Next);

			reader.PlanMarkerList = PlanMarkerList;
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			worldNameString.SetPadding(4).ReadFromPointer(reader, worldName);
			Name = worldNameString.ToString();

			new SR1_StructureSeries<MultiSignal>().ReadFromPointer(reader, SignalListStart, SignalListEnd);
			SR1_Structure terrainStruct = new Terrain().ReadFromPointer(reader, terrain);

			lightListStruct0.ReadFromPointer(reader, lightList);
			if (reader.File._Version < SR1_File.Version.Apr14 &&
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

			var vmoList = new SR1_StructureSeries<VMObject>();
			vmoList.SetReadCount(numVMObjects.Value);
			vmoList.ReadFromPointer(reader, vmobjectList);

			// Why only before May? They still seem supported.
			if (reader.File._Version <= SR1_File.Version.May12)
			{
				new SR1_StructureArray<SpotLight>(numSpotLights.Value).ReadFromPointer(reader, spotLightList);
			}
			new LightList().ReadFromPointer(reader, spectrallightList);

			if (reader.File._Version < SR1_File.Version.Apr14)
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

			SR1_StructureSeries<Intro> intros = new SR1_StructureSeries<Intro>();
			intros.SetReadCount(numIntros.Value);
			intros.ReadFromPointer(reader, introList);

			SR1_Structure objectNameListStruct = new ObjectNameList().ReadFromPointer(reader, objectNameList);
			new SR1_StructureArray<PlanMkr>(NumberOfPlanMarkers.Value).SetPadding(4).ReadFromPointer(reader, PlanMarkerList);
			new SR1_StructureArray<SFXMkr>(NumberOfSFXMarkers.Value).ReadFromPointer(reader, SFXMarkerList);
			if (reader.File._Version >= SR1_File.Version.Jun01)
			{
				new SR1_String(4).SetPadding(4).ReadFromPointer(reader, dynamicMusicName);
			}
			else if (reader.File._Version < SR1_File.Version.May12 &&
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
			if (events.Start != 0 && !reader.File._Structures.ContainsKey(events.End))
			{
				reader.BaseStream.Position = events.End;
				new Mirror().Read(reader, null, "");
			}

			// Could try new align stuff instead of this.
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

			if (reader.MultiSplineDictionary.Count > 0)
			{
                SR1_StructureArray<MultiSpline> multiSplines = new SR1_StructureArray<MultiSpline>(reader.MultiSplineDictionary.Count);
                multiSplines.ReadFromPointer(reader, reader.MultiSplineDictionary.Values[0]);
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
			waterZLevel.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			gexStartPosition.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount1.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			collectableCount2.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorR.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorG.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			farColorB.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			cpad0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			backColorR.Write(writer);
			backColorG.Write(writer);
			backColorB.Write(writer);
			cpad1.Write(writer);
			objectFarColorR.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			objectFarColorG.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			objectFarColorB.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			specturalColorR.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			specturalColorG.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			specturalColorB.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			cpad2.Write(writer);
			fogFar.Write(writer);
			fogNear.Write(writer);
			spectralFogFar.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Next);
			spectralFogNear.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.Next);
			waterFogFar.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			waterFogNear.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			depthQBlendStart.Write(writer);
			spectrallightList.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			depthQPTable.Write(writer, SR1_File.Version.First, SR1_File.Version.Apr14);
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
			vramBlock.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			vramPad.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			depthQFogStart.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			morphLastStep.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			worldName.Write(writer);
			vramSize.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
			numberBrotherUnits.Write(writer, SR1_File.Version.First, SR1_File.Version.Apr14);
			brotherUnitsNames.Write(writer, SR1_File.Version.First, SR1_File.Version.Apr14);
			unknown0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			currentCollectable.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			unknown1.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			holdFogFar.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			holdFogNear.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			numberEnemyUnits.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			enemyUnitsNames.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			timesSignalList.Write(writer);
			spectralSignal.Write(writer);
			materialSignal.Write(writer);
			startUnitLoadedSignal.Write(writer);
			startUnitMainSignal.Write(writer);
			startGoingIntoWaterSignal.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			startGoingOutOfWaterSignal.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			unitFlags.Write(writer);
			SignalListStart.Write(writer);
			SignalListEnd.Write(writer);
			numBSPLeaves.Write(writer);
			PuzzleInstances.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			NumberOfPlanMarkers.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			PlanMarkerList.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			NumberOfSFXMarkers.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			SFXMarkerList.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			versionNumber.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			dynamicMusicName.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			streamUnitID.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			tClassAttr.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODLighting.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColor.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogNearFar.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODRedScale.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODGrnScale.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODBluScale.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogNear.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODfogFar.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			short_pad.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorR.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorG.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			TODbackColorB.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			cpad3.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
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

				if (introList.Offset != 0)
				{
					SR1_StructureSeries<Intro> intros = (SR1_StructureSeries<Intro>)file._Structures[introList.Offset];
					List<Intro> introsToRemove = new List<Intro>();

					foreach (int i in file._Overrides.IntrosToRemove)
					{
						introsToRemove.Add((Intro)intros[i]);
					}

					foreach (Intro intro in introsToRemove)
					{
						intros.Remove(intro);

						if (intro.data.Offset != 0)
						{
							file._Structures.Remove(intro.data.Offset);
							intro.data.Offset = 0;
						}
					}

					numIntros.Value = intros.Count;
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
					// Can't use SignalListStart because I included data before that in the Events structure.
					if (events.Start != 0)
					{
						file._Structures.Remove(events.Start);
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

			if (file._Version <= SR1_File.Version.Feb16 && targetVersion >= SR1_File.Version.Retail_PC)
			{
				if (Name == "undrct20" || Name == "undrct2")
				{
					unitFlags.Value = 0;
				}
			}

			if (file._Version < SR1_File.Version.Jan23 && targetVersion >= SR1_File.Version.Retail_PC)
			{
				SR1_String musicName = new SR1_String();
				musicName.SetReadMax(true);
				// Length is 4, so I don't need to track down everything to align.
				musicName.SetText("ct\0", 4);
				file._MigrationStructures.Add(End, musicName);
				dynamicMusicName.Offset = End;
				dynamicMusicName.Heuristic = PtrHeuristic.Migration;
			}

			/*if (Name == "city12")
			{
				SR1_StructureSeries<MultiSignal> multiSignals =
					(SR1_StructureSeries<MultiSignal>)file._Structures[SignalListStart.Offset];

				MultiSignal multiSignalA = (MultiSignal)multiSignals[0];
				Signal signalA = new Signal();
				SignalStreamLevel signalStreamLevelA = new SignalStreamLevel();
				signalStreamLevelA.currentnum.Value = multiSignalA.signalNum.Value;
				signalStreamLevelA.toname.SetReadMax(true);
				signalStreamLevelA.toname.SetText("city11,2", 16);
				signalStreamLevelA.streamID.Value = 154;
				signalA.data = signalStreamLevelA;
				signalA.id.Value = (int)Signal.SignalTypeJun01.HandleStreamLevel;
				multiSignalA.signalList.Add(signalA);

				MultiSignal multiSignalB = (MultiSignal)multiSignals[1];
				Signal signalB = new Signal();
				SignalStreamLevel signalStreamLevelB = new SignalStreamLevel();
				signalStreamLevelB.currentnum.Value = multiSignalB.signalNum.Value;
				signalStreamLevelB.toname.SetText("city16,97", 16);
				signalStreamLevelB.streamID.Value = 215;
				signalB.data = signalStreamLevelB;
				signalB.id.Value = (int)Signal.SignalTypeJun01.HandleStreamLevel;
				multiSignalB.signalList.Add(signalB);
			}*/
		}
	}
}
