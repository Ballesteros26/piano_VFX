using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Disposition header.</summary>
	// Token: 0x02000596 RID: 1430
	public class ContentDisposition
	{
		// Token: 0x06002C80 RID: 11392 RVA: 0x000AF82C File Offset: 0x000ADA2C
		static ContentDisposition()
		{
			ContentDisposition.validators.Add("creation-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("modification-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("read-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("size", ContentDisposition.longParser);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with a <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> of <see cref="F:System.Net.Mime.DispositionTypeNames.Attachment" />. </summary>
		// Token: 0x06002C81 RID: 11393 RVA: 0x000AF8BD File Offset: 0x000ADABD
		public ContentDisposition()
		{
			this.isChanged = true;
			this.dispositionType = "attachment";
			this.disposition = this.dispositionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with the specified disposition information.</summary>
		/// <param name="disposition">A <see cref="T:System.Net.Mime.DispositionTypeNames" /> value that contains the disposition.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="disposition" /> is null or equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x06002C82 RID: 11394 RVA: 0x000AF8E3 File Offset: 0x000ADAE3
		public ContentDisposition(string disposition)
		{
			if (disposition == null)
			{
				throw new ArgumentNullException("disposition");
			}
			this.isChanged = true;
			this.disposition = disposition;
			this.ParseValue();
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000AF910 File Offset: 0x000ADB10
		internal DateTime GetDateParameter(string parameterName)
		{
			SmtpDateTime smtpDateTime = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet(parameterName) as SmtpDateTime;
			if (smtpDateTime == null)
			{
				return DateTime.MinValue;
			}
			return smtpDateTime.Date;
		}

		/// <summary>Gets or sets the disposition type for an e-mail attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the disposition type. The value is not restricted but is typically one of the <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000AF943 File Offset: 0x000ADB43
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000AF94B File Offset: 0x000ADB4B
		public string DispositionType
		{
			get
			{
				return this.dispositionType;
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
				this.isChanged = true;
				this.dispositionType = value;
			}
		}

		/// <summary>Gets the parameters included in the Content-Disposition header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains parameter name/value pairs.</returns>
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000AF98B File Offset: 0x000ADB8B
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the suggested file name for an e-mail attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the file name. </returns>
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x000AF9AB File Offset: 0x000ADBAB
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000AF9BD File Offset: 0x000ADBBD
		public string FileName
		{
			get
			{
				return this.Parameters["filename"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Parameters.Remove("filename");
					return;
				}
				this.Parameters["filename"] = value;
			}
		}

		/// <summary>Gets or sets the creation date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file creation date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000AF9E9 File Offset: 0x000ADBE9
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x000AF9F8 File Offset: 0x000ADBF8
		public DateTime CreationDate
		{
			get
			{
				return this.GetDateParameter("creation-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("creation-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the modification date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file modification date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000AFA22 File Offset: 0x000ADC22
		// (set) Token: 0x06002C8C RID: 11404 RVA: 0x000AFA30 File Offset: 0x000ADC30
		public DateTime ModificationDate
		{
			get
			{
				return this.GetDateParameter("modification-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("modification-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines the disposition type (Inline or Attachment) for an e-mail attachment.</summary>
		/// <returns>true if content in the attachment is presented inline as part of the e-mail body; otherwise, false. </returns>
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000AFA5A File Offset: 0x000ADC5A
		// (set) Token: 0x06002C8E RID: 11406 RVA: 0x000AFA6C File Offset: 0x000ADC6C
		public bool Inline
		{
			get
			{
				return this.dispositionType == "inline";
			}
			set
			{
				this.isChanged = true;
				if (value)
				{
					this.dispositionType = "inline";
					return;
				}
				this.dispositionType = "attachment";
			}
		}

		/// <summary>Gets or sets the read date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file read date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002C8F RID: 11407 RVA: 0x000AFA8F File Offset: 0x000ADC8F
		// (set) Token: 0x06002C90 RID: 11408 RVA: 0x000AFA9C File Offset: 0x000ADC9C
		public DateTime ReadDate
		{
			get
			{
				return this.GetDateParameter("read-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("read-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the size of a file attachment.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of bytes in the file attachment. The default value is -1, which indicates that the file size is unknown.</returns>
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000AFAC8 File Offset: 0x000ADCC8
		// (set) Token: 0x06002C92 RID: 11410 RVA: 0x000AFAF7 File Offset: 0x000ADCF7
		public long Size
		{
			get
			{
				object obj = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet("size");
				if (obj == null)
				{
					return -1L;
				}
				return (long)obj;
			}
			set
			{
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("size", value);
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000AFB14 File Offset: 0x000ADD14
		internal void Set(string contentDisposition, HeaderCollection headers)
		{
			this.disposition = contentDisposition;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000AFB3C File Offset: 0x000ADD3C
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000AFB6F File Offset: 0x000ADD6F
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the property values for this instance.</returns>
		// Token: 0x06002C96 RID: 11414 RVA: 0x000AFB90 File Offset: 0x000ADD90
		public override string ToString()
		{
			if (this.disposition == null || this.isChanged || (this.parameters != null && this.parameters.IsChanged))
			{
				this.disposition = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.disposition;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.dispositionType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentDisposition.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentDisposition.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000AFC90 File Offset: 0x000ADE90
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

		/// <summary>Determines whether the content-disposition header of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object is equal to the content-disposition header of this object.</summary>
		/// <returns>true if the content-disposition headers are the same; otherwise false.</returns>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentDisposition" /> object to compare with this object.</param>
		// Token: 0x06002C99 RID: 11417 RVA: 0x000AC92E File Offset: 0x000AAB2E
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06002C9A RID: 11418 RVA: 0x000AFD0A File Offset: 0x000ADF0A
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000AFD1C File Offset: 0x000ADF1C
		private void ParseValue()
		{
			int num = 0;
			try
			{
				this.dispositionType = MailBnfHelper.ReadToken(this.disposition, ref num, null);
				if (string.IsNullOrEmpty(this.dispositionType))
				{
					throw new FormatException(global::SR.GetString("The mail header is malformed."));
				}
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				else
				{
					this.parameters.Clear();
				}
				while (MailBnfHelper.SkipCFWS(this.disposition, ref num))
				{
					if (this.disposition[num++] != ';')
					{
						throw new FormatException(global::SR.GetString("An invalid character was found in the mail header: '{0}'.", new object[] { this.disposition[num - 1] }));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						break;
					}
					string text = MailBnfHelper.ReadParameterAttribute(this.disposition, ref num, null);
					if (this.disposition[num++] != '=')
					{
						throw new FormatException(global::SR.GetString("The mail header is malformed."));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						throw new FormatException(global::SR.GetString("The specified content disposition is invalid."));
					}
					string text2;
					if (this.disposition[num] == '"')
					{
						text2 = MailBnfHelper.ReadQuotedString(this.disposition, ref num, null);
					}
					else
					{
						text2 = MailBnfHelper.ReadToken(this.disposition, ref num, null);
					}
					if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
					{
						throw new FormatException(global::SR.GetString("The specified content disposition is invalid."));
					}
					this.Parameters.Add(text, text2);
				}
			}
			catch (FormatException ex)
			{
				throw new FormatException(global::SR.GetString("The specified content disposition is invalid."), ex);
			}
			this.parameters.IsChanged = false;
		}

		// Token: 0x040024EB RID: 9451
		private string dispositionType;

		// Token: 0x040024EC RID: 9452
		private TrackingValidationObjectDictionary parameters;

		// Token: 0x040024ED RID: 9453
		private bool isChanged;

		// Token: 0x040024EE RID: 9454
		private bool isPersisted;

		// Token: 0x040024EF RID: 9455
		private string disposition;

		// Token: 0x040024F0 RID: 9456
		private const string creationDate = "creation-date";

		// Token: 0x040024F1 RID: 9457
		private const string readDate = "read-date";

		// Token: 0x040024F2 RID: 9458
		private const string modificationDate = "modification-date";

		// Token: 0x040024F3 RID: 9459
		private const string size = "size";

		// Token: 0x040024F4 RID: 9460
		private const string fileName = "filename";

		// Token: 0x040024F5 RID: 9461
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue dateParser = (object value) => new SmtpDateTime(value.ToString());

		// Token: 0x040024F6 RID: 9462
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue longParser = delegate(object value)
		{
			long num;
			if (!long.TryParse(value.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				throw new FormatException(global::SR.GetString("The specified content disposition is invalid."));
			}
			return num;
		};

		// Token: 0x040024F7 RID: 9463
		private static readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators = new Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue>();
	}
}
