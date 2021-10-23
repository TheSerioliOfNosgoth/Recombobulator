using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class GenericBubbleParams : SR1_Structure
	{
		BubbleParams bubbleParams = new BubbleParams();
		SR1_Primative<short> number = new SR1_Primative<short>();
		SR1_Primative<short> birthRadius = new SR1_Primative<short>();
		SR1_Primative<short> velx = new SR1_Primative<short>();
		SR1_Primative<short> vely = new SR1_Primative<short>();
		SR1_Primative<short> velz = new SR1_Primative<short>();
		SR1_Primative<short> velxrange = new SR1_Primative<short>();
		SR1_Primative<short> velyrange = new SR1_Primative<short>();
		SR1_Primative<short> velzrange = new SR1_Primative<short>();
		SR1_Primative<short> accx = new SR1_Primative<short>();
		SR1_Primative<short> accy = new SR1_Primative<short>();
		SR1_Primative<short> accz = new SR1_Primative<short>();
		SR1_Primative<short> accxrange = new SR1_Primative<short>();
		SR1_Primative<short> accyrange = new SR1_Primative<short>();
		SR1_Primative<short> acczrange = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			bubbleParams.Read(reader, this, "number");
			number.Read(reader, this, "number");
			birthRadius.Read(reader, this, "birthRadius");
			velx.Read(reader, this, "velx");
			vely.Read(reader, this, "vely");
			velz.Read(reader, this, "velz");
			velxrange.Read(reader, this, "velxrange");
			velyrange.Read(reader, this, "velyrange");
			velzrange.Read(reader, this, "velzrange");
			accx.Read(reader, this, "accx");
			accy.Read(reader, this, "accy");
			accz.Read(reader, this, "accz");
			accxrange.Read(reader, this, "accxrange");
			accyrange.Read(reader, this, "accyrange");
			acczrange.Read(reader, this, "acczrange");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			bubbleParams.Write(writer);
			number.Write(writer);
			birthRadius.Write(writer);
			velx.Write(writer);
			vely.Write(writer);
			velz.Write(writer);
			velxrange.Write(writer);
			velyrange.Write(writer);
			velzrange.Write(writer);
			accx.Write(writer);
			accy.Write(writer);
			accz.Write(writer);
			accxrange.Write(writer);
			accyrange.Write(writer);
			acczrange.Write(writer);
		}
	}
}
