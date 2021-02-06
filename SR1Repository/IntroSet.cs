﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class IntroSet
    {
        readonly List<Intro> _introList = new List<Intro>();
        public int MaxID { get; set; }
        public int Count { get { return _introList.Count; } }
        public Intro[] Intros {
            get { return _introList.ToArray(); }
            set { _introList.Clear(); _introList.AddRange(value); }
        }

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
