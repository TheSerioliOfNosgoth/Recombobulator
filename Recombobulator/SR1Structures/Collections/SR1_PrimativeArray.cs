using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_PrimativeArray<T> : SR1_PrimativeBase // where T : struct
    {
        protected int[] _dimensions = null;
        protected T[] _array = null;

        public T this[int i] { get { return _array[i]; } set { _array[i] = value; } }
        public int Length { get { return _array == null ? 0 : _array.Length; } }

        public IReadOnlyCollection<T> List { get { return Array.AsReadOnly(_array); } }

        public override bool IsArray() { return true; }

        protected override void Register(SR1_Writer writer)
        {
            if (_array != null)
            {
                writer.File._Primatives.Add(this.Start, this);
            }
        }

        public SR1_PrimativeArray(params int[] dimensions)
        {
            _dimensions = new int[dimensions.Length];
            dimensions.CopyTo(_dimensions, 0);

            int arrayLength = 1;
            foreach (int d in dimensions)
            {
                arrayLength *= d;
            }

            if (arrayLength > 0)
            {
                _array = new T[arrayLength];
            }
        }

        public override TreeList.Node CreateNode()
        {
            string offsetString = " (0x" + Start.ToString("X8") + "-" + "0x" + End.ToString("X8") + ")";
            string typeString = GetTypeName(true);
            string nameString = Name;
            string valueString = ToString();
            TreeList.Node node = new TreeList.Node(new string[] { offsetString, typeString, nameString, valueString });

            if (_array != null)
            {
                TreeList.Node[] elementNodes = new TreeList.Node[_array.Length];
                uint size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(_array[0]);
                uint start = Start;
                uint end = Start + size;
                for (int i = 0; i < _array.Length; i++)
                {
                    offsetString = " (0x" + start.ToString("X8") + "-" + "0x" + end.ToString("X8") + ")";
                    typeString = GetTypeName(false);
                    nameString = "";
                    valueString = _array[i].ToString();

                    int index = i;
                    int d = _dimensions.Length;
                    while (d > 0)
                    {
                        d--;
                        int subIndex = index % _dimensions[d];
                        index /= _dimensions[d];
                        string indexName = "";
                        indexName += "[";
                        indexName += subIndex.ToString();
                        indexName += "]";
                        indexName += nameString;
                        nameString = indexName;
                    }

                    elementNodes[i] = new TreeList.Node(new string[] { offsetString, typeString, nameString, valueString });

                    start += size;
                    end += size;
                }

                node.Nodes.AddRange(elementNodes);
            }

            return node;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            if (_array != null)
            {
                for (int i = 0; i < _array.Length; i++)
                {
                    _array[i] = (T)ReadPrimativeType<T>(reader);
                }
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            // Primatives don't contain references.
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            if (_array != null)
            {
                for (int i = 0; i < _array.Length; i++)
                {
                   WritePrimativeType<T>(writer, _array[i]);
                }
            }
        }

        public override string ToString()
        {
            if (typeof(T) == typeof(char))
            {
                if (_array != null && _dimensions.Length == 1)
                {
                    string result = "";
                    foreach (object o in _array)
                    {
                        if ((char)o == '\0')
                        {
                            break;
                        }

                        result += (char)o;
                    }
                    return result;
                }

                return "";
            }
            else
            {
                if (_array == null)
                {
                    return "{ }";
                }

                if (_dimensions.Length == 1)
                {
                    string result = "{ ";
                    foreach (object o in _array)
                    {
                        result += o.ToString();
                        result += ", ";
                    }
                    result = result.Trim(',', ' ');
                    result += " }";
                    return result;
                }

                return "{...}";
            }
        }

        public override string GetTypeName(bool includeDimensions)
        {
            string typeName = GetPrimativeTypeName(Type.GetTypeCode(typeof(T)));

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
