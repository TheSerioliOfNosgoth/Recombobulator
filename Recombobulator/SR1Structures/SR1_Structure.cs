using System;
using System.Collections;
using System.Collections.Generic;
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

		public readonly List<SR1_Structure> MembersWritten = new List<SR1_Structure>();

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
				AddToRead(reader);

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
				AddToWritten(writer);

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

			if (Parent != null)
			{
				Parent.MembersWritten.Add(this);
			}
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

		protected virtual void AddToRead(SR1_Reader reader)
		{

		}

		protected virtual void AddToWritten(SR1_Writer writer)
		{

		}

		public override string ToString()
		{
			return "";
		}

		public void WriteToConsole(string prefix, int depth, bool writeMembers)
		{
			bool isPtr = this is SR1_PointerBase;
			string depthStr = "";
			for (int d = 0; d < depth; d++)
			{
				depthStr += isPtr ? "****" : "    ";
			}

			Console.WriteLine(depthStr + prefix + "(0x" + NewStart.ToString("X8") + "): " + GetTypeName(true) + " " + Name + " = " + ToString());

			if (writeMembers)
			{
				foreach (SR1_Structure structure in MembersWritten)
				{
					// Only use prefix at root.
					structure.WriteToConsole("", depth + 1, true);
				}
			}
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

		protected string GetPrimativeAsHex<T>(T primative)
		{
			try
			{
				string hexString = "0x";
				TypeCode typeCode = Type.GetTypeCode(typeof(T));
				int typeLength = GetPrimativeTypeLength(typeCode);
				T[] typeArray = new T[] { primative };
				byte[] byteArray = new byte[typeLength];
				Buffer.BlockCopy(typeArray, 0, byteArray, 0, typeLength);
				while (typeLength-- > 0)
				{
					hexString += byteArray[typeLength].ToString("X2");
				}

				return hexString;
			}
			catch { }

			return "";
		}
	}
}
