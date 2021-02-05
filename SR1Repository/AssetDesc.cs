using System;
using System.Runtime.InteropServices;

namespace SR1Repository
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct BigfileCode
    {
        [FieldOffset(0)] public char code0;
        [FieldOffset(1)] public char code1;
        [FieldOffset(2)] public char code2;
        [FieldOffset(3)] public char code3;
        [FieldOffset(0)] public uint code;
    }

    class AssetDesc
    {
        public string FilePath { get; set; } = "";
        public uint FileHash { get; set; } = 0;
        public uint FileLength { get; set; } = 0;
        public int FileIndex { get; set; } = 0;
        public uint FileCode { get { return Code.code; } set { Code.code = value; } }

        public uint FileOffset = 0;

        public BigfileCode Code;
    }
}