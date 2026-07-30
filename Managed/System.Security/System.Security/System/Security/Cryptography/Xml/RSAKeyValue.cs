using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;RSAKeyValue&gt; element of an XML signature.</summary>
	// Token: 0x0200006C RID: 108
	public class RSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with a new randomly generated <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		// Token: 0x060002B6 RID: 694 RVA: 0x00009FAB File Offset: 0x000081AB
		public RSAKeyValue()
		{
			this._key = RSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key. </param>
		// Token: 0x060002B7 RID: 695 RVA: 0x00009FBE File Offset: 0x000081BE
		public RSAKeyValue(RSA key)
		{
			this._key = key;
		}

		/// <summary>Gets or sets the instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</summary>
		/// <returns>The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00009FCD File Offset: 0x000081CD
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00009FD5 File Offset: 0x000081D5
		public RSA Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002BA RID: 698 RVA: 0x00009FE0 File Offset: 0x000081E0
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A004 File Offset: 0x00008204
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			RSAParameters rsaparameters = this._key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("RSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("Modulus", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Modulus)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Exponent", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Exponent)));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads an <see cref="T:System.Security.Cryptography.RSA" /> key clause from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.RSA" /> key clause. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.RSA" /> key clause XML element. </exception>
		// Token: 0x060002BC RID: 700 RVA: 0x0000A0AC File Offset: 0x000082AC
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException(string.Format("Root element must be {0} element in namespace {1}", "KeyValue", "http://www.w3.org/2000/09/xmldsig#"));
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode(string.Format("{0}:{1}", "dsig", "RSAKeyValue"), xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException(string.Format("{0} must contain child element {1}", "KeyValue", "RSAKeyValue"));
			}
			try
			{
				this.Key.ImportParameters(new RSAParameters
				{
					Modulus = Convert.FromBase64String(xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "Modulus"), xmlNamespaceManager).InnerText),
					Exponent = Convert.FromBase64String(xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "Exponent"), xmlNamespaceManager).InnerText)
				});
			}
			catch (Exception ex)
			{
				throw new CryptographicException(string.Format("An error occurred parsing the {0} and {1} elements", "Modulus", "Exponent"), ex);
			}
		}

		// Token: 0x04000175 RID: 373
		private RSA _key;

		// Token: 0x04000176 RID: 374
		private const string KeyValueElementName = "KeyValue";

		// Token: 0x04000177 RID: 375
		private const string RSAKeyValueElementName = "RSAKeyValue";

		// Token: 0x04000178 RID: 376
		private const string ModulusElementName = "Modulus";

		// Token: 0x04000179 RID: 377
		private const string ExponentElementName = "Exponent";
	}
}
