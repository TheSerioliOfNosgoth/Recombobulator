using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR1Repository
{
    public class TexSet
    {
        public int Index { get; set; } = 0;

        public string Name { get; set; } = "";

        public ushort[] TextureIDs { get; set; } = new ushort[8];
    }
}
