using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class TFace : SR1_Structure
    {
        Face face = new Face();
        SR1_Primative<byte> attr = new SR1_Primative<byte>();
        SR1_Primative<sbyte> sortPush = new SR1_Primative<sbyte>();
        SR1_Primative<ushort> normal = new SR1_Primative<ushort>();
        SR1_Primative<ushort> textoff = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            face.Read(reader, this, "face");
            attr.Read(reader, this, "attr");
            sortPush.Read(reader, this, "sortPush");
            normal.Read(reader, this, "normal");
            textoff.Read(reader, this, "textoff");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            face.Write(writer);
            attr.Write(writer);
            sortPush.Write(writer);
            normal.Write(writer);
            textoff.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if (file._Version <= SR1_File.Version.Feb16 && targetVersion > SR1_File.Version.Feb16)
            {
                textoff.Value = (ushort)((textoff.Value * 12) / 16);
                attr.Value = (byte)(attr.Value & 0x02);
            }
        }
    }
}
