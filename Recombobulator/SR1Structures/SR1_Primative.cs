using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_Primative<T> : SR1_PrimativeBase // where T : struct
    {
        protected bool _showAsHex;

        public T Value { get; set; }

        public SR1_Primative()
        {
        }

        public SR1_Primative<T> ShowAsHex(bool hex)
        {
            _showAsHex = hex;
            return this;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            Value = (T)ReadPrimativeType<T>(reader);
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            // Primatives don't contain references.
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            WritePrimativeType<T>(writer, Value);
        }

        public override string ToString()
        {
            if (_showAsHex)
            {
                return GetPrimativeAsHex(Value);
            }

            return Value.ToString();
        }

        public override string GetTypeName(bool includeDimensions)
        {
            return GetPrimativeTypeName(Type.GetTypeCode(typeof(T)));
        }
    }
}
