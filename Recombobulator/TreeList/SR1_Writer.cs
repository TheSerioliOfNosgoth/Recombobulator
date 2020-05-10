using System.Collections.Generic;
using System.IO;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
    class SR1_Writer : BinaryWriter
    {
        public List<SR1_PointerBase> Pointers { get; } = new List<SR1_PointerBase>();
        public StringWriter Errors = new StringWriter();

        public SR1_Writer(Stream output)
            : base(output, System.Text.Encoding.UTF8)
        {
        }

        public void LogError(string error)
        {
            Errors.WriteLine(error);
        }
    }
}