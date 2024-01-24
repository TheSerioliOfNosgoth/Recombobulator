using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	public class VGroup : SR1_Structure
	{
		SR1_Primative<int> id = new SR1_Primative<int>();
		SR1_Primative<int> numVertices = new SR1_Primative<int>();
		SR1_PointerArrayPointer<TVertex> vertexList = new SR1_PointerArrayPointer<TVertex>();

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			id.Read(reader, this, "id");
			numVertices.Read(reader, this, "numVertices");
			vertexList.Read(reader, this, "vertexList");
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
			new SR1_PointerArray<TVertex>(numVertices.Value, false).ReadFromPointer(reader, vertexList);
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			id.Write(writer);
			numVertices.Write(writer);
			vertexList.Write(writer);
		}
	}
}
