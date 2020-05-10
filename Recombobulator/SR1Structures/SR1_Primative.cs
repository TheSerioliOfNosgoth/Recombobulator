using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_Primative<T> : SR1_PrimativeBase // where T : struct
    {
        public T Value { get; set; }

        public SR1_Primative()
        {
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
            return Value.ToString();
        }

        public override string GetTypeName(bool includeDimensions)
        {
            return GetPrimativeTypeName(Type.GetTypeCode(typeof(T)));
        }
    }
}
