using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Level : SR1_Structure
    {
        // Use      //.*\r?\n     to search and remove comments.

        SR1_Pointer<Terrain> terrain = new SR1_Pointer<Terrain>();
        SR1_Pointer<LightList> lightList = new SR1_Pointer<LightList>();
        SR1_Primative<int> numVMObjects = new SR1_Primative<int>();
        SR1_Pointer<VMObject> vmobjectList = new SR1_Pointer<VMObject>();
        SR1_Primative<int> numSpotLights = new SR1_Primative<int>();
        SR1_Pointer<SpotLight> spotLightList = new SR1_Pointer<SpotLight>();
        SR1_Primative<int> numPointLights = new SR1_Primative<int>();
        SR1_Pointer<PointLight> pointLightList = new SR1_Pointer<PointLight>();
        SR1_Primative<int> numSpotSpecturalLights = new SR1_Primative<int>();
        SR1_Pointer<SpotLight> spotSpecturalLightList = new SR1_Pointer<SpotLight>();
        SR1_Primative<int> numPointSpecturalLights = new SR1_Primative<int>();
        SR1_Pointer<PointLight> pointSpecturalLightList = new SR1_Pointer<PointLight>();
        SR1_Primative<int> numBGObjects = new SR1_Primative<int>();
        SR1_Pointer<BGObject> bgObjectList = new SR1_Pointer<BGObject>();
        SR1_Primative<int> waterZLevel = new SR1_Primative<int>();
        SR1_Primative<byte> backColorR = new SR1_Primative<byte>();
        SR1_Primative<byte> backColorG = new SR1_Primative<byte>();
        SR1_Primative<byte> backColorB = new SR1_Primative<byte>();
        SR1_Primative<byte> cpad1 = new SR1_Primative<byte>();
        SR1_Primative<byte> specturalColorR = new SR1_Primative<byte>();
        SR1_Primative<byte> specturalColorG = new SR1_Primative<byte>();
        SR1_Primative<byte> specturalColorB = new SR1_Primative<byte>();
        SR1_Primative<byte> cpad2 = new SR1_Primative<byte>();
        SR1_Primative<ushort> fogFar = new SR1_Primative<ushort>();
        SR1_Primative<ushort> fogNear = new SR1_Primative<ushort>();
        SR1_Primative<ushort> spectralFogFar = new SR1_Primative<ushort>();
        SR1_Primative<ushort> spectralFogNear = new SR1_Primative<ushort>();
        SR1_Primative<ushort> waterFogFar = new SR1_Primative<ushort>();
        SR1_Primative<ushort> waterFogNear = new SR1_Primative<ushort>();
        SR1_Primative<int> depthQBlendStart = new SR1_Primative<int>();
        SR1_Pointer<LightList> spectrallightList = new SR1_Pointer<LightList>();
        SR1_Primative<int> numCameras = new SR1_Primative<int>();
        SR1_Pointer<CameraKey> cameraList = new SR1_Pointer<CameraKey>(); // void in sym, but really a CameraKey
        SR1_Primative<int> bspPlaneError = new SR1_Primative<int>();
        SR1_Primative<int> numVGroups = new SR1_Primative<int>();
        SR1_Pointer<VGroup> vGroupList = new SR1_Pointer<VGroup>();
        SR1_Primative<int> deathZ = new SR1_Primative<int>();
        SR1_Primative<int> flags = new SR1_Primative<int>();
        SR1_Pointer<MultiSignal> startSignal = new SR1_Pointer<MultiSignal>();
        SR1_Primative<int> numIntros = new SR1_Primative<int>();
        SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>();
        SR1_Pointer<DrMoveAniTex> bgAniList = new SR1_Pointer<DrMoveAniTex>();
        SR1_Primative<int> numHotSpots = new SR1_Primative<int>();
        SR1_Pointer<HotSpot> hotSpotList = new SR1_Pointer<HotSpot>();
        SR1_Pointer<ObjectNameList> objectNameList = new SR1_Pointer<ObjectNameList>();
        SR1_Primative<int> depthQFogStart = new SR1_Primative<int>();
        SR1_Primative<int> morphLastStep = new SR1_Primative<int>();
        SR1_PrimativePointer<char> worldName = new SR1_PrimativePointer<char>();
        VramSize vramSize = new VramSize();
        SR1_Primative<ushort> holdFogFar = new SR1_Primative<ushort>();
        SR1_Primative<ushort> holdFogNear = new SR1_Primative<ushort>();
        SR1_Primative<int> numberEnemyUnits = new SR1_Primative<int>();
        SR1_PrimativePointer<char> enemyUnitsNames = new SR1_PrimativePointer<char>();
        SR1_PrimativePointer<int> timesSignalList = new SR1_PrimativePointer<int>();
        SR1_Pointer<MultiSignal> spectralSignal = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> materialSignal = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> startUnitLoadedSignal = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> startUnitMainSignal = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> startGoingIntoWaterSignal = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> startGoingOutOfWaterSignal = new SR1_Pointer<MultiSignal>();
        SR1_Primative<int> unitFlags = new SR1_Primative<int>();
        SR1_Pointer<MultiSignal> SignalListStart = new SR1_Pointer<MultiSignal>();
        SR1_Pointer<MultiSignal> SignalListEnd = new SR1_Pointer<MultiSignal>();
        SR1_Primative<int> numBSPLeaves = new SR1_Primative<int>();
        SR1_Pointer<EventPointers> PuzzleInstances = new SR1_Pointer<EventPointers>();
        SR1_Primative<int> NumberOfPlanMarkers = new SR1_Primative<int>();
        SR1_Pointer<PlanMkr> PlanMarkerList = new SR1_Pointer<PlanMkr>();
        SR1_Primative<int> NumberOfSFXMarkers = new SR1_Primative<int>();
        SR1_Pointer<SFXMkr> SFXMarkerList = new SR1_Pointer<SFXMkr>();
        SR1_Primative<uint> versionNumber = new SR1_Primative<uint>();
        SR1_PrimativePointer<char> dynamicMusicName = new SR1_PrimativePointer<char>();
        SR1_Primative<int> streamUnitID = new SR1_Primative<int>();
        SR1_StructureArray<TClassAttr> tClassAttr = new SR1_StructureArray<TClassAttr>(16);
        SR1_PrimativeArray<short> TODLighting = new SR1_PrimativeArray<short>(4, 3);
        SR1_PrimativeArray<byte> TODbackColor = new SR1_PrimativeArray<byte>(4, 3);
        SR1_PrimativeArray<ushort> TODfogNearFar = new SR1_PrimativeArray<ushort>(4, 2);
        SR1_Primative<short> TODRedScale = new SR1_Primative<short>();
        SR1_Primative<short> TODGrnScale = new SR1_Primative<short>();
        SR1_Primative<short> TODBluScale = new SR1_Primative<short>();
        SR1_Primative<short> TODfogNear = new SR1_Primative<short>();
        SR1_Primative<short> TODfogFar = new SR1_Primative<short>();
        SR1_Primative<short> short_pad = new SR1_Primative<short>();
        SR1_Primative<byte> TODbackColorR = new SR1_Primative<byte>();
        SR1_Primative<byte> TODbackColorG = new SR1_Primative<byte>();
        SR1_Primative<byte> TODbackColorB = new SR1_Primative<byte>();
        SR1_Primative<byte> cpad3 = new SR1_Primative<byte>();
        SR1_Pointer<LightGroup> razielLightGroup = new SR1_Pointer<LightGroup>();
        SR1_Pointer<LightGroup> razielSpectralLightGroup = new SR1_Pointer<LightGroup>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
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
            waterFogFar.Read(reader, this, "waterFogFar");
            waterFogNear.Read(reader, this, "waterFogNear");
            depthQBlendStart.Read(reader, this, "depthQBlendStart");
            spectrallightList.Read(reader, this, "spectrallightList");
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
            vramSize.Read(reader, this, "vramSize");
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
            NumberOfSFXMarkers.Read(reader, this, "NumberOfSFXMarkers");
            SFXMarkerList.Read(reader, this, "SFXMarkerList");
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
            razielLightGroup.Read(reader, this, "razielLightGroup");
            razielSpectralLightGroup.Read(reader, this, "razielSpectralLightGroup");

            reader.PlanMarkerList = PlanMarkerList;
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            SR1_Structure temp = null;

            temp = new Terrain().ReadFromPointer(reader, terrain);
            new LightList().ReadFromPointer(reader, lightList);
            new VMObjectList(numVMObjects.Value).ReadFromPointer(reader, vmobjectList);
            new LightList().ReadFromPointer(reader, spectrallightList);
            new SR1_StructureArray<CameraKey>(numCameras.Value).ReadFromPointer(reader, cameraList);
            new SR1_StructureArray<VGroup>(numVGroups.Value).ReadFromPointer(reader, vGroupList);
            new SR1_StructureArray<Intro>(numIntros.Value).ReadFromPointer(reader, introList);
            new ObjectNameList().ReadFromPointer(reader, objectNameList);
            new SR1_String(12).SetPadding(4).ReadFromPointer(reader, worldName);
            new SR1_StructureSeries<MultiSignal>((int)(SignalListEnd.Offset - SignalListStart.Offset)).ReadFromPointer(reader, SignalListStart);
            new SR1_StructureArray<PlanMkr>(NumberOfPlanMarkers.Value).SetPadding(4).ReadFromPointer(reader, PlanMarkerList);
            new SR1_StructureArray<SFXMkr>(NumberOfSFXMarkers.Value).ReadFromPointer(reader, SFXMarkerList);
            new SR1_PrimativeArray<char>(4).ReadFromPointer(reader, dynamicMusicName);
            new LightGroup().ReadFromPointer(reader, razielLightGroup);
            new LightGroup().ReadFromPointer(reader, razielSpectralLightGroup);

            if (PuzzleInstances.Offset != 0)
            {
                reader.BaseStream.Position = PuzzleInstances.Offset;
                EventPointers tempEventPointers = new EventPointers();
                tempEventPointers.ReadTemp(reader);
                uint firstEvent = uint.MaxValue;
                foreach (SR1_PointerBase eventInstance in tempEventPointers.eventInstances.List)
                {
                    if (eventInstance.Offset != 0 && eventInstance.Offset < firstEvent)
                    {
                        firstEvent = eventInstance.Offset;
                    }
                }

                if (firstEvent != 0 && firstEvent != uint.MaxValue)
                {
                    reader.BaseStream.Position = firstEvent;
                    Events events = new Events(tempEventPointers.numPuzzles.Value);
                    events.Read(reader, null, "");
                    reader.Events = events;
                }
            }

            if (temp.End != 0)
            {
                foreach (SR1_Pointer<SFXFileData> pointer in reader.SFXDictionary.Values)
                {
                    SR1_Structure sfxFileDataList = new SFXFileDataList().ReadFromPointer(reader, pointer);
                    if (!reader.File._Structures.ContainsKey(sfxFileDataList.End))
                    {
                        reader.BaseStream.Position = sfxFileDataList.End;
                        new SR1_PrimativeArray<byte>((int)(temp.Start - sfxFileDataList.End)).Read(reader, null, "");
                    }
                    break;
                }
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
            waterFogFar.Write(writer);
            waterFogNear.Write(writer);
            depthQBlendStart.Write(writer);
            spectrallightList.Write(writer);
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
            vramSize.Write(writer);
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
            NumberOfSFXMarkers.Write(writer);
            SFXMarkerList.Write(writer);
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
            razielLightGroup.Write(writer);
            razielSpectralLightGroup.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion)
        {
            base.MigrateVersion(file, targetVersion);

            if ((file._Version == SR1_File.Version.Retail || file._Version == SR1_File.Version.Beta) &&
                targetVersion == SR1_File.Version.Retail_PC)
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
            }
        }
    }
}
