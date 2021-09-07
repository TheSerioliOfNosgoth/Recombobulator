using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class TextureFT3 : SR1_Structure
    {
        public readonly SR1_Primative<byte> u0 = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> v0 = new SR1_Primative<byte>();
        public readonly SR1_Primative<ushort> clut = new SR1_Primative<ushort>();
        public readonly SR1_Primative<byte> u1 = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> v1 = new SR1_Primative<byte>();
        public readonly SR1_Primative<ushort> tpage = new SR1_Primative<ushort>();
        public readonly SR1_Primative<ushort> attr2 = new SR1_Primative<ushort>();
        public readonly SR1_Primative<byte> u2 = new SR1_Primative<byte>();
        public readonly SR1_Primative<byte> v2 = new SR1_Primative<byte>();
        public readonly SR1_Primative<ushort> attr = new SR1_Primative<ushort>();
        public readonly SR1_Primative<int> color = new SR1_Primative<int>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            u0.Read(reader, this, "u0");
            v0.Read(reader, this, "v0");

            clut.Read(reader, this, "clut", SR1_File.Version.First, SR1_File.Version.Retail_PC);
            tpage.Read(reader, this, "tpage", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u1.Read(reader, this, "u1");
            v1.Read(reader, this, "v1");

            tpage.Read(reader, this, "tpage", SR1_File.Version.First, SR1_File.Version.Retail_PC);
            attr2.Read(reader, this, "attr2", SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u2.Read(reader, this, "u2");
            v2.Read(reader, this, "v2");
            attr.Read(reader, this, "attr");

            color.Read(reader, this, "color", SR1_File.Version.Feb16, SR1_File.Version.May12);

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
            attr2.Write(writer, SR1_File.Version.Retail_PC, SR1_File.Version.Next);

            u2.Write(writer);
            v2.Write(writer);
            attr.Write(writer);

            color.Write(writer, SR1_File.Version.Feb16, SR1_File.Version.May12);
        }

        public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
        {
            base.MigrateVersion(file, targetVersion, migrateFlags);

            if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
            {
                int textureID = file._Overrides.NewTextureIDs[tpage.Value & 0x00000007];
                tpage.Value = (ushort)textureID;
                attr2.Value = 0x0108;

                if (file._Version <= SR1_File.Version.Feb16)
                {
                    if ((attr.Value & 0x0010) != 0)
                    {
                        tpage.Value |= 0x4000;
                        attr2.Value |= 0x0060;
                    }
                }
                else
                {
                    if ((attr.Value & 0x0040) != 0)
                    {
                        tpage.Value |= 0x4000;
                        attr2.Value |= 0x0060;
                    }
                }

                //attr.Value &= ~0x0040;
            }
        }
    }
}
