﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_PrimativeBase : SR1_Structure
	{
		protected bool _showAsHex;

		public virtual bool IsArray() { return false; }

		protected override void Register(SR1_Writer writer)
		{
			writer.File._LastPrimative = this;

			// Might be safer to add primatives during import rather than export.
			if (Start == End || writer.File._Primatives.ContainsKey(Start))
			{
				return;
			}

			writer.File._Primatives.Add(Start, this);
		}

		public bool IsShowAsHex()
		{
			return _showAsHex;
		}
	}
}
