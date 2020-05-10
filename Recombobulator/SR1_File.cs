using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
    class SR1_File
    {
        public const UInt32 FIRST_VERSION = 0x00000000;
        public const UInt32 ALPHA_VERSION_1_X = 0x3c204127;
        public const UInt32 ALPHA_VERSION_1 = 0x3c204128;
        public const UInt32 ALPHA_VERSION_2 = 0x3c204129;
        public const UInt32 ALPHA_VERSION_3 = 0x3c204131;
        public const UInt32 BETA_VERSION = 0x3c204139;
        public const UInt32 RETAIL_VERSION = 0x3C20413B;

        public enum Version
        {
            First = 0,
            Alpha_1_X,
            Alpha_1,
            Alpha_2,
            Alpha_3,
            Beta,
            Retail,
            Retail_PC, // No unique ID. Detect from TextureFT3.
            Next
        }

        public string _FileName { get; private set; } = "";
        public Version _Version { get; private set; } = SR1_File.Version.Retail;
        public readonly SortedList<uint, SR1_Structure> _Structures = new SortedList<uint, SR1_Structure>();
        public readonly SortedDictionary<uint, SR1_PrimativeBase> _Primatives = new SortedDictionary<uint, SR1_PrimativeBase>();
        public readonly SortedDictionary<uint, SR1_Structure> _MigrationStructures = new SortedDictionary<uint, SR1_Structure>();
        public List<SR1_PointerBase> _Pointers = new List<SR1_PointerBase>();
        public readonly List<ushort> _TextureIDs = new List<ushort>();
        public readonly StringWriter _ImportErrors = new StringWriter();
        public ushort _TextureStartingID { get; private set; } = 0;

        public SR1_File()
        {
        }

        public void Import(string fileName)
        {
            _Structures.Clear();
            _Primatives.Clear();
            _MigrationStructures.Clear();
            _Pointers.Clear();
            _TextureIDs.Clear();
            _ImportErrors.GetStringBuilder().Clear();

            _FileName = fileName;

            MemoryStream stream = new MemoryStream();

            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader fileReader = new BinaryReader(file, System.Text.Encoding.UTF8, true);
            BinaryWriter streamWriter = new BinaryWriter(stream, System.Text.Encoding.UTF8, true);

            uint dataStart = ((fileReader.ReadUInt32() >> 9) << 11) + 0x00000800;
            bool isLevel = fileReader.ReadUInt32() == 0;

            fileReader.BaseStream.Position = dataStart;
            streamWriter.BaseStream.Position = 0;
            fileReader.BaseStream.CopyTo(streamWriter.BaseStream);
            streamWriter.BaseStream.Position = 0;

            streamWriter.Dispose();
            fileReader.Dispose();
            file.Close();

            SR1_Structure root;
            SR1_Reader streamReader = new SR1_Reader(this, stream);

            if (isLevel)
            {
                streamReader.BaseStream.Position = 0x9C;
                if (streamReader.ReadUInt64() == 0xFFFFFFFFFFFFFFFF)
                {
                    _Version = Version.Retail_PC;
                }
                else
                {
                    _Version = Version.Retail;
                }

                root = new SR1Structures.Level();
            }
            else
            {
                _Version = Version.Retail;
                root = new SR1Structures.Object();
            }

            streamReader.BaseStream.Position = 0;

            root.Read(streamReader, null, "");
            streamReader.Dispose();

            stream.Close();
        }

        public void Export(string fileName)
        {
            Export(fileName, _Version, 0);
        }

        public void Export(string fileName, Version targetVersion, ushort textureStartingID)
        {
            _TextureStartingID = textureStartingID;

            MemoryStream stream = new MemoryStream();

            SR1_Writer streamWriter = new SR1_Writer(this, stream);

            _Primatives.Clear();
            _MigrationStructures.Clear();
            _Pointers.Clear();
            _TextureIDs.Clear();

            SR1_Structure[] structures = new SR1_Structure[_Structures.Values.Count];
            _Structures.Values.CopyTo(structures, 0);
            foreach (SR1_Structure structure in structures)
            {
                structure.MigrateVersion(this, targetVersion);
            }

            _Version = targetVersion;

            foreach (KeyValuePair<uint, SR1_Structure> entry in _Structures)
            {
                entry.Value.Write(streamWriter);
            }

            List<uint> sortedPrimativeKeys = new List<uint>(_Primatives.Keys);

            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter fileWriter = new BinaryWriter(file, System.Text.Encoding.UTF8);

            fileWriter.Write(_Pointers.Count);

            foreach (SR1_PointerBase pointer in _Pointers)
            {
                if (pointer.Offset != 0)
                {
                    fileWriter.Write(pointer.NewStart);
                }
            }

            uint dataStart = (uint)fileWriter.BaseStream.Position;
            uint mod = dataStart % 0x800;
            if (mod > 0)
            {
                uint padding = 0x800 - mod;
                dataStart += padding;
            }

            //uint dataStart = ((uint)fileWriter.BaseStream.Position + 0x00000800) & 0xFFFFF800;
            while (fileWriter.BaseStream.Position < dataStart)
            {
                fileWriter.Write((sbyte)0);
            }

            foreach (SR1_PointerBase pointer in _Pointers)
            {
                if (_MigrationStructures.ContainsKey(pointer.Offset))
                {
                    streamWriter.BaseStream.Position = pointer.NewStart;
                    streamWriter.Write(_MigrationStructures[pointer.Offset].NewStart);
                    continue;
                }

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

                    streamWriter.BaseStream.Position = pointer.NewStart;
                    streamWriter.Write(newOffset);
                }

                //streamWriter.BaseStream.Position = pointer.Start;
                //streamWriter.Write(pointer.Offset);
            }

            stream.WriteTo(file);

            file.Flush();
            file.Dispose();

            stream.Close();
        }

        public bool TestExport()
        {
            bool result = true;

            MemoryStream stream = new MemoryStream();

            SR1_Writer streamWriter = new SR1_Writer(this, stream);

            _Primatives.Clear();
            _MigrationStructures.Clear();
            _Pointers.Clear();
            _TextureIDs.Clear();

            SR1_Structure[] structures = new SR1_Structure[_Structures.Values.Count];
            _Structures.Values.CopyTo(structures, 0);
            foreach (SR1_Structure structure in structures)
            {
                structure.MigrateVersion(this, _Version);
            }

            foreach (KeyValuePair<uint, SR1_Structure> entry in _Structures)
            {
                entry.Value.Write(streamWriter);
            }

            List<uint> sortedPrimativeKeys = new List<uint>(_Primatives.Keys);

            MemoryStream file = new MemoryStream();
            BinaryWriter fileWriter = new BinaryWriter(file, System.Text.Encoding.UTF8);

            fileWriter.Write(_Pointers.Count);

            foreach (SR1_PointerBase pointer in _Pointers)
            {
                if (pointer.Offset != 0)
                {
                    fileWriter.Write(pointer.NewStart);
                }
            }

            uint dataStart = (uint)fileWriter.BaseStream.Position;
            uint mod = dataStart % 0x800;
            if (mod > 0)
            {
                uint padding = 0x800 - mod;
                dataStart += padding;
            }

            //uint dataStart = ((uint)fileWriter.BaseStream.Position + 0x00000800) & 0xFFFFF800;
            while (fileWriter.BaseStream.Position < dataStart)
            {
                fileWriter.Write((sbyte)0);
            }

            foreach (SR1_PointerBase pointer in _Pointers)
            {
                if (_MigrationStructures.ContainsKey(pointer.Offset))
                {
                    streamWriter.BaseStream.Position = pointer.NewStart;
                    streamWriter.Write(_MigrationStructures[pointer.Offset].NewStart);
                    continue;
                }

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

                    streamWriter.BaseStream.Position = pointer.NewStart;
                    streamWriter.Write(newOffset);
                }

                //streamWriter.BaseStream.Position = pointer.Start;
                //streamWriter.Write(pointer.Offset);
            }

            stream.WriteTo(file);

            if (File.Exists(_FileName))
            {
                file.Position = 0;
                FileStream compareFile = new FileStream(_FileName, FileMode.Open, FileAccess.Read);
                if (file.Length != compareFile.Length)
                {
                    result = false;
                }
                else
                {
                    long fileLength = Math.Min(file.Length, compareFile.Length);
                    for (long i = 0; i < fileLength; i++)
                    {
                        if (file.ReadByte() != compareFile.ReadByte())
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

            file.Flush();
            file.Dispose();

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

        public string GetErrors()
        {
            return _ImportErrors.ToString();
        }

        public static string[] TestFolder(string folderName, bool listAllFiles)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.pcm", SearchOption.AllDirectories);
            List<string> results = new List<string>();
            int numSucceeded = 0;
            foreach (FileInfo fileInfo in fileInfos)
            {
                try
                {
                    SR1_File file = new SR1_File();
                    file.Import(fileInfo.FullName);
                    if (file.TestExport())
                    {
                        if (listAllFiles)
                        {
                            results.Add(fileInfo.Name + "- Success");
                        }

                        numSucceeded++;
                    }
                    else
                    {
                        results.Add(fileInfo.Name + "- Fail");
                    }
                }
                catch
                {
                    results.Add(fileInfo.Name + "- Error");
                }
            }

            results.Add("Files Read: " + fileInfos.Length);
            results.Add("Succeeded: " + numSucceeded);
            results.Add("Failed: " + (fileInfos.Length - numSucceeded));

            return results.ToArray();
        }
    }
}
