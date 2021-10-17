using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class DrMoveAniTex : SR1_Structure
	{
		SR1_Primative<int> numAniTextures = new SR1_Primative<int>();
		SR1_PointerArray<DrMoveAniTexDestInfo> aniTexInfo = new SR1_PointerArray<DrMoveAniTexDestInfo>(0, true);

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			numAniTextures.Read(reader, this, "numAniTextures");
			aniTexInfo = new SR1_PointerArray<DrMoveAniTexDestInfo>(numAniTextures.Value, true);
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

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				foreach (SR1_PointerBase aniTexDestInfoPointer in aniTexInfo.List)
				{
					file._Structures.Remove(aniTexDestInfoPointer.Offset);
				}
			}
		}
	}
}
