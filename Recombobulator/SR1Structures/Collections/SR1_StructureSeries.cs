using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    abstract class SR1_StructureSeries : SR1_StructureSeriesBase<SR1_Structure>
    {
    }

    class SR1_StructureSeries<T> : SR1_StructureSeries where T : SR1_Structure, new()
    {
        protected int _BufferLength;

        public SR1_StructureSeries(int bufferLength)
        {
            _BufferLength = bufferLength;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            long endPosition = reader.BaseStream.Position + _BufferLength;
            for (int i = 0; reader.BaseStream.Position < endPosition; i++)
            {
                T newEntry = new T();
                newEntry.Read(reader, this, "[" + i.ToString() + "]");
                _List.Add(newEntry);
            }
        }

        public override string GetTypeName(bool includeDimensions)
        {
            string typeName = typeof(T).Name;

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

            return typeName;
        }
    }
}
