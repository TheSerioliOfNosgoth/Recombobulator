using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GlyphColors : SR1_Structure
	{
		SR1_Primative<uint> topLeft = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> topRight = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> bottonLeft = new SR1_Primative<uint>().ShowAsHex(true);
		SR1_Primative<uint> bottomRight = new SR1_Primative<uint>().ShowAsHex(true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			topLeft.Read(reader, this, "topLeft");
			topRight.Read(reader, this, "topRight");
			bottonLeft.Read(reader, this, "bottonLeft");
			bottomRight.Read(reader, this, "bottomRight");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			topLeft.Write(writer);
			topRight.Write(writer);
			bottonLeft.Write(writer);
			bottomRight.Write(writer);
		}
	}
}
