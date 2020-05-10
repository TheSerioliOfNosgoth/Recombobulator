using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_PrimativePointer<T> : SR1_PointerBase // where T : struct
    {
        public override Type GetGenericType()
        {
            return typeof(T);
        }

        public override object CreateObject(SR1_Structure parent, SR1_Reader reader)
        {
            return null;
        }

        public SR1_PrimativePointer()
        {
        }
    }
}
