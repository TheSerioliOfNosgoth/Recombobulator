﻿using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class Object : SR1_Structure
    {
        private enum ScriptType
        {
            Raziel = 0,
            SoulReaver,
            GlyphIcon,
            Physical,
            Monster,
            Particle,
            LightShaft,
        }

        public readonly SR1_Primative<int> oflags = new SR1_Primative<int>();
        public readonly SR1_Primative<short> id = new SR1_Primative<short>();
        public readonly SR1_Primative<short> sfxFileHandle = new SR1_Primative<short>();
        public readonly SR1_Primative<short> numModels = new SR1_Primative<short>();
        public readonly SR1_Primative<short> numAnims = new SR1_Primative<short>();
        public readonly SR1_PointerArrayPointer<Model> modelList = new SR1_PointerArrayPointer<Model>();
        public readonly SR1_PointerArrayPointer<G2AnimKeylist_Type> animList = new SR1_PointerArrayPointer<G2AnimKeylist_Type>();
        public readonly SR1_Primative<short> introDist = new SR1_Primative<short>();
        public readonly SR1_Primative<short> vvIntroDist = new SR1_Primative<short>();
        public readonly SR1_Primative<short> removeDist = new SR1_Primative<short>();
        public readonly SR1_Primative<short> vvRemoveDist = new SR1_Primative<short>();
        public readonly SR1_PrimativePointer<byte> data = new SR1_PrimativePointer<byte>();
        public readonly SR1_PrimativePointer<char> script = new SR1_PrimativePointer<char>();
        public readonly SR1_PrimativePointer<char> name = new SR1_PrimativePointer<char>();
        public readonly SR1_PrimativePointer<byte> soundData = new SR1_PrimativePointer<byte>();
        public readonly SR1_Primative<int> oflags2 = new SR1_Primative<int>();
        public readonly SR1_Primative<short> sectionA = new SR1_Primative<short>();
        public readonly SR1_Primative<short> sectionB = new SR1_Primative<short>();
        public readonly SR1_Primative<short> sectionC = new SR1_Primative<short>();
        public readonly SR1_Primative<short> numberOfEffects = new SR1_Primative<short>();
        public readonly SR1_Pointer<ObjectEffect> effectList = new SR1_Pointer<ObjectEffect>();
        public readonly SR1_Pointer<RelocateList> relocList = new SR1_Pointer<RelocateList>();
        public readonly SR1_PrimativePointer<byte> relocModule = new SR1_PrimativePointer<byte>();
        public readonly VramSize vramSize = new VramSize();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            reader.Object = this;

            oflags.Read(reader, this, "oflags");
            id.Read(reader, this, "id");
            sfxFileHandle.Read(reader, this, "sfxFileHandle");
            numModels.Read(reader, this, "numModels");
            numAnims.Read(reader, this, "numAnims");
            modelList.Read(reader, this, "modelList");
            animList.Read(reader, this, "animList");
            introDist.Read(reader, this, "introDist");
            vvIntroDist.Read(reader, this, "vvIntroDist");
            removeDist.Read(reader, this, "removeDist");
            vvRemoveDist.Read(reader, this, "vvRemoveDist");
            data.Read(reader, this, "data");
            script.Read(reader, this, "script");
            name.Read(reader, this, "name");
            soundData.Read(reader, this, "soundData");
            oflags2.Read(reader, this, "oflags2");
            sectionA.Read(reader, this, "sectionA");
            sectionB.Read(reader, this, "sectionB");
            sectionC.Read(reader, this, "sectionC");
            numberOfEffects.Read(reader, this, "numberOfEffects");
            effectList.Read(reader, this, "effectList");
            relocList.Read(reader, this, "relocList");
            relocModule.Read(reader, this, "relocModule");
            vramSize.Read(reader, this, "vramSize");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            SR1_Structure temp;
            string scriptName;

            new SR1_PointerArray<Model>(numModels.Value, true).ReadFromPointer(reader, modelList);

            if (numAnims.Value > 0)
            {
                SR1_PointerArray<G2AnimKeylist_Type> keyListPointers = new SR1_PointerArray<G2AnimKeylist_Type>(numAnims.Value, false);
                keyListPointers.ReadFromPointer(reader, animList);
                SR1_StructureArray<G2AnimKeylist_Type> keyLists = new SR1_StructureArray<G2AnimKeylist_Type>(numAnims.Value);
                keyLists.ReadFromPointer(reader, keyListPointers[0]);
                if (reader.AnimFXDictionary.Count > 0)
                {
                    SR1_StructureArray<G2AnimFXList> fxLists = new SR1_StructureArray<G2AnimFXList>(reader.AnimFXDictionary.Count);
                    fxLists.SetPadding(4).ReadFromPointer(reader, reader.AnimFXDictionary.Values[0]);
                }
            }

            temp = new SR1_String(12).SetPadding(4).ReadFromPointer(reader, script);
            scriptName = temp.ToString();
            new SR1_String(12).SetPadding(4).ReadFromPointer(reader, name);

            temp = new SR1_StructureArray<ObjectEffect>(numberOfEffects.Value).ReadFromPointer(reader, effectList);

            // 8 mystery bytes after effectList. THIS APPEARS TO BE A PHYSOBLIGHT. REMOVE THIS?
            //if (temp.End != 0x00000000 && !reader.File._Structures.ContainsKey(temp.End))
            //{
            //    reader.BaseStream.Position = temp.End;
            //    new SR1_PrimativeArray<byte>(8).Read(reader, null, "");
            //}

            if ((oflags2.Value & 0x00040000) != 0)
            {
                // new PhysObProperties().ReadFromPointer(reader, data);

                reader.BaseStream.Position = (long)data.Offset;
                PhysObProperties tempPOP = new PhysObProperties();
                tempPOP.ReadTemp(reader);
                reader.BaseStream.Position = (long)data.Offset;
                ((SR1_Structure)tempPOP.CreateReplacementObject()).ReadFromPointer(reader, data);
            }
            else if ((oflags2.Value & 0x00080000) != 0)
            {
                //_MonsterAttributes
                // monsterAttributes->magic number: -0x531fff9b
                // MONTABLE_SetupTablePointer whatAmI

                MonsterAttributes monAttributes = new MonsterAttributes();
                monAttributes.ReadFromPointer(reader, data);

                if (scriptName == "aluka___")
                {
                    new AlukaTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "alukabss")
                {
                    new AlukaBssTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "hunter__")
                {
                    new HunterTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "kain____")
                {
                    new KainTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "roninbss")
                {
                    new RoninBssTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "skinbos_")
                {
                    new SkinBosTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "walboss_")
                {
                    new WalBosTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "walbosb_")
                {
                    new WalBosBTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "wallcr__")
                {
                    new WallcrData().ReadFromPointer(reader, monAttributes.tunData);
                }
                else if (scriptName == "vwraith_")
                {
                    new VWraithTuneData().ReadFromPointer(reader, monAttributes.tunData);
                }
            }
            else if (scriptName == "raziel__")
            {
                new RazielData().ReadFromPointer(reader, data);
            }
            else if (scriptName == "sreavr__")
            {
                new ReaverTuneData().ReadFromPointer(reader, data);
            }
            else if (scriptName == "glphicon")
            {
                new GlyphTuneData().ReadFromPointer(reader, data);
            }
            else if (scriptName == "physical")
            {
                // new PhysObProperties().ReadFromPointer(reader, data);

                reader.BaseStream.Position = (long)data.Offset;
                PhysObProperties tempPOP = new PhysObProperties();
                tempPOP.ReadTemp(reader);
                reader.BaseStream.Position = (long)data.Offset;
                ((SR1_Structure)tempPOP.CreateReplacementObject()).ReadFromPointer(reader, data);
            }
            else if (scriptName == "monster_")
            {
                new MonsterAttributes().ReadFromPointer(reader, data);
            }
            else if (scriptName == "particle")
            {
                // GenericFXObject?
                // See FX_RelocateGeneric?
                new GenericFXObject().ReadFromPointer(reader, data);
            }
            else if (scriptName == "litshaft")
            {
                new LitShaftProperties().ReadFromPointer(reader, data);
            }

            new SFXFileData().ReadFromPointer(reader, soundData);

            if (relocList.Offset != 0)
            {
                new RelocateList().ReadFromPointer(reader, relocList);
            }

            if (relocModule.Offset != 0)
            {
                new RelocateModule().ReadFromPointer(reader, relocModule);
            }
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            oflags.Write(writer);
            id.Write(writer);
            sfxFileHandle.Write(writer);
            numModels.Write(writer);
            numAnims.Write(writer);
            modelList.Write(writer);
            animList.Write(writer);
            introDist.Write(writer);
            vvIntroDist.Write(writer);
            removeDist.Write(writer);
            vvRemoveDist.Write(writer);
            data.Write(writer);
            script.Write(writer);
            name.Write(writer);
            soundData.Write(writer);
            oflags2.Write(writer);
            sectionA.Write(writer);
            sectionB.Write(writer);
            sectionC.Write(writer);
            numberOfEffects.Write(writer);
            effectList.Write(writer);
            relocList.Write(writer);
            relocModule.Write(writer);
            vramSize.Write(writer);
        }
    }
}
