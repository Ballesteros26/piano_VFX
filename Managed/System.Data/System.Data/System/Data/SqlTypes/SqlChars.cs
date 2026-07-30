using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>
	///   <see cref="T:System.Data.SqlTypes.SqlChars" /> is a mutable reference type that wraps a <see cref="T:System.Char" /> array or a <see cref="T:System.Data.SqlTypes.SqlString" /> instance.</summary>
	// Token: 0x020002C1 RID: 705
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlChars : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class.</summary>
		// Token: 0x06001E9D RID: 7837 RVA: 0x00094424 File Offset: 0x00092624
		public SqlChars()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified character array.</summary>
		/// <param name="buffer">A <see cref="T:System.Char" /> array.</param>
		// Token: 0x06001E9E RID: 7838 RVA: 0x00094434 File Offset: 0x00092634
		public SqlChars(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._stream = null;
			if (this._rgchBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgchBuf.Length;
			}
			this._rgchWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlChars" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x06001E9F RID: 7839 RVA: 0x0009448B File Offset: 0x0009268B
		public SqlChars(SqlString value)
			: this(value.IsNull ? null : value.Value.ToCharArray())
		{
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000944AB File Offset: 0x000926AB
		internal SqlChars(SqlStreamChars s)
		{
			this._rgchBuf = null;
			this._lCurLen = -1L;
			this._stream = s;
			this._state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this._rgchWorkBuf = null;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlChars" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlChars" /> is null. Otherwise, false. </returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000944DD File Offset: 0x000926DD
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlChars" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x000944E8 File Offset: 0x000926E8
		public char[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgchBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that indicates the length in characters of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.Returns -1 if no buffer is available to the instance, or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00094500 File Offset: 0x00092700
		public long Length
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Null)
				{
					throw new SqlNullValueException();
				}
				if (state != SqlBytesCharsState.Stream)
				{
					return this._lCurLen;
				}
				return this._stream.Length;
			}
		}

		/// <summary>Gets the maximum length in two-byte characters of the value the internal buffer can hold.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value representing the maximum length in two-byte characters of the value of the internal buffer. Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00094538 File Offset: 0x00092738
		public long MaxLength
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this._rgchBuf != null)
				{
					return (long)this._rgchBuf.Length;
				}
				return -1L;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlChars" /> as an array of characters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00094568 File Offset: 0x00092768
		public char[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					char[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new char[this._lCurLen];
						Array.Copy(this._rgchBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new char[this._stream.Length];
						if (this._stream.Position != 0L)
						{
							this._stream.Seek(0L, SeekOrigin.Begin);
						}
						this._stream.Read(array, 0, checked((int)this._stream.Length));
					}
					return array;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance at the specified index.</summary>
		/// <returns>A <see cref="T:System.Char" /> value. </returns>
		/// <param name="offset">An <see cref="T:System.Int64" /> value.</param>
		// Token: 0x170005A1 RID: 1441
		public char this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this._rgchWorkBuf == null)
				{
					this._rgchWorkBuf = new char[1];
				}
				this.Read(offset, this._rgchWorkBuf, 0, 1);
				return this._rgchWorkBuf[0];
			}
			set
			{
				if (this._rgchWorkBuf == null)
				{
					this._rgchWorkBuf = new char[1];
				}
				this._rgchWorkBuf[0] = value;
				this.Write(offset, this._rgchWorkBuf, 0, 1);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00094698 File Offset: 0x00092898
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x000946AF File Offset: 0x000928AF
		internal SqlStreamChars Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlChars(this);
				}
				return this._stream;
			}
			set
			{
				this._lCurLen = -1L;
				this._stream = value;
				this._state = ((value == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			}
		}

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000946D0 File Offset: 0x000928D0
		public StorageState Storage
		{
			get
			{
				switch (this._state)
				{
				case SqlBytesCharsState.Null:
					throw new SqlNullValueException();
				case SqlBytesCharsState.Buffer:
					return StorageState.Buffer;
				case SqlBytesCharsState.Stream:
					return StorageState.Stream;
				}
				return StorageState.UnmanagedBuffer;
			}
		}

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to null.</summary>
		// Token: 0x06001EAB RID: 7851 RVA: 0x00094707 File Offset: 0x00092907
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" />long value representing the length.</param>
		// Token: 0x06001EAC RID: 7852 RVA: 0x00094720 File Offset: 0x00092920
		public void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.FStream())
			{
				this._stream.SetLength(value);
				return;
			}
			if (this._rgchBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (value > (long)this._rgchBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this._state = SqlBytesCharsState.Buffer;
			}
			this._lCurLen = value;
		}

		/// <summary>Copies characters from this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to the passed-in buffer and returns the number of copied characters.</summary>
		/// <returns>An <see cref="T:System.Int64" />long value representing the number of copied bytes.</returns>
		/// <param name="offset">An <see cref="T:System.Int64" />long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer value representing the number of characters to copy.</param>
		// Token: 0x06001EAD RID: 7853 RVA: 0x00094794 File Offset: 0x00092994
		public long Read(long offset, char[] buffer, int offsetInBuffer, int count)
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > this.Length || offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offsetInBuffer > buffer.Length || offsetInBuffer < 0)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > this.Length - offset)
			{
				count = (int)(this.Length - offset);
			}
			if (count != 0)
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Stream)
				{
					if (this._stream.Position != offset)
					{
						this._stream.Seek(offset, SeekOrigin.Begin);
					}
					this._stream.Read(buffer, offsetInBuffer, count);
				}
				else
				{
					Array.Copy(this._rgchBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		/// <summary>Copies characters from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="offset">A long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</param>
		/// <param name="buffer">The character array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of characters to copy.</param>
		// Token: 0x06001EAE RID: 7854 RVA: 0x00094870 File Offset: 0x00092A70
		public void Write(long offset, char[] buffer, int offsetInBuffer, int count)
		{
			if (this.FStream())
			{
				if (this._stream.Position != offset)
				{
					this._stream.Seek(offset, SeekOrigin.Begin);
				}
				this._stream.Write(buffer, offsetInBuffer, count);
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this._rgchBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this._rgchBuf.Length)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (offsetInBuffer < 0 || offsetInBuffer > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offsetInBuffer");
			}
			if (count < 0 || count > buffer.Length - offsetInBuffer)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if ((long)count > (long)this._rgchBuf.Length - offset)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (this.IsNull)
			{
				if (offset != 0L)
				{
					throw new SqlTypeException("Cannot write to non-zero offset, because current value is Null.");
				}
				this._lCurLen = 0L;
				this._state = SqlBytesCharsState.Buffer;
			}
			else if (offset > this._lCurLen)
			{
				throw new SqlTypeException("Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.");
			}
			if (count != 0)
			{
				Array.Copy(buffer, (long)offsetInBuffer, this._rgchBuf, offset, (long)count);
				if (this._lCurLen < offset + (long)count)
				{
					this._lCurLen = offset + (long)count;
				}
			}
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance to its equivalent <see cref="T:System.Data.SqlTypes.SqlString" /> representation.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> representation of this type.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001EAF RID: 7855 RVA: 0x000949AB File Offset: 0x00092BAB
		public SqlString ToSqlString()
		{
			if (!this.IsNull)
			{
				return new string(this.Value);
			}
			return SqlString.Null;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to a <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlChars" /> structure to be converted.</param>
		// Token: 0x06001EB0 RID: 7856 RVA: 0x000949CB File Offset: 0x00092BCB
		public static explicit operator SqlString(SqlChars value)
		{
			return value.ToSqlString();
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> structure to a <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlChars" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> structure to be converted.</param>
		// Token: 0x06001EB1 RID: 7857 RVA: 0x000949D3 File Offset: 0x00092BD3
		public static explicit operator SqlChars(SqlString value)
		{
			return new SqlChars(value);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000949DB File Offset: 0x00092BDB
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000949E4 File Offset: 0x00092BE4
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000949F0 File Offset: 0x00092BF0
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
			}
			if (this._rgchBuf == null || (long)this._rgchBuf.Length < length)
			{
				this._rgchBuf = new char[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgchBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x00094A84 File Offset: 0x00092C84
		private void SetBuffer(char[] buffer)
		{
			this._rgchBuf = buffer;
			this._lCurLen = ((this._rgchBuf == null) ? (-1L) : ((long)this._rgchBuf.Length));
			this._stream = null;
			this._state = ((this._rgchBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06001EB6 RID: 7862 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x06001EB7 RID: 7863 RVA: 0x00094AC4 File Offset: 0x00092CC4
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
				return;
			}
			char[] array = r.ReadElementString().ToCharArray();
			this.SetBuffer(array);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06001EB8 RID: 7864 RVA: 0x00094B10 File Offset: 0x00092D10
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			char[] buffer = this.Buffer;
			writer.WriteString(new string(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001EB9 RID: 7865 RVA: 0x00094B5B File Offset: 0x00092D5B
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlChars" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06001EBA RID: 7866 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlChars.IsNull" /> returns true. For more information, see Handling Null Values (ADO.NET).</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00094B6C File Offset: 0x00092D6C
		public static SqlChars Null
		{
			get
			{
				return new SqlChars(null);
			}
		}

		// Token: 0x040015C7 RID: 5575
		internal char[] _rgchBuf;

		// Token: 0x040015C8 RID: 5576
		private long _lCurLen;

		// Token: 0x040015C9 RID: 5577
		internal SqlStreamChars _stream;

		// Token: 0x040015CA RID: 5578
		private SqlBytesCharsState _state;

		// Token: 0x040015CB RID: 5579
		private char[] _rgchWorkBuf;

		// Token: 0x040015CC RID: 5580
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x040015CD RID: 5581
		private const long x_lNull = -1L;
	}
}
