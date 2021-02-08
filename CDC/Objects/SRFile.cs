using System;
using System.Collections.Generic;
using System.IO;
using CDC.Objects.Models;

namespace CDC.Objects
{
    public enum RenderMode
    {
        Standard,
        NoTextures,
        Wireframe,  // not implemented
        PointCloud, // not implemented
        DebugPolygonFlags1,
        DebugPolygonFlags2,
        DebugPolygonFlags3,
        DebugPolygonFlagsSoulReaverA,
        DebugPolygonFlagsHash,
        DebugTextureAttributes1,
        DebugTextureAttributes2,
        DebugTextureAttributes3,
        DebugTextureAttributes4,
        DebugTextureAttributes5,
        DebugTextureAttributes6,
        DebugTextureAttributesHash,
        DebugTextureAttributesA1,
        DebugTextureAttributesA2,
        DebugTextureAttributesA3,
        DebugTextureAttributesA4,
        DebugTextureAttributesA5,
        DebugTextureAttributesA6,
        DebugTextureAttributesAHash,
        DebugCLUT1,
        DebugCLUT2,
        DebugCLUT3,
        DebugCLUT4,
        DebugCLUT5,
        DebugCLUT6,
        DebugCLUTHash,
        DebugCLUTNonRowColBits1,
        DebugCLUTNonRowColBits2,
        DebugCLUTNonRowColBitsHash,
        DebugTexturePage1,
        DebugTexturePage2,
        DebugTexturePage3,
        DebugTexturePage4,
        DebugTexturePage5,
        DebugTexturePage6,
        DebugTexturePageHash,
        DebugTexturePageUpper28BitsHash,
        DebugTexturePageUpper5BitsHash,
        DebugBSPRootTreeNumber,
        DebugBSPTreeNodeID,
        DebugBSPRootTreeFlags1,
        DebugBSPRootTreeFlags2,
        DebugBSPRootTreeFlags3,
        DebugBSPRootTreeFlags4,
        DebugBSPRootTreeFlags5,
        DebugBSPRootTreeFlags6,
        DebugBSPRootTreeFlagsHash,
        DebugBSPTreeImmediateParentFlags1,
        DebugBSPTreeImmediateParentFlags2,
        DebugBSPTreeImmediateParentFlags3,
        DebugBSPTreeImmediateParentFlags4,
        DebugBSPTreeImmediateParentFlags5,
        DebugBSPTreeImmediateParentFlags6,
        DebugBSPTreeImmediateParentFlagsHash,
        DebugBSPTreeAllParentFlagsORd1,
        DebugBSPTreeAllParentFlagsORd2,
        DebugBSPTreeAllParentFlagsORd3,
        DebugBSPTreeAllParentFlagsORd4,
        DebugBSPTreeAllParentFlagsORd5,
        DebugBSPTreeAllParentFlagsORd6,
        DebugBSPTreeAllParentFlagsORdHash,
        DebugBSPTreeLeafFlags1,
        DebugBSPTreeLeafFlags2,
        DebugBSPTreeLeafFlags3,
        DebugBSPTreeLeafFlags4,
        DebugBSPTreeLeafFlags5,
        DebugBSPTreeLeafFlags6,
        DebugBSPTreeLeafFlagsHash,
        DebugBoneIDHash,
        DebugSortPushHash,
        DebugSortPushFlags1,
        DebugSortPushFlags2,
        DebugSortPushFlags3,
        AverageVertexAlpha,
        PolygonAlpha,
        PolygonOpacity
    }

    public class ExportOptions
    {
        public bool DiscardPortalPolygons;
        public bool DiscardNonVisible;
        public bool DiscardMeshesWithNoNonZeroFlags;
        public bool ExportSpectral;
        public bool UnhideCompletelyTransparentTextures;
        public bool AlwaysUseGreyscaleForMissingPalettes;
        public bool ExportDoubleSidedMaterials;
        public bool MakeAllPolygonsVisible;
        public bool MakeAllPolygonsOpaque;
        public bool SetAllPolygonColoursToValue;
        public float PolygonColourAlpha;
        public float PolygonColourRed;
        public float PolygonColourGreen;
        public float PolygonColourBlue;
        public bool BSPRenderingIncludeRootTreeFlagsWhenORing;
        public bool BSPRenderingIncludeLeafFlagsWhenORing;
        public bool InterpolatePolygonColoursWhenColouringBasedOnVertices;
        public bool UseEachUniqueTextureCLUTVariation;
        public bool AlsoInferAlphaMaskingFromTexturePixels;
        public bool IgnorePolygonFlag2ForTerrain;
        public bool DistinctMaterialsForAllFlags;
        public bool AdjustUVs;
        public bool IgnoreVertexColours;
        public List<string> TextureFileLocations;
        public RenderMode RenderMode;
        public Platform ForcedPlatform;

        public ExportOptions()
        {
            DiscardPortalPolygons = false;
            DiscardNonVisible = false;
            DiscardMeshesWithNoNonZeroFlags = false;
            ExportSpectral = false;
            UnhideCompletelyTransparentTextures = false;
            AlwaysUseGreyscaleForMissingPalettes = false;
            ExportDoubleSidedMaterials = false;
            MakeAllPolygonsVisible = false;
            MakeAllPolygonsOpaque = false;
            SetAllPolygonColoursToValue = false;
            PolygonColourAlpha = 0.7f;
            PolygonColourRed = 0.0f;
            PolygonColourGreen = 1.0f;
            PolygonColourBlue = 0.0f;
            BSPRenderingIncludeRootTreeFlagsWhenORing = false;
            BSPRenderingIncludeLeafFlagsWhenORing = false;
            InterpolatePolygonColoursWhenColouringBasedOnVertices = false;
            UseEachUniqueTextureCLUTVariation = false;
            AlsoInferAlphaMaskingFromTexturePixels = false;
            IgnorePolygonFlag2ForTerrain = false;
            DistinctMaterialsForAllFlags = true;
            AdjustUVs = false;
            IgnoreVertexColours = false;
            TextureFileLocations = new List<string>();
            RenderMode = RenderMode.Standard;
            ForcedPlatform = Platform.None;
        }

        public bool TextureLoadRequired()
        {
            bool result = true;

            if (RenderMode != RenderMode.Standard)
            {
                result = false;
            }

            return result;
        }
    }

    public abstract class SRFile
    {
        public const string TextureExtension = ".png";
        public const float ExportSizeMultiplier = 0.001f;

        protected String _name;
        protected UInt32 _version;
        protected UInt32 _dataStart;
        protected UInt16 _modelCount;
        protected UInt16 _animCount;
        protected UInt32 _modelStart;
        protected SRModel[] _models;
        protected UInt32 _animStart;
        protected UInt32 _instanceCount;
        protected UInt32 _instanceStart;
        protected String[] _instanceNames;
        protected UInt32 _instanceTypeStart;
        protected String[] _instanceTypeNames;
        protected UInt32 portalCount;
        protected UInt32 _connectedUnitStart;
        protected String[] _portalNames;
        protected Game _game;
        protected Asset _asset;
        protected Platform _platform;

        public String Name { get { return _name; } }
        public UInt32 Version { get { return _version; } }
        public UInt16 ModelCount { get { return _modelCount; } }
        public UInt16 AnimCount { get { return _animCount; } }
        public SRModel[] Models { get { return _models; } }
        public UInt32 InstanceCount { get { return _instanceCount; } }
        public String[] Instances { get { return _instanceNames; } }
        public String[] InstanceTypeNames { get { return _instanceTypeNames; } }
        public UInt32 ConectedUnitCount { get { return portalCount; } }
        public String[] ConnectedUnit { get { return _portalNames; } }
        public Game Game { get { return _game; } }
        public Asset Asset { get { return _asset; } }
        public Platform Platform { get { return _platform; } }

        public static StreamWriter m_xLogFile = null;

        protected SRFile(String strFileName, Game game, CDC.Objects.ExportOptions options)
        {
            _name = Path.GetFileNameWithoutExtension(strFileName);
            _game = game;

            FileStream xFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
            BinaryReader xReader = new BinaryReader(xFile, System.Text.Encoding.ASCII);
            MemoryStream xStream = new MemoryStream((int)xFile.Length);
            BinaryWriter xWriter = new BinaryWriter(xStream, System.Text.Encoding.ASCII);

            //String strDebugFileName = Path.GetDirectoryName(strFileName) + "\\" + Path.GetFileNameWithoutExtension(strFileName) + "-Debug.txt";
            //m_xLogFile = File.CreateText(strDebugFileName);

            ResolvePointers(xReader, xWriter);
            xReader.Close();
            xReader = new BinaryReader(xStream, System.Text.Encoding.ASCII);

            ReadHeaderData(xReader, options);

            if (_asset == Asset.Object)
            {
                ReadObjectData(xReader, options);
            }
            else
            {
                ReadUnitData(xReader, options);
            }

            xReader.Close();

            if (m_xLogFile != null)
            {
                m_xLogFile.Close();
                m_xLogFile = null;
            }
        }

        protected abstract void ReadHeaderData(BinaryReader xReader, CDC.Objects.ExportOptions options);

        protected abstract void ReadObjectData(BinaryReader xReader, CDC.Objects.ExportOptions options);

        protected abstract void ReadUnitData(BinaryReader xReader, CDC.Objects.ExportOptions options);

        protected abstract void ResolvePointers(BinaryReader xReader, BinaryWriter xWriter);

        public static float[] UInt32ARGBToFloatARGB(UInt32 argb)
        {
            float[] result = new float[4];

            result[0] = (float)((argb & 0xFF000000) >> 24) / 255.0f;
            result[1] = (float)((argb & 0x00FF0000) >> 16) / 255.0f;
            result[2] = (float)((argb & 0x0000FF00) >> 8) / 255.0f;
            result[3] = (float)((argb & 0x000000FF)) / 255.0f;

            return result;
        }

        public static UInt32 FloatARGBToUInt32ARGB(float[] argb)
        {
            UInt32 result;

            result = ((uint)(Math.Round(argb[0] * 255.0f))) << 24;
            result |= ((uint)(Math.Round(argb[1] * 255.0f))) << 16;
            result |= ((uint)(Math.Round(argb[2] * 255.0f))) << 8;
            result |= ((uint)(Math.Round(argb[3] * 255.0f)));

            return result;
        }
    }
}
