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
        List<SFXClip> _sfxClipList = new List<SFXClip>();
        public int MaxID { get; set; }
        public int Count { get { return _sfxClipList.Count; } }
        public List<SFXClip> SFXs { get { return _sfxClipList; } set { _sfxClipList = value; } }

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
