using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class INICommandType : SR1_Primative<short>
	{
		public INICommandType()
			: base()
		{
		}

		public override string ToString()
		{
			// See MON_ProcessIntro and MON_SetDefaults for monster intro types.

			switch (Value)
			{
				case 0x01:
					return "MonsterInitialBehavior";
				case 0x02:
					return "MonsterTriggeredBehavior";
				case 0x06:
					return "MonsterAge";
				case 0x07:
					// A monster's extraData is a _MonsterVars. _MonsterVars.mvFlags != 0x100000
					// Maybe leader, hungry sluagh, flee sluagh, food for other sluagh?
					return "MonsterVarsFlag";
				case 0x08:
					return "MonsterHealth";
				case 0x09:
					return "RegenTime";
				case 0x12:
					return "ObjectModel";
				case 0x13:
					return "UpdraftAmount";
				case 0x15:
					return "IsBurning";
				case 0x1A:
					return "AttachedInstance";
			}

			return Value.ToString();
		}

		public override string GetTypeName(bool includeDimensions)
		{
			return "INICommandType";
		}
	}
}
