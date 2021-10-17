using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class AniTex : SR1_Structure
	{
		SR1_Primative<int> numAniTextures = new SR1_Primative<int>();
		SR1_StructureArray<AniTexInfo> aniTexInfo = new SR1_StructureArray<AniTexInfo>(0);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numAniTextures.Read(reader, this, "numAniTextures");
			aniTexInfo = new SR1_StructureArray<AniTexInfo>(numAniTextures.Value);
			aniTexInfo.Read(reader, this, "aniTexInfo");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			numAniTextures.Write(writer);

			if (numAniTextures.Value > 0)
			{
				aniTexInfo.Write(writer);
			}
		}
	}
}
