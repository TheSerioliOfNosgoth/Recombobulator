using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class IntroList
    {
        List<Intro> _list = new List<Intro>();
        public Intro this[int index]
        {
            get { return _list[index]; }
        }
        public int NextAvailableID { get; set; }
        public int Count { get { return _list.Count; } }
        public List<Intro> Intros { get { return _list; } set { _list = value; } }

        public void Add(Intro intro)
        {
            _list.Add(intro);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public Intro Find(Predicate<Intro> match)
        {
            return _list.Find(match);
        }

        public List<Intro> FindAll(Predicate<Intro> match)
        {
            return _list.FindAll(match);
        }
    }
}
