using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class SR1_PointerArrayPointer<T> : SR1_PointerBase where T : SR1_Structure, new()
	{
		public override Type GetGenericType()
		{
			return typeof(T);
		}

		public override object CreateObject(SR1_Structure parent, SR1_Reader reader)
		{
			return null;
		}

		public SR1_PointerArrayPointer()
		{
		}
	}
}
