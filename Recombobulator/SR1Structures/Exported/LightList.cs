using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class LightList : SR1_Structure
	{
		CVector ambient = new CVector();
		SR1_Primative<int> numLightGroups = new SR1_Primative<int>();
		SR1_Pointer<LightGroup> lightGroupList = new SR1_Pointer<LightGroup>();

		SR1_StructureArray<LightGroup> lightGroupListBuf = new SR1_StructureArray<LightGroup>(0);

		bool skipAmbient = false;

		public void SetSkipAmbient(bool skip)
		{
			skipAmbient = skip;
		}

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			if (!skipAmbient)
			{
				ambient.Read(reader, this, "ambient");
			}

			numLightGroups.Read(reader, this, "numLightGroups");
			lightGroupList.Read(reader, this, "lightGroupList");

			lightGroupListBuf = new SR1_StructureArray<LightGroup>(numLightGroups.Value);
			lightGroupListBuf.Read(reader, this, "lightGroupListBuf");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			if (!skipAmbient)
			{
				ambient.Write(writer);
			}

			numLightGroups.Write(writer);
			lightGroupList.Write(writer);

			lightGroupListBuf.Write(writer);
		}
	}
}
