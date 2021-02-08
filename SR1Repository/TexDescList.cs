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
        List<TexDesc> _texDescList = new List<TexDesc>();
        public int Count { get { return _texDescList.Count; } }
        public List<TexDesc> Textures { get { return _texDescList; } set { _texDescList = value; } }

        public void Add(TexDesc texture)
        {
            _texDescList.Add(texture);
        }

        public void Add(IEnumerable<TexDesc> textures)
        {
            _texDescList.AddRange(textures);
        }

        public void Clear()
        {
            _texDescList.Clear();
        }

        public TexDesc Find(Predicate<TexDesc> match)
        {
            return _texDescList.Find(match);
        }
    }
}
