using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class MonsterMissile : SR1_Structure
    {
        SR1_Primative<ushort> speed = new SR1_Primative<ushort>();
        SR1_Primative<ushort> range = new SR1_Primative<ushort>();
        SR1_Primative<byte> frame = new SR1_Primative<byte>();
        SR1_Primative<byte> anim = new SR1_Primative<byte>();
        SR1_Primative<byte> segment = new SR1_Primative<byte>();
        SR1_Primative<byte> damage = new SR1_Primative<byte>();
        SR1_Primative<byte> type = new SR1_Primative<byte>();
        SR1_Primative<byte> graphic = new SR1_Primative<byte>();
        SR1_Primative<sbyte> gravity = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> reload = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> fireChance = new SR1_Primative<sbyte>();
        SR1_Primative<byte> numAnims = new SR1_Primative<byte>();
        SR1_PrimativeArray<sbyte> animList = new SR1_PrimativeArray<sbyte>(2);

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            speed.Read(reader, this, "speed");
            range.Read(reader, this, "range");
            frame.Read(reader, this, "frame");
            anim.Read(reader, this, "anim");
            segment.Read(reader, this, "segment");
            damage.Read(reader, this, "damage");
            type.Read(reader, this, "type");
            graphic.Read(reader, this, "graphic");
            gravity.Read(reader, this, "gravity");
            reload.Read(reader, this, "reload");
            fireChance.Read(reader, this, "fireChance");
            numAnims.Read(reader, this, "numAnims");
            animList.Read(reader, this, "animList");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            speed.Write(writer);
            range.Write(writer);
            frame.Write(writer);
            anim.Write(writer);
            segment.Write(writer);
            damage.Write(writer);
            type.Write(writer);
            graphic.Write(writer);
            gravity.Write(writer);
            reload.Write(writer);
            fireChance.Write(writer);
            numAnims.Write(writer);
            animList.Write(writer);
        }
    }
}
