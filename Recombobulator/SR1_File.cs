using System;
using System.Collections.Generic;
using System.IO;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
	class SR1_File
	{
		public const UInt32 PROTO_19981025_VERSION = 0x00000000;
		public const UInt32 ALPHA_19990123_VERSION_1_X = 0x3c204127;
		public const UInt32 ALPHA_19990123_VERSION_1 = 0x3c204128;
		public const UInt32 ALPHA_19990204_VERSION_2 = 0x3c204129;
		public const UInt32 ALPHA_19990216_VERSION_3 = 0x3c204131;
		public const UInt32 BETA_19990512_VERSION = 0x3c204139;
		public const UInt32 RETAIL_VERSION = 0x3C20413B;

		public enum Version
		{
			First = 0,
			Alpha_1_X,
			Alpha_1,
			Alpha_2,
			Feb16,
			May12,
			Jun01,
			Jun10,
			Jun18,
			Jul14,
			Retail_PC, // No unique ID. Detect from TextureFT3.
			Next
		}

		public enum ImportFlags : int
		{
			None = 0,
			LogErrors = 1,
			LogScripts = 2,
			DetectPCRetail = 4,
		}

		public enum MigrateFlags : int
		{
			None = 0,
			RemoveEvents = 1,
			RemoveSignals = 2,
			RemovePortals = 4,
			RemoveVertexMorphs = 8,
			ForceWaterTranslucent = 16,
		}

		public enum TestFlags : int
		{
			None = 0,
			ListAllFiles = 1,
			ListObjectTypes = 2,
			ListRelocModules = 4,
			IgnoreDuplicates = 8,
		}

		public class Overrides
		{
			public string NewName;
			public ushort[] NewTextureIDs;
			public int NewStreamUnitID;
			public int[] NewIntroIDs;
			public readonly Dictionary<string, string> NewObjectNames = new Dictionary<string, string>();
			public int NextIntroID;
		}

		public string _FilePath { get; private set; } = "";
		public uint _FileLength { get; private set; } = 0;
		public Version _Version { get; private set; } = SR1_File.Version.Jun01;
		public bool _IsLevel { get; private set; }
		public readonly SortedList<uint, SR1_Structure> _Structures = new SortedList<uint, SR1_Structure>();
		public readonly SortedDictionary<uint, SR1_PrimativeBase> _Primatives = new SortedDictionary<uint, SR1_PrimativeBase>();
		public readonly SortedDictionary<uint, SR1_Structure> _MigrationStructures = new SortedDictionary<uint, SR1_Structure>();
		public List<SR1_PointerBase> _Pointers = new List<SR1_PointerBase>();
		public readonly List<ushort> _TextureIDs = new List<ushort>();
		public readonly List<string> _IntroNames = new List<string>();
		public readonly List<int> _IntroIDs = new List<int>();
		public readonly List<string> _ObjectNames = new List<string>();
		public readonly StringWriter _ImportErrors = new StringWriter();
		public readonly StringWriter _Scripts = new StringWriter();
		public SR1_PrimativeBase _LastPrimative = null;
		public Overrides _Overrides { get; private set; }
		public ImportFlags _ImportFlags { get; private set; } = ImportFlags.None;

		public void Import(string fileName, ImportFlags flags = ImportFlags.None | ImportFlags.DetectPCRetail)
		{
			_ImportFlags = flags;
			_FilePath = fileName;

			FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			Import(file);
			file.Close();
		}

		private void Import(Stream inputStream)
		{
			_Structures.Clear();
			_Primatives.Clear();
			_MigrationStructures.Clear();
			_Pointers.Clear();
			_TextureIDs.Clear();
			_IntroNames.Clear();
			_IntroIDs.Clear();
			_ObjectNames.Clear();

			_LastPrimative = null;

			_ImportErrors.GetStringBuilder().Clear();
			_Scripts.GetStringBuilder().Clear();

			MemoryStream dataStream = new MemoryStream();
			BinaryWriter dataWriter = new BinaryWriter(dataStream, System.Text.Encoding.UTF8, true);
			SR1_Reader dataReader = new SR1_Reader(this, dataStream, System.Text.Encoding.UTF8, true);

			using (BinaryReader inputReader = new BinaryReader(inputStream, System.Text.Encoding.UTF8, true))
			{
				uint dataStart = ((inputReader.ReadUInt32() >> 9) << 11) + 0x00000800;
				_FileLength = (uint)inputStream.Length - dataStart;

				_IsLevel = inputReader.ReadUInt32() == 0;

				inputReader.BaseStream.Position = dataStart;
				dataWriter.BaseStream.Position = 0;
				inputReader.BaseStream.CopyTo(dataWriter.BaseStream);
				dataWriter.BaseStream.Position = 0;
			}

			SR1_Structure root;

			if (_IsLevel)
			{
				bool validVersion = false;

				if (!validVersion && ((_ImportFlags & ImportFlags.DetectPCRetail) != 0))
				{
					dataReader.BaseStream.Position = 0x9C;
					if (dataReader.ReadUInt64() == 0xFFFFFFFFFFFFFFFF)
					{
						_Version = Version.Retail_PC;
						validVersion = true;
					}
				}

				if (!validVersion)
				{
					dataReader.BaseStream.Position = 0xF0;
					UInt32 version = dataReader.ReadUInt32();
					if (!validVersion && version == RETAIL_VERSION)
					{
						_Version = Version.Jun01;
						validVersion = true;
					}

					if (!validVersion && version == BETA_19990512_VERSION)
					{
						_Version = Version.May12;
						validVersion = true;
					}

					dataReader.BaseStream.Position = 0xE4;
					version = dataReader.ReadUInt32();

					if (!validVersion && version == ALPHA_19990216_VERSION_3)
					{
						_Version = Version.Feb16;
						validVersion = true;
					}
				}

				if (!validVersion)
				{
					_Version = Version.Jun01;
					validVersion = true;
				}

				root = new SR1Structures.Level();
			}
			else
			{
				bool validVersion = false;

				if (!validVersion)
				{
					dataReader.BaseStream.Position = 0x2C;
					UInt32 oflags2 = dataReader.ReadUInt32();
					if ((oflags2 & 0x00080000) != 0)
					{
						dataReader.BaseStream.Position = 0x1C;
						UInt32 dataPos = dataReader.ReadUInt32();
						if (dataPos != 0)
						{
							dataReader.BaseStream.Position = dataPos;
							UInt32 magicNum = dataReader.ReadUInt32();
							if (magicNum == 0xACE00065)
							{
								_Version = Version.Jul14;
								validVersion = true;
							}
							else if (magicNum == 0xACE00064)
							{
								_Version = Version.Jun10;
								validVersion = true;
							}
							else if (magicNum == 0xACE00063)
							{
								_Version = Version.Jun01;
								validVersion = true;
							}
							else if (magicNum == 0xACE00062)
							{
								_Version = Version.May12;
								validVersion = true;
							}
							else if (magicNum == 0xACE0005C)
							{
								_Version = Version.Feb16;
								validVersion = true;
							}
						}
					}
					else
					{
						dataWriter.BaseStream.Position = 0x24;
						uint namePos = dataReader.ReadUInt32();
						if (namePos == 0x00000044)
						{
							_Version = Version.Feb16;
							validVersion = true;
						}

						if (!validVersion)
						{
							dataReader.BaseStream.Position = 0x50;
							char[] objectName = dataReader.ReadChars(8);
							string objectNameStr = new string(objectName);
							if (objectNameStr == "flamegs_")
							{
								_Version = Version.Feb16;
								validVersion = true;
							}
						}

						if (!validVersion)
						{
							dataReader.BaseStream.Position = 0x4C;
							char[] objectName = dataReader.ReadChars(8);
							string objectNameStr = new string(objectName);
							if (objectNameStr == "particle")
							{
								dataReader.BaseStream.Position = 0x0424;
								if (dataReader.ReadUInt32() == 0x00000440 &&
									dataReader.ReadUInt32() == 0x00003000 &&
									dataReader.ReadUInt32() == 0x00003150 &&
									dataReader.ReadUInt32() == 0x0000355C &&
									dataReader.ReadUInt32() == 0x00003B60 &&
									dataReader.ReadUInt32() == 0x00003EE8 &&
									dataReader.ReadUInt32() == 0x00003EA0)
								{
									_Version = Version.May12;
								}
								else
								{
									_Version = Version.Jun01;
								}

								validVersion = true;
							}
						}
					}
				}

				if (!validVersion)
				{
					_Version = Version.Jun01;
					validVersion = true;
				}

				root = new SR1Structures.Object();
			}

			dataReader.BaseStream.Position = 0;

			root.Read(dataReader, null, "");

			if (_IsLevel && (_ImportFlags & ImportFlags.LogScripts) != 0)
			{
				dataReader.ScriptParser.ParseAll(dataReader);
			}

			dataReader.Dispose();
			dataWriter.Dispose();
			dataStream.Close();
		}

		public void Export(string fileName)
		{
			Export(fileName, _Version, MigrateFlags.None, new Overrides());
		}

		public void Export(string fileName, Version targetVersion, MigrateFlags migrateFlags, Overrides overrides)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(fileName));
			FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
			Export(file, targetVersion, migrateFlags, overrides);
			Import(file);
			file.Close();
		}

		private void Export(Stream outputStream, Version targetVersion, MigrateFlags migrateFlags, Overrides overrides)
		{
			_Overrides = overrides;

			MemoryStream dataStream = new MemoryStream();

			BinaryWriter outputWriter = new BinaryWriter(outputStream, System.Text.Encoding.UTF8, true);
			SR1_Writer dataWriter = new SR1_Writer(this, dataStream, true);

			_Primatives.Clear();
			_MigrationStructures.Clear();
			_Pointers.Clear();
			_TextureIDs.Clear();
			_IntroNames.Clear();
			_IntroIDs.Clear();
			_ObjectNames.Clear();

			_LastPrimative = null;

			SR1_Structure[] structures = new SR1_Structure[_Structures.Values.Count];
			_Structures.Values.CopyTo(structures, 0);
			foreach (SR1_Structure structure in structures)
			{
				structure.MigrateVersion(this, targetVersion, migrateFlags);
			}

			_Version = targetVersion;

			foreach (KeyValuePair<uint, SR1_Structure> entry in _Structures)
			{
				entry.Value.Write(dataWriter);
			}

			List<uint> sortedPrimativeKeys = new List<uint>(_Primatives.Keys);
			List<uint> sortedStructureKeys = new List<uint>(_Structures.Keys);

			outputWriter.Write(_Pointers.Count);

			foreach (SR1_PointerBase pointer in _Pointers)
			{
				if (pointer.Offset != 0)
				{
					outputWriter.Write(pointer.NewStart);
				}
			}

			uint dataStart = (uint)outputWriter.BaseStream.Position;
			uint mod = dataStart % 0x800;
			if (mod > 0)
			{
				uint padding = 0x800 - mod;
				dataStart += padding;
			}

			while (outputWriter.BaseStream.Position < dataStart)
			{
				outputWriter.Write((sbyte)0);
			}

			foreach (SR1_PointerBase pointer in _Pointers)
			{
				if (_MigrationStructures.ContainsKey(pointer.Offset))
				{
					dataWriter.BaseStream.Position = pointer.NewStart;
					dataWriter.Write(_MigrationStructures[pointer.Offset].NewStart);
					continue;
				}

				if (pointer.Offset == _LastPrimative.End)
				{
					uint newOffset = _LastPrimative.NewEnd + (pointer.Offset - _LastPrimative.End);
					dataWriter.BaseStream.Position = pointer.NewStart;
					dataWriter.Write(newOffset);
				}
				else
				{
					int index = sortedPrimativeKeys.BinarySearch(pointer.Offset);
					if (index < 0)
					{
						index = (~index - 1);
					}

					if (index >= 0)
					{
						uint newOffset = 0;
						SR1_PrimativeBase primative = _Primatives[sortedPrimativeKeys[index]];
						if (pointer.Offset >= primative.Start && pointer.Offset < primative.End)
						{
							newOffset = primative.NewStart + (pointer.Offset - primative.Start);
						}
						else
						{
							index = sortedStructureKeys.BinarySearch(pointer.Offset);
							if (index >= 0)
							{
								SR1_Structure structure = _Structures[sortedStructureKeys[index]];
								if (pointer.Offset == structure.Start)
								{
									newOffset = structure.NewStart;
								}
							}
						}

						dataWriter.BaseStream.Position = pointer.NewStart;
						dataWriter.Write(newOffset);
					}
				}
			}

			dataStream.WriteTo(outputStream);
			outputStream.Flush();

			dataWriter.Dispose();
			outputWriter.Dispose();

			dataStream.Close();

			outputStream.Position = 0;

			_Overrides = null;
		}

		public bool TestExport()
		{
			bool result = true;

			MemoryStream stream = new MemoryStream();

			Export(stream, _Version, MigrateFlags.None, new Overrides());

			if (File.Exists(_FilePath))
			{
				FileStream compareFile = new FileStream(_FilePath, FileMode.Open, FileAccess.Read);
				if (stream.Length != compareFile.Length)
				{
					result = false;
				}
				else
				{
					long fileLength = Math.Min(stream.Length, compareFile.Length);
					for (long i = 0; i < fileLength; i++)
					{
						if (stream.ReadByte() != compareFile.ReadByte())
						{
							result = false;
						}
					}
				}
				compareFile.Close();
			}
			else
			{
				result = false;
			}

			stream.Close();

			return result;
		}

		public TreeList.Node[] CreateChunkNodes()
		{
			List<TreeList.Node> nodeList = new List<TreeList.Node>();
			foreach (KeyValuePair<uint, SR1_Structure> entry in _Structures)
			{
				uint lookUpID = entry.Key;
				SR1_Structure structure = entry.Value;

				if (lookUpID > 0 && structure.Parent != null)
				{
					continue;
				}

				nodeList.Add(structure.CreateNode());
			}

			TreeList.Node dummyNode = new TreeList.Node(new string[] { "", "", "", "" });
			nodeList.Add(dummyNode);

			TreeList.Node[] nodes = nodeList.ToArray();

			return nodes;
		}

		public string GetScripts()
		{
			return _Scripts.ToString();
		}

		public string GetErrors()
		{
			return _ImportErrors.ToString();
		}

		public static string[] TestFolder(string folderName, TestFlags flags, ref int filesRead, ref int filesToRead, ref string recentMessage)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
			FileInfo[] fileInfos = directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
			fileInfos = Array.FindAll(fileInfos, info => (info.Extension == ".pcm" || info.Extension == ".drm"));

			List<string> physObs = new List<string>();
			List<string> genericTunes = new List<string>();
			List<string> monAttributes = new List<string>();
			List<string> monFuncTables = new List<string>();
			List<string> results = new List<string>();
			int numSucceeded = 0;
			int numSkipped = 0;

			System.Threading.Interlocked.Exchange(ref filesRead, 0);
			System.Threading.Interlocked.Exchange(ref filesToRead, fileInfos.Length);

			foreach (FileInfo fileInfo in fileInfos)
			{
				string cleanName = Path.GetFileNameWithoutExtension(fileInfo.Name).PadRight(20);
				if ((flags & TestFlags.IgnoreDuplicates) != 0 && cleanName.Contains("duplicate"))
				{
					System.Threading.Interlocked.Increment(ref filesRead);
					numSkipped++;
					continue;
				}

				System.Threading.Interlocked.Exchange(ref recentMessage, (string)fileInfo.Name.Clone());

				string fileDesc = fileInfo.Name.PadRight(20) + "(Size = 0xFFFFFFFF bytes)";

				try
				{
					SR1_File file = new SR1_File();
					file.Import(fileInfo.FullName, ImportFlags.LogErrors);
					fileDesc = fileInfo.Name.PadRight(20) + "(Size = 0x" + file._FileLength.ToString("X8") + " bytes)";

					if (file.TestExport())
					{
						if ((flags & TestFlags.ListAllFiles) != 0)
						{
							results.Add(fileDesc + " - Success");
						}

						numSucceeded++;
					}
					else
					{
						results.Add(fileDesc + " - Fail");
					}

					if (!file._IsLevel)
					{
						if ((flags & TestFlags.ListObjectTypes) != 0)
						{
							SR1Structures.Object obj = (SR1Structures.Object)file._Structures[0];
							SR1_Structure data = file._Structures[obj.data.Offset];
							if (data is PhysObPropertiesBase)
							{
								PhysObPropertiesBase physOb = (PhysObPropertiesBase)data;
								physObs.Add("\t" + cleanName + "\t{ oflags = " + obj.oflags.ToString() + ", oflags2 = " + obj.oflags2.ToString() + ", physOb.Properties.Type = " + physOb.Properties.ID.ToString() + " }");
							}
							else if (data is GenericTune)
							{
								GenericTune genericTune = (GenericTune)data;
								genericTunes.Add("\t" + cleanName + "\t{ oflags = " + obj.oflags.ToString() + ", oflags2 = " + obj.oflags2.ToString() + ", genericTune.flags = " + genericTune.flags.ToString() + " }");
							}
							else if (data is MonsterAttributes)
							{
								MonsterAttributes monsterAttributes = (MonsterAttributes)data;
								monAttributes.Add("\t" + cleanName + "\t{ oflags = " + obj.oflags.ToString() + ", oflags2 = " + obj.oflags2.ToString() + ", monsterAttributes.magicNum = " + monsterAttributes.magicnum.ToString() + ", monsterAttributes.whatAmI = " + monsterAttributes.whatAmI.ToString() + " }");
							}
						}

						if ((flags & TestFlags.ListRelocModules) != 0)
						{
							SR1Structures.Object obj = (SR1Structures.Object)file._Structures[0];
							if (obj.relocModule.Offset != 0)
							{
								SR1_Structure relocModule = file._Structures[obj.relocModule.Offset];
								if (relocModule is MonsterFunctionTable)
								{
									MonsterFunctionTable mft = (MonsterFunctionTable)relocModule;
									string relocStart = "Start = 0x" + mft.stateChoices.Start.ToString("X8");
									string relocEnd = "End = 0x" + mft.stateChoices.End.ToString("X8");
									string relocSize = "Size = 0x" + (mft.stateChoices.End - mft.stateChoices.Start).ToString("X8");
									monFuncTables.Add(
										"\t" + cleanName + "\t{ MonsterFunctionTable (" +
										relocStart + ", " +
										relocEnd + ", " +
										relocSize + ") }");
								}
							}
						}
					}
				}
				catch
				{
					results.Add(fileDesc + " - Error");
				}

				System.Threading.Interlocked.Increment(ref filesRead);
			}

			if ((flags & TestFlags.ListObjectTypes) != 0)
			{
				results.Add("\r\nPhysObs:");
				results.AddRange(physObs);
				results.Add("\r\nGenericTunes:");
				results.AddRange(genericTunes);
				results.Add("\r\nMonsterAttibutes:");
				results.AddRange(monAttributes);
				results.Add("");
			}

			if ((flags & TestFlags.ListRelocModules) != 0)
			{
				results.Add("\r\nMonsterFunctionTables:");
				results.AddRange(monFuncTables);
				results.Add("");
			}

			//results.Add("\r\nCollectibles:");
			//results.AddRange(collectibles);
			//results.Add("");

			results.Add("Files Read: " + (filesRead - numSkipped));
			results.Add("Succeeded: " + numSucceeded);
			results.Add("Failed: " + (filesRead - numSkipped - numSucceeded));

			return results.ToArray();
		}
	}
}
