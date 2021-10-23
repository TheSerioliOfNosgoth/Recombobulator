using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class MonsterSenses : SR1_Structure
	{
		SR1_Primative<short> sightArc = new SR1_Primative<short>();
		SR1_Primative<short> sightRadius = new SR1_Primative<short>();
		SR1_Primative<short> sightElevation = new SR1_Primative<short>();
		SR1_Primative<short> soundArc = new SR1_Primative<short>();
		SR1_Primative<short> soundRadius = new SR1_Primative<short>();
		SR1_Primative<short> soundElevation = new SR1_Primative<short>();
		SR1_Primative<ushort> scentRadius = new SR1_Primative<ushort>();
		SR1_Primative<short> alarmRadius = new SR1_Primative<short>();
		SR1_Primative<ushort> allyRadius = new SR1_Primative<ushort>();
		SR1_Primative<short> pad = new SR1_Primative<short>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			sightArc.Read(reader, this, "sightArc");
			sightRadius.Read(reader, this, "sightRadius");
			sightElevation.Read(reader, this, "sightElevation");
			soundArc.Read(reader, this, "soundArc");
			soundRadius.Read(reader, this, "soundRadius");
			soundElevation.Read(reader, this, "soundElevation");
			scentRadius.Read(reader, this, "scentRadius");
			alarmRadius.Read(reader, this, "alarmRadius");
			allyRadius.Read(reader, this, "allyRadius");
			pad.Read(reader, this, "pad");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			sightArc.Write(writer);
			sightRadius.Write(writer);
			sightElevation.Write(writer);
			soundArc.Write(writer);
			soundRadius.Write(writer);
			soundElevation.Write(writer);
			scentRadius.Write(writer);
			alarmRadius.Write(writer);
			allyRadius.Write(writer);
			pad.Write(writer);
		}
	}
}
