using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class ObjectList
    {
        List<Object> _objectList = new List<Object>();
        public int Count { get { return _objectList.Count; } }
        public List<Object> Objects { get { return _objectList; } set { _objectList = value; } }

        public void Add(Object obj)
        {
            _objectList.Add(obj);
        }

        public void Clear()
        {
            _objectList.Clear();
        }

        public Object Find(Predicate<Object> match)
        {
            return _objectList.Find(match);
        }
    }
}
