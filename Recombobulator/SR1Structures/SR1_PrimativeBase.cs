using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_PrimativeBase : SR1_Structure
	{
		protected bool _showAsHex;

		public virtual bool IsArray() { return false; }

		protected override void AddToRead(SR1_Reader reader)
		{
			if (Start != End &&
				!reader.File._PrimsRead.Contains(this))
			{
				reader.File._PrimsRead.Add(this);
			}
		}

		protected override void AddToWritten(SR1_Writer writer)
		{
			if (Start != End && !writer.File.IsWritingMigStruct &&
				!writer.File._PrimsWritten.Contains(this))
			{
				writer.File._PrimsWritten.Add(this);
			}
		}

		public bool IsShowAsHex()
		{
			return _showAsHex;
		}
	}
}
