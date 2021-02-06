using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class TexDescList
    {
        readonly List<TexDesc> _texDescList = new List<TexDesc>();
        public int Count { get { return _texDescList.Count; } }
        public TexDesc[] Textures
        { 
            get { return _texDescList.ToArray(); }
            set { _texDescList.Clear(); _texDescList.AddRange(value); }
        }

        public void Add(TexDesc texture)
        {
            _texDescList.Add(texture);
        }

        public void Clear()
        {
            _texDescList.Clear();
        }

        public List<TexDesc> FindAll(Predicate<TexDesc> match)
        {
            return _texDescList.FindAll(match);
        }
    }
}
