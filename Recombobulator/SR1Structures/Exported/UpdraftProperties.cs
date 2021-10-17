using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class UpdraftProperties : PhysObPropertiesBase
	{
		new public readonly PhysObProperties Properties;
		SR1_StructureList<PhysObDraftProperties> physObDraftProperties = new SR1_StructureList<PhysObDraftProperties>();

		public UpdraftProperties()
		{
			PhysObDraftProperties physObDraftProperties0 = new PhysObDraftProperties();
			physObDraftProperties.Add(physObDraftProperties0);
			Properties = physObDraftProperties0.Properties;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			PhysObDraftProperties temp = new PhysObDraftProperties();

			// The first one was already added, and there will be at least that one, so skip to the next.
			temp.TestRange(reader);

			// Normally 13 in the list.
			while (temp.TestRange(reader))
			{
				physObDraftProperties.Add(new PhysObDraftProperties());
			}

			reader.BaseStream.Position = Start;
			physObDraftProperties.Read(reader, this, "physObDraftProperties");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			physObDraftProperties.Write(writer);
		}
	}
}