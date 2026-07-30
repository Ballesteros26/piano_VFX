using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;CipherData&gt; element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200004D RID: 77
	public sealed class CipherData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class.</summary>
		// Token: 0x060001B2 RID: 434 RVA: 0x00002050 File Offset: 0x00000250
		public CipherData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a byte array as the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> value.</summary>
		/// <param name="cipherValue">The encrypted data to use for the &lt;CipherValue&gt; element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x060001B3 RID: 435 RVA: 0x000062AC File Offset: 0x000044AC
		public CipherData(byte[] cipherValue)
		{
			this.CipherValue = cipherValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <param name="cipherReference">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x060001B4 RID: 436 RVA: 0x000062BB File Offset: 0x000044BB
		public CipherData(CipherReference cipherReference)
		{
			this.CipherReference = cipherReference;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000062CA File Offset: 0x000044CA
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Gets or sets the &lt;CipherReference&gt; element.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" />  property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" />  property was set more than once.</exception>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000062D5 File Offset: 0x000044D5
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000062DD File Offset: 0x000044DD
		public CipherReference CipherReference
		{
			get
			{
				return this._cipherReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherValue != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherReference = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the &lt;CipherValue&gt; element.</summary>
		/// <returns>A byte array that represents the &lt;CipherValue&gt; element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" />  property was set to null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" />  property was set more than once.</exception>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000630E File Offset: 0x0000450E
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006316 File Offset: 0x00004516
		public byte[] CipherValue
		{
			get
			{
				return this._cipherValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherReference != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherValue = (byte[])value.Clone();
				this._cachedXml = null;
			}
		}

		/// <summary>Gets the XML values for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlElement" /> object that represents the XML information for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001BA RID: 442 RVA: 0x00006354 File Offset: 0x00004554
		public XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this._cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006384 File Offset: 0x00004584
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			if (this.CipherValue != null)
			{
				XmlElement xmlElement2 = document.CreateElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(this.CipherValue)));
				xmlElement.AppendChild(xmlElement2);
			}
			else
			{
				if (this.CipherReference == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				xmlElement.AppendChild(this.CipherReference.GetXml(document));
			}
			return xmlElement;
		}

		/// <summary>Loads XML data from an <see cref="T:System.Xml.XmlElement" /> into a <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> that represents the XML data to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are null.</exception>
		// Token: 0x060001BC RID: 444 RVA: 0x00006404 File Offset: 0x00004604
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:CipherValue", xmlNamespaceManager);
			XmlNode xmlNode2 = value.SelectSingleNode("enc:CipherReference", xmlNamespaceManager);
			if (xmlNode != null)
			{
				if (xmlNode2 != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode.InnerText));
			}
			else
			{
				if (xmlNode2 == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherReference = new CipherReference();
				this._cipherReference.LoadXml((XmlElement)xmlNode2);
			}
			this._cachedXml = value;
		}

		// Token: 0x0400011D RID: 285
		private XmlElement _cachedXml;

		// Token: 0x0400011E RID: 286
		private CipherReference _cipherReference;

		// Token: 0x0400011F RID: 287
		private byte[] _cipherValue;
	}
}
