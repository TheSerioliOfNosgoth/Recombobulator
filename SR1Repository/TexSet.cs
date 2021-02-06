using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class TexSet
    {
        public string UnitName { get; set; } = "";

        public ushort[] TextureIDs { get; private set; } = new ushort[8];
    }
}
