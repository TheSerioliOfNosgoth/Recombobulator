using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class GenericParticleParams : SR1_Structure
    {
        SR1_Primative<short> size = new SR1_Primative<short>();
        SR1_Primative<byte> StartOnInit = new SR1_Primative<byte>();
        SR1_Primative<byte> type = new SR1_Primative<byte>();
        SR1_Primative<short> birthRadius = new SR1_Primative<short>();
        SR1_Primative<sbyte> startSegment = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> endSegment = new SR1_Primative<sbyte>();
        Position direction = new Position();
        SR1_Primative<byte> spectral_colorize = new SR1_Primative<byte>();
        SR1_Primative<byte> absolute_direction = new SR1_Primative<byte>();
        Position acceleration = new Position();
        SR1_Primative<sbyte> accx = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> accy = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> accz = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> useInstanceModel = new SR1_Primative<sbyte>();
        SR1_Primative<short> useInstanceModel_b = new SR1_Primative<short>();
        SR1_Primative<int> startColor = new SR1_Primative<int>();
        SR1_Primative<byte> startColor_red = new SR1_Primative<byte>();
        SR1_Primative<byte> startColor_green = new SR1_Primative<byte>();
        SR1_Primative<byte> startColor_blue = new SR1_Primative<byte>();
        SR1_Primative<sbyte> model = new SR1_Primative<sbyte>();
        SR1_Primative<short> model_b = new SR1_Primative<short>();
        SR1_Primative<int> endColor = new SR1_Primative<int>();
        SR1_Primative<byte> endColor_red = new SR1_Primative<byte>();
        SR1_Primative<byte> endColor_green = new SR1_Primative<byte>();
        SR1_Primative<byte> endColor_blue = new SR1_Primative<byte>();
        SR1_Primative<sbyte> texture = new SR1_Primative<sbyte>();
        SR1_Primative<short> texture_b = new SR1_Primative<short>();
        SR1_Primative<short> lifeTime = new SR1_Primative<short>();
        SR1_Primative<byte> primLifeTime = new SR1_Primative<byte>();
        SR1_Primative<short> primLifeTime_b = new SR1_Primative<short>();
        SR1_Primative<sbyte> use_child = new SR1_Primative<sbyte>();
        SR1_Primative<short> startFadeValue = new SR1_Primative<short>();
        SR1_Primative<short> fadeStep = new SR1_Primative<short>();
        SR1_Primative<sbyte> numberBirthParticles = new SR1_Primative<sbyte>();
        SR1_Primative<sbyte> z_undulation_mode = new SR1_Primative<sbyte>();
        SR1_Primative<short> scaleSpeed = new SR1_Primative<short>();
        Position offset = new Position();
        SR1_Primative<short> startScale = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            if (reader.File._Version >= SR1_File.Version.Jun01)
            {
                size.Read(reader, this, "size");
                StartOnInit.Read(reader, this, "StartOnInit");
                type.Read(reader, this, "type");
                birthRadius.Read(reader, this, "birthRadius");
                startSegment.Read(reader, this, "startSegment");
                endSegment.Read(reader, this, "endSegment");
                direction.Read(reader, this, "direction");
                spectral_colorize.Read(reader, this, "spectral_colorize");
                absolute_direction.Read(reader, this, "absolute_direction");
                accx.Read(reader, this, "accx");
                accy.Read(reader, this, "accy");
                accz.Read(reader, this, "accz");
                useInstanceModel.Read(reader, this, "useInstanceModel");
                startColor_red.Read(reader, this, "startColor_red");
                startColor_green.Read(reader, this, "startColor_green");
                startColor_blue.Read(reader, this, "startColor_blue");
                model.Read(reader, this, "model");
                endColor_red.Read(reader, this, "endColor_red");
                endColor_green.Read(reader, this, "endColor_green");
                endColor_blue.Read(reader, this, "endColor_blue");
                texture.Read(reader, this, "texture");
                lifeTime.Read(reader, this, "lifeTime");
                primLifeTime.Read(reader, this, "primLifeTime");
                use_child.Read(reader, this, "use_child");
                startFadeValue.Read(reader, this, "startFadeValue");
                fadeStep.Read(reader, this, "fadeStep");
                numberBirthParticles.Read(reader, this, "numberBirthParticles");
                z_undulation_mode.Read(reader, this, "z_undulation_mode");
                scaleSpeed.Read(reader, this, "scaleSpeed");
                offset.Read(reader, this, "offset");
                startScale.Read(reader, this, "startScale");
            }
            else // if (reader.File._Version >= SR1_File.Version.May12)
            {
                model_b.Read(reader, this, "model");
                texture_b.Read(reader, this, "texture");
                size.Read(reader, this, "size");
                StartOnInit.Read(reader, this, "StartOnInit");
                type.Read(reader, this, "type");
                birthRadius.Read(reader, this, "birthRadius");
                startSegment.Read(reader, this, "startSegment");
                endSegment.Read(reader, this, "endSegment");
                direction.Read(reader, this, "direction");
                spectral_colorize.Read(reader, this, "spectral_colorize");
                absolute_direction.Read(reader, this, "absolute_direction");
                acceleration.Read(reader, this, "acceleration");
                useInstanceModel_b.Read(reader, this, "useInstanceModel");
                startColor.Read(reader, this, "startColor");
                endColor.Read(reader, this, "endColor");
                lifeTime.Read(reader, this, "lifeTime");
                primLifeTime_b.Read(reader, this, "primLifeTime");
                startFadeValue.Read(reader, this, "startFadeValue");
                fadeStep.Read(reader, this, "fadeStep");
                numberBirthParticles.Read(reader, this, "numberBirthParticles");
                z_undulation_mode.Read(reader, this, "z_undulation_mode");
                scaleSpeed.Read(reader, this, "scaleSpeed");
                offset.Read(reader, this, "offset");
                startScale.Read(reader, this, "startScale");
            }
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            if (writer.File._Version >= SR1_File.Version.Jun01)
            {
                size.Write(writer);
                StartOnInit.Write(writer);
                type.Write(writer);
                birthRadius.Write(writer);
                startSegment.Write(writer);
                endSegment.Write(writer);
                direction.Write(writer);
                spectral_colorize.Write(writer);
                absolute_direction.Write(writer);
                accx.Write(writer);
                accy.Write(writer);
                accz.Write(writer);
                useInstanceModel.Write(writer);
                startColor_red.Write(writer);
                startColor_green.Write(writer);
                startColor_blue.Write(writer);
                model.Write(writer);
                endColor_red.Write(writer);
                endColor_green.Write(writer);
                endColor_blue.Write(writer);
                texture.Write(writer);
                lifeTime.Write(writer);
                primLifeTime.Write(writer);
                use_child.Write(writer);
                startFadeValue.Write(writer);
                fadeStep.Write(writer);
                numberBirthParticles.Write(writer);
                z_undulation_mode.Write(writer);
                scaleSpeed.Write(writer);
                offset.Write(writer);
                startScale.Write(writer);
            }
            else // if (reader.File._Version >= SR1_File.Version.May12)
            {
                model_b.Write(writer);
                texture_b.Write(writer);
                size.Write(writer);
                StartOnInit.Write(writer);
                type.Write(writer);
                birthRadius.Write(writer);
                startSegment.Write(writer);
                endSegment.Write(writer);
                direction.Write(writer);
                spectral_colorize.Write(writer);
                absolute_direction.Write(writer);
                acceleration.Write(writer);
                useInstanceModel_b.Write(writer);
                startColor.Write(writer);
                endColor.Write(writer);
                lifeTime.Write(writer);
                primLifeTime_b.Write(writer);
                startFadeValue.Write(writer);
                fadeStep.Write(writer);
                numberBirthParticles.Write(writer);
                z_undulation_mode.Write(writer);
                scaleSpeed.Write(writer);
                offset.Write(writer);
                startScale.Write(writer);
            }
        }
    }
}
