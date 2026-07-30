using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a mutable reference type that wraps either a <see cref="P:System.Data.SqlTypes.SqlBytes.Buffer" /> or a <see cref="P:System.Data.SqlTypes.SqlBytes.Stream" />.</summary>
	// Token: 0x020002BF RID: 703
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlBytes : INullable, IXmlSerializable, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class.</summary>
		// Token: 0x06001E6D RID: 7789 RVA: 0x00093937 File Offset: 0x00091B37
		public SqlBytes()
		{
			this.SetNull();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified byte array.</summary>
		/// <param name="buffer">The array of unsigned bytes. </param>
		// Token: 0x06001E6E RID: 7790 RVA: 0x00093948 File Offset: 0x00091B48
		public SqlBytes(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._stream = null;
			if (this._rgbBuf == null)
			{
				this._state = SqlBytesCharsState.Null;
				this._lCurLen = -1L;
			}
			else
			{
				this._state = SqlBytesCharsState.Buffer;
				this._lCurLen = (long)this._rgbBuf.Length;
			}
			this._rgbWorkBuf = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> value.</param>
		// Token: 0x06001E6F RID: 7791 RVA: 0x0009399F File Offset: 0x00091B9F
		public SqlBytes(SqlBinary value)
			: this(value.IsNull ? null : value.Value)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBytes" /> class based on the specified <see cref="T:System.IO.Stream" /> value.</summary>
		/// <param name="s">A <see cref="T:System.IO.Stream" />. </param>
		// Token: 0x06001E70 RID: 7792 RVA: 0x000939BA File Offset: 0x00091BBA
		public SqlBytes(Stream s)
		{
			this._rgbBuf = null;
			this._lCurLen = -1L;
			this._stream = s;
			this._state = ((s == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Stream);
			this._rgbWorkBuf = null;
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBytes" /> is null, false otherwise.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000939EC File Offset: 0x00091BEC
		public bool IsNull
		{
			get
			{
				return this._state == SqlBytesCharsState.Null;
			}
		}

		/// <summary>Returns a reference to the internal buffer. </summary>
		/// <returns>Returns a reference to the internal buffer. For <see cref="T:System.Data.SqlTypes.SqlBytes" /> instances created on top of unmanaged pointers, it returns a managed copy of the internal buffer.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000939F7 File Offset: 0x00091BF7
		public byte[] Buffer
		{
			get
			{
				if (this.FStream())
				{
					this.CopyStreamToBuffer();
				}
				return this._rgbBuf;
			}
		}

		/// <summary>Gets the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value representing the length of the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance. Returns -1 if no buffer is available to the instance or if the value is null. Returns a <see cref="P:System.IO.Stream.Length" /> for a stream-wrapped instance.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00093A10 File Offset: 0x00091C10
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

		/// <summary>Gets the maximum length of the value of the internal buffer of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>A long representing the maximum length of the value of the internal buffer. Returns -1 for a stream-wrapped <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00093A48 File Offset: 0x00091C48
		public long MaxLength
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state == SqlBytesCharsState.Stream)
				{
					return -1L;
				}
				if (this._rgbBuf != null)
				{
					return (long)this._rgbBuf.Length;
				}
				return -1L;
			}
		}

		/// <summary>Returns a managed copy of the value held by this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as an array of bytes.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00093A78 File Offset: 0x00091C78
		public byte[] Value
		{
			get
			{
				SqlBytesCharsState state = this._state;
				if (state != SqlBytesCharsState.Null)
				{
					byte[] array;
					if (state != SqlBytesCharsState.Stream)
					{
						array = new byte[this._lCurLen];
						Array.Copy(this._rgbBuf, 0, array, 0, (int)this._lCurLen);
					}
					else
					{
						if (this._stream.Length > 2147483647L)
						{
							throw new SqlTypeException("The buffer is insufficient. Read or write operation failed.");
						}
						array = new byte[this._stream.Length];
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

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance at the specified index.</summary>
		/// <returns>A <see cref="T:System.Byte" /> value. </returns>
		/// <param name="offset">A <see cref="T:System.Int64" /> value.</param>
		// Token: 0x17000593 RID: 1427
		public byte this[long offset]
		{
			get
			{
				if (offset < 0L || offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (this._rgbWorkBuf == null)
				{
					this._rgbWorkBuf = new byte[1];
				}
				this.Read(offset, this._rgbWorkBuf, 0, 1);
				return this._rgbWorkBuf[0];
			}
			set
			{
				if (this._rgbWorkBuf == null)
				{
					this._rgbWorkBuf = new byte[1];
				}
				this._rgbWorkBuf[0] = value;
				this.Write(offset, this._rgbWorkBuf, 0, 1);
			}
		}

		/// <summary>Returns information about the storage state of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.StorageState" /> enumeration.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00093BA8 File Offset: 0x00091DA8
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

		/// <summary>Gets or sets the data of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> as a stream.</summary>
		/// <returns>The stream that contains the SqlBytes data.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00093BDF File Offset: 0x00091DDF
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x00093BF6 File Offset: 0x00091DF6
		public Stream Stream
		{
			get
			{
				if (!this.FStream())
				{
					return new StreamOnSqlBytes(this);
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

		/// <summary>Sets this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to null.</summary>
		// Token: 0x06001E7B RID: 7803 RVA: 0x00093C14 File Offset: 0x00091E14
		public void SetNull()
		{
			this._lCurLen = -1L;
			this._stream = null;
			this._state = SqlBytesCharsState.Null;
		}

		/// <summary>Sets the length of this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="value">The <see cref="T:System.Int64" /> long value representing the length.</param>
		// Token: 0x06001E7C RID: 7804 RVA: 0x00093C2C File Offset: 0x00091E2C
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
			if (this._rgbBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (value > (long)this._rgbBuf.Length)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			if (this.IsNull)
			{
				this._state = SqlBytesCharsState.Buffer;
			}
			this._lCurLen = value;
		}

		/// <summary>Copies bytes from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance to the passed-in buffer and returns the number of copied bytes.</summary>
		/// <returns>An <see cref="T:System.Int64" /> long value representing the number of copied bytes.</returns>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		// Token: 0x06001E7D RID: 7805 RVA: 0x00093CA0 File Offset: 0x00091EA0
		public long Read(long offset, byte[] buffer, int offsetInBuffer, int count)
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
					Array.Copy(this._rgbBuf, offset, buffer, (long)offsetInBuffer, (long)count);
				}
			}
			return (long)count;
		}

		/// <summary>Copies bytes from the passed-in buffer to this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="offset">An <see cref="T:System.Int64" /> long value offset into the value that is contained in the <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</param>
		/// <param name="buffer">The byte array buffer to copy into.</param>
		/// <param name="offsetInBuffer">An <see cref="T:System.Int32" /> integer offset into the buffer to start copying into.</param>
		/// <param name="count">An <see cref="T:System.Int32" /> integer representing the number of bytes to copy.</param>
		// Token: 0x06001E7E RID: 7806 RVA: 0x00093D7C File Offset: 0x00091F7C
		public void Write(long offset, byte[] buffer, int offsetInBuffer, int count)
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
			if (this._rgbBuf == null)
			{
				throw new SqlTypeException("There is no buffer. Read or write operation failed.");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset > (long)this._rgbBuf.Length)
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
			if ((long)count > (long)this._rgbBuf.Length - offset)
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
				Array.Copy(buffer, (long)offsetInBuffer, this._rgbBuf, offset, (long)count);
				if (this._lCurLen < offset + (long)count)
				{
					this._lCurLen = offset + (long)count;
				}
			}
		}

		/// <summary>Constructs and returns a <see cref="T:System.Data.SqlTypes.SqlBinary" /> from this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> from this instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001E7F RID: 7807 RVA: 0x00093EB7 File Offset: 0x000920B7
		public SqlBinary ToSqlBinary()
		{
			if (!this.IsNull)
			{
				return new SqlBinary(this.Value);
			}
			return SqlBinary.Null;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure to be converted.</param>
		// Token: 0x06001E80 RID: 7808 RVA: 0x00093ED2 File Offset: 0x000920D2
		public static explicit operator SqlBinary(SqlBytes value)
		{
			return value.ToSqlBinary();
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBytes" /> structure.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to be converted.</param>
		// Token: 0x06001E81 RID: 7809 RVA: 0x00093EDA File Offset: 0x000920DA
		public static explicit operator SqlBytes(SqlBinary value)
		{
			return new SqlBytes(value);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00093EE2 File Offset: 0x000920E2
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			bool isNull = this.IsNull;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00093EEC File Offset: 0x000920EC
		private void CopyStreamToBuffer()
		{
			long length = this._stream.Length;
			if (length >= 2147483647L)
			{
				throw new SqlTypeException("Cannot write from an offset that is larger than current length. It would leave uninitialized data in the buffer.");
			}
			if (this._rgbBuf == null || (long)this._rgbBuf.Length < length)
			{
				this._rgbBuf = new byte[length];
			}
			if (this._stream.Position != 0L)
			{
				this._stream.Seek(0L, SeekOrigin.Begin);
			}
			this._stream.Read(this._rgbBuf, 0, (int)length);
			this._stream = null;
			this._lCurLen = length;
			this._state = SqlBytesCharsState.Buffer;
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00093F80 File Offset: 0x00092180
		internal bool FStream()
		{
			return this._state == SqlBytesCharsState.Stream;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00093F8B File Offset: 0x0009218B
		private void SetBuffer(byte[] buffer)
		{
			this._rgbBuf = buffer;
			this._lCurLen = ((this._rgbBuf == null) ? (-1L) : ((long)this._rgbBuf.Length));
			this._stream = null;
			this._state = ((this._rgbBuf == null) ? SqlBytesCharsState.Null : SqlBytesCharsState.Buffer);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06001E86 RID: 7814 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="r">XmlReader</param>
		// Token: 0x06001E87 RID: 7815 RVA: 0x00093FC8 File Offset: 0x000921C8
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			byte[] array = null;
			string attribute = r.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				r.ReadElementString();
				this.SetNull();
			}
			else
			{
				string text = r.ReadElementString();
				if (text == null)
				{
					array = Array.Empty<byte>();
				}
				else
				{
					text = text.Trim();
					if (text.Length == 0)
					{
						array = Array.Empty<byte>();
					}
					else
					{
						array = Convert.FromBase64String(text);
					}
				}
			}
			this.SetBuffer(array);
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06001E88 RID: 7816 RVA: 0x0009403C File Offset: 0x0009223C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			byte[] buffer = this.Buffer;
			writer.WriteString(Convert.ToBase64String(buffer, 0, (int)this.Length));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified XmlSchemaSet.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001E89 RID: 7817 RVA: 0x00092937 File Offset: 0x00090B37
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets serialization information with all the data needed to reinstantiate this <see cref="T:System.Data.SqlTypes.SqlBytes" /> instance.</summary>
		/// <param name="info">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x06001E8A RID: 7818 RVA: 0x0003BAC5 File Offset: 0x00039CC5
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Returns a null instance of this <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>Returns an instance in such a way that <see cref="P:System.Data.SqlTypes.SqlBytes.IsNull" /> returns true.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00094087 File Offset: 0x00092287
		public static SqlBytes Null
		{
			get
			{
				return new SqlBytes(null);
			}
		}

		// Token: 0x040015BE RID: 5566
		internal byte[] _rgbBuf;

		// Token: 0x040015BF RID: 5567
		private long _lCurLen;

		// Token: 0x040015C0 RID: 5568
		internal Stream _stream;

		// Token: 0x040015C1 RID: 5569
		private SqlBytesCharsState _state;

		// Token: 0x040015C2 RID: 5570
		private byte[] _rgbWorkBuf;

		// Token: 0x040015C3 RID: 5571
		private const long x_lMaxLen = 2147483647L;

		// Token: 0x040015C4 RID: 5572
		private const long x_lNull = -1L;
	}
}
