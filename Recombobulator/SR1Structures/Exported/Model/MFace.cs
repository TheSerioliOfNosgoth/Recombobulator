using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MFace : SR1_Structure
	{
		public readonly Face face = new Face();
		public readonly SR1_Primative<byte> normal = new SR1_Primative<byte>();
		public readonly SR1_Primative<byte> flags = new SR1_Primative<byte>();
		// TextureMT3 pointer if 2nd bit is set in flags, otherwise color.
		public readonly SR1_Pointer<TextureMT3> texture = new SR1_Pointer<TextureMT3>();
		public readonly SR1_Primative<int> color = new SR1_Primative<int>();

		public TextureMT3 Texture = null;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			face.Read(reader, this, "face");
			normal.Read(reader, this, "normal");
			flags.Read(reader, this, "flags");

			if ((flags.Value & 2) != 0)
			{
				texture.Read(reader, this, "texture");
			}
			else
			{
				color.Read(reader, this, "color");
			}
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			face.Write(writer);
			normal.Write(writer);
			flags.Write(writer);

			if ((flags.Value & 2) != 0)
			{
				texture.Write(writer);
			}
			else
			{
				color.Write(writer);
			}
		}
	}
}
