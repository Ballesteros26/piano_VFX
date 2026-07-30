using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Encapsulates the encryption algorithm used for XML encryption. </summary>
	// Token: 0x0200005B RID: 91
	public class EncryptionMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class. </summary>
		// Token: 0x0600023C RID: 572 RVA: 0x00009003 File Offset: 0x00007203
		public EncryptionMethod()
		{
			this._cachedXml = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class specifying an algorithm Uniform Resource Identifier (URI). </summary>
		/// <param name="algorithm">The Uniform Resource Identifier (URI) that describes the algorithm represented by an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class. </param>
		// Token: 0x0600023D RID: 573 RVA: 0x00009012 File Offset: 0x00007212
		public EncryptionMethod(string algorithm)
		{
			this._algorithm = algorithm;
			this._cachedXml = null;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009028 File Offset: 0x00007228
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Gets or sets the algorithm key size used for XML encryption. </summary>
		/// <returns>The algorithm key size, in bits, used for XML encryption.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionMethod.KeySize" /> property was set to a value that was less than 0.</exception>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009033 File Offset: 0x00007233
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000903B File Offset: 0x0000723B
		public int KeySize
		{
			get
			{
				return this._keySize;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "The key size should be a non negative integer.");
				}
				this._keySize = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption. </summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the algorithm to use for XML encryption.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000905F File Offset: 0x0000725F
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00009067 File Offset: 0x00007267
		public string KeyAlgorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> class.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000243 RID: 579 RVA: 0x00009078 File Offset: 0x00007278
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

		// Token: 0x06000244 RID: 580 RVA: 0x000090A8 File Offset: 0x000072A8
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this._algorithm))
			{
				xmlElement.SetAttribute("Algorithm", this._algorithm);
			}
			if (this._keySize > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("KeySize", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(this._keySize.ToString(null, null)));
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The key size expressed in the <paramref name="value" /> parameter was less than 0. </exception>
		// Token: 0x06000245 RID: 581 RVA: 0x00009124 File Offset: 0x00007324
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._algorithm = Utils.GetAttribute(value, "Algorithm", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:KeySize", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.KeySize = Convert.ToInt32(Utils.DiscardWhiteSpaces(xmlNode.InnerText), null);
			}
			this._cachedXml = value;
		}

		// Token: 0x04000161 RID: 353
		private XmlElement _cachedXml;

		// Token: 0x04000162 RID: 354
		private int _keySize;

		// Token: 0x04000163 RID: 355
		private string _algorithm;
	}
}
