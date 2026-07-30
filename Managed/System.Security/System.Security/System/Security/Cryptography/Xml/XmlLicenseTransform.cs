using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the license transform algorithm used to normalize XrML licenses for signatures.</summary>
	// Token: 0x02000084 RID: 132
	public class XmlLicenseTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> class. </summary>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000F904 File Offset: 0x0000DB04
		public XmlLicenseTransform()
		{
			base.Algorithm = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; you can pass only objects of one of these types to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000F954 File Offset: 0x0000DB54
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are valid outputs from the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlLicenseTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Gets or sets the decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000F964 File Offset: 0x0000DB64
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000F96C File Offset: 0x0000DB6C
		public IRelDecryptor Decryptor
		{
			get
			{
				return this._relDecryptor;
			}
			set
			{
				this._relDecryptor = value;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F978 File Offset: 0x0000DB78
		private void DecryptEncryptedGrants(XmlNodeList encryptedGrantList, IRelDecryptor decryptor)
		{
			int i = 0;
			int count = encryptedGrantList.Count;
			while (i < count)
			{
				XmlElement xmlElement = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:EncryptionMethod", this._namespaceManager) as XmlElement;
				XmlElement xmlElement2 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/dsig:KeyInfo", this._namespaceManager) as XmlElement;
				XmlElement xmlElement3 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:CipherData", this._namespaceManager) as XmlElement;
				if (xmlElement != null && xmlElement2 != null && xmlElement3 != null)
				{
					EncryptionMethod encryptionMethod = new EncryptionMethod();
					KeyInfo keyInfo = new KeyInfo();
					CipherData cipherData = new CipherData();
					encryptionMethod.LoadXml(xmlElement);
					keyInfo.LoadXml(xmlElement2);
					cipherData.LoadXml(xmlElement3);
					MemoryStream memoryStream = null;
					Stream stream = null;
					StreamReader streamReader = null;
					try
					{
						memoryStream = new MemoryStream(cipherData.CipherValue);
						stream = this._relDecryptor.Decrypt(encryptionMethod, keyInfo, memoryStream);
						if (stream == null || stream.Length == 0L)
						{
							throw new CryptographicException("Unable to decrypt grant content.");
						}
						streamReader = new StreamReader(stream);
						string text = streamReader.ReadToEnd();
						encryptedGrantList[i].ParentNode.InnerXml = text;
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (stream != null)
						{
							stream.Close();
						}
						if (streamReader != null)
						{
							streamReader.Close();
						}
					}
				}
				i++;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object that are suitable to be included as subelements of an XMLDSIG &lt;Transform&gt; element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object in an XMLDSIG &lt;Transform&gt; element.</returns>
		// Token: 0x060003CE RID: 974 RVA: 0x00003BEE File Offset: 0x00001DEE
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x060003CF RID: 975 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		public override object GetOutput()
		{
			return this._license;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlDocument" /> is the only valid type for this parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.</exception>
		// Token: 0x060003D0 RID: 976 RVA: 0x0000FAFC File Offset: 0x0000DCFC
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlDocument) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return this.GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a &lt;Transform&gt; element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates the transform to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. </param>
		// Token: 0x060003D1 RID: 977 RVA: 0x00004938 File Offset: 0x00002B38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. The type of the input object must be <see cref="T:System.Xml.XmlDocument" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The context was not set before this transform was invoked.-or-The &lt;issuer&gt; element was not set before this transform was invoked.-or-The &lt;license&gt; element was not set before this transform was invoked.-or-The <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.Decryptor" /> property was not set before this transform was invoked.</exception>
		// Token: 0x060003D2 RID: 978 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public override void LoadInput(object obj)
		{
			if (base.Context == null)
			{
				throw new CryptographicException("Null Context property encountered.");
			}
			this._license = new XmlDocument();
			this._license.PreserveWhitespace = true;
			this._namespaceManager = new XmlNamespaceManager(this._license.NameTable);
			this._namespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			this._namespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._namespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			XmlElement xmlElement = base.Context.SelectSingleNode("ancestor-or-self::r:issuer[1]", this._namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException("Issuer node is required.");
			}
			XmlNode xmlNode = xmlElement.SelectSingleNode("descendant-or-self::dsig:Signature[1]", this._namespaceManager) as XmlElement;
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
			XmlElement xmlElement2 = xmlElement.SelectSingleNode("ancestor-or-self::r:license[1]", this._namespaceManager) as XmlElement;
			if (xmlElement2 == null)
			{
				throw new CryptographicException("License node is required.");
			}
			XmlNodeList xmlNodeList = xmlElement2.SelectNodes("descendant-or-self::r:license[1]/r:issuer", this._namespaceManager);
			int i = 0;
			int count = xmlNodeList.Count;
			while (i < count)
			{
				if (xmlNodeList[i] != xmlElement && xmlNodeList[i].LocalName == "issuer" && xmlNodeList[i].NamespaceURI == "urn:mpeg:mpeg21:2003:01-REL-R-NS")
				{
					xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
				}
				i++;
			}
			XmlNodeList xmlNodeList2 = xmlElement2.SelectNodes("/r:license/r:grant/r:encryptedGrant", this._namespaceManager);
			if (xmlNodeList2.Count > 0)
			{
				if (this._relDecryptor == null)
				{
					throw new CryptographicException("IRelDecryptor is required.");
				}
				this.DecryptEncryptedGrants(xmlNodeList2, this._relDecryptor);
			}
			this._license.InnerXml = xmlElement2.OuterXml;
		}

		// Token: 0x040001DA RID: 474
		private Type[] _inputTypes = new Type[] { typeof(XmlDocument) };

		// Token: 0x040001DB RID: 475
		private Type[] _outputTypes = new Type[] { typeof(XmlDocument) };

		// Token: 0x040001DC RID: 476
		private XmlNamespaceManager _namespaceManager;

		// Token: 0x040001DD RID: 477
		private XmlDocument _license;

		// Token: 0x040001DE RID: 478
		private IRelDecryptor _relDecryptor;

		// Token: 0x040001DF RID: 479
		private const string ElementIssuer = "issuer";

		// Token: 0x040001E0 RID: 480
		private const string NamespaceUriCore = "urn:mpeg:mpeg21:2003:01-REL-R-NS";
	}
}
