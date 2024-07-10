using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Recombobulator.SR1Structures
{
	public class SR1_Primative<T> : SR1_PrimativeBase // where T : struct
	{

		public T Value { get; set; }

		public SR1_Primative()
		{
		}

		public SR1_Primative<T> ShowAsHex(bool hex)
		{
			_showAsHex = hex;
			return this;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			Value = (T)ReadPrimativeType<T>(reader);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			// Primatives don't contain references.
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			WritePrimativeType<T>(writer, Value);
		}

		public override string ToString()
		{
			if (_showAsHex)
			{
				return GetPrimativeAsHex(Value);
			}

			return Value.ToString();
		}

		public override bool TryParse(string value)
		{
			if (_showAsHex && GetHexAsPrimitive(value, out T fromHex))
			{
				Value = fromHex;
				return true;
			}

			if (!_showAsHex && GetStringAsPrimitive(value, out T fromString))
			{
				Value = fromString;
				return true;
			}

			return false;
		}

		public override string GetTypeName(bool includeDimensions)
		{
			return GetPrimativeTypeName(Type.GetTypeCode(typeof(T)));
		}
	}
}
