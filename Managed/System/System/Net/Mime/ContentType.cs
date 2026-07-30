using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Type header.</summary>
	// Token: 0x02000598 RID: 1432
	public class ContentType
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Net.Mime.ContentType" /> class. </summary>
		// Token: 0x06002CA0 RID: 11424 RVA: 0x000AFF2C File Offset: 0x000AE12C
		public ContentType()
			: this(ContentType.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentType" /> class using the specified string. </summary>
		/// <param name="contentType">A <see cref="T:System.String" />, for example, "text/plain; charset=us-ascii", that contains the MIME media type, subtype, and optional parameters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contentType" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is in a form that cannot be parsed.</exception>
		// Token: 0x06002CA1 RID: 11425 RVA: 0x000AFF3C File Offset: 0x000AE13C
		public ContentType(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(global::SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "contentType" }), "contentType");
			}
			this.isChanged = true;
			this.type = contentType;
			this.ParseValue();
		}

		/// <summary>Gets or sets the value of the boundary parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the boundary parameter.</returns>
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x000AFFA1 File Offset: 0x000AE1A1
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x000AFFB3 File Offset: 0x000AE1B3
		public string Boundary
		{
			get
			{
				return this.Parameters["boundary"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("boundary");
					return;
				}
				this.Parameters["boundary"] = value;
			}
		}

		/// <summary>Gets or sets the value of the charset parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the charset parameter.</returns>
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000AFFE7 File Offset: 0x000AE1E7
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x000AFFF9 File Offset: 0x000AE1F9
		public string CharSet
		{
			get
			{
				return this.Parameters["charset"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("charset");
					return;
				}
				this.Parameters["charset"] = value;
			}
		}

		/// <summary>Gets or sets the media type value included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the media type and subtype value. This value does not include the semicolon (;) separator that follows the subtype.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">The value specified for a set operation is in a form that cannot be parsed.</exception>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000B002D File Offset: 0x000AE22D
		// (set) Token: 0x06002CA7 RID: 11431 RVA: 0x000B0048 File Offset: 0x000AE248
		public string MediaType
		{
			get
			{
				return this.mediaType + "/" + this.subType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException(global::SR.GetString("This property cannot be set to an empty string."), "value");
				}
				int num = 0;
				this.mediaType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.mediaType.Length == 0 || num >= value.Length || value[num++] != '/')
				{
					throw new FormatException(global::SR.GetString("The specified media type is invalid."));
				}
				this.subType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.subType.Length == 0 || num < value.Length)
				{
					throw new FormatException(global::SR.GetString("The specified media type is invalid."));
				}
				this.isChanged = true;
				this.isPersisted = false;
			}
		}

		/// <summary>Gets or sets the value of the name parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the name parameter. </returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000B0110 File Offset: 0x000AE310
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x000B013E File Offset: 0x000AE33E
		public string Name
		{
			get
			{
				string text = this.Parameters["name"];
				if (MimeBasePart.DecodeEncoding(text) != null)
				{
					text = MimeBasePart.DecodeHeaderValue(text);
				}
				return text;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("name");
					return;
				}
				this.Parameters["name"] = value;
			}
		}

		/// <summary>Gets the dictionary that contains the parameters included in the Content-Type header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains name and value pairs.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000B0172 File Offset: 0x000AE372
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null && this.type == null)
				{
					this.parameters = new TrackingStringDictionary();
				}
				return this.parameters;
			}
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000B0195 File Offset: 0x000AE395
		internal void Set(string contentType, HeaderCollection headers)
		{
			this.type = contentType;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000B01BD File Offset: 0x000AE3BD
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000B01F0 File Offset: 0x000AE3F0
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Net.Mime.ContentType" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the current settings for this <see cref="T:System.Net.Mime.ContentType" />.</returns>
		// Token: 0x06002CAE RID: 11438 RVA: 0x000B0211 File Offset: 0x000AE411
		public override string ToString()
		{
			if (this.type == null || this.IsChanged)
			{
				this.type = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.type;
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000B0250 File Offset: 0x000AE450
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.mediaType);
			stringBuilder.Append('/');
			stringBuilder.Append(this.subType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentType.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentType.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000B0308 File Offset: 0x000AE508
		private static void EncodeToBuffer(string value, StringBuilder builder, bool allowUnicode)
		{
			Encoding encoding = MimeBasePart.DecodeEncoding(value);
			if (encoding != null)
			{
				builder.Append("\"" + value + "\"");
				return;
			}
			if ((allowUnicode && !MailBnfHelper.HasCROrLF(value)) || MimeBasePart.IsAscii(value, false))
			{
				MailBnfHelper.GetTokenOrQuotedString(value, builder, allowUnicode);
				return;
			}
			encoding = Encoding.GetEncoding("utf-8");
			builder.Append("\"" + MimeBasePart.EncodeHeaderValue(value, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding)) + "\"");
		}

		/// <summary>Determines whether the content-type header of the specified <see cref="T:System.Net.Mime.ContentType" /> object is equal to the content-type header of this object.</summary>
		/// <returns>true if the content-type headers are the same; otherwise false.</returns>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentType" /> object to compare with this object.</param>
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000AC92E File Offset: 0x000AAB2E
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentType" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000AFD0A File Offset: 0x000ADF0A
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000B0384 File Offset: 0x000AE584
		private void ParseValue()
		{
			int num = 0;
			Exception ex = null;
			this.parameters = new TrackingStringDictionary();
			try
			{
				this.mediaType = MailBnfHelper.ReadToken(this.type, ref num, null);
				if (this.mediaType == null || this.mediaType.Length == 0 || num >= this.type.Length || this.type[num++] != '/')
				{
					ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
				}
				if (ex == null)
				{
					this.subType = MailBnfHelper.ReadToken(this.type, ref num, null);
					if (this.subType == null || this.subType.Length == 0)
					{
						ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
					}
				}
				if (ex == null)
				{
					while (MailBnfHelper.SkipCFWS(this.type, ref num))
					{
						if (this.type[num++] != ';')
						{
							ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							break;
						}
						string text = MailBnfHelper.ReadParameterAttribute(this.type, ref num, null);
						if (text == null || text.Length == 0)
						{
							ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
							break;
						}
						if (num >= this.type.Length || this.type[num++] != '=')
						{
							ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
							break;
						}
						string text2;
						if (this.type[num] == '"')
						{
							text2 = MailBnfHelper.ReadQuotedString(this.type, ref num, null);
						}
						else
						{
							text2 = MailBnfHelper.ReadToken(this.type, ref num, null);
						}
						if (text2 == null)
						{
							ex = new FormatException(global::SR.GetString("The specified content type is invalid."));
							break;
						}
						this.parameters.Add(text, text2);
					}
				}
				this.parameters.IsChanged = false;
			}
			catch (FormatException)
			{
				throw new FormatException(global::SR.GetString("The specified content type is invalid."));
			}
			if (ex != null)
			{
				throw new FormatException(global::SR.GetString("The specified content type is invalid."));
			}
		}

		// Token: 0x040024F9 RID: 9465
		private string mediaType;

		// Token: 0x040024FA RID: 9466
		private string subType;

		// Token: 0x040024FB RID: 9467
		private bool isChanged;

		// Token: 0x040024FC RID: 9468
		private string type;

		// Token: 0x040024FD RID: 9469
		private bool isPersisted;

		// Token: 0x040024FE RID: 9470
		private TrackingStringDictionary parameters;

		// Token: 0x040024FF RID: 9471
		internal static readonly string Default = "application/octet-stream";
	}
}
