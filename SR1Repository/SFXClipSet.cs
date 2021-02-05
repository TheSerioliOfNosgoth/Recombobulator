using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    class SFXClipSet
    {
        readonly SortedList<int, SFXClip> _SFXClipList = new SortedList<int, SFXClip>();
        public int MaxID { get; set; }
        public int Count { get { return _SFXClipList.Count; } }
        public SFXClip[] SFXs { get { return _SFXClipList.Values.ToArray(); } }

        public void Add(SFXClip clip)
        {
            _SFXClipList.Add(clip.SFXID, clip);
        }

        public void Clear()
        {
            _SFXClipList.Clear();
        }
    }
}
