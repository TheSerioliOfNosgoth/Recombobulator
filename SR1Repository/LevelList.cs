using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class LevelList
    {
        List<Level> _list = new List<Level>();
        public Level this[int index]
        {
            get { return _list[index]; }
        }
        public int NextAvailableID { get; set; }
        public int Count { get { return _list.Count; } }
        public List<Level> Levels { get { return _list; } set { _list = value; } }

        public void Add(Level level)
        {
            _list.Add(level);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public Level Find(Predicate<Level> match)
        {
            return _list.Find(match);
        }

        public List<Level> FindAll(Predicate<Level> match)
        {
            return _list.FindAll(match);
        }
    }
}
