using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class IntroList
    {
        List<Intro> _introList = new List<Intro>();
        public int NextAvailableID { get; set; }
        public int Count { get { return _introList.Count; } }
        public List<Intro> Intros { get { return _introList; } set { _introList = value; } }

        public void Add(Intro intro)
        {
            _introList.Add(intro);
        }

        public void Clear()
        {
            _introList.Clear();
        }
    }
}
