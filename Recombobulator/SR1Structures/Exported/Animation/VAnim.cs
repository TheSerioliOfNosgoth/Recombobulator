using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class VAnim : SR1_Structure
	{
		SR1_Primative<byte> anim0 = new SR1_Primative<byte>();
		SR1_Primative<byte> anim1 = new SR1_Primative<byte>();
		SR1_Primative<byte> anim2 = new SR1_Primative<byte>();
		SR1_Primative<byte> frames = new SR1_Primative<byte>();
		SR1_Primative<byte> mode = new SR1_Primative<byte>();
		SR1_Primative<byte> alpha = new SR1_Primative<byte>();
		SR1_Primative<ushort> frame = new SR1_Primative<ushort>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			anim0.Read(reader, this, "anim0");
			anim1.Read(reader, this, "anim1");
			anim2.Read(reader, this, "anim2");
			frames.Read(reader, this, "frames");
			mode.Read(reader, this, "mode");
			alpha.Read(reader, this, "alpha");
			frame.Read(reader, this, "frame");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			anim0.Write(writer);
			anim1.Write(writer);
			anim2.Write(writer);
			frames.Write(writer);
			mode.Write(writer);
			alpha.Write(writer);
			frame.Write(writer);
		}
	}
}
