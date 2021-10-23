using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PhysObProperties : SR1_Structure
	{
		public readonly SR1_Primative<short> version = new SR1_Primative<short>().ShowAsHex(true);
		public readonly SR1_Primative<short> family = new SR1_Primative<short>();
		public readonly SR1_Primative<short> ID = new SR1_Primative<short>().ShowAsHex(true);
		public readonly SR1_Primative<short> Type = new SR1_Primative<short>().ShowAsHex(true);

		public object CreateReplacementObject()
		{
			// Weapon figured out from PhysObGetWeapon.
			// Switch from SwitchPhysOb.
			// Interact from InteractPhysOb.
			// Collectible from gstone, gforce, gfire, healthu, etc.
			// Updraft from loading updraft.pcm.
			// Family 1 only seems concerned with gravity, so likely a standard physical object.
			// That leaves 6 as the animated physical object.
			switch (family.Value)
			{
				case 0:
					return new PhysObWeaponProperties();
				case 1:
					return new PhysObGenericProperties(4);
				case 2:
					return new PhysObSwitchProperties();
				case 3:
					return new PhysObInteractProperties();
				case 4:
					return new UpdraftProperties();
				case 5:
					return new PhysObCollectibleProperties();
				case 6:
					return new PhysObAnimatedProperties();
				case 7:
					// There's a __PhysObProjectileData in extraData containing a PhysObWeaponAttributes
					return new PhysObProjectileProperties();
				default:
					return new PhysObGenericProperties(0);
			}
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			version.Read(reader, this, "version");
			family.Read(reader, this, "family");
			ID.Read(reader, this, "ID");
			Type.Read(reader, this, "type");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			version.Write(writer);
			family.Write(writer);
			ID.Write(writer);
			Type.Write(writer);
		}
	}
}
