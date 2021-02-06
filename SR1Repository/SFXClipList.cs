using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class SFXClipList
    {
        readonly List<SFXClip> _sfxClipList = new List<SFXClip>();
        public int MaxID { get; set; }
        public int Count { get { return _sfxClipList.Count; } }
        public SFXClip[] SFXs {
            get { return _sfxClipList.ToArray(); }
            set { _sfxClipList.Clear(); _sfxClipList.AddRange(value); }
        }

        public void Add(SFXClip clip)
        {
            _sfxClipList.Add(clip);
        }

        public void Clear()
        {
            _sfxClipList.Clear();
        }
    }
}
