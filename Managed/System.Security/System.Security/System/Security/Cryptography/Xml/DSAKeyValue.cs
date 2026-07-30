using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see cref="T:System.Security.Cryptography.DSA" /> private key of the &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000051 RID: 81
	public class DSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with a new, randomly-generated <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		// Token: 0x060001CA RID: 458 RVA: 0x00006B0E File Offset: 0x00004D0E
		public DSAKeyValue()
		{
			this._key = DSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of the <see cref="T:System.Security.Cryptography.DSA" /> class that holds the public key. </param>
		// Token: 0x060001CB RID: 459 RVA: 0x00006B21 File Offset: 0x00004D21
		public DSAKeyValue(DSA key)
		{
			this._key = key;
		}

		/// <summary>Gets or sets the key value represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>The public key represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006B30 File Offset: 0x00004D30
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00006B38 File Offset: 0x00004D38
		public DSA Key
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

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001CE RID: 462 RVA: 0x00006B44 File Offset: 0x00004D44
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006B68 File Offset: 0x00004D68
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			DSAParameters dsaparameters = this._key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("P", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.P)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Q", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Q)));
			xmlElement2.AppendChild(xmlElement4);
			XmlElement xmlElement5 = xmlDocument.CreateElement("G", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.G)));
			xmlElement2.AppendChild(xmlElement5);
			XmlElement xmlElement6 = xmlDocument.CreateElement("Y", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement6.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Y)));
			xmlElement2.AppendChild(xmlElement6);
			if (dsaparameters.J != null)
			{
				XmlElement xmlElement7 = xmlDocument.CreateElement("J", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.J)));
				xmlElement2.AppendChild(xmlElement7);
			}
			if (dsaparameters.Seed != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("Seed", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Seed)));
				xmlElement2.AppendChild(xmlElement8);
				XmlElement xmlElement9 = xmlDocument.CreateElement("PgenCounter", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement9.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter))));
				xmlElement2.AppendChild(xmlElement9);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> XML element. </exception>
		// Token: 0x060001D0 RID: 464 RVA: 0x00006D28 File Offset: 0x00004F28
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Name != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException(string.Format("Root element must be {0} element in namepsace {1}", "KeyValue", "http://www.w3.org/2000/09/xmldsig#"));
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode(string.Format("{0}:{1}", "dsig", "DSAKeyValue"), xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException(string.Format("{0} must contain child element {1}", "KeyValue", "DSAKeyValue"));
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "Y"), xmlNamespaceManager);
			if (xmlNode2 == null)
			{
				throw new CryptographicException(string.Format("{0} is missing", "Y"));
			}
			XmlNode xmlNode3 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "P"), xmlNamespaceManager);
			XmlNode xmlNode4 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "Q"), xmlNamespaceManager);
			if ((xmlNode3 == null && xmlNode4 != null) || (xmlNode3 != null && xmlNode4 == null))
			{
				throw new CryptographicException(string.Format("{0} and {1} can only occour in combination", "P", "Q"));
			}
			XmlNode xmlNode5 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "G"), xmlNamespaceManager);
			XmlNode xmlNode6 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "J"), xmlNamespaceManager);
			XmlNode xmlNode7 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "Seed"), xmlNamespaceManager);
			XmlNode xmlNode8 = xmlNode.SelectSingleNode(string.Format("{0}:{1}", "dsig", "PgenCounter"), xmlNamespaceManager);
			if ((xmlNode7 == null && xmlNode8 != null) || (xmlNode7 != null && xmlNode8 == null))
			{
				throw new CryptographicException(string.Format("{0} and {1} can only occur in combination", "Seed", "PgenCounter"));
			}
			try
			{
				this.Key.ImportParameters(new DSAParameters
				{
					P = ((xmlNode3 != null) ? Convert.FromBase64String(xmlNode3.InnerText) : null),
					Q = ((xmlNode4 != null) ? Convert.FromBase64String(xmlNode4.InnerText) : null),
					G = ((xmlNode5 != null) ? Convert.FromBase64String(xmlNode5.InnerText) : null),
					Y = Convert.FromBase64String(xmlNode2.InnerText),
					J = ((xmlNode6 != null) ? Convert.FromBase64String(xmlNode6.InnerText) : null),
					Seed = ((xmlNode7 != null) ? Convert.FromBase64String(xmlNode7.InnerText) : null),
					Counter = ((xmlNode8 != null) ? Utils.ConvertByteArrayToInt(Convert.FromBase64String(xmlNode8.InnerText)) : 0)
				});
			}
			catch (Exception ex)
			{
				throw new CryptographicException("An error occurred parsing the key components", ex);
			}
		}

		// Token: 0x04000121 RID: 289
		private DSA _key;

		// Token: 0x04000122 RID: 290
		private const string KeyValueElementName = "KeyValue";

		// Token: 0x04000123 RID: 291
		private const string DSAKeyValueElementName = "DSAKeyValue";

		// Token: 0x04000124 RID: 292
		private const string PElementName = "P";

		// Token: 0x04000125 RID: 293
		private const string QElementName = "Q";

		// Token: 0x04000126 RID: 294
		private const string GElementName = "G";

		// Token: 0x04000127 RID: 295
		private const string JElementName = "J";

		// Token: 0x04000128 RID: 296
		private const string YElementName = "Y";

		// Token: 0x04000129 RID: 297
		private const string SeedElementName = "Seed";

		// Token: 0x0400012A RID: 298
		private const string PgenCounterElementName = "PgenCounter";
	}
}
