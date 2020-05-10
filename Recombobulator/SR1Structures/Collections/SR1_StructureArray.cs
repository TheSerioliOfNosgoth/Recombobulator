using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    abstract class SR1_StructureArray : SR1_StructureArrayBase<SR1_Structure>
    {
        public SR1_StructureArray(params int[] dimensions)
            : base(dimensions)
        {
        }
    }

    class SR1_StructureArray<T> : SR1_StructureArray where T : SR1_Structure, new()
    {
        public SR1_StructureArray(params int[] dimensions)
            : base(dimensions)
        {
        }

        protected override SR1_Structure CreateElement()
        {
            return new T();
        }

        public override string GetTypeName(bool includeDimensions)
        {
            string typeName = typeof(T).Name;

            if (_array == null)
            {
                typeName += "[]";
                return typeName;
            }

            if (includeDimensions)
            {
                int rank = _dimensions.Length;
                for (int r = 0; r < rank; r++)
                {
                    typeName += "[";
                    typeName += _dimensions[r].ToString();
                    typeName += "]";
                }
            }

            return typeName;
        }
    }
}
