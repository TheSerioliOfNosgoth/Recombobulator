using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class EventInstance
    {
        public uint EventInstanceOffset { get; set; } = 0;
        public short ID { get; set; } = 0;
        public int UnitID { get; set; } = 0;
        public int IntroID { get; set; } = 0;
    }
}
