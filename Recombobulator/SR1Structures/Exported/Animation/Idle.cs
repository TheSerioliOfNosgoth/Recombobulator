using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class Idle : SR1_Structure
	{
		SR1_Primative<byte> anim = new SR1_Primative<byte>();
		SR1_Primative<byte> frame = new SR1_Primative<byte>();
		SR1_Primative<byte> frames = new SR1_Primative<byte>();
		SR1_Primative<byte> type = new SR1_Primative<byte>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			anim.Read(reader, this, "anim");
			frame.Read(reader, this, "frame");
			frames.Read(reader, this, "frames");
			type.Read(reader, this, "type");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			anim.Write(writer);
			frame.Write(writer);
			frames.Write(writer);
			type.Write(writer);
		}
	}
}
