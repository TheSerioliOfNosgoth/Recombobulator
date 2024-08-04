using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_PointerBase : SR1_PrimativeBase
	{
		public uint Offset { get; set; }
		public bool PointsToMigStruct { get; set; }
		public bool PointsToStartOfStruct { get; set; }
		public bool PointsToEndOfStruct { get; set; }

		public abstract object CreateObject(SR1_Structure parent, SR1_Reader reader);

		public abstract Type GetGenericType();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			Offset = reader.ReadUInt32();
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			// A pointer doesn't contain pointers. It might point to them, but that's not handled here.
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			// Placeholder. Needs to be fixed in a second pass once everything has a new location.
			writer.Write(0x00000000u);
		}

		protected override void AddToWritten(SR1_Writer writer)
		{
			base.AddToWritten(writer);

			// Store this pointer in the list so that it can be fixed.
			if (Offset != 0)
			{
				writer.File._Pointers.Add(this);
			}
		}

		public override string ToString()
		{
			return "0x" + Offset.ToString("X8");
		}

		public override string GetTypeName(bool includeDimensions)
		{
			Type genericType = GetGenericType();
			string typeName;

			if (genericType.IsPrimitive)
			{
				typeName = GetPrimativeTypeName(Type.GetTypeCode(genericType));
			}
			else if (genericType.IsClass && genericType.BaseType == null)
			{
				typeName = "void";
			}
			else
			{
				typeName = genericType.Name;
			}

			typeName += "*";
			return typeName;
		}

		public bool PrepareToReadReference(SR1_Reader reader)
		{
			if (Offset != 0 && !reader.File._Structures.ContainsKey(Offset))
			{
				reader.BaseStream.Position = Offset;
				return true;
			}

			return false;
		}
	}
}
