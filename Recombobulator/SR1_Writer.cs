using System.Collections.Generic;
using System.IO;
using Recombobulator.SR1Structures;

namespace Recombobulator
{
	class SR1_Writer : BinaryWriter
	{
		public SR1_File File = null;
		public StringWriter Errors = new StringWriter();

		public SR1_Writer(SR1_File file, Stream output, bool leaveOpen)
			: base(output, System.Text.Encoding.UTF8, leaveOpen)
		{
			File = file;
		}

		public void LogError(string error)
		{
			Errors.WriteLine(error);
		}
	}
}