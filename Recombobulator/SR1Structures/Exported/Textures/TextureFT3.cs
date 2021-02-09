using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class TextureFT3 : SR1_Structure
    {
        SR1_Primative<byte> u0 = new SR1_Primative<byte>();
        SR1_Primative<byte> v0 = new SR1_Primative<byte>();
        SR1_Primative<ushort> clut = new SR1_Primative<ushort>();
        SR1_Primative<byte> u1 = new SR1_Primative<byte>();
        SR1_Primative<byte> v1 = new SR1_Primative<byte>();
        SR1_Primative<ushort> tpage = new SR1_Primative<ushort>();
        SR1_Primative<ushort> pad = new SR1_Primative<ushort>();
        SR1_Primative<byte> u2 = new SR1_Primative<byte>();
        SR1_Primative<byte> v2 = new SR1_Primative<byte>();
        SR1_Primative<ushort> attr = new SR1_Primative<ushort>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            u0.Read(reader, this, "u0");
            v0.Read(reader, this, "v0");

            clut.Read(reader, this, "clut", SR1_File.Version.First, SR1_File.Version.Retail_PC);
            tpage.Read(reader, this, "tpage", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u1.Read(reader, this, "u1");
            v1.Read(reader, this, "v1");

            tpage.Read(reader, this, "tpage", SR1_File.Version.First, SR1_File.Version.Retail_PC);
            pad.Read(reader, this, "pad", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u2.Read(reader, this, "u2");
            v2.Read(reader, this, "v2");
            attr.Read(reader, this, "attr");

            ushort textureID;
            if (reader.File._Version == SR1_File.Version.Retail_PC)
            {
                textureID = (ushort)((uint)tpage.Value & 0x000007FF);
            }
            else
            {
                textureID = (ushort)((uint)tpage.Value & 0x00000007);
            }

            if (!reader.File._TextureIDs.Contains(textureID))
            {
                reader.File._TextureIDs.Add(textureID);
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            u0.Write(writer);
            v0.Write(writer);

            clut.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
            tpage.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u1.Write(writer);
            v1.Write(writer);

            tpage.Write(writer, SR1_File.Version.First, SR1_File.Version.Retail_PC);
            pad.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u2.Write(writer);
            v2.Write(writer);
            attr.Write(writer);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion)
        {
            base.MigrateVersion(file, targetVersion);

            if ((file._Version == SR1_File.Version.Retail || file._Version == SR1_File.Version.Beta) &&
                targetVersion == SR1_File.Version.Retail_PC)
            {
                int textureID = file._NewTextureIDs[tpage.Value & 0x00000007];
                tpage.Value = (ushort)textureID;
                pad.Value = 264;
            }
        }
    }
}
