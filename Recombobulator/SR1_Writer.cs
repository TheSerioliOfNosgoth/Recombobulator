using System.Collections.Generic;
using System.IO;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
    class SR1_Writer : BinaryWriter
    {
        public SR1_File File = null;
        public StringWriter Errors = new StringWriter();

        public SR1_Writer(SR1_File file,Stream output)
            : base(output, System.Text.Encoding.UTF8)
        {
            File = file;
        }

        public void LogError(string error)
        {
            Errors.WriteLine(error);
        }
    }
}