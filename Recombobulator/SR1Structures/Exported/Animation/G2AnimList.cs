using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
    class G2AnimList : SR1_Structure
    {
        int _NumAnims;

        SR1_PointerArray<G2AnimKeylist_Type> keyListPointers = new SR1_PointerArray<G2AnimKeylist_Type>(0, false);
        //SR1_StructureArray<G2AnimKeylist_Type> keyLists = new SR1_StructureArray<G2AnimKeylist_Type>(0);
        //SR1_StructureList<G2AnimFXList> fxLists = new SR1_StructureList<G2AnimFXList>();

        public G2AnimList(int numAnims)
        {
            _NumAnims = numAnims;
        }

        protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
        {
            keyListPointers = new SR1_PointerArray<G2AnimKeylist_Type>(_NumAnims, false);
            keyListPointers.Read(reader, this, "keyListPointers");

            SR1_StructureArray<G2AnimKeylist_Type> keyLists = new SR1_StructureArray<G2AnimKeylist_Type>(_NumAnims);
            keyLists.ReadFromPointer(reader, keyListPointers[0]);

            SR1_StructureArray<G2AnimFXList> fxLists = new SR1_StructureArray<G2AnimFXList>(reader.AnimFXDictionary.Count);
            fxLists.SetPadding(4).ReadFromPointer(reader, reader.AnimFXDictionary.Values[0]);
        }

        protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
        {

        }

        public override void WriteMembers(SR1_Writer writer)
        {
            keyListPointers.Write(writer);
        }
    }
}
