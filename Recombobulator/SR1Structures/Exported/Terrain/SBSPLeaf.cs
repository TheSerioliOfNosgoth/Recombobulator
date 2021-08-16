using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class SBSPLeaf : SR1_Structure
    {
        Sphere_noSq sphere = new Sphere_noSq();
        SR1_Pointer<Intro> introList = new SR1_Pointer<Intro>();
        SR1_Primative<short> numIntros = new SR1_Primative<short>();
        SR1_Primative<short> flags = new SR1_Primative<short>();
        SR1_Pointer<CDLight> lightList = new SR1_Pointer<CDLight>();
        SR1_Primative<short> numLights = new SR1_Primative<short>();
        SR1_Primative<short> pad = new SR1_Primative<short>();

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            sphere.Read(reader, this, "sphere");
            introList.Read(reader, this, "introList");
            numIntros.Read(reader, this, "numIntros");
            flags.Read(reader, this, "flags");
            lightList.Read(reader, this, "lightList");
            numLights.Read(reader, this, "numLights");
            pad.Read(reader, this, "pad");
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {
            //if (numIntros.Value > 0 && introList.Offset != 0 && !reader.IntroListDictionary.ContainsKey(introList.Offset))
            //{
            //    reader.IntroListDictionary.Add(introList.Offset, new SR1_PointerArray<Intro>(numIntros.Value, false));
            //}
            new SR1_PointerArray<Intro>(numIntros.Value, false).ReadFromPointer(reader, introList);
            new SR1_PointerArray<CDLight>(numLights.Value, false).ReadFromPointer(reader, lightList);
        }

        public override void WriteMembers(SR1_Writer writer)
        {
            sphere.Write(writer);
            introList.Write(writer);
            numIntros.Write(writer);
            flags.Write(writer);
            lightList.Write(writer);
            numLights.Write(writer);
            pad.Write(writer);
        }
    }
}
