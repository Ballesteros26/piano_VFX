using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which the classes <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> and <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> derive.</summary>
	// Token: 0x02000059 RID: 89
	public abstract class EncryptedType
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00007EDA File Offset: 0x000060DA
		internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Gets or sets the Id attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string of the Id attribute of the &lt;EncryptedType&gt; element.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00007EE5 File Offset: 0x000060E5
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00007EED File Offset: 0x000060ED
		public virtual string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the Type attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the text form of the encrypted data.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00007EFD File Offset: 0x000060FD
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00007F05 File Offset: 0x00006105
		public virtual string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the MimeType attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the media type of the encrypted data.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00007F15 File Offset: 0x00006115
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00007F1D File Offset: 0x0000611D
		public virtual string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				this._mimeType = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the Encoding attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the encoding of the encrypted data.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007F2D File Offset: 0x0000612D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00007F35 File Offset: 0x00006135
		public virtual string Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets of sets the &lt;KeyInfo&gt; element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00007F45 File Offset: 0x00006145
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00007F60 File Offset: 0x00006160
		public KeyInfo KeyInfo
		{
			get
			{
				if (this._keyInfo == null)
				{
					this._keyInfo = new KeyInfo();
				}
				return this._keyInfo;
			}
			set
			{
				this._keyInfo = value;
			}
		}

		/// <summary>Gets or sets the &lt;EncryptionMethod&gt; element for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that represents the &lt;EncryptionMethod&gt; element.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00007F69 File Offset: 0x00006169
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00007F71 File Offset: 0x00006171
		public virtual EncryptionMethod EncryptionMethod
		{
			get
			{
				return this._encryptionMethod;
			}
			set
			{
				this._encryptionMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the &lt;EncryptionProperties&gt; element in XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007F81 File Offset: 0x00006181
		public virtual EncryptionPropertyCollection EncryptionProperties
		{
			get
			{
				if (this._props == null)
				{
					this._props = new EncryptionPropertyCollection();
				}
				return this._props;
			}
		}

		/// <summary>Adds an &lt;EncryptionProperty&gt; child element to the &lt;EncryptedProperties&gt; element in the current <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object in XML encryption.</summary>
		/// <param name="ep">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</param>
		// Token: 0x06000211 RID: 529 RVA: 0x00007F9C File Offset: 0x0000619C
		public void AddProperty(EncryptionProperty ep)
		{
			this.EncryptionProperties.Add(ep);
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> value for an instance of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedType.CipherData" /> property was set to null.</exception>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007FAB File Offset: 0x000061AB
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00007FC6 File Offset: 0x000061C6
		public virtual CipherData CipherData
		{
			get
			{
				if (this._cipherData == null)
				{
					this._cipherData = new CipherData();
				}
				return this._cipherData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._cipherData = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Loads XML information into the &lt;EncryptedType&gt; element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use in the &lt;EncryptedType&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is null.</exception>
		// Token: 0x06000214 RID: 532
		public abstract void LoadXml(XmlElement value);

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the &lt;EncryptedType&gt; element in XML encryption.</returns>
		// Token: 0x06000215 RID: 533
		public abstract XmlElement GetXml();

		// Token: 0x0400013C RID: 316
		private string _id;

		// Token: 0x0400013D RID: 317
		private string _type;

		// Token: 0x0400013E RID: 318
		private string _mimeType;

		// Token: 0x0400013F RID: 319
		private string _encoding;

		// Token: 0x04000140 RID: 320
		private EncryptionMethod _encryptionMethod;

		// Token: 0x04000141 RID: 321
		private CipherData _cipherData;

		// Token: 0x04000142 RID: 322
		private EncryptionPropertyCollection _props;

		// Token: 0x04000143 RID: 323
		private KeyInfo _keyInfo;

		// Token: 0x04000144 RID: 324
		internal XmlElement _cachedXml;
	}
}
