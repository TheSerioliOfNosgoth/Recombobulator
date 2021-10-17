using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class PhysObProjectileData : SR1_Structure
	{
		SR1_Primative<byte> model = new SR1_Primative<byte>();
		SR1_Primative<byte> startAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> loopAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> endAnim = new SR1_Primative<byte>();
		SR1_Primative<byte> pad = new SR1_Primative<byte>();
		SR1_Primative<int> flags = new SR1_Primative<int>();
		SR1_Pointer<PhysObWeaponAttributes> weapon = new SR1_Pointer<PhysObWeaponAttributes>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			model.Read(reader, this, "model");
			startAnim.Read(reader, this, "startAnim");
			loopAnim.Read(reader, this, "loopAnim", SR1_File.Version.May12, SR1_File.Version.Next);
			endAnim.Read(reader, this, "endAnim");
			pad.Read(reader, this, "pad", SR1_File.Version.First, SR1_File.Version.May12);
			flags.Read(reader, this, "flags", SR1_File.Version.May12, SR1_File.Version.Next);
			weapon.Read(reader, this, "weapon");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new PhysObWeaponAttributes().ReadFromPointer(reader, weapon);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			model.Write(writer);
			startAnim.Write(writer);
			loopAnim.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			endAnim.Write(writer);
			pad.Write(writer, SR1_File.Version.First, SR1_File.Version.May12);
			flags.Write(writer, SR1_File.Version.May12, SR1_File.Version.Next);
			weapon.Write(writer);
		}
	}
}