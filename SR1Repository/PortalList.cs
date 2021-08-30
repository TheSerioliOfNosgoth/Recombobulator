using System;
using System.Collections.Generic;

namespace SR1Repository
{
    public class PortalList : ListBase<Portal>
    {
        public List<Portal> Portals { get { return _list; } set { _list = value; } }
    }
}
