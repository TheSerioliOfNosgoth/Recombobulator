using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class Level
    {
        public string UnitName { get; set; } = "";
        public int StreamUnitID { get; set; } = 0;
        public bool IsNew { get; set; } = false;
        public string TextureSet { get; set; } = "";
    }
}
