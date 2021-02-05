using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    class LevelSet
    {
        readonly List<Level> _levelList = new List<Level>();
        public int MaxID { get; set; }
        public int Count { get { return _levelList.Count; } }
        public Level[] Levels { get { return _levelList.ToArray(); } }

        public void Add(Level level)
        {
            _levelList.Add(level);
        }

        public void Clear()
        {
            _levelList.Clear();
        }
    }
}
