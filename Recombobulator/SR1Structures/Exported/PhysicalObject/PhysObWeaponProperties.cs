using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PhysObWeaponProperties : PhysObPropertiesBase
	{
		public readonly PhysObWeaponAttributes WeaponAttributes = new PhysObWeaponAttributes();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			Properties.Read(reader, this, "Properties");
			WeaponAttributes.Read(reader, this, "WeaponAttributes");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			Properties.Write(writer);
			WeaponAttributes.Write(writer);
		}
	}
}
