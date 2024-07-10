using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Recombobulator.SR1Structures
{
	public abstract class SR1_Structure
	{
		public uint Start { get; protected set; }
		public uint NewStart { get; protected set; }
		public uint End { get; protected set; }
		public uint NewEnd { get; protected set; }

		public SR1_Structure Parent { get; protected set; }

		public string Name { get; protected set; }

		public readonly List<SR1_Structure> MembersRead = new List<SR1_Structure>();

		private byte[] _padding = null;

		public virtual TreeList.Node CreateNode()
		{
			string offsetString = " (0x" + Start.ToString("X8") + "-" + "0x" + End.ToString("X8") + ")";
			string typeString = GetTypeName(true);
			string valueString = ToString();
			TreeList.Node node = new TreeList.Node(new string[] { offsetString, typeString, Name, valueString });

			foreach (SR1_Structure s in MembersRead)
			{
				node.Nodes.Add(s.CreateNode());
			}

			return node;
		}

		public SR1_Structure SetPadding(int padding)
		{
			if (padding > 0)
			{
				_padding = new byte[padding];
			}

			return this;
		}

		protected static object ReadPrimativeType<T>(SR1_Reader reader)
		{
			TypeCode typeCode = Type.GetTypeCode(typeof(T));
			switch (typeCode)
			{
				case TypeCode.Boolean:
					return reader.ReadBoolean();
				case TypeCode.Char:
					return (char)reader.ReadSByte();
				case TypeCode.SByte:
					return reader.ReadSByte();
				case TypeCode.Byte:
					return reader.ReadByte();
				case TypeCode.Int16:
					return reader.ReadInt16();
				case TypeCode.UInt16:
					return reader.ReadUInt16();
				case TypeCode.Int32:
					return reader.ReadInt32();
				case TypeCode.UInt32:
					return reader.ReadUInt32();
				case TypeCode.Int64:
					return reader.ReadInt64();
				case TypeCode.UInt64:
					return reader.ReadUInt64();
				case TypeCode.Single:
					return reader.ReadSingle();
				case TypeCode.Double:
					return reader.ReadDouble();
				default:
					throw new Exception("Unhandled primative type.");
			}
		}

		public void Read(SR1_Reader reader, SR1_Structure parent, string name)
		{
			Start = (uint)reader.BaseStream.Position;
			End = (uint)reader.BaseStream.Position;
			Parent = parent;
			Name = name;

			if (Parent == null)
			{
				reader.File._Structures.Add(Start, this);
			}
			else
			{
				Parent.MembersRead.Add(this);
			}

			try
			{
				ReadMembers(reader, parent);

				if (_padding != null &&
					reader.PlanMarkerList != null &&
					reader.PlanMarkerList.Offset != 0 &&
					reader.BaseStream.Position == reader.PlanMarkerList.Offset)
				{
					_padding = null;
				}

				if (_padding != null)
				{
					uint mod = (uint)reader.BaseStream.Position % (uint)_padding.Length;
					if (mod > 0)
					{
						uint padding = (uint)_padding.Length - mod;
						for (int i = 0; i < padding; i++)
						{
							_padding[i] = reader.ReadByte();
						}
					}
				}

				End = (uint)reader.BaseStream.Position;

				ReadReferences(reader, parent);
			}
			catch (Exception ex)
			{
				reader.LogError(GetTypeName(true) + " (0x" + Start.ToString("X8") + "):\r\n" + ex.Message);
			}

			reader.BaseStream.Position = End;
		}

		public void Read(SR1_Reader reader, SR1_Structure parent, string name, SR1_File.Version versionAdded, SR1_File.Version versionRemoved)
		{
			if (reader.File._Version >= versionAdded && reader.File._Version < versionRemoved)
			{
				Read(reader, parent, name);
			}
			else
			{
				Parent = parent;
				Name = name;
			}
		}

		public SR1_Structure ReadOrphan(SR1_Reader reader, uint offset)
		{
			long oldPos = reader.BaseStream.Position;
			reader.BaseStream.Position = offset;
			Read(reader, null, "");
			reader.BaseStream.Position = oldPos;
			return this;
		}

		public void ReadTemp(SR1_Reader reader)
		{
			Start = (uint)reader.BaseStream.Position;
			End = (uint)reader.BaseStream.Position;
			Parent = null;
			Name = "";

			try
			{
				ReadMembers(reader, null);
				End = (uint)reader.BaseStream.Position;
			}
			catch (Exception ex)
			{
				reader.LogError(ex.Message);
			}

			reader.BaseStream.Position = End;
		}

		public SR1_Structure ReadFromPointer(SR1_Reader reader, SR1_PointerBase pointer)
		{
			if (pointer.PrepareToReadReference(reader))
			{
				Read(reader, null, "");
			}

			return this;
		}

		protected abstract void ReadMembers(SR1_Reader reader, SR1_Structure parent);

		protected abstract void ReadReferences(SR1_Reader reader, SR1_Structure parent);

		protected static void WritePrimativeType<T>(SR1_Writer writer, object primative)
		{
			TypeCode typeCode = Type.GetTypeCode(typeof(T));

			switch (typeCode)
			{
				case TypeCode.Boolean:
					writer.Write((bool)primative);
					return;
				case TypeCode.Char:
					writer.Write((sbyte)(char)primative);
					return;
				case TypeCode.SByte:
					writer.Write((sbyte)primative);
					return;
				case TypeCode.Byte:
					writer.Write((byte)primative);
					return;
				case TypeCode.Int16:
					writer.Write((short)primative);
					return;
				case TypeCode.UInt16:
					writer.Write((ushort)primative);
					return;
				case TypeCode.Int32:
					writer.Write((int)primative);
					return;
				case TypeCode.UInt32:
					writer.Write((uint)primative);
					return;
				case TypeCode.Int64:
					writer.Write((long)primative);
					return;
				case TypeCode.UInt64:
					writer.Write((ulong)primative);
					return;
				case TypeCode.Single:
					writer.Write((float)primative);
					return;
				case TypeCode.Double:
					writer.Write((double)primative);
					return;
				default:
					throw new Exception("Unhandled primative type.");
			}
		}

		public void Write(SR1_Writer writer)
		{
			NewStart = (uint)writer.BaseStream.Position;

			try
			{
				WriteMembers(writer);
				Register(writer);

				if (_padding != null)
				{
					uint mod = (uint)writer.BaseStream.Position % (uint)_padding.Length;
					if (mod > 0)
					{
						uint padding = (uint)_padding.Length - mod;
						for (int i = 0; i < padding; i++)
						{
							writer.Write(_padding[i]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				writer.LogError(ex.Message);
			}

			NewEnd = (uint)writer.BaseStream.Position;
		}

		public void Write(SR1_Writer writer, SR1_File.Version versionAdded, SR1_File.Version versionRemoved)
		{
			if (writer.File._Version >= versionAdded && writer.File._Version < versionRemoved)
			{
				Write(writer);
			}
		}

		public abstract void WriteMembers(SR1_Writer writer);

		public virtual void MigrateVersion(SR1_File file, SR1_File.Version targetVersion, SR1_File.MigrateFlags migrateFlags)
		{
			foreach (SR1_Structure structure in MembersRead)
			{
				structure.MigrateVersion(file, targetVersion, migrateFlags);
			}
		}

		protected virtual void Register(SR1_Writer writer)
		{

		}

		public override string ToString()
		{
			return "";
		}

		public virtual bool TryParse(string value)
		{
			return false;
		}

		public virtual string GetTypeName(bool includeDimensions)
		{
			return GetType().Name;
		}

		protected string GetPrimativeTypeName(TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Boolean:
					return "bool";
				case TypeCode.Char:
				case TypeCode.SByte:
					return "char";
				case TypeCode.Byte:
					return "unsigned char";
				case TypeCode.Int16:
					return "short";
				case TypeCode.UInt16:
					return "unsigned short";
				case TypeCode.Int32:
					return "long"; // "int";
				case TypeCode.UInt32:
					return "unsigned int";
				case TypeCode.Int64:
					return "long long";
				case TypeCode.UInt64:
					return "unsigned long long";
				case TypeCode.Single:
					return "float";
				case TypeCode.Double:
					return "double";
				default:
					throw new Exception("Unhandled primative type.");
			}
		}

		protected int GetPrimativeTypeLength(TypeCode typeCode)
		{
			switch (typeCode)
			{
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
					return 1;
				case TypeCode.Int16:
				case TypeCode.UInt16:
					return 2;
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Single:
					return 4;
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Double:
					return 8;
				default:
					throw new Exception("Unhandled primative type.");
			}
		}

		protected bool GetStringAsPrimitive<T>(string value, out T primitive, bool showAsHex = false)
		{
			primitive = default;

			try
			{
				TypeCode typeCode = Type.GetTypeCode(typeof(T));

				switch (typeCode)
				{
					case TypeCode.Boolean:
					{
						if (Boolean.TryParse(value, out Boolean v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Char:
					{
						if (Char.TryParse(value, out Char v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.SByte:
					{
						if (SByte.TryParse(value, out SByte v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Byte:
					{
						if (Byte.TryParse(value, out Byte v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Int16:
					{
						if (Int16.TryParse(value, out Int16 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.UInt16:
					{
						if (UInt16.TryParse(value, out UInt16 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Int32:
					{
						if (Int32.TryParse(value, out Int32 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.UInt32:
					{
						if (UInt32.TryParse(value, out UInt32 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Int64:
					{
						if (Int64.TryParse(value, out Int64 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.UInt64:
					{
						if (UInt64.TryParse(value, out UInt64 v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Single:
					{
						if (Single.TryParse(value, out Single v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					case TypeCode.Double:
					{
						if (Double.TryParse(value, out Double v))
						{
							primitive = (T)(object)v;
							return true;
						}
						return false;
					}
					default:
					{
						throw new Exception("Unhandled primative type.");
					}
				}
			}
			catch
			{
			}

			return false;
		}

		protected bool GetHexAsPrimitive<T>(string value, out T primitive, bool showAsHex = false)
		{
			primitive = default;

			try
			{
				TypeCode typeCode = Type.GetTypeCode(typeof(T));
				if (typeCode == TypeCode.Boolean)
				{
					if (value == "0x1")
					{
						primitive = (T)(object)true;
						return true;
					}

					if (value == "0x0")
					{
						primitive = (T)(object)false;
						return true;
					}

					return false;
				}

				if (value.StartsWith("0x"))
				{
					value = value.Substring(2);
				}

				int typeLength = GetPrimativeTypeLength(typeCode);
				NumberStyles styles = NumberStyles.HexNumber;
				CultureInfo culture = CultureInfo.InvariantCulture;

				if (value.Length <= (typeLength * 2) &&
					UInt64.TryParse(value, styles, culture, out UInt64 u64Value))
				{
					T[] typeArray = new T[1];
					UInt64[] u64Array = new UInt64[] { u64Value };
					Buffer.BlockCopy(u64Array, 0, typeArray, 0, typeLength);
					primitive = typeArray[0];
					return true;
				}
			}
			catch
			{
			}

			return false;
		}

		protected string GetPrimativeAsHex<T>(T primative)
		{
			try
			{
				TypeCode typeCode = Type.GetTypeCode(typeof(T));
				if (typeCode == TypeCode.Boolean)
				{
					return ((bool)(object)primative) ? "0x0" : "0x1";
				}

				int typeLength = GetPrimativeTypeLength(typeCode);
				T[] typeArray = new T[] { primative };
				UInt64[] u64Array = new UInt64[1];
				Buffer.BlockCopy(typeArray, 0, u64Array, 0, typeLength);

				string hexString = u64Array[0].ToString("X8");
				hexString = hexString.TrimStart('0');
				hexString = hexString.PadLeft(typeLength * 2, '0');
				return "0x" + hexString;
			}
			catch
			{
			}

			return "";
		}
	}
}
