using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class BSPTree : SR1_Structure
	{
		public readonly SR1_Pointer<BSPNode> bspRoot = new SR1_Pointer<BSPNode>();
		public readonly SR1_Pointer<BSPLeaf> startLeaves = new SR1_Pointer<BSPLeaf>();
		public readonly SR1_Pointer<BSPLeaf> endLeaves = new SR1_Pointer<BSPLeaf>();
		public readonly Position globalOffset = new Position();
		public readonly SR1_Primative<short> flags = new SR1_Primative<short>().ShowAsHex(true);
		public readonly Position localOffset = new Position();
		public readonly SR1_Primative<short> ID = new SR1_Primative<short>();
		public readonly SR1_Primative<int> splineID = new SR1_Primative<int>();
		public readonly SR1_Pointer<Instance> instanceSpline = new SR1_Pointer<Instance>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			bspRoot.Read(reader, this, "bspRoot");
			bspRoot.PointsToStartOfStruct = true;
			startLeaves.Read(reader, this, "startLeaves");
			startLeaves.PointsToStartOfStruct = true;
			endLeaves.Read(reader, this, "endLeaves");
			endLeaves.PointsToEndOfStruct = true;
			globalOffset.Read(reader, this, "globalOffset");
			flags.Read(reader, this, "flags");
			localOffset.Read(reader, this, "localOffset");
			ID.Read(reader, this, "ID");
			splineID.Read(reader, this, "splineID");
			instanceSpline.Read(reader, this, "instanceSpline");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			if ((int)(startLeaves.Offset - bspRoot.Offset) > 0)
			{
				new SR1_StructureSeries<BSPNode>((int)(startLeaves.Offset - bspRoot.Offset)).ReadFromPointer(reader, bspRoot);
			}

			if ((int)(endLeaves.Offset - startLeaves.Offset) > 0)
			{
				new SR1_StructureSeries<BSPLeaf>((int)(endLeaves.Offset - startLeaves.Offset)).ReadFromPointer(reader, startLeaves);
			}
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			bspRoot.Write(writer);
			startLeaves.Write(writer);
			endLeaves.Write(writer);
			globalOffset.Write(writer);
			flags.Write(writer);
			localOffset.Write(writer);
			ID.Write(writer);
			splineID.Write(writer);
			instanceSpline.Write(writer);
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				// Open doors to mrlock13
				if (file._Structures[0].Name == "mrlock6" && ID.Value == 4 || ID.Value == 5 || ID.Value == 6 || ID.Value == 7 || ID.Value == 9)
				{
					flags.Value |= 0x0003; // Invisible and no collision.
				}

				// Open doors to mrlock13
				if (file._Structures[0].Name == "nightb5")
				{
					// Open doors to adda1
					if (ID.Value == 3 || ID.Value == 4)
					{
						flags.Value |= 0x0003; // Invisible and no collision.
					}

					// Clise doors to adda1
					if (ID.Value == 5 || ID.Value == 6)
					{
						flags.Value &= ~0x0003; // Invisible and no collision.
					}
				}

				if (file._Structures[0].Name == "movie2")
                {
					if (ID.Value == 1 || ID.Value == 3 || ID.Value == 6)
                    {
                        flags.Value |= 0x0003; // Invisible and no collision.
                    }
					else if (ID.Value == 2 || ID.Value == 5)
					{
						flags.Value &= unchecked((short)0xFFFC); // Visible and collision.
					}
                }

				// Burn in sunlight. Set on fire.
				// The 0x0040 seems right, but not sure about the 0x0002.
				if ((flags.Value & 0x0002) != 0)
				{
					flags.Value |= 0x0040;
				}
			}
		}
	}
}
