using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class SFXClipList
    {
        List<SFXClip> _list = new List<SFXClip>();
        public SFXClip this[int index]
        {
            get { return _list[index]; }
        }
        public int MaxID { get; set; }
        public int Count { get { return _list.Count; } }
        public List<SFXClip> SFXs { get { return _list; } set { _list = value; } }

        public void Add(SFXClip clip)
        {
            _list.Add(clip);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public SFXClip Find(Predicate<SFXClip> match)
        {
            return _list.Find(match);
        }

        public List<SFXClip> FindAll(Predicate<SFXClip> match)
        {
            return _list.FindAll(match);
        }
    }
}
