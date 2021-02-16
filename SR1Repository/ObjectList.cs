using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class ObjectList : ListBase<Object>
    {
        public List<Object> Objects { get { return _list; } set { _list = value; } }
    }
}
