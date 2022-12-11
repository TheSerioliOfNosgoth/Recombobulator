using System.Collections.Generic;
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

		public readonly SR1_Primative<int> oflags = new SR1_Primative<int>().ShowAsHex(true);
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
		public readonly SR1_Primative<int> oflags2 = new SR1_Primative<int>().ShowAsHex(true);
		public readonly SR1_Primative<short> sectionA = new SR1_Primative<short>();
		public readonly SR1_Primative<short> sectionB = new SR1_Primative<short>();
		public readonly SR1_Primative<short> sectionC = new SR1_Primative<short>();
		public readonly SR1_Primative<short> numberOfEffects = new SR1_Primative<short>();
		public readonly SR1_Pointer<ObjectEffect> effectList = new SR1_Pointer<ObjectEffect>();
		public readonly SR1_Pointer<RelocateList> relocList = new SR1_Pointer<RelocateList>();
		public readonly SR1_PrimativePointer<byte> relocModule = new SR1_PrimativePointer<byte>();
		public readonly VramSize vramSize = new VramSize();

		SR1_String nameString = new SR1_String(12);
		SR1_String scriptString = new SR1_String(12);

		public uint AnimKeyListStart { get; private set; } = 0xFFFFFFFF;

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
			vramSize.Read(reader, this, "vramSize", SR1_File.Version.Apr14, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			nameString.SetPadding(4).ReadFromPointer(reader, name);
			scriptString.SetPadding(4).ReadFromPointer(reader, script);

			Name = scriptString.ToString();

			bool useFlameGSHack = false;

			if (reader.File._Version == SR1_File.Version.Feb16 && Name == "flamegs_")
			{
				new SR1_String(12).SetPadding(4).ReadOrphan(reader, 0x44);
				useFlameGSHack = true;
			}

			SR1_Structure modelListStruct = new SR1_PointerArray<Model>(System.Math.Max(1, (int)numModels.Value), true).ReadFromPointer(reader, modelList);
			SR1_Structure animListStruct = new SR1_PointerArray<G2AnimKeylist_Type>(numAnims.Value, false).ReadFromPointer(reader, animList);
			SR1_Structure effectListStruct = new SR1_StructureArray<ObjectEffect>(numberOfEffects.Value).ReadFromPointer(reader, effectList);
			SR1_Structure soundDataStruct = new SFXFileData().SetPadding(4).ReadFromPointer(reader, soundData);
			SR1_Structure relocListStruct = new RelocateList().ReadFromPointer(reader, relocList);
			SR1_Structure relocModuleStruct =
				(Name == "cinemax_") ? new CinemaFnTableT().ReadFromPointer(reader, relocModule) :
				(Name == "mcardx__") ? new MCardMTableT().ReadFromPointer(reader, relocModule) :
				new MonsterFunctionTable().ReadFromPointer(reader, relocModule);

			PhysObProperties physObBase = null;
			PhysObPropertiesBase physOb = null;
			MonsterAttributes monAttributes = null;
			if (data.Offset != 0)
			{
				if (Name == "pshblkb_" || Name == "urn_____")
				{
					new PhysObGenericProperties(0).ReadFromPointer(reader, data);
				}
				else if ((oflags2.Value & 0x00040000) != 0 ||
					Name == "catdora_" ||
					Name == "walbosc_" ||
					Name == "flamesk_" ||
					Name == "flamesl_")
				{
					// new PhysObProperties().ReadFromPointer(reader, data);

					reader.BaseStream.Position = (long)data.Offset;
					physObBase = new PhysObProperties();
					physObBase.ReadTemp(reader);
					reader.BaseStream.Position = (long)data.Offset;
					physOb = (PhysObPropertiesBase)physObBase.CreateReplacementObject();
					physOb.ReadFromPointer(reader, data);
					reader.PhysObProperties = physOb;
				}
				else if ((oflags2.Value & 0x00080000) != 0)
				{
					//_MonsterAttributes
					// monsterAttributes->magic number: -0x531fff9b
					// MONTABLE_SetupTablePointer whatAmI

					monAttributes = new MonsterAttributes();
					monAttributes.ReadFromPointer(reader, data);

					if (Name == "aluka___")
					{
						new AlukaTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "alukabss")
					{
						new AlukaBssTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "hunter__")
					{
						new HunterTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "kain____")
					{
						KainTuneData kainData = new KainTuneData();
						kainData.SetPadding(soundData.Offset != 0 ? 0 : 4);
						kainData.ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "roninbss")
					{
						new RoninBssTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "skinbos_")
					{
						new SkinBosTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "walboss_")
					{
						new WalBosTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "walbosb_")
					{
						new WalBosBTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "wallcr__")
					{
						new WallcrData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "vwraith_")
					{
						new VWraithTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
					else if (Name == "priests_")
					{
						new PriestsTuneData().ReadFromPointer(reader, monAttributes.tunData);
					}
				}
				else if (Name == "raziel__")
				{
					new RazielData().ReadFromPointer(reader, data);
				}
				else if (Name == "sreavr__")
				{
					new ReaverTuneData().ReadFromPointer(reader, data);
				}
				else if (Name == "glphicon")
				{
					new GlyphTuneData().ReadFromPointer(reader, data);
				}
				else if (Name == "monster_")
				{
					new MonsterAttributes().ReadFromPointer(reader, data);
				}
				else if (Name == "particle")
				{
					// GenericFXObject?
					// See FX_RelocateGeneric?
					new GenericFXObject().ReadFromPointer(reader, data);
				}
				else if (Name == "litshaft")
				{
					new LitShaftProperties().ReadFromPointer(reader, data);
				}
				else if (Name == "waterfx_")
				{
					new WaterFXProperties().ReadFromPointer(reader, data);
				}
				else if ((oflags2.Value & 0x00040000) == 0)
				{
					new GenericTune().ReadFromPointer(reader, data);
				}
			}

			if (numAnims.Value > 0)
			{
				SR1_StructureArray<G2AnimKeylist_Type> keyLists = new SR1_StructureArray<G2AnimKeylist_Type>(numAnims.Value);
				if (reader.File._Version >= SR1_File.Version.Feb16 && reader.File._Version < SR1_File.Version.May12 &&
					Name == "wrshp___")
				{
					((G2AnimKeylist_Type)keyLists[14]).OverridePadLength(8);
				}
				if (reader.File._Version >= SR1_File.Version.May12 && reader.File._Version < SR1_File.Version.Jun01 &&
					Name == "wrshp___")
				{
					((G2AnimKeylist_Type)keyLists[15]).OverridePadLength(8);
				}
				else if (reader.File._Version >= SR1_File.Version.Jun01 && reader.File._Version < SR1_File.Version.Next &&
					Name == "wrshp___")
				{
					((G2AnimKeylist_Type)keyLists[13]).OverridePadLength(8);
				}

				keyLists.ReadFromPointer(reader, ((SR1_PointerArray<G2AnimKeylist_Type>)animListStruct)[0]);
				AnimKeyListStart = keyLists.Start;

				bool readUnusedAnimFX = false;

				if (reader.File._Version >= SR1_File.Version.May12 && reader.File._Version < SR1_File.Version.Jul14 &&
					(Name == "hunter__" || Name == "wrshp___" ||
					Name == "vlgra___" || Name == "vlgrb___" || Name == "vlgrc___"))
				{
					readUnusedAnimFX = true;
				}
				else if (reader.File._Version == SR1_File.Version.Feb16)
				{
					readUnusedAnimFX = true;
				}

				if (readUnusedAnimFX)
				{
					reader.BaseStream.Position = keyLists.Start - 1;
					while (!reader.File._Structures.ContainsKey((uint)reader.BaseStream.Position))
					{
						reader.BaseStream.Position--;
					}
					reader.BaseStream.Position = reader.File._Structures[(uint)reader.BaseStream.Position].End;
					uint length = keyLists.Start - (uint)reader.BaseStream.Position;
					if (length > 0)
					{
						new SR1_StructureSeries<G2AnimFXList>((int)length).SetPadding(4).Read(reader, null, "");
					}
				}
				else if (reader.AnimFXDictionary.Count > 0)
				{
					// Superceeded by code above?
					int numEffects = reader.AnimFXDictionary.Count;
					if (reader.Object.Name == "wrshp___")
					{
						if (reader.File._Version >= SR1_File.Version.Jul14)
						{
							numEffects = 11;
						}
						else if (reader.File._Version >= SR1_File.Version.Jun18)
						{
							numEffects = 28;
						}
					}

					SR1_StructureArray<G2AnimFXList> fxLists = new SR1_StructureArray<G2AnimFXList>(numEffects);
					fxLists.SetPadding(4).ReadFromPointer(reader, reader.AnimFXDictionary.Values[0]);
				}
			}

			uint padAdress = End;
			if (nameString.End > padAdress) padAdress = nameString.End;
			if (scriptString.End > padAdress) padAdress = scriptString.End;
			if (modelListStruct.End > padAdress) padAdress = modelListStruct.End;
			if (animListStruct.End > padAdress) padAdress = animListStruct.End;
			if (effectListStruct.End > padAdress) padAdress = effectListStruct.End;

			// 8 mystery bytes after effectList. THIS APPEARS TO BE A PHYSOBLIGHT. REMOVE THIS?
			if (physObBase != null && !reader.File._Structures.ContainsKey(padAdress))
			{
				reader.BaseStream.Position = padAdress;
				new SR1_PrimativeArray<byte>(useFlameGSHack ? 4 : 8).Read(reader, null, "");
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
			vramSize.Write(writer, SR1_File.Version.Apr14, SR1_File.Version.Next);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version != targetVersion)
			{
				if (file._Overrides.NewName != null)
				{
					nameString.SetText(file._Overrides.NewName, 12);
					scriptString.SetText(file._Overrides.NewName, 12);
				}
			}

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				//oflags.Value = 0x280042F1;
				//oflags2.Value = 0x06E80004;

				if (relocList.Offset != 0)
				{
					file._Structures.Remove(relocList.Offset);
					relocList.Offset = 0;
				}

				if (relocModule.Offset != 0)
				{
					file._Structures.Remove(relocModule.Offset);
					relocModule.Offset = 0;
				}
			}
		}
	}
}
