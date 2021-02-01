using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SR1_StructureList<T> : SR1_Structure where T : SR1_Structure
    {
        private List<SR1_Structure> _List = new List<SR1_Structure>();
        public int Count { get { return _List.Count; } }

        public IReadOnlyCollection<SR1_Structure> List { get { return _List; } }

        public SR1_StructureList()
        {
        }

        public void Add(SR1_Structure entry)
        {
            _List.Add(entry);
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            for (int i = 0; i < _List.Count; i++)
            {
                Type type = _List[i].GetType();
                if (type == typeof(SR1Structures.EventBasicObject))
                {
                    long oldPosition = reader.BaseStream.Position;
                    SR1Structures.EventBasicObject tempEBO = new SR1Structures.EventBasicObject();
                    tempEBO.ReadTemp(reader);
                    reader.BaseStream.Position = oldPosition;

                    _List[i] = (T)tempEBO.CreateReplacementObject();
                }
                else if (type == typeof(SR1Structures.ObjectSound))
                {
                    long oldPosition = reader.BaseStream.Position;
                    SR1Structures.ObjectSound tempOS = new SR1Structures.ObjectSound();
                    tempOS.ReadTemp(reader);
                    reader.BaseStream.Position = oldPosition;

                    _List[i] = (T)tempOS.CreateReplacementObject();
                }
                else if (type == typeof(SR1Structures.PhysObProperties))
                {
                    long oldPosition = reader.BaseStream.Position;
                    SR1Structures.PhysObProperties tempPOP = new SR1Structures.PhysObProperties();
                    tempPOP.ReadTemp(reader);
                    reader.BaseStream.Position = oldPosition;

                    _List[i] = (T)tempPOP.CreateReplacementObject();
                }
                else if (type == typeof(SR1Structures.VMObject))
                {
                    long oldPosition = reader.BaseStream.Position;
                    SR1Structures.VMObject tempVMO = new SR1Structures.VMObject();
                    tempVMO.ReadTemp(reader);
                    reader.BaseStream.Position = oldPosition;

                    _List[i] = (T)tempVMO.CreateReplacementObject();
                }

                _List[i].Read(reader, this, "[" + i.ToString() + "]");
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            for (int i = 0; i < _List.Count; i++)
            {
                _List[i].Write(writer);
            }
        }

        public override string ToString()
        {
            if (_List.Count == 0)
            {
                return "{ }";
            }

            return "{...}";
        }

        public override string GetTypeName(bool includeDimensions)
        {
            Type type = typeof(T);
            string typeName = type.Name;

            if (type.IsGenericType)
            {
                typeName = type.GenericTypeArguments[type.GenericTypeArguments.Length-1].Name;
            }

            if (type.BaseType == typeof(SR1_PointerBase) || type.BaseType == typeof(SR1_PointerArray))
            {
                typeName += "*";
            }

            if (_List.Count == 0)
            {
                typeName += "[]";
                return typeName;
            }

            if (includeDimensions)
            {
                typeName += "[";
                typeName += _List.Count;
                typeName += "]";
            }

            if (type.BaseType == typeof(SR1_PointerArray))
            {
                typeName += "[]";
            }

            return typeName;
        }
    }
}
