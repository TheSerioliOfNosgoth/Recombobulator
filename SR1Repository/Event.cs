using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class Event
    {
        public string UnitName { get; set; } = "";
        public int StreamUnitID { get; set; } = 0;
        public short EventNumber { get; set; } = 0;
        public short NumInstances { get; set; } = 0;
        public uint EventOffset { get; set; } = 0;
        public uint InstanceListOffset { get; set; } = 0;

        public EventInstanceList Instances { get; set; } = new EventInstanceList();
    }
}
