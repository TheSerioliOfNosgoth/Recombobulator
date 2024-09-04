using System;
using System.Collections.Generic;
using System.IO;

namespace Recombobulator.SR1Structures
{
	class TFace : SR1_Structure
	{
		public readonly Face face = new Face();
		public readonly SR1_Primative<ushort> attr0 = new SR1_Primative<ushort>().ShowAsHex(true);
		public readonly SR1_Primative<byte> attr = new SR1_Primative<byte>().ShowAsHex(true);
		public readonly SR1_Primative<sbyte> sortPush = new SR1_Primative<sbyte>();
		public readonly SR1_Primative<ushort> normal = new SR1_Primative<ushort>();
		public readonly SR1_Primative<ushort> morph = new SR1_Primative<ushort>();
		public readonly SR1_Pointer<TextureFT3> texture = new SR1_Pointer<TextureFT3>();
		public readonly SR1_Primative<ushort> textoff = new SR1_Primative<ushort>().ShowAsHex(true);

		public bool IsInSignalGroup = false;
		public bool IsWater = false;
		public bool IsSunlight = false;
		public MultiSignal MultiSignal = null;
		public Signal Signal = null;
		public StreamUnitPortal Portal = null;
		public TextureFT3 Texture = null;

		protected override void ReadMembers(SR1_Reader reader, SR1_Structure parent)
		{
			face.Read(reader, this, "face");
			attr0.Read(reader, this, "attr", SR1_File.Version.First, SR1_File.Version.Jan23);
			attr.Read(reader, this, "attr", SR1_File.Version.Jan23, SR1_File.Version.Next);
			sortPush.Read(reader, this, "sortPush", SR1_File.Version.Jan23, SR1_File.Version.Next);
			normal.Read(reader, this, "normal");
			morph.Read(reader, this, "morph", SR1_File.Version.First, SR1_File.Version.Jan23);
			texture.Read(reader, this, "texture", SR1_File.Version.First, SR1_File.Version.Jan23);
			textoff.Read(reader, this, "textoff", SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		protected override void ReadReferences(SR1_Reader reader, SR1_Structure parent)
		{
		}

		public override void WriteMembers(SR1_Writer writer)
		{
			face.Write(writer);
			attr0.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			attr.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			sortPush.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
			normal.Write(writer);
			morph.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			texture.Write(writer, SR1_File.Version.First, SR1_File.Version.Jan23);
			textoff.Write(writer, SR1_File.Version.Jan23, SR1_File.Version.Next);
		}

		public static void Copy(TFace to, TFace from)
		{
			Face.Copy(to.face, from.face);
			to.attr0.Value = from.attr0.Value;
			to.attr.Value = from.attr.Value;
			to.sortPush.Value = from.sortPush.Value;
			to.normal.Value = from.normal.Value;
			to.morph.Value = from.morph.Value;
			to.texture.Offset = from.texture.Offset;
			to.textoff.Value = from.textoff.Value;

			to.IsInSignalGroup = from.IsInSignalGroup;
			to.MultiSignal = from.MultiSignal;
			to.Signal = from.Signal;
			to.Portal = from.Portal;
			to.Texture = from.Texture;
		}

		public override void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			base.MigrateVersion(file, targetVersion, migrateFlags);

			// Set it as 'null' here and calculate the real value in Terrain.cs
			// once the textures have been written to their new locations.
			textoff.Value = 0xFFFF;

			if (file._Version < SR1_File.Version.Retail_PC && targetVersion >= SR1_File.Version.Retail_PC)
			{
				if (IsInSignalGroup)
				{
					if (file._Version < SR1_File.Version.Jan23)
					{
						// Effectively disable the FFace.
						// This doesn't apply to the new tFaces created in Texture.MigrateVersion.
						face.v2.Value = face.v1.Value;
						MultiSignal = null;
						Signal = null;
						Portal = null;

						IsInSignalGroup = false;
					}
					else
					{
						bool removeSignal = false;

						if (file._Version >= SR1_File.Version.Jan23 &&
							file._Version < SR1_File.Version.May12 &&
							targetVersion >= SR1_File.Version.May12)
						{
							// Looks like there are other things triggered besides portals/signals.
							removeSignal |= (attr.Value != 0x44);

							if (file._Version < SR1_File.Version.Apr14 &&
								file._Structures[0].Name == "adda1" && MultiSignal != null && MultiSignal.signalNum.Value == 51)
							{
								// On PC, nop 004ABBBA to make it draw the adjacent area without the camera being inside it.
								// This makes it behave as if STREAM_GetClipRect returned true.

								normal.Value = 1448; // 1466
							}

							if (file._Structures[0].Name == "undrct15" && MultiSignal != null &&
								(MultiSignal.signalNum.Value == 3 || MultiSignal.signalNum.Value == 4))
							{
								normal.Value = unchecked((ushort)(-(short)normal.Value));
							}
						}

						removeSignal |= MultiSignal != null && MultiSignal.OmitFromMigration;
						removeSignal |= Signal != null && Signal.OmitFromMigration;
						removeSignal |= Portal != null && Portal.OmitFromMigration;

						// 0x004ABBBA has something to do with the portals.
						// COLLIDE_LineWithSignals does care about TFace::texoff. See address 00490DF6 in game.
						// It's an offset into Terrain->signals, not Level->signalListStart
						if (removeSignal)
						{
							attr.Value = 0;

							MultiSignal = null;
							Signal = null;
							Portal = null;
						}
					}
				}
				else
				{
					if (file._Version < SR1_File.Version.Jan23)
					{
						if (IsWater)
						{
							attr.Value |= 0x08;
						}
						else if (IsSunlight)
						{
							// Effectively disable the FFace.
							// This doesn't apply to the new tFaces created in Texture.MigrateVersion.
							face.v2.Value = face.v1.Value;
							Texture = null;
						}
					}

					// Backface collision.
					//if ((attr0.Value & 0x2000) != 0)
					//{
					//	attr.Value |= 0x20;
					//}

					// 0x02 lets forge 6 work and prevents crashing in retreat when using portals.
					// 0x08 is water?
					// 0x46 lets portals work but not fully, however crashes forge 5.
					// attr.Value = (byte)(attr.Value & 0x46);
					// attr.Value = (byte)(attr.Value & 0x02); // 0x44 is used for signals. Renderable stuff too?
				}
			}
		}

		public override string ToString()
		{
			string result = base.ToString();

			if (IsInSignalGroup)
			{
				if (MultiSignal != null)
				{
					result += "{ MultiSignal = 0x" + MultiSignal.Start.ToString("X8") + " }";
				}
				else
				{
					result += "{ MultiSignal = 0x00000000 }";
				}
			}
			else
			{
				if (Texture != null)
				{
					result += "{ Texture = 0x" + Texture.Start.ToString("X8") + " }";
				}
				else
				{
					result += "{ Texture = 0x00000000 }";
				}
			}

			return result;
		}
	}
}
