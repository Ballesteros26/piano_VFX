using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Text
{
	/// <summary>Represents a character encoding.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200027C RID: 636
	[ComVisible(true)]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class.</summary>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006CE54 File Offset: 0x0006B054
		protected Encoding()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.Encoding" /> class that corresponds to the specified code page.</summary>
		/// <param name="codePage">The code page identifier of the preferred encoding.-or- 0, to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codePage" /> is less than zero. </exception>
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006CE5D File Offset: 0x0006B05D
		protected Encoding(int codePage)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.SetDefaultFallbacks();
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0006CE88 File Offset: 0x0006B088
		protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.encoderFallback = encoderFallback ?? new InternalEncoderBestFitFallback(this);
			this.decoderFallback = decoderFallback ?? new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0006CEDA File Offset: 0x0006B0DA
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = new InternalEncoderBestFitFallback(this);
			this.decoderFallback = new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0006CEF4 File Offset: 0x0006B0F4
		internal void OnDeserializing()
		{
			this.encoderFallback = null;
			this.decoderFallback = null;
			this.m_isReadOnly = true;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0006CF0B File Offset: 0x0006B10B
		internal void OnDeserialized()
		{
			if (this.encoderFallback == null || this.decoderFallback == null)
			{
				this.m_deserializedFromEverett = true;
				this.SetDefaultFallbacks();
			}
			this.dataItem = null;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0006CF31 File Offset: 0x0006B131
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.OnDeserializing();
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0006CF39 File Offset: 0x0006B139
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006CF41 File Offset: 0x0006B141
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.dataItem = null;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0006CF4C File Offset: 0x0006B14C
		internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			this.dataItem = null;
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0006D018 File Offset: 0x0006B218
		internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_isReadOnly", this.m_isReadOnly);
			info.AddValue("encoderFallback", this.EncoderFallback);
			info.AddValue("decoderFallback", this.DecoderFallback);
			info.AddValue("m_codePage", this.m_codePage);
			info.AddValue("dataItem", null);
			info.AddValue("Encoding+m_codePage", this.m_codePage);
			info.AddValue("Encoding+dataItem", null);
		}

		/// <summary>Converts an entire byte array from one encoding to another.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the results of converting <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <param name="srcEncoding">The encoding format of <paramref name="bytes" />. </param>
		/// <param name="dstEncoding">The target encoding format. </param>
		/// <param name="bytes">The bytes to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is null.-or- <paramref name="dstEncoding" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D03 RID: 7427 RVA: 0x0006D0A0 File Offset: 0x0006B2A0
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
		}

		/// <summary>Converts a range of bytes in a byte array from one encoding to another.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" /> containing the result of converting a range of bytes in <paramref name="bytes" /> from <paramref name="srcEncoding" /> to <paramref name="dstEncoding" />.</returns>
		/// <param name="srcEncoding">The encoding of the source array, <paramref name="bytes" />. </param>
		/// <param name="dstEncoding">The encoding of the output array. </param>
		/// <param name="bytes">The array of bytes to convert. </param>
		/// <param name="index">The index of the first element of <paramref name="bytes" /> to convert. </param>
		/// <param name="count">The number of bytes to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="srcEncoding" /> is null.-or- <paramref name="dstEncoding" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> and <paramref name="count" /> do not specify a valid range in the byte array. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-srcEncoding.<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-dstEncoding.<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D04 RID: 7428 RVA: 0x0006D0BC File Offset: 0x0006B2BC
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null || dstEncoding == null)
			{
				throw new ArgumentNullException((srcEncoding == null) ? "srcEncoding" : "dstEncoding", Environment.GetResourceString("Array cannot be null."));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0006D118 File Offset: 0x0006B318
		private static object InternalSyncObject
		{
			get
			{
				if (Encoding.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, null);
				}
				return Encoding.s_InternalSyncObject;
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0006D144 File Offset: 0x0006B344
		[SecurityCritical]
		public static void RegisterProvider(EncodingProvider provider)
		{
			EncodingProvider.AddProvider(provider);
		}

		/// <summary>Returns the encoding associated with the specified code page identifier.</summary>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.-or- 0 (zero), to use the default encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D07 RID: 7431 RVA: 0x0006D14C File Offset: 0x0006B34C
		[SecuritySafeCritical]
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[] { 0, 65535 }));
			}
			if (Encoding.encodings != null)
			{
				encoding = (Encoding)Encoding.encodings[codepage];
			}
			if (encoding == null)
			{
				object internalSyncObject = Encoding.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Encoding.encodings == null)
					{
						Encoding.encodings = new Hashtable();
					}
					if ((encoding = (Encoding)Encoding.encodings[codepage]) != null)
					{
						return encoding;
					}
					if (codepage <= 1201)
					{
						if (codepage <= 3)
						{
							if (codepage == 0)
							{
								encoding = Encoding.Default;
								goto IL_0245;
							}
							if (codepage - 1 > 2)
							{
								goto IL_01C2;
							}
						}
						else if (codepage != 42)
						{
							if (codepage == 1200)
							{
								encoding = Encoding.Unicode;
								goto IL_0245;
							}
							if (codepage != 1201)
							{
								goto IL_01C2;
							}
							encoding = Encoding.BigEndianUnicode;
							goto IL_0245;
						}
						throw new ArgumentException(Environment.GetResourceString("{0} is not a supported code page.", new object[] { codepage }), "codepage");
					}
					if (codepage <= 20127)
					{
						if (codepage == 12000)
						{
							encoding = Encoding.UTF32;
							goto IL_0245;
						}
						if (codepage == 12001)
						{
							encoding = new UTF32Encoding(true, true);
							goto IL_0245;
						}
						if (codepage == 20127)
						{
							encoding = Encoding.ASCII;
							goto IL_0245;
						}
					}
					else
					{
						if (codepage == 28591)
						{
							encoding = Encoding.Latin1;
							goto IL_0245;
						}
						if (codepage == 65000)
						{
							encoding = Encoding.UTF7;
							goto IL_0245;
						}
						if (codepage == 65001)
						{
							encoding = Encoding.UTF8;
							goto IL_0245;
						}
					}
					IL_01C2:
					if (EncodingTable.GetCodePageDataItem(codepage) == null)
					{
						throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { codepage }));
					}
					if (codepage != 12000)
					{
						if (codepage != 12001)
						{
							encoding = (Encoding)EncodingHelper.InvokeI18N("GetEncoding", new object[] { codepage });
							if (encoding == null)
							{
								throw new NotSupportedException(string.Format("Encoding {0} data could not be found. Make sure you have correct international codeset assembly installed and enabled.", codepage));
							}
						}
						else
						{
							encoding = new UTF32Encoding(true, true);
						}
					}
					else
					{
						encoding = Encoding.UTF32;
					}
					IL_0245:
					Encoding.encodings.Add(codepage, encoding);
				}
				return encoding;
			}
			return encoding;
		}

		/// <summary>Returns the encoding associated with the specified code page identifier. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <param name="codepage">The code page identifier of the preferred encoding. Possible values are listed in the Code Page column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.-or- 0 (zero), to use the default encoding. </param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding. </param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codepage" /> is less than zero or greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="codepage" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0006D3DC File Offset: 0x0006B5DC
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		/// <summary>Returns the encoding associated with the specified code page name.</summary>
		/// <returns>The encoding  associated with the specified code page.</returns>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by the <see cref="P:System.Text.Encoding.WebName" /> property is valid. Possible values are listed in the Name column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.-or- The code page indicated by <paramref name="name" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D09 RID: 7433 RVA: 0x0006D418 File Offset: 0x0006B618
		public static Encoding GetEncoding(string name)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		/// <summary>Returns the encoding associated with the specified code page name. Parameters specify an error handler for characters that cannot be encoded and byte sequences that cannot be decoded.</summary>
		/// <returns>The encoding that is associated with the specified code page.</returns>
		/// <param name="name">The code page name of the preferred encoding. Any value returned by the <see cref="P:System.Text.Encoding.WebName" /> property is valid. Possible values are listed in the Name column of the table that appears in the <see cref="T:System.Text.Encoding" /> class topic.</param>
		/// <param name="encoderFallback">An object that provides an error-handling procedure when a character cannot be encoded with the current encoding. </param>
		/// <param name="decoderFallback">An object that provides an error-handling procedure when a byte sequence cannot be decoded with the current encoding. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid code page name.-or- The code page indicated by <paramref name="name" /> is not supported by the underlying platform. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D0A RID: 7434 RVA: 0x0006D43C File Offset: 0x0006B63C
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
		}

		/// <summary>Returns an array that contains all encodings.</summary>
		/// <returns>An array that contains all encodings.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D0B RID: 7435 RVA: 0x0006D464 File Offset: 0x0006B664
		public static EncodingInfo[] GetEncodings()
		{
			return EncodingTable.GetEncodings();
		}

		/// <summary>When overridden in a derived class, returns a sequence of bytes that specifies the encoding used.</summary>
		/// <returns>A byte array containing a sequence of bytes that specifies the encoding used.-or- A byte array of length zero, if a preamble is not required.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D0C RID: 7436 RVA: 0x0006D46B File Offset: 0x0006B66B
		public virtual byte[] GetPreamble()
		{
			return EmptyArray<byte>.Value;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0006D474 File Offset: 0x0006B674
		private void GetDataItem()
		{
			if (this.dataItem == null)
			{
				this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
				if (this.dataItem == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.", new object[] { this.m_codePage }));
				}
			}
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent body tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> that can be used with mail agent body tags.-or- An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0006D4C6 File Offset: 0x0006B6C6
		public virtual string BodyName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.BodyName;
			}
		}

		/// <summary>When overridden in a derived class, gets the human-readable description of the current encoding.</summary>
		/// <returns>The human-readable description of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0006D4E1 File Offset: 0x0006B6E1
		public virtual string EncodingName
		{
			get
			{
				return Environment.GetResourceStringEncodingName(this.m_codePage);
			}
		}

		/// <summary>When overridden in a derived class, gets a name for the current encoding that can be used with mail agent header tags.</summary>
		/// <returns>A name for the current <see cref="T:System.Text.Encoding" /> to use with mail agent header tags.-or- An empty string (""), if the current <see cref="T:System.Text.Encoding" /> cannot be used.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0006D4EE File Offset: 0x0006B6EE
		public virtual string HeaderName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.HeaderName;
			}
		}

		/// <summary>When overridden in a derived class, gets the name registered with the Internet Assigned Numbers Authority (IANA) for the current encoding.</summary>
		/// <returns>The IANA name for the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0006D509 File Offset: 0x0006B709
		public virtual string WebName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.WebName;
			}
		}

		/// <summary>When overridden in a derived class, gets the Windows operating system code page that most closely corresponds to the current encoding.</summary>
		/// <returns>The Windows operating system code page that most closely corresponds to the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0006D524 File Offset: 0x0006B724
		public virtual int WindowsCodePage
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.UIFamilyCodePage;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for displaying content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for displaying content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0006D53F File Offset: 0x0006B73F
		public virtual bool IsBrowserDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 2U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by browser clients for saving content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by browser clients for saving content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0006D55F File Offset: 0x0006B75F
		public virtual bool IsBrowserSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 512U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for displaying content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for displaying content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x0006D583 File Offset: 0x0006B783
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 1U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding can be used by mail and news clients for saving content.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> can be used by mail and news clients for saving content; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0006D5A3 File Offset: 0x0006B7A3
		public virtual bool IsMailNewsSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 256U) > 0U;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding uses single-byte code points.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> uses single-byte code points; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		public virtual bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.EncoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The encoder fallback object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0006D5C7 File Offset: 0x0006B7C7
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0006D5CF File Offset: 0x0006B7CF
		[ComVisible(false)]
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Text.DecoderFallback" /> object for the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>The decoder fallback object for the current <see cref="T:System.Text.Encoding" /> object. </returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A value cannot be assigned in a set operation because the current <see cref="T:System.Text.Encoding" /> object is read-only.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0006D5FE File Offset: 0x0006B7FE
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0006D606 File Offset: 0x0006B806
		[ComVisible(false)]
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		/// <summary>When overridden in a derived class, creates a shallow copy of the current <see cref="T:System.Text.Encoding" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Text.Encoding" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D1C RID: 7452 RVA: 0x0006D635 File Offset: 0x0006B835
		[ComVisible(false)]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.m_isReadOnly = false;
			return encoding;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> is read-only; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0006D649 File Offset: 0x0006B849
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an encoding for the ASCII (7-bit) character set.</summary>
		/// <returns>An  encoding for the ASCII (7-bit) character set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0006D651 File Offset: 0x0006B851
		public static Encoding ASCII
		{
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					Encoding.asciiEncoding = new ASCIIEncoding();
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x0006D66F File Offset: 0x0006B86F
		private static Encoding Latin1
		{
			get
			{
				if (Encoding.latin1Encoding == null)
				{
					Encoding.latin1Encoding = new Latin1Encoding();
				}
				return Encoding.latin1Encoding;
			}
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding all the characters in the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding all the characters in the specified character array.</returns>
		/// <param name="chars">The character array containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D20 RID: 7456 RVA: 0x0006D68D File Offset: 0x0006B88D
		public virtual int GetByteCount(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetByteCount(chars, 0, chars.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding the characters in the specified string.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D21 RID: 7457 RVA: 0x0006D6B4 File Offset: 0x0006B8B4
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters from the specified character array.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D22 RID: 7458
		public abstract int GetByteCount(char[] chars, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of bytes produced by encoding a set of characters starting at the specified character pointer.</summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D23 RID: 7459 RVA: 0x0006D6E4 File Offset: 0x0006B8E4
		[ComVisible(false)]
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0006D74A File Offset: 0x0006B94A
		[SecurityCritical]
		internal unsafe virtual int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			return this.GetByteCount(chars, count);
		}

		/// <summary>When overridden in a derived class, encodes all the characters in the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D25 RID: 7461 RVA: 0x0006D754 File Offset: 0x0006B954
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="index">The index of the first character to encode. </param>
		/// <param name="count">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D26 RID: 7462 RVA: 0x0006D77C File Offset: 0x0006B97C
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified character array into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="chars">The character array containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D27 RID: 7463
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		/// <summary>When overridden in a derived class, encodes all the characters in the specified string into a sequence of bytes.</summary>
		/// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
		/// <param name="s">The string containing the characters to encode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D28 RID: 7464 RVA: 0x0006D7A8 File Offset: 0x0006B9A8
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			byte[] array = new byte[this.GetByteCount(s)];
			this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, encodes a set of characters from the specified string into the specified byte array.</summary>
		/// <returns>The actual number of bytes written into <paramref name="bytes" />.</returns>
		/// <param name="s">The string containing the set of characters to encode. </param>
		/// <param name="charIndex">The index of the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes. </param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charIndex" /> or <paramref name="charCount" /> or <paramref name="byteIndex" /> is less than zero.-or- <paramref name="charIndex" /> and <paramref name="charCount" /> do not denote a valid range in <paramref name="chars" />.-or- <paramref name="byteIndex" /> is not a valid index in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="bytes" /> does not have enough capacity from <paramref name="byteIndex" /> to the end of the array to accommodate the resulting bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D29 RID: 7465 RVA: 0x0006D7EC File Offset: 0x0006B9EC
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0006D80E File Offset: 0x0006BA0E
		[SecurityCritical]
		internal unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			return this.GetBytes(chars, charCount, bytes, byteCount);
		}

		/// <summary>When overridden in a derived class, encodes a set of characters starting at the specified character pointer into a sequence of bytes that are stored starting at the specified byte pointer.</summary>
		/// <returns>The actual number of bytes written at the location indicated by the <paramref name="bytes" /> parameter.</returns>
		/// <param name="chars">A pointer to the first character to encode. </param>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <param name="bytes">A pointer to the location at which to start writing the resulting sequence of bytes. </param>
		/// <param name="byteCount">The maximum number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is null.-or- <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> or <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="byteCount" /> is less than the resulting number of bytes. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D2B RID: 7467 RVA: 0x0006D81C File Offset: 0x0006BA1C
		[SecurityCritical]
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("Array cannot be null."));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding all the bytes in the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D2C RID: 7468 RVA: 0x0006D8CC File Offset: 0x0006BACC
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D2D RID: 7469
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		/// <summary>When overridden in a derived class, calculates the number of characters produced by decoding a sequence of bytes starting at the specified byte pointer.</summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D2E RID: 7470 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
		[CLSCompliant(false)]
		[ComVisible(false)]
		[SecurityCritical]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0006D957 File Offset: 0x0006BB57
		[SecurityCritical]
		internal unsafe virtual int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return this.GetCharCount(bytes, count);
		}

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D30 RID: 7472 RVA: 0x0006D961 File Offset: 0x0006BB61
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a set of characters.</summary>
		/// <returns>A character array containing the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D31 RID: 7473 RVA: 0x0006D988 File Offset: 0x0006BB88
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into the specified character array.</summary>
		/// <returns>The actual number of characters written into <paramref name="chars" />.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="byteIndex">The index of the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">The character array to contain the resulting set of characters. </param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteIndex" /> or <paramref name="byteCount" /> or <paramref name="charIndex" /> is less than zero.-or- <paramref name="byteindex" /> and <paramref name="byteCount" /> do not denote a valid range in <paramref name="bytes" />.-or- <paramref name="charIndex" /> is not a valid index in <paramref name="chars" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="chars" /> does not have enough capacity from <paramref name="charIndex" /> to the end of the array to accommodate the resulting characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D32 RID: 7474
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		/// <summary>When overridden in a derived class, decodes a sequence of bytes starting at the specified byte pointer into a set of characters that are stored starting at the specified character pointer.</summary>
		/// <returns>The actual number of characters written at the location indicated by the <paramref name="chars" /> parameter.</returns>
		/// <param name="bytes">A pointer to the first byte to decode. </param>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <param name="chars">A pointer to the location at which to start writing the resulting set of characters. </param>
		/// <param name="charCount">The maximum number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null.-or- <paramref name="chars" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> or <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="charCount" /> is less than the resulting number of characters. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D33 RID: 7475 RVA: 0x0006D9B4 File Offset: 0x0006BBB4
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("Non-negative number required."));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0006DA64 File Offset: 0x0006BC64
		[SecurityCritical]
		internal unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			return this.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0006DA71 File Offset: 0x0006BC71
		[SecurityCritical]
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("Non-negative number required."));
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		/// <summary>When overridden in a derived class, gets the code page identifier of the current <see cref="T:System.Text.Encoding" />.</summary>
		/// <returns>The code page identifier of the current <see cref="T:System.Text.Encoding" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0006DAAE File Offset: 0x0006BCAE
		public virtual int CodePage
		{
			get
			{
				return this.m_codePage;
			}
		}

		/// <summary>Gets a value indicating whether the current encoding is always normalized, using the default normalization form.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> is always normalized; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D37 RID: 7479 RVA: 0x0006DAB6 File Offset: 0x0006BCB6
		[ComVisible(false)]
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the current encoding is always normalized, using the specified normalization form.</summary>
		/// <returns>true if the current <see cref="T:System.Text.Encoding" /> object is always normalized using the specified <see cref="T:System.Text.NormalizationForm" /> value; otherwise, false. The default is false.</returns>
		/// <param name="form">One of the <see cref="T:System.Text.NormalizationForm" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D38 RID: 7480 RVA: 0x00015ED5 File Offset: 0x000140D5
		[ComVisible(false)]
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return false;
		}

		/// <summary>When overridden in a derived class, obtains a decoder that converts an encoded sequence of bytes into a sequence of characters.</summary>
		/// <returns>A <see cref="T:System.Text.Decoder" /> that converts an encoded sequence of bytes into a sequence of characters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D39 RID: 7481 RVA: 0x0006DABF File Offset: 0x0006BCBF
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0006DAC7 File Offset: 0x0006BCC7
		[SecurityCritical]
		private static Encoding CreateDefaultEncoding()
		{
			Encoding encoding = EncodingHelper.GetDefaultEncoding();
			encoding.m_isReadOnly = true;
			return encoding;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0006DAD5 File Offset: 0x0006BCD5
		internal void setReadOnly(bool value = true)
		{
			this.m_isReadOnly = value;
		}

		/// <summary>Gets an encoding for the operating system's current ANSI code page.</summary>
		/// <returns>An encoding for the operating system's current ANSI code page.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006DADE File Offset: 0x0006BCDE
		public static Encoding Default
		{
			[SecuritySafeCritical]
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
				}
				return Encoding.defaultEncoding;
			}
		}

		/// <summary>When overridden in a derived class, obtains an encoder that converts a sequence of Unicode characters into an encoded sequence of bytes.</summary>
		/// <returns>A <see cref="T:System.Text.Encoder" /> that converts a sequence of Unicode characters into an encoded sequence of bytes.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D3D RID: 7485 RVA: 0x0006DAFC File Offset: 0x0006BCFC
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		/// <summary>When overridden in a derived class, calculates the maximum number of bytes produced by encoding the specified number of characters.</summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="charCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D3E RID: 7486
		public abstract int GetMaxByteCount(int charCount);

		/// <summary>When overridden in a derived class, calculates the maximum number of characters produced by decoding the specified number of bytes.</summary>
		/// <returns>The maximum number of characters produced by decoding the specified number of bytes.</returns>
		/// <param name="byteCount">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is less than zero. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D3F RID: 7487
		public abstract int GetMaxCharCount(int byteCount);

		/// <summary>When overridden in a derived class, decodes all the bytes in the specified byte array into a string.</summary>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D40 RID: 7488 RVA: 0x0006DB04 File Offset: 0x0006BD04
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("Array cannot be null."));
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		/// <summary>When overridden in a derived class, decodes a sequence of bytes from the specified byte array into a string.</summary>
		/// <returns>A string that contains the results of decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode. </param>
		/// <param name="index">The index of the first byte to decode. </param>
		/// <param name="count">The number of bytes to decode. </param>
		/// <exception cref="T:System.ArgumentException">The byte array contains invalid Unicode code points.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bytes" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or- <paramref name="index" /> and <paramref name="count" /> do not denote a valid range in <paramref name="bytes" />. </exception>
		/// <exception cref="T:System.Text.DecoderFallbackException">A fallback occurred (see Character Encoding in the .NET Framework for complete explanation)-and-<see cref="P:System.Text.Encoding.DecoderFallback" /> is set to <see cref="T:System.Text.DecoderExceptionFallback" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D41 RID: 7489 RVA: 0x0006DB29 File Offset: 0x0006BD29
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		/// <summary>Gets an encoding for the UTF-16 format using the little endian byte order.</summary>
		/// <returns>An encoding for the UTF-16 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0006DB39 File Offset: 0x0006BD39
		public static Encoding Unicode
		{
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
				}
				return Encoding.unicodeEncoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-16 format that uses the big endian byte order.</summary>
		/// <returns>An encoding object for the UTF-16 format that uses the big endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0006DB59 File Offset: 0x0006BD59
		public static Encoding BigEndianUnicode
		{
			get
			{
				if (Encoding.bigEndianUnicode == null)
				{
					Encoding.bigEndianUnicode = new UnicodeEncoding(true, true);
				}
				return Encoding.bigEndianUnicode;
			}
		}

		/// <summary>Gets an encoding for the UTF-7 format.</summary>
		/// <returns>An encoding for the UTF-7 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0006DB79 File Offset: 0x0006BD79
		public static Encoding UTF7
		{
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					Encoding.utf7Encoding = new UTF7Encoding();
				}
				return Encoding.utf7Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-8 format.</summary>
		/// <returns>An encoding for the UTF-8 format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0006DB97 File Offset: 0x0006BD97
		public static Encoding UTF8
		{
			get
			{
				if (Encoding.utf8Encoding == null)
				{
					Encoding.utf8Encoding = new UTF8Encoding(true);
				}
				return Encoding.utf8Encoding;
			}
		}

		/// <summary>Gets an encoding for the UTF-32 format using the little endian byte order.</summary>
		/// <returns>An  encoding object for the UTF-32 format using the little endian byte order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0006DBB6 File Offset: 0x0006BDB6
		public static Encoding UTF32
		{
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					Encoding.utf32Encoding = new UTF32Encoding(false, true);
				}
				return Encoding.utf32Encoding;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current instance.</summary>
		/// <returns>true if <paramref name="value" /> is an instance of <see cref="T:System.Text.Encoding" /> and is equal to the current instance; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D47 RID: 7495 RVA: 0x0006DBD8 File Offset: 0x0006BDD8
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals(encoding.EncoderFallback)) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>The hash code for the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D48 RID: 7496 RVA: 0x0006DC25 File Offset: 0x0006BE25
		public override int GetHashCode()
		{
			return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0006DC45 File Offset: 0x0006BE45
		internal virtual char[] GetBestFitUnicodeToBytesData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0006DC45 File Offset: 0x0006BE45
		internal virtual char[] GetBestFitBytesToUnicodeData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0006DC4C File Offset: 0x0006BE4C
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output byte buffer is too small to contain the encoded data, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.EncoderFallback.GetType()
			}), "bytes");
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x0006DC7F File Offset: 0x0006BE7F
		[SecurityCritical]
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder.m_throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0006DCB3 File Offset: 0x0006BEB3
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("The output char buffer is too small to contain the decoded characters, encoding '{0}' fallback '{1}'.", new object[]
			{
				this.EncodingName,
				this.DecoderFallback.GetType()
			}), "chars");
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0006DCE6 File Offset: 0x0006BEE6
		[SecurityCritical]
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder.m_throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x04000FF9 RID: 4089
		private static volatile Encoding defaultEncoding;

		// Token: 0x04000FFA RID: 4090
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04000FFB RID: 4091
		private static volatile Encoding bigEndianUnicode;

		// Token: 0x04000FFC RID: 4092
		private static volatile Encoding utf7Encoding;

		// Token: 0x04000FFD RID: 4093
		private static volatile Encoding utf8Encoding;

		// Token: 0x04000FFE RID: 4094
		private static volatile Encoding utf32Encoding;

		// Token: 0x04000FFF RID: 4095
		private static volatile Encoding asciiEncoding;

		// Token: 0x04001000 RID: 4096
		private static volatile Encoding latin1Encoding;

		// Token: 0x04001001 RID: 4097
		private static volatile Hashtable encodings;

		// Token: 0x04001002 RID: 4098
		private const int MIMECONTF_MAILNEWS = 1;

		// Token: 0x04001003 RID: 4099
		private const int MIMECONTF_BROWSER = 2;

		// Token: 0x04001004 RID: 4100
		private const int MIMECONTF_SAVABLE_MAILNEWS = 256;

		// Token: 0x04001005 RID: 4101
		private const int MIMECONTF_SAVABLE_BROWSER = 512;

		// Token: 0x04001006 RID: 4102
		private const int CodePageDefault = 0;

		// Token: 0x04001007 RID: 4103
		private const int CodePageNoOEM = 1;

		// Token: 0x04001008 RID: 4104
		private const int CodePageNoMac = 2;

		// Token: 0x04001009 RID: 4105
		private const int CodePageNoThread = 3;

		// Token: 0x0400100A RID: 4106
		private const int CodePageNoSymbol = 42;

		// Token: 0x0400100B RID: 4107
		private const int CodePageUnicode = 1200;

		// Token: 0x0400100C RID: 4108
		private const int CodePageBigEndian = 1201;

		// Token: 0x0400100D RID: 4109
		private const int CodePageWindows1252 = 1252;

		// Token: 0x0400100E RID: 4110
		private const int CodePageMacGB2312 = 10008;

		// Token: 0x0400100F RID: 4111
		private const int CodePageGB2312 = 20936;

		// Token: 0x04001010 RID: 4112
		private const int CodePageMacKorean = 10003;

		// Token: 0x04001011 RID: 4113
		private const int CodePageDLLKorean = 20949;

		// Token: 0x04001012 RID: 4114
		private const int ISO2022JP = 50220;

		// Token: 0x04001013 RID: 4115
		private const int ISO2022JPESC = 50221;

		// Token: 0x04001014 RID: 4116
		private const int ISO2022JPSISO = 50222;

		// Token: 0x04001015 RID: 4117
		private const int ISOKorean = 50225;

		// Token: 0x04001016 RID: 4118
		private const int ISOSimplifiedCN = 50227;

		// Token: 0x04001017 RID: 4119
		private const int EUCJP = 51932;

		// Token: 0x04001018 RID: 4120
		private const int ChineseHZ = 52936;

		// Token: 0x04001019 RID: 4121
		private const int DuplicateEUCCN = 51936;

		// Token: 0x0400101A RID: 4122
		private const int EUCCN = 936;

		// Token: 0x0400101B RID: 4123
		private const int EUCKR = 51949;

		// Token: 0x0400101C RID: 4124
		internal const int CodePageASCII = 20127;

		// Token: 0x0400101D RID: 4125
		internal const int ISO_8859_1 = 28591;

		// Token: 0x0400101E RID: 4126
		private const int ISCIIAssemese = 57006;

		// Token: 0x0400101F RID: 4127
		private const int ISCIIBengali = 57003;

		// Token: 0x04001020 RID: 4128
		private const int ISCIIDevanagari = 57002;

		// Token: 0x04001021 RID: 4129
		private const int ISCIIGujarathi = 57010;

		// Token: 0x04001022 RID: 4130
		private const int ISCIIKannada = 57008;

		// Token: 0x04001023 RID: 4131
		private const int ISCIIMalayalam = 57009;

		// Token: 0x04001024 RID: 4132
		private const int ISCIIOriya = 57007;

		// Token: 0x04001025 RID: 4133
		private const int ISCIIPanjabi = 57011;

		// Token: 0x04001026 RID: 4134
		private const int ISCIITamil = 57004;

		// Token: 0x04001027 RID: 4135
		private const int ISCIITelugu = 57005;

		// Token: 0x04001028 RID: 4136
		private const int GB18030 = 54936;

		// Token: 0x04001029 RID: 4137
		private const int ISO_8859_8I = 38598;

		// Token: 0x0400102A RID: 4138
		private const int ISO_8859_8_Visual = 28598;

		// Token: 0x0400102B RID: 4139
		private const int ENC50229 = 50229;

		// Token: 0x0400102C RID: 4140
		private const int CodePageUTF7 = 65000;

		// Token: 0x0400102D RID: 4141
		private const int CodePageUTF8 = 65001;

		// Token: 0x0400102E RID: 4142
		private const int CodePageUTF32 = 12000;

		// Token: 0x0400102F RID: 4143
		private const int CodePageUTF32BE = 12001;

		// Token: 0x04001030 RID: 4144
		internal int m_codePage;

		// Token: 0x04001031 RID: 4145
		internal CodePageDataItem dataItem;

		// Token: 0x04001032 RID: 4146
		[NonSerialized]
		internal bool m_deserializedFromEverett;

		// Token: 0x04001033 RID: 4147
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001034 RID: 4148
		[OptionalField(VersionAdded = 2)]
		internal EncoderFallback encoderFallback;

		// Token: 0x04001035 RID: 4149
		[OptionalField(VersionAdded = 2)]
		internal DecoderFallback decoderFallback;

		// Token: 0x04001036 RID: 4150
		private static object s_InternalSyncObject;

		// Token: 0x0200027D RID: 637
		[Serializable]
		internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
		{
			// Token: 0x06001D4F RID: 7503 RVA: 0x0006DD1A File Offset: 0x0006BF1A
			public DefaultEncoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06001D50 RID: 7504 RVA: 0x0006DD30 File Offset: 0x0006BF30
			internal DefaultEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (EncoderFallback)info.GetValue("m_fallback", typeof(EncoderFallback));
					this.charLeftOver = (char)info.GetValue("charLeftOver", typeof(char));
				}
				catch (SerializationException)
				{
				}
			}

			// Token: 0x06001D51 RID: 7505 RVA: 0x0006DDC8 File Offset: 0x0006BFC8
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Encoder encoder = this.m_encoding.GetEncoder();
				if (this.m_fallback != null)
				{
					encoder.m_fallback = this.m_fallback;
				}
				if (this.charLeftOver != '\0')
				{
					EncoderNLS encoderNLS = encoder as EncoderNLS;
					if (encoderNLS != null)
					{
						encoderNLS.charLeftOver = this.charLeftOver;
					}
				}
				return encoder;
			}

			// Token: 0x06001D52 RID: 7506 RVA: 0x0006DE1E File Offset: 0x0006C01E
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06001D53 RID: 7507 RVA: 0x0006DE3F File Offset: 0x0006C03F
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06001D54 RID: 7508 RVA: 0x0006DE4F File Offset: 0x0006C04F
			[SecurityCritical]
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, count);
			}

			// Token: 0x06001D55 RID: 7509 RVA: 0x0006DE5E File Offset: 0x0006C05E
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x06001D56 RID: 7510 RVA: 0x0006DE72 File Offset: 0x0006C072
			[SecurityCritical]
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x04001037 RID: 4151
			private Encoding m_encoding;

			// Token: 0x04001038 RID: 4152
			[NonSerialized]
			private bool m_hasInitializedEncoding;

			// Token: 0x04001039 RID: 4153
			[NonSerialized]
			internal char charLeftOver;
		}

		// Token: 0x0200027E RID: 638
		[Serializable]
		internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
		{
			// Token: 0x06001D57 RID: 7511 RVA: 0x0006DE84 File Offset: 0x0006C084
			public DefaultDecoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06001D58 RID: 7512 RVA: 0x0006DE9C File Offset: 0x0006C09C
			internal DefaultDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this.m_fallback = null;
				}
			}

			// Token: 0x06001D59 RID: 7513 RVA: 0x0006DF1C File Offset: 0x0006C11C
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Decoder decoder = this.m_encoding.GetDecoder();
				if (this.m_fallback != null)
				{
					decoder.m_fallback = this.m_fallback;
				}
				return decoder;
			}

			// Token: 0x06001D5A RID: 7514 RVA: 0x0006DF54 File Offset: 0x0006C154
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06001D5B RID: 7515 RVA: 0x0006B37F File Offset: 0x0006957F
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x06001D5C RID: 7516 RVA: 0x0006DF75 File Offset: 0x0006C175
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x06001D5D RID: 7517 RVA: 0x0006DF85 File Offset: 0x0006C185
			[SecurityCritical]
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, count);
			}

			// Token: 0x06001D5E RID: 7518 RVA: 0x0006B484 File Offset: 0x00069684
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x06001D5F RID: 7519 RVA: 0x0006DF94 File Offset: 0x0006C194
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x06001D60 RID: 7520 RVA: 0x0006DFA8 File Offset: 0x0006C1A8
			[SecurityCritical]
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x0400103A RID: 4154
			private Encoding m_encoding;

			// Token: 0x0400103B RID: 4155
			[NonSerialized]
			private bool m_hasInitializedEncoding;
		}

		// Token: 0x0200027F RID: 639
		internal class EncodingCharBuffer
		{
			// Token: 0x06001D61 RID: 7521 RVA: 0x0006DFBC File Offset: 0x0006C1BC
			[SecurityCritical]
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this.enc = enc;
				this.decoder = decoder;
				this.chars = charStart;
				this.charStart = charStart;
				this.charEnd = charStart + charCount;
				this.byteStart = byteStart;
				this.bytes = byteStart;
				this.byteEnd = byteStart + byteCount;
				if (this.decoder == null)
				{
					this.fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.decoder.FallbackBuffer;
				}
				this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
			}

			// Token: 0x06001D62 RID: 7522 RVA: 0x0006E058 File Offset: 0x0006C258
			[SecurityCritical]
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this.chars != null)
				{
					if (this.chars >= this.charEnd)
					{
						this.bytes -= numBytes;
						this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
						return false;
					}
					char* ptr = this.chars;
					this.chars = ptr + 1;
					*ptr = ch;
				}
				this.charCountResult++;
				return true;
			}

			// Token: 0x06001D63 RID: 7523 RVA: 0x0006E0D1 File Offset: 0x0006C2D1
			[SecurityCritical]
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x06001D64 RID: 7524 RVA: 0x0006E0DC File Offset: 0x0006C2DC
			[SecurityCritical]
			internal bool AddChar(char ch1, char ch2, int numBytes)
			{
				if (this.chars >= this.charEnd - 1)
				{
					this.bytes -= numBytes;
					this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
					return false;
				}
				return this.AddChar(ch1, numBytes) && this.AddChar(ch2, numBytes);
			}

			// Token: 0x06001D65 RID: 7525 RVA: 0x0006E13F File Offset: 0x0006C33F
			[SecurityCritical]
			internal void AdjustBytes(int count)
			{
				this.bytes += count;
			}

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0006E14F File Offset: 0x0006C34F
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.bytes < this.byteEnd;
				}
			}

			// Token: 0x06001D67 RID: 7527 RVA: 0x0006E15F File Offset: 0x0006C35F
			[SecurityCritical]
			internal bool EvenMoreData(int count)
			{
				return this.bytes == this.byteEnd - count;
			}

			// Token: 0x06001D68 RID: 7528 RVA: 0x0006E174 File Offset: 0x0006C374
			[SecurityCritical]
			internal unsafe byte GetNextByte()
			{
				if (this.bytes >= this.byteEnd)
				{
					return 0;
				}
				byte* ptr = this.bytes;
				this.bytes = ptr + 1;
				return *ptr;
			}

			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0006E1A3 File Offset: 0x0006C3A3
			internal int BytesUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.bytes - this.byteStart));
				}
			}

			// Token: 0x06001D6A RID: 7530 RVA: 0x0006E1B8 File Offset: 0x0006C3B8
			[SecurityCritical]
			internal bool Fallback(byte fallbackByte)
			{
				byte[] array = new byte[] { fallbackByte };
				return this.Fallback(array);
			}

			// Token: 0x06001D6B RID: 7531 RVA: 0x0006E1D8 File Offset: 0x0006C3D8
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2)
			{
				byte[] array = new byte[] { byte1, byte2 };
				return this.Fallback(array);
			}

			// Token: 0x06001D6C RID: 7532 RVA: 0x0006E1FC File Offset: 0x0006C3FC
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
			{
				byte[] array = new byte[] { byte1, byte2, byte3, byte4 };
				return this.Fallback(array);
			}

			// Token: 0x06001D6D RID: 7533 RVA: 0x0006E228 File Offset: 0x0006C428
			[SecurityCritical]
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this.chars != null)
				{
					char* ptr = this.chars;
					if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
					{
						this.bytes -= byteBuffer.Length;
						this.fallbackBuffer.InternalReset();
						this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
						return false;
					}
					this.charCountResult += (int)((long)(this.chars - ptr));
				}
				else
				{
					this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
				}
				return true;
			}

			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0006E2D7 File Offset: 0x0006C4D7
			internal int Count
			{
				get
				{
					return this.charCountResult;
				}
			}

			// Token: 0x0400103C RID: 4156
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x0400103D RID: 4157
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x0400103E RID: 4158
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x0400103F RID: 4159
			private int charCountResult;

			// Token: 0x04001040 RID: 4160
			private Encoding enc;

			// Token: 0x04001041 RID: 4161
			private DecoderNLS decoder;

			// Token: 0x04001042 RID: 4162
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x04001043 RID: 4163
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x04001044 RID: 4164
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x04001045 RID: 4165
			private DecoderFallbackBuffer fallbackBuffer;
		}

		// Token: 0x02000280 RID: 640
		internal class EncodingByteBuffer
		{
			// Token: 0x06001D6F RID: 7535 RVA: 0x0006E2E0 File Offset: 0x0006C4E0
			[SecurityCritical]
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this.enc = inEncoding;
				this.encoder = inEncoder;
				this.charStart = inCharStart;
				this.chars = inCharStart;
				this.charEnd = inCharStart + inCharCount;
				this.bytes = inByteStart;
				this.byteStart = inByteStart;
				this.byteEnd = inByteStart + inByteCount;
				if (this.encoder == null)
				{
					this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.encoder.FallbackBuffer;
					if (this.encoder.m_throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.", new object[]
						{
							this.encoder.Encoding.EncodingName,
							this.encoder.Fallback.GetType()
						}));
					}
				}
				this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, this.bytes != null);
			}

			// Token: 0x06001D70 RID: 7536 RVA: 0x0006E3F8 File Offset: 0x0006C5F8
			[SecurityCritical]
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this.bytes != null)
				{
					if (this.bytes >= this.byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* ptr = this.bytes;
					this.bytes = ptr + 1;
					*ptr = b;
				}
				this.byteCountResult++;
				return true;
			}

			// Token: 0x06001D71 RID: 7537 RVA: 0x0006E44A File Offset: 0x0006C64A
			[SecurityCritical]
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x06001D72 RID: 7538 RVA: 0x0006E454 File Offset: 0x0006C654
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x06001D73 RID: 7539 RVA: 0x0006E45F File Offset: 0x0006C65F
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x06001D74 RID: 7540 RVA: 0x0006E477 File Offset: 0x0006C677
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3)
			{
				return this.AddByte(b1, b2, b3, 0);
			}

			// Token: 0x06001D75 RID: 7541 RVA: 0x0006E483 File Offset: 0x0006C683
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
			{
				return this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected) && this.AddByte(b3, moreBytesExpected);
			}

			// Token: 0x06001D76 RID: 7542 RVA: 0x0006E4AA File Offset: 0x0006C6AA
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
			{
				return this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1) && this.AddByte(b4, 0);
			}

			// Token: 0x06001D77 RID: 7543 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
			[SecurityCritical]
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this.chars != this.charStart)
				{
					this.chars--;
				}
				if (bThrow)
				{
					this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
				}
			}

			// Token: 0x06001D78 RID: 7544 RVA: 0x0006E53E File Offset: 0x0006C73E
			[SecurityCritical]
			internal bool Fallback(char charFallback)
			{
				return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
			}

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0006E552 File Offset: 0x0006C752
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this.chars < this.charEnd;
				}
			}

			// Token: 0x06001D7A RID: 7546 RVA: 0x0006E574 File Offset: 0x0006C774
			[SecurityCritical]
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this.chars < this.charEnd)
				{
					char* ptr = this.chars;
					this.chars = ptr + 1;
					c = *ptr;
				}
				return c;
			}

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0006E5B2 File Offset: 0x0006C7B2
			internal int CharsUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.chars - this.charStart));
				}
			}

			// Token: 0x17000424 RID: 1060
			// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0006E5C5 File Offset: 0x0006C7C5
			internal int Count
			{
				get
				{
					return this.byteCountResult;
				}
			}

			// Token: 0x04001046 RID: 4166
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x04001047 RID: 4167
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x04001048 RID: 4168
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x04001049 RID: 4169
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x0400104A RID: 4170
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x0400104B RID: 4171
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x0400104C RID: 4172
			private int byteCountResult;

			// Token: 0x0400104D RID: 4173
			private Encoding enc;

			// Token: 0x0400104E RID: 4174
			private EncoderNLS encoder;

			// Token: 0x0400104F RID: 4175
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
