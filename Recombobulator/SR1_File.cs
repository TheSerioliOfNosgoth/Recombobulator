﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
	public class SR1_File
	{
		public const UInt32 PROTO_19981025_VERSION = 0x00000000;
		public const UInt32 ALPHA_19990123_VERSION_1_X = 0x3c204127;
		public const UInt32 ALPHA_19990123_VERSION_1 = 0x3c204128;
		public const UInt32 ALPHA_19990204_VERSION_2 = 0x3c204129;
		public const UInt32 ALPHA_19990216_VERSION_3 = 0x3c204131;
		public const UInt32 ALPHA_19990414_VERSION_4 = 0x3c204137;
		public const UInt32 BETA_19990512_VERSION = 0x3c204139;
		public const UInt32 RETAIL_VERSION = 0x3C20413B;

		public enum Version
		{
			Detect = -1,
			Detect_Retail_PC = -2,
			First = 0,
			Alpha_1_X,
			Alpha_1,
			Feb04,
			Feb16,
			Apr14,
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
		}

		public enum MigrateFlags : int
		{
			None = 0,
			RemoveEvents = 1,
			RemoveSignals = 2,
			RemovePortals = 4,
			RemoveVertexMorphs = 8,
			RemoveAnimatedTextures = 16,
			ForceWaterTranslucent = 32,
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
			public readonly Dictionary<ushort, ushort> NewTextureIDs = new Dictionary<ushort, ushort>();
			public int OldStreamUnitID;
			public int NewStreamUnitID;
			public readonly Dictionary<int, int> NewIntroIDs = new Dictionary<int, int>();
			public readonly Dictionary<string, string> NewObjectNames = new Dictionary<string, string>();
		}

		public class ExportData
		{
			public readonly SortedDictionary<uint, SR1_PrimativeBase> writtenStart = new SortedDictionary<uint, SR1_PrimativeBase>();
			public readonly List<uint> writtenStartKeys = new List<uint>();
			public readonly SortedDictionary<uint, SR1_PrimativeBase> readEnd = new SortedDictionary<uint, SR1_PrimativeBase>();
			public readonly List<uint> readEndKeys = new List<uint>();

			public ExportData(List<SR1_PrimativeBase> primsRead, List<SR1_PrimativeBase> primsWritten)
			{
				foreach (var read in primsRead)
				{
					readEnd.Add(read.End, read);
				}
				readEndKeys.AddRange(readEnd.Keys);

				foreach (var written in primsWritten)
				{
					writtenStart.Add(written.Start, written);
				}
				writtenStartKeys.AddRange(writtenStart.Keys);
			}
		}

		public string _FilePath { get; private set; } = "";
		public uint _FileLength { get; private set; } = 0;
		public Version _Version { get; private set; } = SR1_File.Version.Jun01;
		public bool _IsLevel { get; private set; }
		public readonly SortedList<uint, SR1_Structure> _Structures = new SortedList<uint, SR1_Structure>();
		public readonly List<SR1_PrimativeBase> _PrimsWritten = new List<SR1_PrimativeBase>();
		public readonly List<SR1_PrimativeBase> _PrimsRead = new List<SR1_PrimativeBase>();
		public readonly SortedDictionary<uint, SR1_Structure> _MigrationStructures = new SortedDictionary<uint, SR1_Structure>();
		public List<SR1_PointerBase> _Pointers = new List<SR1_PointerBase>();
		public readonly List<ushort> _TextureIDs = new List<ushort>();
		public readonly List<string> _IntroNames = new List<string>();
		public readonly List<int> _IntroIDs = new List<int>();
		public readonly List<string> _ObjectNames = new List<string>();
		public readonly StringWriter _ImportErrors = new StringWriter();
		public readonly StringWriter _Scripts = new StringWriter();
		public bool IsWritingMigStruct = false;
		public Overrides _Overrides { get; private set; }
		public ImportFlags _ImportFlags { get; private set; } = ImportFlags.None;

		public void Reset()
		{
			_FileLength = 0;
			_Version = SR1_File.Version.Jun01;
			_IsLevel = false;
			_Structures.Clear();
			_PrimsRead.Clear();
			_PrimsWritten.Clear();
			_MigrationStructures.Clear();
			_Pointers.Clear();
			_TextureIDs.Clear();
			_IntroNames.Clear();
			_IntroIDs.Clear();
			_ObjectNames.Clear();
			_ImportErrors.GetStringBuilder().Clear();
			_Scripts.GetStringBuilder().Clear();
			_Overrides = null;
		}

		public void Import(string fileName, ImportFlags flags = ImportFlags.None, Version forcedVersion = Version.Detect_Retail_PC)
		{
			_ImportFlags = flags;
			_FilePath = fileName;

			FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			Import(file, forcedVersion);
			file.Close();
		}

		private void Import(Stream inputStream, Version forcedVersion = Version.Detect)
		{
			Reset();

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
				if (forcedVersion == Version.Detect || forcedVersion == Version.Detect_Retail_PC)
				{
					bool validVersion = false;

					if (!validVersion && forcedVersion == Version.Detect_Retail_PC)
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

						if (!validVersion)
						{
							dataReader.BaseStream.Position = 0xE8;
							version = dataReader.ReadUInt32();
						}

						if (!validVersion && version == ALPHA_19990414_VERSION_4)
						{
							_Version = Version.Apr14;
							validVersion = true;
						}

						if (!validVersion)
						{
							dataReader.BaseStream.Position = 0xE4;
							version = dataReader.ReadUInt32();
						}

						if (!validVersion && version == ALPHA_19990216_VERSION_3)
						{
							_Version = Version.Feb16;
							validVersion = true;
						}

						if (!validVersion)
						{
							dataReader.BaseStream.Position = 0xE0;
							version = dataReader.ReadUInt32();
						}

						if (!validVersion && version == ALPHA_19990204_VERSION_2)
						{
							_Version = Version.Feb04;
							validVersion = true;
						}
					}

					if (!validVersion)
					{
						_Version = Version.Jun01;
						validVersion = true;
					}
				}
				else
				{
					_Version = forcedVersion;
				}

				root = new SR1Structures.Level();
			}
			else
			{
				if (forcedVersion == Version.Detect || forcedVersion == Version.Detect_Retail_PC)
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
								else if (magicNum == 0xACE00060)
								{
									_Version = Version.Apr14;
									validVersion = true;
								}
								else if (magicNum == 0xACE0005C)
								{
									_Version = Version.Feb16;
									validVersion = true;
								}
								else if (magicNum == 0xACE0005B)
								{
									_Version = Version.Feb04;
									validVersion = true;
								}
							}
						}
						else
						{
							dataReader.BaseStream.Position = 0x24;
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
								else if (objectNameStr == "force___")
								{
									dataReader.BaseStream.Position = 0x1730;
									if (dataReader.BaseStream.Length >= 0x173C &&
										dataReader.ReadUInt16() == 0x2FDD &&
										dataReader.ReadUInt16() == 0x0007 &&
										dataReader.ReadUInt16() == 0xB00B &&
										dataReader.ReadUInt16() == 0x8000 &&
										dataReader.ReadUInt32() == 0x0000000C)
									{
										_Version = Version.Apr14;
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
				}
				else
				{
					_Version = forcedVersion;
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
			FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
			Export(file, targetVersion, migrateFlags, overrides);
			Import(file);
			file.Close();
		}

		private void Export(Stream outputStream, Version targetVersion, MigrateFlags migrateFlags, Overrides overrides)
		{
			_Overrides = overrides;

			_PrimsWritten.Clear();
			_MigrationStructures.Clear();
			_Pointers.Clear();
			_TextureIDs.Clear();
			_IntroNames.Clear();
			_IntroIDs.Clear();
			_ObjectNames.Clear();

			BinaryWriter outputWriter = new BinaryWriter(outputStream, System.Text.Encoding.UTF8, true);
			MemoryStream dataStream = new MemoryStream();
			SR1_Writer dataWriter = new SR1_Writer(this, dataStream, true);

			MigrateVersion(targetVersion, migrateFlags);

			WriteBody(dataWriter);

			List<SR1_PointerBase> validPointers = ResolvePointers(dataWriter);
			WriteHeader(outputWriter, validPointers);

			dataStream.WriteTo(outputStream);
			dataWriter.Dispose();
			dataStream.Close();

			outputStream.Flush();
			outputWriter.Dispose();

			outputStream.Position = 0;

			_Overrides = null;
		}

		private void MigrateVersion(Version targetVersion, MigrateFlags migrateFlags)
		{
			SR1_Structure[] structures = new SR1_Structure[_Structures.Values.Count];
			_Structures.Values.CopyTo(structures, 0);
			foreach (SR1_Structure structure in structures)
			{
				structure.MigrateVersion(this, targetVersion, migrateFlags);
			}

			_Version = targetVersion;
		}

		private void WriteHeader(BinaryWriter wtr, List<SR1_PointerBase> ptrs)
		{
			wtr.Write(ptrs.Count);

			foreach (SR1_PointerBase ptr in ptrs)
			{
				if (ptr.Offset != 0)
				{
					wtr.Write(ptr.NewStart);
				}
			}

			uint dataStart = (uint)wtr.BaseStream.Position;
			uint mod = dataStart % 0x800;
			if (mod > 0)
			{
				uint padding = 0x800 - mod;
				dataStart += padding;
			}

			while (wtr.BaseStream.Position < dataStart)
			{
				wtr.Write((sbyte)0);
			}
		}

		private void WriteBody(SR1_Writer wtr)
		{
			var structureEnumerator = _Structures.GetEnumerator();
			var migStructureEnumerator = _MigrationStructures.GetEnumerator();
			SR1_Structure migStructure = migStructureEnumerator.MoveNext() ? migStructureEnumerator.Current.Value : null;
			SR1_Structure extStructure = structureEnumerator.MoveNext() ? structureEnumerator.Current.Value : null;
			while (migStructure != null || extStructure != null)
			{
				while (migStructure != null)
				{
					// If there is a pre-existing structure at/before the new structure's insert point,
					// then break out and do that first.
					// Given that some arrays use pointers to the end, it would be dangerous to insert new items
					// before the next structure.
					if (extStructure != null && extStructure.Start <= migStructureEnumerator.Current.Key)
					{
						break;
					}

					IsWritingMigStruct = true;
					migStructure.Write(wtr);
					IsWritingMigStruct = false;
					//extStructure.WriteToConsole("Migration Structure ", 0, false);
					migStructure = migStructureEnumerator.MoveNext() ? migStructureEnumerator.Current.Value : null;
				}

				if (extStructure != null)
				{
					extStructure.Write(wtr);
					//extStructure.WriteToConsole("Existing Structure ", 0, false);
					extStructure = structureEnumerator.MoveNext() ? structureEnumerator.Current.Value : null;
				}
			}
		}

		private List<SR1_PointerBase> ResolvePointers(SR1_Writer wtr)
		{
			List<SR1_PointerBase> validPointers = new List<SR1_PointerBase>();

			ExportData exportData = new ExportData(_PrimsRead, _PrimsWritten);

			foreach (SR1_PointerBase pointer in _Pointers)
			{
				bool result;

				if (pointer.PointsToMigStruct)
				{
					result = ResolveMigStructPointer(wtr, pointer);
				}
				else if (pointer.PointsToEndOfStruct ||
					pointer.Offset == exportData.readEndKeys.Last())
				{
					result = ResolveStructEndPointer(wtr, pointer, exportData);
				}
				else
				{
					result = ResolveStandardPointer(wtr, pointer, exportData);
				}

				if (result)
				{
					validPointers.Add(pointer);
				}
			}

			return validPointers;
		}

		// Standard method of locating the new position of a structure.
		// If new fields are added for removed, other fields might move as a
		// result.
		// It's possible for pointers to reference fields within other structures,
		// so limiting the search to whole structures won't work.
		// Instead, search all the primitives that were written, based on their
		// original poditions, and if the primitive is not found, assume it was
		// removed.
		// Because it would be expensive to track every single primitive in the
		// file, primitive arrays are grouped together and the offset within the
		// that can be calculated.
		// This also accounts for the frequest reinterperating of types in this
		// game.
		private bool ResolveStandardPointer(SR1_Writer wtr, SR1_PointerBase ptr, ExportData exd)
		{
			int index = exd.writtenStartKeys.BinarySearch(ptr.Offset);
			if (index >= 0)
			{
				SR1_PrimativeBase primative = exd.writtenStart[exd.writtenStartKeys[index]];
				if (ptr.Offset >= primative.Start && ptr.Offset < primative.End)
				{
					uint newOffset = primative.NewStart + (ptr.Offset - primative.Start);
					wtr.BaseStream.Position = ptr.NewStart;
					wtr.Write(newOffset);
					return true;
				}
			}

			return false;
		}

		// Some pointers are used to mark the end of a series of structures.
		// In these cases, the goal is to locate the end point of that series.
		// There is a chance that the structure at the end of the series was
		// removed or something else was added after it in the same series.
		// To do this, the pointers that were originally read are sorted by
		// their end positions.
		// To attempt to locate the new end point, this function recurses upward
		// through the parents of the structures, for as long as they share the
		// same original end point.
		// If the last of these hasn't been removed, then it will usually be
		// the have the correct end point.
		// This may not be accurate 100% of the time, but should suffice for
		// special cases specified by the PointsToEndOfStruct flag.
		private bool ResolveStructEndPointer(SR1_Writer wtr, SR1_PointerBase ptr, ExportData exd)
		{
			int index = exd.readEndKeys.BinarySearch(ptr.Offset);
			if (index >= 0)
			{
				SR1_PrimativeBase primative = exd.readEnd[exd.readEndKeys[index]];
				SR1_Structure structure = GetTopStructure(primative);

				// Make sure the primitive was writen otherwise it can't be used.
				if (structure != null && exd.writtenStartKeys.Contains(primative.Start))
				{
					wtr.Write(structure.NewEnd);
					return true;
				}
			}

			return false;
		}

		// If the target structure was added while migrating to a new version,
		// it may have been inserted where an existing one was located.
		// In order to make sure the correct one is found, this function only
		// searches for the structures that were added during that process.
		// The PointsToMigStruct flag specifies whwn to do this.
		private bool ResolveMigStructPointer(SR1_Writer wtr, SR1_PointerBase ptr)
		{
			if (_MigrationStructures.ContainsKey(ptr.Offset))
			{
				wtr.BaseStream.Position = ptr.NewStart;
				wtr.Write(_MigrationStructures[ptr.Offset].NewStart);
				return true;
			}

			return false;
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

		private SR1_Structure GetTopStructure(SR1_Structure structure)
		{
			if (structure == null)
			{
				return null;
			}

			while (structure.Parent != null && structure.Parent.End == structure.End)
			{
				structure = structure.Parent;
			}

			return structure;
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
			List<string> weaponProperties = new List<string>();
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

								if (data is PhysObWeaponProperties)
								{
									PhysObWeaponProperties physObWeaponProperties = (PhysObWeaponProperties)data;
									weaponProperties.Add("\t" + cleanName + "\t{ class = " + physObWeaponProperties.WeaponAttributes.Class.ToString() + " }");
								}
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
				results.Add("\r\nWeaponProperties:");
				results.AddRange(weaponProperties);
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
