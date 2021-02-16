using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class SFXClipList : ListBase<SFXClip>
    {
        public int MaxID { get; set; }
        public List<SFXClip> SFXs { get { return _list; } set { _list = value; } }
    }
}
