using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_String : SR1_PrimativeArray<char>
    {
        public SR1_String(int maxLength)
            : base(maxLength)
        {
        }

        public SR1_String()
            : base(0)
        {
        }

        public void SetText(string text)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (i < text.Length)
                {
                    _array[i] = text[i];
                }
                else
                {
                    _array[i] = '\0';
                }
            }
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            if (_array != null)
            {
                int length = 0;
                for (int i = 0; i < _array.Length; i++)
                {
                    length++;
                    _array[i] = (char)ReadPrimativeType<char>(reader);
                    if (_array[i] == 0)
                    {
                        break;
                    }
                }

                Array.Resize(ref _array, length);
                _dimensions[0] = length;
            }
        }

        public override string GetTypeName(bool includeDimensions)
        {
            string typeName = GetPrimativeTypeName(Type.GetTypeCode(typeof(char)));

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
