using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    class TexDescSet
    {
        readonly List<TexDesc> _TexDescList = new List<TexDesc>();
        public int Count { get { return _TexDescList.Count; } }
        public TexDesc[] Textures
        { 
            get { return _TexDescList.ToArray(); }
            set { _TexDescList.Clear(); _TexDescList.AddRange(value); }
        }

        public void Add(TexDesc texture)
        {
            _TexDescList.Add(texture);
        }

        public void Clear()
        {
            _TexDescList.Clear();
        }

        public List<TexDesc> FindAll(Predicate<TexDesc> match)
        {
            return _TexDescList.FindAll(match);
        }
    }
}
