using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SFXFileData : SR1_Structure
    {
        SR1_Primative<ushort> type = new SR1_Primative<ushort>();
        SR1_Primative<ushort> numSounds = new SR1_Primative<ushort>();
        SR1_StructureArray<ObjectSound> sounds = new SR1_StructureArray<ObjectSound>(0);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            type.Read(reader, this, "type");
            numSounds.Read(reader, this, "numSounds");

            // A sound was ommitted from flameh and sgwindw in one version of the game, but the array length is still 2.
            // Hack to make it export correctly.
            if (reader.Object != null &&
                reader.Object.Name == "flameh__" &&
                reader.Model.Start == Start + 0x18)
            {
                sounds = new SR1_StructureArray<ObjectSound>(1);
            }
            else if (reader.Object != null &&
                reader.Object.Name == "sgwindw_" &&
                reader.Model.Start == Start + 0x24)
            {
                sounds = new SR1_StructureArray<ObjectSound>(2);
            }
            else
            {
                sounds = new SR1_StructureArray<ObjectSound>(numSounds.Value);
            }

            sounds.Read(reader, this, "entries");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            type.Write(writer);
            numSounds.Write(writer);
            sounds.Write(writer);
        }
    }
}
