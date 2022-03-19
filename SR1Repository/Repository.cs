using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SR1Repository
{
	public class Repository
	{
		private enum SignalTypes
		{
			HandleLightGroup,           // long lightGroup;
			HandleCameraAdjust,         // long cameraAdjust;
			HandleCameraMode,           // long cameraMode;
			HandleCameraKey,            // CameraKey *cameraKey
			HandleCameraTimer,          // long cameraTimer;
			HandleCameraSmooth,         // long cameraSmooth;
			HandleCameraValue,          // long index; long value;
			HandleCameraLock,           // long cameraLock;
			HandleCameraUnlock,         // long cameraUnlock;
			HandleCameraSave,           // long cameraSave;
			HandleCameraRestore,        // long cameraRestore;
			HandleFogNear,              // long fogNear;
			HandleFogFar,               // long fogFar;
			HandleCameraShake,          // long time; long scale;
			HandleCallSignal,           // void* callSignal; (callSignal is itself a signal?)
			HandleEnd,                  // (nothing)
			HandleStopPlayerControl,    // (nothing)
			HandleStartPlayerControl,   // (nothing)
			HandleStreamLevel,          // long currentnum; long streamID; char toname[16];
			HandleCameraSpline,         // long index; Intro* intro;
			HandleScreenWipe,           // short type; short time;
			HandleBlendStart,           // long blendStart;
			HandleScreenWipeColor,      // unsigned char r; unsigned char g; unsigned char b; unsigned char pad;
			HandleSetSlideAngle,        // long slideAngle;
			HandleResetSlideAngle,      // (nothing)
			HandleSetCameraTilt,        // long cameraTilt;
			HandleSetCameraDistance,    // long cameraDistance;
		}

		const int textureWidth = 256;
		const int textureHeight = 256;
		const int headerLength = 4096;

		string _projectFolderName;
		string _dataFolderName;
		string _textureFolderName;
		string _sfxFolderName;
		string _outputFolderName;

		string _sourceBigfileName;
		string _sourceTexturesFileName;

		string _assetsFileName;
		string _levelsFileName;
		string _introsFileName;
		string _objectsFileName;
		string _clipsFileName;
		string _allClipsFileName;
		string _texturesFileName;
		string _textureSetsFileName;

		string _outputBigFileName;
		string _outputTexturesFileName;

		AssetDescList _assets;
		LevelList _levels;
		IntroList _intros;
		ObjectList _objects;
		SFXClipList _sfxClips;
		TexDescList _textures;
		TexSetList _textureSets;

		public AssetDescList Assets { get { return _assets; } }
		public LevelList Levels { get { return _levels; } }
		public IntroList Intros { get { return _intros; } }
		public ObjectList Objects { get { return _objects; } }
		public SFXClipList SFXClips { get { return _sfxClips; } }
		public TexDescList Textures { get { return _textures; } }
		public TexSetList TextureSets { get { return _textureSets; } }

		public int FilesRead = 0;
		public int FilesToRead = 0;
		public string RecentMessage = "";

		public Repository(string projectFolderName)
		{
			_projectFolderName = projectFolderName;
			_dataFolderName = Path.Combine(projectFolderName, "kain2");
			_textureFolderName = Path.Combine(projectFolderName, "textures");
			_sfxFolderName = Path.Combine(projectFolderName, "sfx");
			_outputFolderName = Path.Combine(projectFolderName, "output");

			_sourceBigfileName = Path.Combine(projectFolderName, "bigfile.dat");
			_sourceTexturesFileName = Path.Combine(projectFolderName, "textures.big");

			_assetsFileName = Path.Combine(projectFolderName, "assets.json");
			_levelsFileName = Path.Combine(projectFolderName, "levels.json");
			_introsFileName = Path.Combine(projectFolderName, "intros.json");
			_objectsFileName = Path.Combine(projectFolderName, "objects.json");
			_clipsFileName = Path.Combine(projectFolderName, "clips.json");
			_allClipsFileName = Path.Combine(projectFolderName, "allSFX.pmf");
			_texturesFileName = Path.Combine(projectFolderName, "textures.json");
			_textureSetsFileName = Path.Combine(projectFolderName, "textureSets.json");

			_outputBigFileName = Path.Combine(_outputFolderName, "bigfile.dat");
			_outputTexturesFileName = Path.Combine(_outputFolderName, "textures.big");

			ClearReposititory();
		}

		void CreateDirectories()
		{
			Directory.CreateDirectory(_dataFolderName);
			Directory.CreateDirectory(_textureFolderName);
			Directory.CreateDirectory(_sfxFolderName);
			Directory.CreateDirectory(_outputFolderName);
		}

		bool LoadHashTable(out Hashtable hashTable)
		{
			hashTable = new Hashtable();

			try
			{
				FileStream hashesFile = new FileStream("Hashes-SR1.txt", FileMode.Open, FileAccess.Read);
				StreamReader hashesReader = new StreamReader(hashesFile);

				while (!hashesReader.EndOfStream)
				{
					string currentLine = hashesReader.ReadLine();
					if (currentLine.Trim() != "")
					{
						string[] cl = currentLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
						if (cl.Length > 1)
						{
							string hashKey = cl[0].Trim().ToUpper();
							string hashValue = cl[1].Trim();
							if (!hashTable.Contains(hashKey))
							{
								hashTable.Add(hashKey, hashValue);
							}
						}
					}
				}

				hashesReader.Close();
				hashesFile.Close();
			}
			catch (Exception)
			{
				hashTable.Clear();
				Console.WriteLine("Error: Couldn't load the hash table.");
				return false;
			}

			return true;
		}

		bool InitializeAssets(AssetDescList assets, TexDescList textures)
		{
			if (!LoadHashTable(out Hashtable hashTable))
			{
				return false;
			}

			assets.Clear();
			textures.Clear();

			try
			{
				FileStream sourceBigFile = new FileStream(_sourceBigfileName, FileMode.Open, FileAccess.Read);
				BinaryReader bigFileReader = new BinaryReader(sourceBigFile);

				uint assetCount = bigFileReader.ReadUInt32();
				while (assetCount > 0)
				{
					AssetDesc entry = new AssetDesc();
					entry.FileHash = bigFileReader.ReadUInt32();
					entry.FileLength = bigFileReader.ReadUInt32();
					entry.FileOffset = bigFileReader.ReadUInt32();
					entry.Code.code = bigFileReader.ReadUInt32();

					string hashString = string.Format("{0:X8}", entry.FileHash);
					if (hashTable.Contains(hashString))
					{
						entry.FilePath = (string)hashTable[hashString];

						// Hack for hash collision.
						if (entry.FileCode == 0x444E554F)
						{
							if (entry.FileHash == 0xCA5731E8)
							{
								entry.FilePath = "kain2\\object\\alsound\\alsound.pcm";
							}
							else if (entry.FileHash == 0xCA5731E9)
							{
								entry.FilePath = "kain2\\object\\alsound\\alsound.crm";
							}
						}
					}

					assets.Add(entry);

					assetCount--;
				}

				bigFileReader.Close();
				sourceBigFile.Close();

				// Sort by offsets.
				SortedList<uint, AssetDesc> sortedAssets = new SortedList<uint, AssetDesc>();
				foreach (AssetDesc asset in assets.Assets)
				{
					sortedAssets.Add(asset.FileOffset, asset);
				}

				// Fill indices according to their offset.
				int assetIndex = 0;
				foreach (AssetDesc asset in sortedAssets.Values)
				{
					asset.FileIndex = assetIndex;
					assetIndex++;
				}

				FileStream sourceTexturesFile = new FileStream(_sourceTexturesFileName, FileMode.Open, FileAccess.Read);
				BinaryReader texturesReader = new BinaryReader(sourceTexturesFile);

				texturesReader.BaseStream.Position = headerLength;
				uint fileLength = (uint)sourceTexturesFile.Length;
				uint textureCount = (uint)((fileLength - (long)headerLength) / (long)(textureWidth * textureHeight * 2) - 1);
				for (int t = 0; t <= textureCount; t++)
				{
					string relativePath = MakeTextureFilePath(t);
					string outputFileName = Path.Combine(_projectFolderName, relativePath);

					TexDesc texDesc = new TexDesc();
					texDesc.TextureIndex = textures.Count;
					texDesc.FilePath = relativePath;
					textures.Add(texDesc);
				}

				texturesReader.Close();
				sourceTexturesFile.Close();
			}
			catch (Exception)
			{
				assets.Clear();
				textures.Clear();
				Console.WriteLine("Error: Couldn't initialize the file list.");
				return false;
			}

			return true;
		}

		bool AddAdditionalAssets(AssetDescList assets)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(_dataFolderName);
				FileInfo[] fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

				if (fileInfos.Length <= 0)
				{
					throw new Exception("No files in \"" + _dataFolderName + "\".");
				}

				foreach (FileInfo fileInfo in fileInfos)
				{
					string relativePath = fileInfo.FullName.Substring(_projectFolderName.Length);
					string extension = Path.GetExtension(relativePath);
					int endOfName = relativePath.Length - extension.Length;
					uint hashID = GetSR1HashName(relativePath);

					// There may be more than one file with the same hash ID in the source bigfile.
					// Update the sizes for all of them.
					List<AssetDesc> existingEntries = assets.FindAll(x => x.FileHash == hashID);
					if (existingEntries != null && existingEntries.Count > 0)
					{
						foreach (AssetDesc existingEntry in existingEntries)
						{
							existingEntry.FileLength = (uint)fileInfo.Length;
						}
					}
					else
					{
						AssetDesc newEntry = new AssetDesc();
						newEntry.FilePath = relativePath;
						newEntry.FileHash = hashID;
						newEntry.FileLength = (uint)fileInfo.Length;
						newEntry.Code.code0 = char.ToUpperInvariant(relativePath[endOfName - 4]);
						newEntry.Code.code1 = char.ToUpperInvariant(relativePath[endOfName - 3]);
						newEntry.Code.code2 = char.ToUpperInvariant(relativePath[endOfName - 2]);
						newEntry.Code.code3 = char.ToUpperInvariant(relativePath[endOfName - 1]);
						newEntry.FileIndex = assets.Count;

						assets.Add(newEntry);
					}
				}
			}
			catch (Exception)
			{
				assets.Clear();
				Console.WriteLine("Error: Couldn't generate bigfile entries.");
				return false;
			}

			return true;
		}

		bool AddAdditionalTextures(TexDescList textures)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(_textureFolderName);
				FileInfo[] fileInfos = directoryInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly);
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("texture-([0-9]+).(png)");

				if (fileInfos.Length <= 0)
				{
					throw new Exception("No files in \"" + _textureFolderName + "\".");
				}

				foreach (FileInfo fileInfo in fileInfos)
				{
					if (regex.IsMatch(fileInfo.Name))
					{
						string relativePath = fileInfo.FullName.Substring(_projectFolderName.Length);
						string[] tokens = regex.Split(fileInfo.Name);
						if (tokens.Length == 4 && Int32.TryParse(tokens[1], out int index))
						{
							TexDesc existingTexture = textures.Find(x => x.TextureIndex == index);
							if (existingTexture == null)
							{
								TexDesc newEntry = new TexDesc();
								newEntry.FilePath = relativePath;
								newEntry.TextureIndex = textures.Count;

								textures.Add(newEntry);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				textures.Clear();
				Console.WriteLine("Error: Couldn't add additional textures.");
				return false;
			}

			return true;
		}

		public AssetDesc AddNewAsset(string filePath)
		{
			string fullPath = Path.Combine(_projectFolderName, filePath);
			if (!File.Exists(fullPath))
			{
				return null;
			}

			string extension = Path.GetExtension(filePath);
			int endOfName = filePath.Length - extension.Length;
			uint fileHash = GetSR1HashName(filePath);
			if (Assets.Assets.Find(x => x.FileHash == fileHash) != null)
			{
				return null;
			}

			AssetDesc asset = new AssetDesc();
			FileInfo fileInfo = new FileInfo(fullPath);

			asset.FilePath = filePath;
			asset.FileHash = fileHash;
			asset.FileLength = (uint)fileInfo.Length;
			asset.Code.code0 = char.ToUpperInvariant(filePath[endOfName - 4]);
			asset.Code.code1 = char.ToUpperInvariant(filePath[endOfName - 3]);
			asset.Code.code2 = char.ToUpperInvariant(filePath[endOfName - 2]);
			asset.Code.code3 = char.ToUpperInvariant(filePath[endOfName - 1]);
			asset.FileIndex = Assets.Count;
			asset.IsNew = true;

			Assets.Add(asset);

			return asset;
		}

		public Object AddNewObject(string objectName, string textureSet)
		{
			string fullPath = MakeObjectFilePath(objectName, true);
			if (!File.Exists(fullPath))
			{
				return null;
			}

			if (_objects.Objects.Find(x => x.ObjectName == objectName) != null)
			{
				return null;
			}

			Object obj = new Object();
			obj.TextureSet = textureSet;

			FileStream levelFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(levelFile, System.Text.Encoding.ASCII);
			uint dataStart = ((reader.ReadUInt32() >> 9) << 11) + 0x00000800;
			ProcessObject(obj, reader, dataStart);
			reader.Close();
			levelFile.Close();

			obj.IsNew = true;
			return obj;
		}

		public Level AddNewLevel(string unitName, string sourceUnitName, uint sourceVersion, string textureSet)
		{
			string fullPath = MakeLevelFilePath(unitName, true);
			if (!File.Exists(fullPath))
			{
				return null;
			}

			if (_levels.Levels.Find(x => x.UnitName == unitName) != null)
			{
				return null;
			}

			Level level = new Level();
			level.TextureSet = textureSet;

			FileStream levelFile = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(levelFile, System.Text.Encoding.ASCII);
			uint dataStart = ((reader.ReadUInt32() >> 9) << 11) + 0x00000800;
			ProcessLevel(level, reader, dataStart);
			level.SourceUnitName = sourceUnitName;
			level.SourceVersion = sourceVersion;

			foreach (Portal fromPortal in level.Portals.Portals)
			{
				fromPortal.OldDestVersion = sourceVersion;
				Level targetLevel = _levels.Levels.Find(x => x.SourceUnitName == fromPortal.OldDestUnitName && x.SourceVersion == fromPortal.OldDestVersion);
				if (targetLevel != null)
				{
					fromPortal.DestUnitName = targetLevel.UnitName;
					fromPortal.DestUnitID = targetLevel.StreamUnitID;

					foreach (Portal toPortal in targetLevel.Portals.Portals)
					{
						if (toPortal.OldDestUnitName == level.SourceUnitName &&
							toPortal.OldDestVersion == level.SourceVersion)
						{
							toPortal.DestUnitName = level.UnitName;
							toPortal.DestUnitID = level.StreamUnitID;
							toPortal.DestSignalID = fromPortal.SignalID;
						}
					}

					UpdateLevelPortals(targetLevel);
				}
				else
				{
					fromPortal.DestUnitName = "";
					fromPortal.DestUnitID = 0;
					fromPortal.DestSignalID = 0;
				}
			}

			reader.Close();
			levelFile.Close();

			UpdateLevelPortals(level);

			level.IsNew = true;
			return level;
		}

		void ProcessObject(Object obj, BinaryReader reader, uint dataStart)
		{
			reader.BaseStream.Position = dataStart + 0x00000024;
			reader.BaseStream.Position = dataStart + reader.ReadUInt32();
			obj.ObjectName = CleanName(new string(reader.ReadChars(8)));

			reader.BaseStream.Position = dataStart + 0x00000008;
			obj.NumModels = reader.ReadInt16();
			obj.NumAnimations = reader.ReadInt16();

			reader.BaseStream.Position = dataStart + 0x00000030;
			short sectionA = reader.ReadInt16();
			short sectionB = reader.ReadInt16();
			short sectionC = reader.ReadInt16();

			obj.NumSections += (sectionA > 0 ? sectionA : (short)0);
			obj.NumSections += (sectionB > 0 ? sectionB : (short)0);
			obj.NumSections += (sectionC > 0 ? sectionC : (short)0);

			// There could be duplicate objects when unpacking. Read the data anyway for debugging.
			if (_objects.Find(x => x.ObjectName == obj.ObjectName) == null)
			{
				_objects.Add(obj);
			}
		}

		void ProcessLevel(Level level, BinaryReader reader, uint dataStart)
		{
			bool addingLevel = true;

			reader.BaseStream.Position = dataStart + 0x98;
			reader.BaseStream.Position = dataStart + reader.ReadUInt32();
			level.UnitName = CleanName(new string(reader.ReadChars(8)));
			reader.BaseStream.Position = dataStart + 0xF8;
			level.StreamUnitID = reader.ReadInt32();
			level.SourceUnitName = level.UnitName;
			reader.BaseStream.Position = dataStart + 0xF0;
			level.SourceVersion = reader.ReadUInt32();
			reader.BaseStream.Position = dataStart;
			uint terrainPos = reader.ReadUInt32();
			if (terrainPos != 0)
			{
				reader.BaseStream.Position = dataStart + terrainPos + 0x30;
				uint portalListPos = reader.ReadUInt32();
				if (portalListPos != 0)
				{
					reader.BaseStream.Position = dataStart + portalListPos;
					int numPortals = reader.ReadInt32();
					for (int p = 0; p < numPortals; p++)
					{
						string portalString = new string(reader.ReadChars(16));
						string destUnit = portalString.Substring(0, portalString.IndexOf(','));
						string destSignal = portalString.Substring(portalString.IndexOf(',') + 1);
						int signalID = reader.ReadInt32();
						int streamUnitID = reader.ReadInt32();
						Portal portal = new Portal();
						portal.SignalID = signalID;
						portal.DestUnitName = destUnit;
						portal.DestUnitID = streamUnitID;
						if (Int32.TryParse(destSignal, out int destSignalID))
						{
							portal.DestSignalID = destSignalID;
						}
						portal.OldDestUnitName = destUnit;
						portal.OldDestVersion = level.SourceVersion;
						level.Portals.Add(portal);
						reader.BaseStream.Position += 0x44;
					}
				}
			}

			// There could be duplicate levels when unpacking. Read the data anyway for debugging.
			if (_levels.Find(x => x.UnitName == level.UnitName) == null)
			{
				if (level.StreamUnitID >= _levels.NextAvailableID)
				{
					_levels.NextAvailableID = level.StreamUnitID + 1;
				}

				_levels.Add(level);
			}
			else
			{
				addingLevel = false;
			}

			#region Instances

			reader.BaseStream.Position = dataStart + 0x78;
			uint instanceCount = reader.ReadUInt32();
			uint instanceStart = dataStart + reader.ReadUInt32();

			for (int i = 0; i < instanceCount; i++)
			{
				Intro intro = new Intro();
				reader.BaseStream.Position = instanceStart + 0x4C * i;
				intro.ObjectName = CleanName(new String(reader.ReadChars(16)));
				intro.UnitName = level.UnitName;
				intro.StreamUnitID = level.StreamUnitID;
				reader.BaseStream.Position += 4;
				intro.IntroUniqueID = reader.ReadInt32();
				intro.Rotation.X = reader.ReadInt16();
				intro.Rotation.Y = reader.ReadInt16();
				intro.Rotation.Z = reader.ReadInt16();
				reader.BaseStream.Position += 4;
				intro.Position.X = reader.ReadInt16();
				intro.Position.Y = reader.ReadInt16();
				intro.Position.Z = reader.ReadInt16();

				if (addingLevel)
				{
					Intros.Add(intro);
				}
			}

			#endregion

			#region Events
			/*reader.BaseStream.Position = dataStart + 0xDC;
            uint eventPointersOffset = reader.ReadUInt16();
            reader.BaseStream.Position = dataStart + eventPointersOffset;
            int numPuzzles = reader.ReadInt32();
            for (int p = 0; p < numPuzzles; p++)
            {
                uint eventOffset = reader.ReadUInt32();
                uint nextEventPointer = (uint)reader.BaseStream.Position;

                reader.BaseStream.Position = dataStart + eventOffset;
                reader.BaseStream.Position += 0x02;

                short numInstances = reader.ReadInt16();
                reader.BaseStream.Position += 0x0C;

                for (int i = 0; i < 0; i++)
                {
                    uint instanceOffset = reader.ReadUInt32();
                    uint nextInstancePointer = (uint)reader.BaseStream.Position;

                    reader.BaseStream.Position = dataStart + instanceOffset;
                    short eventType = reader.ReadInt16();
                    // Do EventInstance stuff here.

                    reader.BaseStream.Position = nextInstancePointer;
                }

                reader.BaseStream.Position = nextEventPointer;
            }*/
			#endregion
		}

		public void EditPortal(string fromLevelName, string toLevelName, int fromSignalID, int toSignalID)
		{
			Level fromLevel = _levels.Find(x => x.UnitName == fromLevelName);
			Level toLevel = _levels.Find(x => x.UnitName == toLevelName);

			foreach (Portal portal in fromLevel.Portals.Portals)
			{
				if (portal.SignalID == fromSignalID)
				{
					portal.DestUnitName = toLevel.UnitName;
					portal.DestUnitID = toLevel.StreamUnitID;
					portal.DestSignalID = toSignalID;
					UpdateLevelPortals(fromLevel);
					break;
				}
			}

			foreach (Portal portal in toLevel.Portals.Portals)
			{
				if (portal.SignalID == toSignalID)
				{
					portal.DestUnitName = fromLevel.UnitName;
					portal.DestUnitID = fromLevel.StreamUnitID;
					portal.DestSignalID = fromSignalID;
					UpdateLevelPortals(toLevel);
					break;
				}
			}
		}

		void UpdateLevelPortals(Level level)
		{
			string fullPath = MakeLevelFilePath(level.UnitName, true);
			if (!File.Exists(fullPath))
			{
				return;
			}

			FileStream levelFile = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite);
			BinaryReader reader = new BinaryReader(levelFile, System.Text.Encoding.ASCII);
			BinaryWriter writer = new BinaryWriter(levelFile, System.Text.Encoding.ASCII);
			uint dataStart = ((reader.ReadUInt32() >> 9) << 11) + 0x00000800;

			reader.BaseStream.Position = dataStart;
			uint terrainPos = reader.ReadUInt32();
			if (terrainPos != 0)
			{
				reader.BaseStream.Position = dataStart + terrainPos + 0x30;
				uint portalListPos = reader.ReadUInt32();
				if (portalListPos != 0)
				{
					reader.BaseStream.Position = dataStart + portalListPos;
					int numPortals = reader.ReadInt32();
					for (int p = 0; p < numPortals; p++)
					{
						reader.BaseStream.Position = dataStart + portalListPos + 4 + (p * 0x5C);

						if (p < level.Portals.Count)
						{
							Portal portal = level.Portals.Portals[p];
							if (portal.DestUnitName != "")
							{
								char[] asChars = new char[16];
								string portalString = new string(reader.ReadChars(16)).Trim('\0');
								portalString = portal.DestUnitName + "," + portal.DestSignalID.ToString("D2");
								portalString.CopyTo(0, asChars, 0, Math.Min(portalString.Length, 16));
								writer.BaseStream.Position -= 16;
								writer.Write(asChars);
								writer.BaseStream.Position += 0x04;
								writer.Write(portal.DestUnitID);
							}
						}
					}
				}
			}

			reader.BaseStream.Position = dataStart + 0xD0;
			uint signalListStart = dataStart + reader.ReadUInt32();
			uint signalListEnd = dataStart + reader.ReadUInt32();
			reader.BaseStream.Position = signalListStart;

			while (reader.BaseStream.Position < signalListEnd)
			{
				int numSignals = reader.ReadInt32();
				reader.BaseStream.Position += 0x04;
				for (int s = 0; s < numSignals; s++)
				{
					int signalID = reader.ReadInt32();
					int signalLength = 0;

					switch ((SignalTypes)signalID)
					{
						case SignalTypes.HandleEnd:
						case SignalTypes.HandleStartPlayerControl:
						case SignalTypes.HandleStopPlayerControl:
						case SignalTypes.HandleResetSlideAngle:
							signalLength = 0;
							break;
						case SignalTypes.HandleLightGroup:
						case SignalTypes.HandleCameraAdjust:
						case SignalTypes.HandleCameraMode:
						case SignalTypes.HandleCameraKey:
						case SignalTypes.HandleCameraTimer:
						case SignalTypes.HandleCameraSmooth:
						case SignalTypes.HandleCameraLock:
						case SignalTypes.HandleCameraUnlock:
						case SignalTypes.HandleCameraSave:
						case SignalTypes.HandleCameraRestore:
						case SignalTypes.HandleFogNear:
						case SignalTypes.HandleFogFar:
						case SignalTypes.HandleCallSignal:
						case SignalTypes.HandleScreenWipe:
						case SignalTypes.HandleBlendStart:
						case SignalTypes.HandleScreenWipeColor:
						case SignalTypes.HandleSetSlideAngle:
						case SignalTypes.HandleSetCameraTilt:
						case SignalTypes.HandleSetCameraDistance:
							signalLength = 4;
							break;
						case SignalTypes.HandleCameraValue:
						case SignalTypes.HandleCameraShake:
						case SignalTypes.HandleCameraSpline:
							signalLength = 8;
							break;
						case SignalTypes.HandleStreamLevel:
							signalLength = 24;
							break;
					}

					if ((SignalTypes)signalID == SignalTypes.HandleStreamLevel)
					{
						int currentNum = reader.ReadInt32();
						int streamID = reader.ReadInt32();
						Portal portal = level.Portals.Portals.Find(x => x.SignalID == currentNum);
						string portalString = new string(reader.ReadChars(16)).Trim('\0');
						if (portal != null && portal.DestUnitName != "")
						{
							char[] asChars = new char[16];
							portalString = portal.DestUnitName + "," + portal.DestSignalID.ToString("D2");
							portalString.CopyTo(0, asChars, 0, Math.Min(portalString.Length, 16));
							writer.BaseStream.Position -= 20;
							writer.Write(portal.DestUnitID);
							writer.Write(asChars);
						}
					}
					else
					{
						reader.BaseStream.Position += signalLength;
					}
				}

				reader.BaseStream.Position += 0x04;
			}

			writer.Close();
			reader.Close();
			levelFile.Close();
		}

		public bool FindAvailableStreamUnitID(ref int streamUnitID)
		{
			Comparer<Level> comparer = Comparer<Level>.Create((levelA, levelB) => levelA.StreamUnitID - levelB.StreamUnitID);
			SortedSet<Level> sortedLevels = new SortedSet<Level>(comparer);
			sortedLevels.UnionWith(Levels.Levels);

			int levelID = short.MinValue;
			foreach (Level level in sortedLevels)
			{
				if (levelID == 0)
				{
					levelID++;
				}

				if (levelID < level.StreamUnitID)
				{
					streamUnitID = levelID;
					break;
				}

				// Look one place ahead of the current level's ID for the next one.
				levelID = level.StreamUnitID + 1;
			}

			return true;
		}

		public bool FindAvailableIntroIDs(ref int[] introIDs)
		{
			if (introIDs == null)
			{
				return false;
			}

			Comparer<Intro> comparer = Comparer<Intro>.Create((introA, introB) => introA.IntroUniqueID - introB.IntroUniqueID);
			SortedSet<Intro> sortedIntros = new SortedSet<Intro>(comparer);
			sortedIntros.UnionWith(Intros.Intros);

			int introID = 1; // Not sure if 0 would be valid. Start from 1 to be safe.
			int numFound = 0;
			foreach (Intro intro in sortedIntros)
			{
				// Got enough IDs. Exit.
				if (numFound >= introIDs.Length)
				{
					break;
				}

				while (introID < intro.IntroUniqueID)
				{
					// Keep getting IDs until the current intro's ID is reached.
					introIDs[numFound] = introID;
					introID++;
					numFound++;

					// Got enough IDs. Exit.
					if (numFound >= introIDs.Length)
					{
						break;
					}
				}

				// Look one place ahead of the current intro's ID for the next one.
				introID = intro.IntroUniqueID + 1;
			}

			return true;
		}

		public bool LoadRepository()
		{
			ClearReposititory();

			if (!Directory.Exists(_dataFolderName))
			{
				Console.WriteLine("Error: Cannot find data folder \"" + _dataFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_textureFolderName))
			{
				Console.WriteLine("Error: Cannot find texture folder \"" + _dataFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_sfxFolderName))
			{
				Console.WriteLine("Error: Cannot find sfx folder \"" + _sfxFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_outputFolderName))
			{
				Console.WriteLine("Error: Cannot find output folder \"" + _outputFolderName + "\".");
				return false;
			}

			if (!File.Exists(_sourceBigfileName))
			{
				Console.WriteLine("Error: Cannot find source bigfile \"" + _sourceBigfileName + "\".");
				return false;
			}

			if (!File.Exists(_sourceTexturesFileName))
			{
				Console.WriteLine("Error: Cannot find source texture file \"" + _sourceTexturesFileName + "\".");
				return false;
			}

			if (!File.Exists(_assetsFileName))
			{
				Console.WriteLine("Error: Cannot find asset file \"" + _assetsFileName + "\".");
				return false;
			}

			if (!File.Exists(_levelsFileName))
			{
				Console.WriteLine("Error: Cannot find level file \"" + _levelsFileName + "\".");
				return false;
			}

			if (!File.Exists(_introsFileName))
			{
				Console.WriteLine("Error: Cannot find intro file \"" + _introsFileName + "\".");
				return false;
			}

			if (!File.Exists(_objectsFileName))
			{
				Console.WriteLine("Error: Cannot find object file \"" + _objectsFileName + "\".");
				return false;
			}

			if (!File.Exists(_clipsFileName))
			{
				Console.WriteLine("Error: Cannot find clips file \"" + _clipsFileName + "\".");
				return false;
			}

			if (!File.Exists(_texturesFileName))
			{
				Console.WriteLine("Error: Cannot find texture file \"" + _texturesFileName + "\".");
				return false;
			}

			if (!File.Exists(_textureSetsFileName))
			{
				Console.WriteLine("Error: Cannot find texture set file \"" + _textureSetsFileName + "\".");
				return false;
			}

			try
			{
				string assetsFileData = File.ReadAllText(_assetsFileName, Encoding.ASCII);
				_assets = (AssetDescList)JsonSerializer.Deserialize(assetsFileData, typeof(AssetDescList));

				string levelsFileData = File.ReadAllText(_levelsFileName, Encoding.ASCII);
				_levels = (LevelList)JsonSerializer.Deserialize(levelsFileData, typeof(LevelList));

				string introsFileData = File.ReadAllText(_introsFileName, Encoding.ASCII);
				_intros = (IntroList)JsonSerializer.Deserialize(introsFileData, typeof(IntroList));

				string objectsFileData = File.ReadAllText(_objectsFileName, Encoding.ASCII);
				_objects = (ObjectList)JsonSerializer.Deserialize(objectsFileData, typeof(ObjectList));

				string clipsFileData = File.ReadAllText(_clipsFileName, Encoding.ASCII);
				_sfxClips = (SFXClipList)JsonSerializer.Deserialize(clipsFileData, typeof(SFXClipList));

				string texturesFileData = File.ReadAllText(_texturesFileName, Encoding.ASCII);
				_textures = (TexDescList)JsonSerializer.Deserialize(texturesFileData, typeof(TexDescList));

				string textureSetsFileData = File.ReadAllText(_textureSetsFileName, Encoding.ASCII);
				_textureSets = (TexSetList)JsonSerializer.Deserialize(textureSetsFileData, typeof(TexSetList));
			}
			catch (Exception)
			{
				ClearReposititory();
				Console.WriteLine("Error: Could not load the repository at \"" + _projectFolderName + "\".");
			}

			return true;
		}

		public bool SaveRepository()
		{

			if (!Directory.Exists(_dataFolderName))
			{
				Console.WriteLine("Error: Cannot find data folder \"" + _dataFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_textureFolderName))
			{
				Console.WriteLine("Error: Cannot find texture folder \"" + _dataFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_sfxFolderName))
			{
				Console.WriteLine("Error: Cannot find sfx folder \"" + _sfxFolderName + "\".");
				return false;
			}

			if (!Directory.Exists(_outputFolderName))
			{
				Console.WriteLine("Error: Cannot find output folder \"" + _outputFolderName + "\".");
				return false;
			}

			try
			{
				string assetsFileData = JsonSerializer.Serialize(_assets, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_assetsFileName, assetsFileData, Encoding.ASCII);

				string levelsFileData = JsonSerializer.Serialize(_levels, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_levelsFileName, levelsFileData, Encoding.ASCII);

				string introsFileData = JsonSerializer.Serialize(_intros, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_introsFileName, introsFileData, Encoding.ASCII);

				string clipsFileData = JsonSerializer.Serialize(_sfxClips, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_clipsFileName, clipsFileData, Encoding.ASCII);

				string objectsFileData = JsonSerializer.Serialize(_objects, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_objectsFileName, objectsFileData, Encoding.ASCII);

				string texturesFileData = JsonSerializer.Serialize(_textures, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_texturesFileName, texturesFileData, Encoding.ASCII);

				string textureSetsFileData = JsonSerializer.Serialize(_textureSets, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(_textureSetsFileName, textureSetsFileData, Encoding.ASCII);
			}
			catch (Exception)
			{
				Console.WriteLine("Error: Could not save the repository at \"" + _projectFolderName + "\".");
				return false;
			}

			return true;
		}

		void ClearReposititory()
		{
			_assets = null;
			_levels = null;
			_intros = null;
			_objects = null;
			_sfxClips = null;
			_textures = null;
			_textureSets = null;
		}

		public bool UnpackRepository()
		{
			ClearReposititory();

			FilesToRead = 0;
			FilesRead = 0;

			if (!File.Exists(_sourceBigfileName))
			{
				Console.WriteLine("Error: Cannot find source bigfile \"" + _sourceBigfileName + "\".");
				return false;
			}

			if (!File.Exists(_sourceTexturesFileName))
			{
				Console.WriteLine("Error: Cannot find source textures file \"" + _sourceTexturesFileName + "\".");
				return false;
			}

			try
			{
				_assets = new AssetDescList();
				_levels = new LevelList();
				_intros = new IntroList();
				_objects = new ObjectList();
				_sfxClips = new SFXClipList();
				_textures = new TexDescList();
				_textureSets = new TexSetList();

				FileStream sourceBigFile = new FileStream(_sourceBigfileName, FileMode.Open, FileAccess.Read);
				FileStream sourceTexturesFile = new FileStream(_sourceTexturesFileName, FileMode.Open, FileAccess.Read);

				if (!InitializeAssets(_assets, _textures))
				{
					return false;
				}

				FilesToRead = _assets.Count + _textures.Count;

				CreateDirectories();

				SortedList<int, SFXClip> sortedSFXClips = new SortedList<int, SFXClip>();
				FileStream allClipsFile = new FileStream(_allClipsFileName, FileMode.Create, FileAccess.ReadWrite);
				allClipsFile.Write(new byte[16], 0, 16);

				#region Bigfile

				foreach (AssetDesc asset in _assets.Assets)
				{
					sourceBigFile.Position = asset.FileOffset;

					string outputFileName = Path.Combine(_projectFolderName, asset.FilePath);
					string outputFileDirectory = Path.GetDirectoryName(outputFileName);
					Directory.CreateDirectory(outputFileDirectory);
					FileStream outputFile = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite);
					CopyTo(outputFile, sourceBigFile, (int)asset.FileLength);
					outputFile.Flush();

					#region Metadata
					string ext = Path.GetExtension(outputFileName);
					if (ext == ".pcm")
					{
						BinaryReader reader = new BinaryReader(outputFile, System.Text.Encoding.ASCII);
						reader.BaseStream.Position = 0;

						uint dataStart = ((reader.ReadUInt32() >> 9) << 11) + 0x00000800;

						bool isLevel = (reader.ReadUInt32() == 0x00000000);
						if (isLevel)
						{
							Level level = new Level();
							ProcessLevel(level, reader, dataStart);
						}
						else
						{
							Object obj = new Object();
							ProcessObject(obj, reader, dataStart);
						}
					}
					else if (ext == ".pmf")
					{
						BinaryReader reader = new BinaryReader(outputFile, System.Text.Encoding.ASCII);
						reader.BaseStream.Position = 0;

						uint header = reader.ReadUInt32();
						if (header == 0x61504D46 /*&& entry.FileHash == 0xf2c83bb8*/) // 0xf2c83bb8 for just raziel.pnf
						{
							reader.BaseStream.Position += 4;
							int clipCount = (int)reader.ReadUInt32();
							reader.BaseStream.Position = 16;

							for (int clipNum = 0; clipNum < clipCount; clipNum++)
							{
								long clipHeaderStart = reader.BaseStream.Position;

								ushort sfxID = reader.ReadUInt16();
								ushort waveID = reader.ReadUInt16();

								reader.BaseStream.Position += 16;
								long currentPosition = reader.BaseStream.Position;
								long currentClipLength = reader.ReadUInt32() - 4;
								long currentClipStart = reader.BaseStream.Position + 4;

								long nextClipStart = currentClipStart + currentClipLength;

								reader.BaseStream.Position = clipHeaderStart;
								byte[] clipBuffer = reader.ReadBytes((int)(nextClipStart - clipHeaderStart));
								SHA256 s256 = SHA256.Create();
								byte[] s256Hash = s256.ComputeHash(clipBuffer);
								string s256String = ByteArrayToHexString(s256Hash);

								//string outputClipFileName = Path.Combine(_sfxFolderName, "clip-" + sfxID + "-" + waveID + "-" + s256String + ".sfx");
								string outputClipFileName = Path.Combine(_sfxFolderName, "clip-" + sfxID + ".sfx");
								FileStream outputClipFile = new FileStream(outputClipFileName, FileMode.Create);
								outputClipFile.Write(clipBuffer, 0, clipBuffer.Length);
								outputClipFile.Close();

								if (!sortedSFXClips.ContainsKey(sfxID))
								{
									SFXClip clip = new SFXClip();
									clip.SFXID = sfxID;
									clip.SFXName = "clip-" + sfxID;
									sortedSFXClips.Add(clip.SFXID, clip);
								}

								allClipsFile.Write(clipBuffer, 0, clipBuffer.Length);

								Console.WriteLine("\tExtracted clip: \"" + outputClipFileName + "\"");

								reader.BaseStream.Position = nextClipStart;
							}
						}
					}

					#endregion

					outputFile.Close();

					Console.WriteLine("Extracted file: \"" + outputFileName + "\"");
					RecentMessage = (string)asset.FilePath.Clone();
					FilesRead++;
				}

				foreach (SFXClip clip in sortedSFXClips.Values)
				{
					_sfxClips.Add(clip);
				}

				#endregion

				#region Textures
				BinaryReader texturesReader = new BinaryReader(sourceTexturesFile);
				texturesReader.BaseStream.Position = headerLength;
				foreach (TexDesc texture in _textures.Textures)
				{
					string outputFileName = Path.Combine(_projectFolderName, texture.FilePath);
					Bitmap bitmap = ReadTexture(texturesReader);
					bitmap.Save(outputFileName, System.Drawing.Imaging.ImageFormat.Png);

					Console.WriteLine("Extracted file: \"" + outputFileName + "\"");
					RecentMessage = (string)texture.FilePath.Clone();
					FilesRead++;
				}
				texturesReader.Close();
				#endregion

				sourceBigFile.Close();
				sourceTexturesFile.Close();

				allClipsFile.Position = 0;
				allClipsFile.Write(BitConverter.GetBytes(0x61504D46), 0, 4);
				allClipsFile.Write(BitConverter.GetBytes((short)256), 0, 2);
				allClipsFile.Write(BitConverter.GetBytes((short)0), 0, 2);
				allClipsFile.Write(BitConverter.GetBytes((short)sortedSFXClips.Count), 0, 2);
				allClipsFile.Write(BitConverter.GetBytes((short)0), 0, 2);
				allClipsFile.Write(BitConverter.GetBytes(0x00000000), 0, 4);
				allClipsFile.Close();

				SaveRepository();

				Console.WriteLine("Extracted " + _assets.Count.ToString() + " files from \"" + _sourceBigfileName + "\".");
				Console.WriteLine("Extracted " + _textures.Count.ToString() + " files from \"" + _sourceTexturesFileName + "\".");
				Console.WriteLine("Discovered " + _levels.Count + " unique levels.");
				Console.WriteLine("Discovered " + _intros.Count + " unique intros.");
				Console.WriteLine("Discovered " + _sfxClips.Count + " unique sfxClips.");
				Console.WriteLine("Discovered " + _objects.Count + " unique objects.");
			}
			catch (Exception)
			{
				ClearReposititory();

				Console.WriteLine("Error: Couldn't unpack the repository.");
				FilesToRead = 0;
				FilesRead = 0;
				return false;
			}

			FilesToRead = 0;
			FilesRead = 0;
			return true;
		}

		public bool PackRepository()
		{
			FilesToRead = _assets.Count + _textures.Count;
			FilesRead = 0;

			try
			{
				FileStream outputBigFile = new FileStream(_outputBigFileName, FileMode.Create);
				FileStream outputTexturesFile = new FileStream(_outputTexturesFileName, FileMode.Create);

				// Sort by index. Offset isn't saved.
				SortedList<int, AssetDesc> sortedAssets = new SortedList<int, AssetDesc>();
				foreach (AssetDesc asset in _assets.Assets)
				{
					sortedAssets.Add(asset.FileIndex, asset);
				}

				#region Bigfile
				BinaryWriter bigFileWriter = new BinaryWriter(outputBigFile);
				bigFileWriter.Write(_assets.Count);

				uint fileIndexSize = 4u + (16u * (uint)_assets.Count);
				fileIndexSize += 0x00000800;
				fileIndexSize &= 0xFFFFF800;
				uint offset = fileIndexSize;
				foreach (AssetDesc entry in _assets.Assets)
				{
					bigFileWriter.Write(entry.FileHash);
					bigFileWriter.Write(entry.FileLength);
					bigFileWriter.Write(offset);
					bigFileWriter.Write(entry.Code.code);

					offset += entry.FileLength;

					while ((offset & 0xFFFF800) != offset)
					{
						offset++;
					}
				}

				while (((uint)bigFileWriter.BaseStream.Position & 0xFFFF800) != (uint)bigFileWriter.BaseStream.Position)
				{
					bigFileWriter.Write('\0');
				}

				bigFileWriter.Flush();

				foreach (AssetDesc asset in sortedAssets.Values)
				{
					string inputFileName = Path.Combine(_projectFolderName, asset.FilePath);
					FileStream inputFile = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
					CopyTo(outputBigFile, inputFile, (int)inputFile.Length);
					inputFile.Close();

					Console.WriteLine("Added file: \"" + inputFileName + "\"");
					RecentMessage = (string)asset.FilePath.Clone();
					FilesRead++;

					while (((uint)bigFileWriter.BaseStream.Position & 0xFFFF800) != (uint)bigFileWriter.BaseStream.Position)
					{
						bigFileWriter.Write('\0');
					}

					bigFileWriter.Flush();
				}
				#endregion

				#region Textures
				BinaryWriter texturesWriter = new BinaryWriter(outputTexturesFile);
				texturesWriter.Write((ushort)512);
				texturesWriter.Write((ushort)_textures.Count);
				texturesWriter.Write(new byte[headerLength - 4]);
				uint totalTextures = (uint)_textures.Count - 1;
				foreach (TexDesc texture in _textures.Textures)
				{
					string inputFileName = Path.Combine(_projectFolderName, texture.FilePath);
					Bitmap tempBitmap = new Bitmap(inputFileName);
					WriteTexture(texturesWriter, tempBitmap);

					Console.WriteLine("Added file: \"" + inputFileName + "\"");
					RecentMessage = (string)texture.FilePath.Clone();
					FilesRead++;
				}
				#endregion

				bigFileWriter.Close();
				outputBigFile.Close();

				Console.WriteLine("Packed " + _assets.Count.ToString() + " files into \"" + _outputBigFileName + "\".");
				Console.WriteLine("Packed " + _textures.Count.ToString() + " files into \"" + _outputTexturesFileName + "\".");
			}
			catch (Exception)
			{
				Console.WriteLine("Error: Couldn't pack the repository.");
				FilesToRead = 0;
				FilesRead = 0;
				return false;
			}

			FilesToRead = 0;
			FilesRead = 0;
			return true;
		}

		Bitmap ReadTexture(BinaryReader reader)
		{
			int rFactor = 3;
			int gFactor = 3;
			int bFactor = 3;

			Bitmap retBitmap = new Bitmap(textureWidth, textureHeight);

			for (int iGT = 0; iGT < textureHeight; iGT++)
			{
				for (int jGT = 0; jGT < textureWidth; jGT++)
				{
					ushort pixelData = reader.ReadUInt16();
					ushort a = pixelData;
					ushort r = pixelData;
					ushort g = pixelData;
					ushort b = pixelData;

					//separate out the channels
					a >>= 15;

					r <<= 1;
					r >>= 11;

					g <<= 6;
					g >>= 11;

					b <<= 11;
					b >>= 11;

					if (a > 0)
					{
						a = (ushort)255;
					}
					r = (ushort)(r << rFactor);
					g = (ushort)(g << gFactor);
					b = (ushort)(b << bFactor);

					Color colour = Color.FromArgb(a, r, g, b);
					retBitmap.SetPixel(jGT, iGT, colour);
				}
			}

			return retBitmap;
		}

		void WriteTexture(BinaryWriter writer, Bitmap bitmap)
		{
			int aFactor = 1;
			int rFactor = 3;
			int gFactor = 3;
			int bFactor = 3;

			for (int iGT = 0; iGT < textureHeight; iGT++)
			{
				for (int jGT = 0; jGT < textureWidth; jGT++)
				{
					Color colour = bitmap.GetPixel(jGT, iGT);
					ushort a = (ushort)(colour.A >> aFactor);
					ushort r = (ushort)(colour.R >> rFactor);
					ushort g = (ushort)(colour.G >> gFactor);
					ushort b = (ushort)(colour.B >> bFactor);

					a <<= 15;
					r <<= 10;
					g <<= 5;

					ushort pixelData = (ushort)(a | r | g | b);

					writer.Write(pixelData);
				}
			}
		}

		public string MakeObjectFilePath(string objectName, bool absolute = false)
		{
			string folderName = objectName.TrimEnd("0123456789".ToCharArray());
			string parentFolder = Path.GetFileName(_dataFolderName);
			string filePath = Path.Combine(parentFolder, "object", folderName, objectName + ".pcm");

			if (absolute)
			{
				filePath = Path.Combine(_projectFolderName, filePath);
			}

			return filePath;
		}

		public string MakeLevelFilePath(string areaName, bool absolute = false)
		{
			string folderName = areaName.TrimEnd("0123456789".ToCharArray());
			string parentFolder = Path.GetFileName(_dataFolderName);
			string filePath = Path.Combine(parentFolder, "area", folderName, "bin", areaName + ".pcm");

			if (absolute)
			{
				filePath = Path.Combine(_projectFolderName, filePath);
			}

			return filePath;
		}

		public string MakeTextureFilePath(int textureIndex, bool absolute = false)
		{
			string parentFolder = Path.GetFileName(_textureFolderName);
			string fileName = "texture-" + ZeroFill(textureIndex.ToString(), 5) + ".png";
			string filePath = Path.Combine(parentFolder, fileName);

			if (absolute)
			{
				filePath = Path.Combine(_projectFolderName, filePath);
			}

			return filePath;
		}

		static void CopyTo(Stream destination, Stream source, int length)
		{
			byte[] buffer = new byte[length];
			source.Read(buffer, 0, length);
			destination.Write(buffer, 0, length);
		}

		static string ByteArrayToHexString(byte[] inputArray)
		{
			StringBuilder builder = new StringBuilder();

			foreach (byte b in inputArray)
			{
				builder.Append(string.Format("{0:X2}", b).ToUpper());
			}

			return builder.ToString();
		}

		static String CleanName(String name)
		{
			if (name == null)
			{
				return "";
			}

			int index = name.IndexOfAny(new char[] { '\0' });
			if (index >= 0)
			{
				name = name.Substring(0, index);
			}

			name = name.Trim();
			return name.TrimEnd(new char[] { '_' });
		}

		public static uint GetSR1HashName(string fileName)
		{
			int charsRead = 0, x1 = 0, x2 = 0;
			int currentLetter = 0, hashName = 0, index = 0, extID = 0;
			string extension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
			string[] extensions = { "pcm", "crm", "tim", "smp", "snd", "smf", "snf" };
			for (index = 0; index < 7; index++)
			{
				if (extension.Equals(extensions[index]))
				{
					extID = index;
					break;
				}
			}
			if (index < 7) index = fileName.Length - 5;
			else index = fileName.Length - 1;
			while (index >= 0)
			{
				currentLetter = (int)fileName[index];
				if (currentLetter >= 'a' && currentLetter <= 'z')
				{
					currentLetter &= 0xDF;
				}
				if (currentLetter == '\\')
				{
					index--;
					continue;
				}
				currentLetter += 0xE6;
				currentLetter &= 0xFF;
				x1 = charsRead;
				x1 *= currentLetter;
				x2 += currentLetter;
				hashName ^= x1;
				charsRead++;
				index--;
			}
			charsRead <<= 0x0C;
			charsRead |= x2;
			charsRead <<= 0x0C;
			hashName |= charsRead;
			hashName <<= 0x03;
			hashName |= extID;
			return (uint)hashName;
		}

		static string ZeroFill(string origVal, int length)
		{
			string retString;

			retString = origVal;

			do
				retString = "0" + retString;
			while (retString.Length < length);

			return retString;
		}
	}
}