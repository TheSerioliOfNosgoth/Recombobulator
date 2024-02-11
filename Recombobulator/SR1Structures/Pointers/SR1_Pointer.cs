using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class SR1_Pointer<T> : SR1_PointerBase where T : class, new()
	{
		public override Type GetGenericType()
		{
			return typeof(T);
		}

		public override object CreateObject(SR1_Structure parent, SR1_Reader reader)
		{
			if (typeof(T) == typeof(SR1Structures.EventBasicObject))
			{
				long oldPosition = reader.BaseStream.Position;
				SR1Structures.EventBasicObject tempEBO = new SR1Structures.EventBasicObject();
				tempEBO.ReadTemp(reader);
				reader.BaseStream.Position = oldPosition;

				return tempEBO.CreateReplacementObject();
			}
			else if (typeof(T) == typeof(SR1Structures.ObjectSound))
			{
				long oldPosition = reader.BaseStream.Position;
				SR1Structures.ObjectSound tempOS = new SR1Structures.ObjectSound();
				tempOS.ReadTemp(reader);
				reader.BaseStream.Position = oldPosition;

				return tempOS.CreateReplacementObject();
			}
			else if (typeof(T) == typeof(SR1Structures.PhysObProperties))
			{
				long oldPosition = reader.BaseStream.Position;
				SR1Structures.PhysObProperties tempPOP = new SR1Structures.PhysObProperties();
				tempPOP.ReadTemp(reader);
				reader.BaseStream.Position = oldPosition;

				return tempPOP.CreateReplacementObject();
			}
			else if (typeof(T) == typeof(SR1Structures.VMObject))
			{
				long oldPosition = reader.BaseStream.Position;
				SR1Structures.VMObject tempVMO = new SR1Structures.VMObject();
				tempVMO.ReadTemp(reader);
				reader.BaseStream.Position = oldPosition;

				return tempVMO.CreateReplacementObject();
			}

			return new T();
		}

		public SR1_Pointer()
		{
		}
	}
}
