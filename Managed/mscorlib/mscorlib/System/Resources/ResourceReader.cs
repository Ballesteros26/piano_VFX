using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
	/// <summary>Enumerates the resources in a binary resources (.resources) file by reading sequential resource name/value pairs.</summary>
	// Token: 0x020002AA RID: 682
	[ComVisible(true)]
	public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified named resource file.</summary>
		/// <param name="fileName">The path and name of the resource file to read. <paramref name="filename" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileName" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.BadImageFormatException">The resource file has an invalid format. For example, the length of the file may be zero.</exception>
		// Token: 0x06001F4F RID: 8015 RVA: 0x00079B94 File Offset: 0x00077D94
		[SecuritySafeCritical]
		public ResourceReader(string fileName)
		{
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false, false, false), Encoding.UTF8);
			try
			{
				this.ReadResources();
			}
			catch
			{
				this._store.Close();
				throw;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">The input stream for reading resources. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is not readable. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="stream" /> parameter is null. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred while accessing <paramref name="stream" />. </exception>
		// Token: 0x06001F50 RID: 8016 RVA: 0x00079C0C File Offset: 0x00077E0C
		[SecurityCritical]
		public ResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream was not readable."));
			}
			this._resCache = new Dictionary<string, ResourceLocator>(FastResourceComparer.Default);
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00079C78 File Offset: 0x00077E78
		[SecurityCritical]
		internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
		{
			this._resCache = resCache;
			this._store = new BinaryReader(stream, Encoding.UTF8);
			this._ums = stream as UnmanagedMemoryStream;
			this.ReadResources();
		}

		/// <summary>Releases all operating system resources associated with this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		// Token: 0x06001F52 RID: 8018 RVA: 0x00079CAA File Offset: 0x00077EAA
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Resources.ResourceReader" /> class.</summary>
		// Token: 0x06001F53 RID: 8019 RVA: 0x00079CB3 File Offset: 0x00077EB3
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00079CBC File Offset: 0x00077EBC
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._store != null)
			{
				this._resCache = null;
				if (disposing)
				{
					BinaryReader store = this._store;
					this._store = null;
					if (store != null)
					{
						store.Close();
					}
				}
				this._store = null;
				this._namePositions = null;
				this._nameHashes = null;
				this._ums = null;
				this._namePositionsPtr = null;
				this._nameHashesPtr = null;
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00079D20 File Offset: 0x00077F20
		[SecurityCritical]
		internal unsafe static int ReadUnalignedI4(int* p)
		{
			return (int)(*(byte*)p) | ((int)((byte*)p)[1] << 8) | ((int)((byte*)p)[2] << 16) | ((int)((byte*)p)[3] << 24);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00079D48 File Offset: 0x00077F48
		private void SkipInt32()
		{
			this._store.BaseStream.Seek(4L, SeekOrigin.Current);
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00079D60 File Offset: 0x00077F60
		private void SkipString()
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			this._store.BaseStream.Seek((long)num, SeekOrigin.Current);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00079DA1 File Offset: 0x00077FA1
		[SecuritySafeCritical]
		private int GetNameHash(int index)
		{
			if (this._ums == null)
			{
				return this._nameHashes[index];
			}
			return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00079DC8 File Offset: 0x00077FC8
		[SecuritySafeCritical]
		private int GetNamePosition(int index)
		{
			int num;
			if (this._ums == null)
			{
				num = this._namePositions[index];
			}
			else
			{
				num = ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index);
			}
			if (num < 0 || (long)num > this._dataSectionOffset - this._nameSectionOffset)
			{
				throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into name section.", new object[] { num }));
			}
			return num;
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has already been closed and cannot be accessed. </exception>
		// Token: 0x06001F5A RID: 8026 RVA: 0x00079E2F File Offset: 0x0007802F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</summary>
		/// <returns>An enumerator for this <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The reader has been closed or disposed, and cannot be accessed. </exception>
		// Token: 0x06001F5B RID: 8027 RVA: 0x00079E37 File Offset: 0x00078037
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
			}
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00079E57 File Offset: 0x00078057
		internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
		{
			return new ResourceReader.ResourceEnumerator(this);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00079E60 File Offset: 0x00078060
		internal int FindPosForResource(string name)
		{
			int num = FastResourceComparer.HashFunction(name);
			int i = 0;
			int num2 = this._numResources - 1;
			int num3 = -1;
			bool flag = false;
			while (i <= num2)
			{
				num3 = i + num2 >> 1;
				int nameHash = this.GetNameHash(num3);
				int num4;
				if (nameHash == num)
				{
					num4 = 0;
				}
				else if (nameHash < num)
				{
					num4 = -1;
				}
				else
				{
					num4 = 1;
				}
				if (num4 == 0)
				{
					flag = true;
					break;
				}
				if (num4 < 0)
				{
					i = num3 + 1;
				}
				else
				{
					num2 = num3 - 1;
				}
			}
			if (!flag)
			{
				return -1;
			}
			if (i != num3)
			{
				i = num3;
				while (i > 0 && this.GetNameHash(i - 1) == num)
				{
					i--;
				}
			}
			if (num2 != num3)
			{
				num2 = num3;
				while (num2 < this._numResources - 1 && this.GetNameHash(num2 + 1) == num)
				{
					num2++;
				}
			}
			lock (this)
			{
				int j = i;
				while (j <= num2)
				{
					this._store.BaseStream.Seek(this._nameSectionOffset + (long)this.GetNamePosition(j), SeekOrigin.Begin);
					if (this.CompareStringEqualsName(name))
					{
						int num5 = this._store.ReadInt32();
						if (num5 < 0 || (long)num5 >= this._store.BaseStream.Length - this._dataSectionOffset)
						{
							throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { num5 }));
						}
						return num5;
					}
					else
					{
						j++;
					}
				}
			}
			return -1;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00079FD4 File Offset: 0x000781D4
		[SecuritySafeCritical]
		private unsafe bool CompareStringEqualsName(string name)
		{
			int num = this._store.Read7BitEncodedInt();
			if (num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
			}
			if (this._ums == null)
			{
				byte[] array = new byte[num];
				int num2;
				for (int i = num; i > 0; i -= num2)
				{
					num2 = this._store.Read(array, num - i, i);
					if (num2 == 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. A resource name extends past the end of the stream."));
					}
				}
				return FastResourceComparer.CompareOrdinal(array, num / 2, name) == 0;
			}
			byte* positionPointer = this._ums.PositionPointer;
			this._ums.Seek((long)num, SeekOrigin.Current);
			if (this._ums.Position > this._ums.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Resource name extends past the end of the file."));
			}
			return FastResourceComparer.CompareOrdinal(positionPointer, num, name) == 0;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0007A09C File Offset: 0x0007829C
		[SecurityCritical]
		private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
		{
			long num = (long)this.GetNamePosition(index);
			int num2;
			byte[] array3;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				num2 = this._store.Read7BitEncodedInt();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String length must be non-negative."));
				}
				if (this._ums != null)
				{
					if (this._ums.Position > this._ums.Length - (long)num2)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. String for name index '{0}' extends past the end of the file.", new object[] { index }));
					}
					char* positionPointer = (char*)this._ums.PositionPointer;
					string text;
					if (!BitConverter.IsLittleEndian)
					{
						byte* ptr = (byte*)positionPointer;
						byte[] array = new byte[num2];
						for (int i = 0; i < num2; i += 2)
						{
							array[i] = (ptr + i)[1];
							array[i + 1] = ptr[i];
						}
						byte[] array2;
						byte* ptr2;
						if ((array2 = array) == null || array2.Length == 0)
						{
							ptr2 = null;
						}
						else
						{
							ptr2 = &array2[0];
						}
						text = new string((char*)ptr2, 0, num2 / 2);
						array2 = null;
					}
					else
					{
						text = new string(positionPointer, 0, num2 / 2);
					}
					this._ums.Position += (long)num2;
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { dataOffset }));
					}
					return text;
				}
				else
				{
					array3 = new byte[num2];
					int num3;
					for (int j = num2; j > 0; j -= num3)
					{
						num3 = this._store.Read(array3, num2 - j, j);
						if (num3 == 0)
						{
							throw new EndOfStreamException(Environment.GetResourceString("Corrupt .resources file. The resource name for name index {0} extends past the end of the stream.", new object[] { index }));
						}
					}
					dataOffset = this._store.ReadInt32();
					if (dataOffset < 0 || (long)dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { dataOffset }));
					}
				}
			}
			return Encoding.Unicode.GetString(array3, 0, num2);
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0007A30C File Offset: 0x0007850C
		private object GetValueForNameIndex(int index)
		{
			long num = (long)this.GetNamePosition(index);
			object obj;
			lock (this)
			{
				this._store.BaseStream.Seek(num + this._nameSectionOffset, SeekOrigin.Begin);
				this.SkipString();
				int num2 = this._store.ReadInt32();
				if (num2 < 0 || (long)num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
				{
					throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { num2 }));
				}
				if (this._version == 1)
				{
					obj = this.LoadObjectV1(num2);
				}
				else
				{
					ResourceTypeCode resourceTypeCode;
					obj = this.LoadObjectV2(num2, out resourceTypeCode);
				}
			}
			return obj;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0007A3D8 File Offset: 0x000785D8
		internal string LoadString(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			string text = null;
			int num = this._store.Read7BitEncodedInt();
			if (this._version == 1)
			{
				if (num == -1)
				{
					return null;
				}
				if (this.FindType(num) != typeof(string))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[] { this.FindType(num).FullName }));
				}
				text = this._store.ReadString();
			}
			else
			{
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)num;
				if (resourceTypeCode != ResourceTypeCode.String && resourceTypeCode != ResourceTypeCode.Null)
				{
					string text2;
					if (resourceTypeCode < ResourceTypeCode.StartOfUserTypes)
					{
						text2 = resourceTypeCode.ToString();
					}
					else
					{
						text2 = this.FindType(resourceTypeCode - ResourceTypeCode.StartOfUserTypes).FullName;
					}
					throw new InvalidOperationException(Environment.GetResourceString("Resource was of type '{0}' instead of String - call GetObject instead.", new object[] { text2 }));
				}
				if (resourceTypeCode == ResourceTypeCode.String)
				{
					text = this._store.ReadString();
				}
			}
			return text;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0007A4C4 File Offset: 0x000786C4
		internal object LoadObject(int pos)
		{
			if (this._version == 1)
			{
				return this.LoadObjectV1(pos);
			}
			ResourceTypeCode resourceTypeCode;
			return this.LoadObjectV2(pos, out resourceTypeCode);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0007A4EC File Offset: 0x000786EC
		internal object LoadObject(int pos, out ResourceTypeCode typeCode)
		{
			if (this._version == 1)
			{
				object obj = this.LoadObjectV1(pos);
				typeCode = ((obj is string) ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes);
				return obj;
			}
			return this.LoadObjectV2(pos, out typeCode);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0007A524 File Offset: 0x00078724
		internal object LoadObjectV1(int pos)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV1(pos);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex2);
			}
			return obj;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0007A57C File Offset: 0x0007877C
		[SecuritySafeCritical]
		private object _LoadObjectV1(int pos)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			int num = this._store.Read7BitEncodedInt();
			if (num == -1)
			{
				return null;
			}
			RuntimeType runtimeType = this.FindType(num);
			if (runtimeType == typeof(string))
			{
				return this._store.ReadString();
			}
			if (runtimeType == typeof(int))
			{
				return this._store.ReadInt32();
			}
			if (runtimeType == typeof(byte))
			{
				return this._store.ReadByte();
			}
			if (runtimeType == typeof(sbyte))
			{
				return this._store.ReadSByte();
			}
			if (runtimeType == typeof(short))
			{
				return this._store.ReadInt16();
			}
			if (runtimeType == typeof(long))
			{
				return this._store.ReadInt64();
			}
			if (runtimeType == typeof(ushort))
			{
				return this._store.ReadUInt16();
			}
			if (runtimeType == typeof(uint))
			{
				return this._store.ReadUInt32();
			}
			if (runtimeType == typeof(ulong))
			{
				return this._store.ReadUInt64();
			}
			if (runtimeType == typeof(float))
			{
				return this._store.ReadSingle();
			}
			if (runtimeType == typeof(double))
			{
				return this._store.ReadDouble();
			}
			if (runtimeType == typeof(DateTime))
			{
				return new DateTime(this._store.ReadInt64());
			}
			if (runtimeType == typeof(TimeSpan))
			{
				return new TimeSpan(this._store.ReadInt64());
			}
			if (runtimeType == typeof(decimal))
			{
				int[] array = new int[4];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._store.ReadInt32();
				}
				return new decimal(array);
			}
			return this.DeserializeObject(num);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0007A7D4 File Offset: 0x000789D4
		internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			object obj;
			try
			{
				obj = this._LoadObjectV2(pos, out typeCode);
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex);
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."), ex2);
			}
			return obj;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0007A830 File Offset: 0x00078A30
		[SecuritySafeCritical]
		private object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
		{
			this._store.BaseStream.Seek(this._dataSectionOffset + (long)pos, SeekOrigin.Begin);
			typeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
			switch (typeCode)
			{
			case ResourceTypeCode.Null:
				return null;
			case ResourceTypeCode.String:
				return this._store.ReadString();
			case ResourceTypeCode.Boolean:
				return this._store.ReadBoolean();
			case ResourceTypeCode.Char:
				return (char)this._store.ReadUInt16();
			case ResourceTypeCode.Byte:
				return this._store.ReadByte();
			case ResourceTypeCode.SByte:
				return this._store.ReadSByte();
			case ResourceTypeCode.Int16:
				return this._store.ReadInt16();
			case ResourceTypeCode.UInt16:
				return this._store.ReadUInt16();
			case ResourceTypeCode.Int32:
				return this._store.ReadInt32();
			case ResourceTypeCode.UInt32:
				return this._store.ReadUInt32();
			case ResourceTypeCode.Int64:
				return this._store.ReadInt64();
			case ResourceTypeCode.UInt64:
				return this._store.ReadUInt64();
			case ResourceTypeCode.Single:
				return this._store.ReadSingle();
			case ResourceTypeCode.Double:
				return this._store.ReadDouble();
			case ResourceTypeCode.Decimal:
				return this._store.ReadDecimal();
			case ResourceTypeCode.DateTime:
				return DateTime.FromBinary(this._store.ReadInt64());
			case ResourceTypeCode.TimeSpan:
				return new TimeSpan(this._store.ReadInt64());
			case ResourceTypeCode.ByteArray:
			{
				int num = this._store.ReadInt32();
				if (num < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
				}
				if (this._ums == null)
				{
					if ((long)num > this._store.BaseStream.Length)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
					}
					return this._store.ReadBytes(num);
				}
				else
				{
					if ((long)num > this._ums.Length - this._ums.Position)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num }));
					}
					byte[] array = new byte[num];
					this._ums.Read(array, 0, num);
					return array;
				}
				break;
			}
			case ResourceTypeCode.Stream:
			{
				int num2 = this._store.ReadInt32();
				if (num2 < 0)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num2 }));
				}
				if (this._ums == null)
				{
					return new PinnedBufferMemoryStream(this._store.ReadBytes(num2));
				}
				if ((long)num2 > this._ums.Length - this._ums.Position)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.", new object[] { num2 }));
				}
				return new UnmanagedMemoryStream(this._ums.PositionPointer, (long)num2, (long)num2, FileAccess.Read, true);
			}
			}
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't match the available data in the stream."));
			}
			int num3 = typeCode - ResourceTypeCode.StartOfUserTypes;
			return this.DeserializeObject(num3);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0007ABA0 File Offset: 0x00078DA0
		[SecurityCritical]
		private object DeserializeObject(int typeIndex)
		{
			RuntimeType runtimeType = this.FindType(typeIndex);
			object obj = this._objFormatter.Deserialize(this._store.BaseStream);
			if (obj.GetType() != runtimeType)
			{
				throw new BadImageFormatException(Environment.GetResourceString("The type serialized in the .resources file was not the same type that the .resources file said it contained. Expected '{0}' but read '{1}'.", new object[]
				{
					runtimeType.FullName,
					obj.GetType().FullName
				}));
			}
			return obj;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0007AC08 File Offset: 0x00078E08
		[SecurityCritical]
		private void ReadResources()
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
			this._objFormatter = binaryFormatter;
			try
			{
				this._ReadResources();
			}
			catch (EndOfStreamException ex)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), ex);
			}
			catch (IndexOutOfRangeException ex2)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."), ex2);
			}
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0007AC74 File Offset: 0x00078E74
		[SecurityCritical]
		private unsafe void _ReadResources()
		{
			if (this._store.ReadInt32() != ResourceManager.MagicNumber)
			{
				throw new ArgumentException(Environment.GetResourceString("Stream is not a valid resource file."));
			}
			int num = this._store.ReadInt32();
			int num2 = this._store.ReadInt32();
			if (num2 < 0 || num < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			if (num > 1)
			{
				this._store.BaseStream.Seek((long)num2, SeekOrigin.Current);
			}
			else
			{
				string text = this._store.ReadString();
				AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
				if (!ResourceManager.CompareNames(text, ResourceManager.ResReaderTypeName, assemblyName))
				{
					throw new NotSupportedException(Environment.GetResourceString("This .resources file should not be read with this reader. The resource reader type is \"{0}\".", new object[] { text }));
				}
				this.SkipString();
			}
			int num3 = this._store.ReadInt32();
			if (num3 != 2 && num3 != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The ResourceReader class does not know how to read this version of .resources files. Expected version: {0}  This file: {1}", new object[] { 2, num3 }));
			}
			this._version = num3;
			this._numResources = this._store.ReadInt32();
			if (this._numResources < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			int num4 = this._store.ReadInt32();
			if (num4 < 0)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._typeTable = new RuntimeType[num4];
			this._typeNamePositions = new int[num4];
			for (int i = 0; i < num4; i++)
			{
				this._typeNamePositions[i] = (int)this._store.BaseStream.Position;
				this.SkipString();
			}
			int num5 = (int)this._store.BaseStream.Position & 7;
			if (num5 != 0)
			{
				for (int j = 0; j < 8 - num5; j++)
				{
					this._store.ReadByte();
				}
			}
			if (this._ums == null)
			{
				this._nameHashes = new int[this._numResources];
				for (int k = 0; k < this._numResources; k++)
				{
					this._nameHashes[k] = this._store.ReadInt32();
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num6 = 4 * this._numResources;
				this._nameHashesPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num6, SeekOrigin.Current);
				byte* positionPointer = this._ums.PositionPointer;
			}
			if (this._ums == null)
			{
				this._namePositions = new int[this._numResources];
				for (int l = 0; l < this._numResources; l++)
				{
					int num7 = this._store.ReadInt32();
					if (num7 < 0)
					{
						throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
					}
					this._namePositions[l] = num7;
				}
			}
			else
			{
				if (((long)this._numResources & (long)((ulong)(-536870912))) != 0L)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
				}
				int num8 = 4 * this._numResources;
				this._namePositionsPtr = (int*)this._ums.PositionPointer;
				this._ums.Seek((long)num8, SeekOrigin.Current);
				byte* positionPointer2 = this._ums.PositionPointer;
			}
			this._dataSectionOffset = (long)this._store.ReadInt32();
			if (this._dataSectionOffset < 0L)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
			this._nameSectionOffset = this._store.BaseStream.Position;
			if (this._dataSectionOffset < this._nameSectionOffset)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file."));
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0007AFF4 File Offset: 0x000791F4
		private RuntimeType FindType(int typeIndex)
		{
			if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
			{
				throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't exist."));
			}
			if (this._typeTable[typeIndex] == null)
			{
				long position = this._store.BaseStream.Position;
				try
				{
					this._store.BaseStream.Position = (long)this._typeNamePositions[typeIndex];
					string text = this._store.ReadString();
					this._typeTable[typeIndex] = (RuntimeType)Type.GetType(text, true);
				}
				finally
				{
					this._store.BaseStream.Position = position;
				}
			}
			return this._typeTable[typeIndex];
		}

		/// <summary>Retrieves the type name and data of a named resource from an open resource file or stream.</summary>
		/// <param name="resourceName">The name of a resource.</param>
		/// <param name="resourceType">When this method returns, contains a string that represents the type name of the retrieved resource (see the Remarks section for details). This parameter is passed uninitialized.</param>
		/// <param name="resourceData">When this method returns, contains a byte array that is the binary representation of the retrieved type. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="resourceName" /> does not exist.</exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="resourceName" /> has an invalid type.</exception>
		/// <exception cref="T:System.FormatException">The retrieved resource data is corrupt.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Resources.ResourceReader" /> object is not initialized, probably because it is closed.</exception>
		// Token: 0x06001F6C RID: 8044 RVA: 0x0007B0A8 File Offset: 0x000792A8
		public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
		{
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			if (this._resCache == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
			}
			int[] array = new int[this._numResources];
			int num = this.FindPosForResource(resourceName);
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("The specified resource name \"{0}\" does not exist in the resource file.", new object[] { resourceName }));
			}
			lock (this)
			{
				for (int i = 0; i < this._numResources; i++)
				{
					this._store.BaseStream.Position = this._nameSectionOffset + (long)this.GetNamePosition(i);
					int num2 = this._store.Read7BitEncodedInt();
					if (num2 < 0)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into name section.", new object[] { num2 }));
					}
					this._store.BaseStream.Position += (long)num2;
					int num3 = this._store.ReadInt32();
					if (num3 < 0 || (long)num3 >= this._store.BaseStream.Length - this._dataSectionOffset)
					{
						throw new FormatException(Environment.GetResourceString("Corrupt .resources file. Invalid offset '{0}' into data section.", new object[] { num3 }));
					}
					array[i] = num3;
				}
				Array.Sort<int>(array);
				int num4 = Array.BinarySearch<int>(array, num);
				int num5 = (int)(((num4 < this._numResources - 1) ? ((long)array[num4 + 1] + this._dataSectionOffset) : this._store.BaseStream.Length) - ((long)num + this._dataSectionOffset));
				this._store.BaseStream.Position = this._dataSectionOffset + (long)num;
				ResourceTypeCode resourceTypeCode = (ResourceTypeCode)this._store.Read7BitEncodedInt();
				if (resourceTypeCode < ResourceTypeCode.Null || resourceTypeCode >= ResourceTypeCode.StartOfUserTypes + this._typeTable.Length)
				{
					throw new BadImageFormatException(Environment.GetResourceString("Corrupt .resources file.  The specified type doesn't exist."));
				}
				resourceType = this.TypeNameFromTypeCode(resourceTypeCode);
				num5 -= (int)(this._store.BaseStream.Position - (this._dataSectionOffset + (long)num));
				byte[] array2 = this._store.ReadBytes(num5);
				if (array2.Length != num5)
				{
					throw new FormatException(Environment.GetResourceString("Corrupt .resources file. A resource name extends past the end of the stream."));
				}
				resourceData = array2;
			}
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0007B304 File Offset: 0x00079504
		private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
		{
			if (typeCode < ResourceTypeCode.StartOfUserTypes)
			{
				return "ResourceTypeCode." + typeCode.ToString();
			}
			int num = typeCode - ResourceTypeCode.StartOfUserTypes;
			long position = this._store.BaseStream.Position;
			string text;
			try
			{
				this._store.BaseStream.Position = (long)this._typeNamePositions[num];
				text = this._store.ReadString();
			}
			finally
			{
				this._store.BaseStream.Position = position;
			}
			return text;
		}

		// Token: 0x040010E6 RID: 4326
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x040010E7 RID: 4327
		private BinaryReader _store;

		// Token: 0x040010E8 RID: 4328
		internal Dictionary<string, ResourceLocator> _resCache;

		// Token: 0x040010E9 RID: 4329
		private long _nameSectionOffset;

		// Token: 0x040010EA RID: 4330
		private long _dataSectionOffset;

		// Token: 0x040010EB RID: 4331
		private int[] _nameHashes;

		// Token: 0x040010EC RID: 4332
		[SecurityCritical]
		private unsafe int* _nameHashesPtr;

		// Token: 0x040010ED RID: 4333
		private int[] _namePositions;

		// Token: 0x040010EE RID: 4334
		[SecurityCritical]
		private unsafe int* _namePositionsPtr;

		// Token: 0x040010EF RID: 4335
		private RuntimeType[] _typeTable;

		// Token: 0x040010F0 RID: 4336
		private int[] _typeNamePositions;

		// Token: 0x040010F1 RID: 4337
		private BinaryFormatter _objFormatter;

		// Token: 0x040010F2 RID: 4338
		private int _numResources;

		// Token: 0x040010F3 RID: 4339
		private UnmanagedMemoryStream _ums;

		// Token: 0x040010F4 RID: 4340
		private int _version;

		// Token: 0x020002AB RID: 683
		internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06001F6E RID: 8046 RVA: 0x0007B390 File Offset: 0x00079590
			internal ResourceEnumerator(ResourceReader reader)
			{
				this._currentName = -1;
				this._reader = reader;
				this._dataPosition = -2;
			}

			// Token: 0x06001F6F RID: 8047 RVA: 0x0007B3B0 File Offset: 0x000795B0
			public bool MoveNext()
			{
				if (this._currentName == this._reader._numResources - 1 || this._currentName == -2147483648)
				{
					this._currentIsValid = false;
					this._currentName = int.MinValue;
					return false;
				}
				this._currentIsValid = true;
				this._currentName++;
				return true;
			}

			// Token: 0x1700044F RID: 1103
			// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0007B40C File Offset: 0x0007960C
			public object Key
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
				}
			}

			// Token: 0x17000450 RID: 1104
			// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0007B482 File Offset: 0x00079682
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000451 RID: 1105
			// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0007B48F File Offset: 0x0007968F
			internal int DataPosition
			{
				get
				{
					return this._dataPosition;
				}
			}

			// Token: 0x17000452 RID: 1106
			// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0007B498 File Offset: 0x00079698
			public DictionaryEntry Entry
			{
				[SecuritySafeCritical]
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					object obj = null;
					ResourceReader reader = this._reader;
					string text;
					lock (reader)
					{
						Dictionary<string, ResourceLocator> resCache = this._reader._resCache;
						lock (resCache)
						{
							text = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
							ResourceLocator resourceLocator;
							if (this._reader._resCache.TryGetValue(text, out resourceLocator))
							{
								obj = resourceLocator.Value;
							}
							if (obj == null)
							{
								if (this._dataPosition == -1)
								{
									obj = this._reader.GetValueForNameIndex(this._currentName);
								}
								else
								{
									obj = this._reader.LoadObject(this._dataPosition);
								}
							}
						}
					}
					return new DictionaryEntry(text, obj);
				}
			}

			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0007B5C8 File Offset: 0x000797C8
			public object Value
			{
				get
				{
					if (this._currentName == -2147483648)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
					}
					if (!this._currentIsValid)
					{
						throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
					}
					if (this._reader._resCache == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
					}
					return this._reader.GetValueForNameIndex(this._currentName);
				}
			}

			// Token: 0x06001F75 RID: 8053 RVA: 0x0007B638 File Offset: 0x00079838
			public void Reset()
			{
				if (this._reader._resCache == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ResourceReader is closed."));
				}
				this._currentIsValid = false;
				this._currentName = -1;
			}

			// Token: 0x040010F5 RID: 4341
			private const int ENUM_DONE = -2147483648;

			// Token: 0x040010F6 RID: 4342
			private const int ENUM_NOT_STARTED = -1;

			// Token: 0x040010F7 RID: 4343
			private ResourceReader _reader;

			// Token: 0x040010F8 RID: 4344
			private bool _currentIsValid;

			// Token: 0x040010F9 RID: 4345
			private int _currentName;

			// Token: 0x040010FA RID: 4346
			private int _dataPosition;
		}
	}
}
