using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class DrMoveAniTex : SR1_Structure
	{
		public readonly SR1_Primative<int> numAniTextures = new SR1_Primative<int>();
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
				if ((migrateFlags & SR1_File.MigrateFlags.RemoveAnimatedTextures) != 0)
				{
					foreach (SR1_PointerBase aniTexDestInfoPointer in aniTexInfo)
					{
						file._Structures.Remove(aniTexDestInfoPointer.Offset);
					}
				}
			}
		}

		public int GetAnimatedTextureIndex(SR1_File file, TextureFT3 texture)
		{
			if (file != null && texture != null)
			{
				for (int i = 0; i < aniTexInfo.Count; i++)
				{
					DrMoveAniTexDestInfo drMoveAniTex =
						(DrMoveAniTexDestInfo)file._Structures[aniTexInfo[i].Offset];

					if (drMoveAniTex != null)
					{
						if (drMoveAniTex.IsTextureAnimated(texture))
						{
							return i;
						}
					}
				}
			}

			return -1;
		}
	}
}
