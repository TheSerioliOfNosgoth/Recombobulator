using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class LevelList
    {
        List<Level> _levelList = new List<Level>();
        public int NextAvailableID { get; set; }
        public int Count { get { return _levelList.Count; } }
        public List<Level> Levels { get { return _levelList; } set { _levelList = value; } }

        public void Add(Level level)
        {
            _levelList.Add(level);
        }

        public void Clear()
        {
            _levelList.Clear();
        }

        public Level Find(Predicate<Level> match)
        {
            return _levelList.Find(match);
        }
    }
}
