using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	abstract class EventBaseObject : SR1_Structure
	{
		public SR1_Primative<short> id = new SR1_Primative<short>();
	}
}
