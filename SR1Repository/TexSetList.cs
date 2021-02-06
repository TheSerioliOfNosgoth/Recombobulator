using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class TexSetList
    {
        readonly List<TexSet> _texSetList = new List<TexSet>();
        public int MaxID { get; set; }
        public int Count { get { return _texSetList.Count; } }
        public TexSet[] TexSets {
            get { return _texSetList.ToArray(); }
            set { _texSetList.Clear(); _texSetList.AddRange(value); }
        }

        public void Add(TexSet texSet)
        {
            _texSetList.Add(texSet);
        }

        public void Clear()
        {
            _texSetList.Clear();
        }
    }
}
