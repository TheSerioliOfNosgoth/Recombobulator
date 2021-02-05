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
        string _clipsFileName;
        string _allClipsFileName;
        string _texturesFileName;

        string _outputBigFileName;
        string _outputTexturesFileName;

        void CopyTo(Stream destination, Stream source, int length)
        {
            byte[] buffer = new byte[length];
            source.Read(buffer, 0, length);
            destination.Write(buffer, 0, length);
        }

        public static string ByteArrayToHexString(byte[] inputArray)
        {
            StringBuilder builder = new StringBuilder();

            foreach (byte b in inputArray)
            {
                builder.Append(string.Format("{0:X2}", b).ToUpper());
            }

            return builder.ToString();
        }

        public static String CleanName(String name)
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

            return name.Trim();
        }

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
            _clipsFileName = Path.Combine(projectFolderName, "clips.json");
            _allClipsFileName = Path.Combine(projectFolderName, "allSFX.pmf");
            _texturesFileName = Path.Combine(projectFolderName, "textures.json");

            _outputBigFileName = Path.Combine(_outputFolderName, "bigfile.dat");
            _outputTexturesFileName = Path.Combine(_outputFolderName, "textures.big");
        }

        void CreateDirectories()
        {
            Directory.CreateDirectory(_dataFolderName);
            Directory.CreateDirectory(_textureFolderName);
            Directory.CreateDirectory(_sfxFolderName);
            Directory.CreateDirectory(_outputFolderName);
        }

        private bool LoadHashTable(out Hashtable hashTable)
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

        bool LoadBigfileEntries(AssetDescSet assets)
        {
            if (!LoadHashTable(out Hashtable hashTable))
            {
                return false;
            }

            assets.Clear();

            try
            {
                FileStream sourceFile = new FileStream(_sourceBigfileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(sourceFile);

                uint entryCount = reader.ReadUInt32();
                while (entryCount > 0)
                {
                    AssetDesc entry = new AssetDesc();
                    entry.FileHash = reader.ReadUInt32();
                    entry.FileLength = reader.ReadUInt32();
                    entry.FileOffset = reader.ReadUInt32();
                    entry.Code.code = reader.ReadUInt32();

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

                    entryCount--;
                }

                reader.Close();
                sourceFile.Close();

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
            }
            catch (Exception)
            {
                assets.Clear();
                Console.WriteLine("Error: Couldn't load bigfile entries.");
                return false;
            }

            return true;
        }

        bool AddAdditionalAssets(AssetDescSet assets)
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
                    uint hashID = getSR1HashName(relativePath);

                    // There may be more than one file with the same hash ID in the source bigfile.
                    // Update the sizes for all of them.
                    List<AssetDesc> existingEntries = assets.FindAll(x => x.FileHash == hashID);
                    if (existingEntries != null)
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

        bool AddAdditionalTextures(TexDescSet textures)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_textureFolderName);
                FileInfo[] fileInfos = directoryInfo.GetFiles("*.png", SearchOption.TopDirectoryOnly);
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("Texture-([0-9]+).(png)");

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
                            List<TexDesc> existingEntries = textures.FindAll(x => x.TextureIndex == index);
                            if (existingEntries == null)
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

        public bool UnpackRepository()
        {
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
                AssetDescSet assetData = new AssetDescSet();
                LevelSet levelData = new LevelSet();
                IntroSet introData = new IntroSet();
                SFXClipSet clipData = new SFXClipSet();
                TexDescSet textureData = new TexDescSet();

                if (!LoadBigfileEntries(assetData))
                {
                    return false;
                }

                CreateDirectories();

                FileStream sourceBigFile = new FileStream(_sourceBigfileName, FileMode.Open, FileAccess.Read);
                FileStream sourceTexturesFile = new FileStream(_sourceTexturesFileName, FileMode.Open, FileAccess.Read);

                List<int> sfxIDs = new List<int>();
                FileStream allClipsFile = new FileStream(_allClipsFileName, FileMode.Create, FileAccess.ReadWrite);
                allClipsFile.Write(new byte[16], 0, 16);

                #region Bigfile
                foreach (AssetDesc entry in assetData.Assets)
                {
                    sourceBigFile.Position = entry.FileOffset;

                    string outputFileName = Path.Combine(_projectFolderName, entry.FilePath);
                    string outputFileDirectory = Path.GetDirectoryName(outputFileName);
                    Directory.CreateDirectory(outputFileDirectory);
                    FileStream outputFile = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite);
                    CopyTo(outputFile, sourceBigFile, (int)entry.FileLength);
                    outputFile.Flush();

                    string ext = Path.GetExtension(outputFileName);
                    if (ext == ".pcm")
                    {
                        BinaryReader reader = new BinaryReader(outputFile, System.Text.Encoding.ASCII);
                        reader.BaseStream.Position = 0;

                        UInt32 dataStart = ((reader.ReadUInt32() >> 9) << 11) + 0x00000800;

                        bool isUnit = (reader.ReadUInt32() == 0x00000000);
                        if (isUnit)
                        {
                            Level level = new Level();

                            reader.BaseStream.Position = dataStart + 0x98;
                            reader.BaseStream.Position = dataStart + reader.ReadUInt32();
                            level.UnitName = CleanName(new string(reader.ReadChars(8)));

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

                            reader.BaseStream.Position = dataStart + 0xF8;
                            level.StreamUnitID = reader.ReadInt32();

                            if (level.StreamUnitID > levelData.MaxID)
                            {
                                levelData.MaxID = level.StreamUnitID;
                            }

                            levelData.Add(level);

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

                                if (intro.IntroUniqueID > introData.MaxID)
                                {
                                    introData.MaxID = intro.IntroUniqueID;
                                }

                                introData.Add(intro);
                            }
                            #endregion
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

                                if (!sfxIDs.Contains(sfxID))
                                {
                                    SFXClip clip = new SFXClip();
                                    clip.SFXID = sfxID;
                                    clip.SFXName = "clip-" + sfxID;
                                    clipData.Add(clip);

                                    sfxIDs.Add(sfxID);
                                }

                                allClipsFile.Write(clipBuffer, 0, clipBuffer.Length);

                                Console.WriteLine("\tExtracted clip: \"" + outputClipFileName + "\"");

                                reader.BaseStream.Position = nextClipStart;
                            }
                        }
                    }
                    outputFile.Close();

                    Console.WriteLine("Extracted file: \"" + outputFileName + "\"");
                }
                #endregion

                #region Textures
                BinaryReader texturesReader = new BinaryReader(sourceTexturesFile);
                texturesReader.BaseStream.Position = headerLength;
                uint fileLength = (uint)sourceTexturesFile.Length;
                uint totalTextures = (uint)((fileLength - (long)headerLength) / (long)(textureWidth * textureHeight * 2) - 1);
                for (int t = 0; t <= totalTextures; t++)
                {
                    string textureName = "texture-" + zeroFill(t.ToString(), 5) + ".png";
                    string outputFileName = Path.Combine(_textureFolderName, textureName);
                    string relativePath = outputFileName.Substring(_projectFolderName.Length);

                    TexDesc texDesc = new TexDesc();
                    texDesc.TextureIndex = textureData.Count;
                    texDesc.FilePath = relativePath;
                    textureData.Add(texDesc);

                    Bitmap tempBitmap = ReadTexture(texturesReader);
                    tempBitmap.Save(outputFileName, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine("Extracted file: \"" + outputFileName + "\"");
                }
                texturesReader.Close();
                #endregion

                sourceBigFile.Close();
                sourceTexturesFile.Close();

                allClipsFile.Position = 0;
                allClipsFile.Write(BitConverter.GetBytes(0x61504D46), 0, 4);
                allClipsFile.Write(BitConverter.GetBytes((short)256), 0, 2);
                allClipsFile.Write(BitConverter.GetBytes((short)0), 0, 2);
                allClipsFile.Write(BitConverter.GetBytes((short)sfxIDs.Count), 0, 2);
                allClipsFile.Write(BitConverter.GetBytes((short)0), 0, 2);
                allClipsFile.Write(BitConverter.GetBytes(0x00000000), 0, 4);
                allClipsFile.Close();

                string assetsFileData = JsonSerializer.Serialize(assetData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_assetsFileName, assetsFileData, Encoding.ASCII);

                string levelsFileData = JsonSerializer.Serialize(levelData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_levelsFileName, levelsFileData, Encoding.ASCII);

                string introsFileData = JsonSerializer.Serialize(introData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_introsFileName, introsFileData, Encoding.ASCII);

                string clipsFileData = JsonSerializer.Serialize(clipData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_clipsFileName, clipsFileData, Encoding.ASCII);

                string texturesFileData = JsonSerializer.Serialize(textureData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_texturesFileName, texturesFileData, Encoding.ASCII);

                Console.WriteLine("Extracted " + assetData.Count.ToString() + " files from \"" + _sourceBigfileName + "\".");
                Console.WriteLine("Extracted " + textureData.Count.ToString() + " files from \"" + _sourceTexturesFileName + "\".");
                Console.WriteLine("Discovered " + levelData.Count + " unique levels.");
                Console.WriteLine("Discovered " + introData.Count + " unique intros.");
                Console.WriteLine("Discovered " + clipData.Count + " unique clips.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Couldn't unpack the repository.");
                return false;
            }

            return true;
        }

        public bool PackRepository(bool forceAllFiles = false)
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

            if (!File.Exists(_texturesFileName))
            {
                Console.WriteLine("Error: Cannot find texture file \"" + _texturesFileName + "\".");
                return false;
            }

            try
            {
                FileStream outputBigFile = new FileStream(_outputBigFileName, FileMode.Create);
                FileStream outputTexturesFile = new FileStream(_outputTexturesFileName, FileMode.Create);

                string assetsFileData = File.ReadAllText(_assetsFileName, Encoding.ASCII);
                AssetDescSet assetData = (AssetDescSet)JsonSerializer.Deserialize(assetsFileData, typeof(AssetDescSet));
                if (forceAllFiles && !AddAdditionalAssets(assetData))
                {
                    return false;
                }

                string texturesFileData = File.ReadAllText(_texturesFileName, Encoding.ASCII);
                TexDescSet textureData = (TexDescSet)JsonSerializer.Deserialize(texturesFileData, typeof(TexDescSet));
                if (forceAllFiles && !AddAdditionalTextures(textureData))
                {
                    return false;
                }

                // Sort by index. Offset isn't saved.
                SortedList<int, AssetDesc> sortedAssets = new SortedList<int, AssetDesc>();
                foreach (AssetDesc asset in assetData.Assets)
                {
                    sortedAssets.Add(asset.FileIndex, asset);
                }

                #region Bigfile
                BinaryWriter bigFileWriter = new BinaryWriter(outputBigFile);
                bigFileWriter.Write(assetData.Count);

                uint fileIndexSize = 4u + (16u * (uint)assetData.Count);
                fileIndexSize += 0x00000800;
                fileIndexSize &= 0xFFFFF800;
                uint offset = fileIndexSize;
                foreach (AssetDesc entry in assetData.Assets)
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

                foreach (AssetDesc entry in sortedAssets.Values)
                {
                    string inputFileName = Path.Combine(_projectFolderName, entry.FilePath);
                    FileStream inputFile = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
                    CopyTo(outputBigFile, inputFile, (int)inputFile.Length);
                    inputFile.Close();

                    Console.WriteLine("Added file: \"" + inputFileName + "\"");

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
                texturesWriter.Write((ushort)textureData.Count);
                texturesWriter.Write(new byte[headerLength - 4]);
                uint totalTextures = (uint)textureData.Count - 1;
                for (int t = 0; t <= totalTextures; t++)
                {
                    string inputFileName = Path.Combine(_projectFolderName, textureData.Textures[t].FilePath);
                    Bitmap tempBitmap = new Bitmap(inputFileName);
                    WriteTexture(texturesWriter, tempBitmap);
                    Console.WriteLine("Added file: \"" + inputFileName + "\"");
                }
                #endregion

                bigFileWriter.Close();
                outputBigFile.Close();

                Console.WriteLine("Packed " + assetData.Count.ToString() + " files into \"" + _outputBigFileName + "\".");
                Console.WriteLine("Packed " + textureData.Count.ToString() + " files into \"" + _outputTexturesFileName + "\".");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Couldn't pack the repository.");
                return false;
            }

            return true;
        }

        private uint getSR1HashName(string fileName)
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

        string zeroFill(string origVal, int length)
        {
            string retString;

            retString = origVal;

            do
                retString = "0" + retString;
            while (retString.Length < length);

            return retString;
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
    }
}