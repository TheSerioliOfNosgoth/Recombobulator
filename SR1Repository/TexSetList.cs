using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class TexSetList
    {
        List<TexSet> _texSetList = new List<TexSet>();
        public int Count { get { return _texSetList.Count; } }
        public List<TexSet> TexSets { get { return _texSetList; } set { _texSetList = value; } }

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
