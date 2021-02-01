using System.Collections.Generic;
using System.IO;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
    class SR1_Reader : BinaryReader
    {
        public SR1_File File = null;
        public Object Object = null;
        public Events Events = null;

        public SortedList<uint, SR1_PointerBase> AnimFXDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> AttackAnimDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> AttackAnimSetDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> IdleAnimDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> IdleAnimSetDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> SAnimDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedList<uint, SR1_PointerBase> ThrowAnimDictionary { get; } = new SortedList<uint, SR1_PointerBase>();
        public SortedDictionary<uint, SR1_Pointer<SFXFileData>> SFXDictionary { get; } = new SortedDictionary<uint, SR1_Pointer<SFXFileData>>();
        public SR1_Pointer<PlanMkr> PlanMarkerList = null;
        public SortedList<uint, SR1_PointerArray<Intro>> IntroListDictionary { get; } = new SortedList<uint, SR1_PointerArray<Intro>>();
        public SortedList<uint, SR1_PointerBase> EventList { get; } = new SortedList<uint, SR1_PointerBase>();
        public ScriptParser ScriptParser { get; } = new ScriptParser();

        public SR1_Reader(SR1_File file, Stream input)
            : base(input, System.Text.Encoding.UTF8)
        {
            File = file;
        }

        public void LogScript(string script)
        {
            if ((File._ImportFlags & SR1_File.ImportFlags.LogScripts) != 0)
            {
                File._Scripts.WriteLine(script);
            }
        }

        public void LogError(string error)
        {
            if ((File._ImportFlags & SR1_File.ImportFlags.LogErrors) != 0)
            {
                File._ImportErrors.WriteLine(error);
            }
        }
    }
}