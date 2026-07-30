using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;Signature&gt; element of an XML signature.</summary>
	// Token: 0x02000087 RID: 135
	public class Signature
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0001059B File Offset: 0x0000E79B
		static Signature()
		{
			Signature.dsigNsmgr.AddNamespace("xd", "http://www.w3.org/2000/09/xmldsig#");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> class.</summary>
		// Token: 0x060003EF RID: 1007 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public Signature()
		{
			this.list = new ArrayList();
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />. The default is null.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000105D3 File Offset: 0x0000E7D3
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x000105DB File Offset: 0x0000E7DB
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.element = null;
				this.id = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000105EB File Offset: 0x0000E7EB
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x000105F3 File Offset: 0x0000E7F3
		public KeyInfo KeyInfo
		{
			get
			{
				return this.key;
			}
			set
			{
				this.element = null;
				this.key = value;
			}
		}

		/// <summary>Gets or sets a list of objects to be signed.</summary>
		/// <returns>A list of objects to be signed.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00010603 File Offset: 0x0000E803
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001060B File Offset: 0x0000E80B
		public IList ObjectList
		{
			get
			{
				return this.list;
			}
			set
			{
				this.list = ArrayList.Adapter(value);
			}
		}

		/// <summary>Gets or sets the value of the digital signature.</summary>
		/// <returns>A byte array that contains the value of the digital signature.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00010619 File Offset: 0x0000E819
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00010621 File Offset: 0x0000E821
		public byte[] SignatureValue
		{
			get
			{
				return this.signature;
			}
			set
			{
				this.element = null;
				this.signature = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00010631 File Offset: 0x0000E831
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00010639 File Offset: 0x0000E839
		public SignedInfo SignedInfo
		{
			get
			{
				return this.info;
			}
			set
			{
				this.element = null;
				this.info = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to be added to the list of objects to be signed. </param>
		// Token: 0x060003FA RID: 1018 RVA: 0x00010649 File Offset: 0x0000E849
		public void AddObject(DataObject dataObject)
		{
			this.list.Add(dataObject);
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" /> property is null.-or- The <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" /> property is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003FB RID: 1019 RVA: 0x00010658 File Offset: 0x0000E858
		public XmlElement GetXml()
		{
			return this.GetXml(null);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010664 File Offset: 0x0000E864
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.element != null)
			{
				return this.element;
			}
			if (this.info == null)
			{
				throw new CryptographicException("SignedInfo");
			}
			if (this.signature == null)
			{
				throw new CryptographicException("SignatureValue");
			}
			if (document == null)
			{
				document = new XmlDocument();
			}
			XmlElement xmlElement = document.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			XmlNode xmlNode = this.info.GetXml();
			XmlNode xmlNode2 = document.ImportNode(xmlNode, true);
			xmlElement.AppendChild(xmlNode2);
			if (this.signature != null)
			{
				XmlElement xmlElement2 = document.CreateElement("SignatureValue", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement2.InnerText = Convert.ToBase64String(this.signature);
				xmlElement.AppendChild(xmlElement2);
			}
			if (this.key != null)
			{
				xmlNode = this.key.GetXml();
				xmlNode2 = document.ImportNode(xmlNode, true);
				xmlElement.AppendChild(xmlNode2);
			}
			if (this.list.Count > 0)
			{
				foreach (object obj in this.list)
				{
					xmlNode = ((DataObject)obj).GetXml();
					xmlNode2 = document.ImportNode(xmlNode, true);
					xmlElement.AppendChild(xmlNode2);
				}
			}
			return xmlElement;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000107C0 File Offset: 0x0000E9C0
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.InnerText;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Signature" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Signature" /> state. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" />.-or- The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" />. </exception>
		// Token: 0x060003FE RID: 1022 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName == "Signature" && value.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
			{
				this.id = this.GetAttribute(value, "Id");
				int num = this.NextElementPos(value.ChildNodes, 0, "SignedInfo", "http://www.w3.org/2000/09/xmldsig#", true);
				XmlElement xmlElement = (XmlElement)value.ChildNodes[num];
				this.info = new SignedInfo();
				this.info.LoadXml(xmlElement);
				num = this.NextElementPos(value.ChildNodes, num + 1, "SignatureValue", "http://www.w3.org/2000/09/xmldsig#", true);
				XmlElement xmlElement2 = (XmlElement)value.ChildNodes[num];
				this.signature = Convert.FromBase64String(xmlElement2.InnerText);
				num = this.NextElementPos(value.ChildNodes, num + 1, "KeyInfo", "http://www.w3.org/2000/09/xmldsig#", false);
				if (num > 0)
				{
					XmlElement xmlElement3 = (XmlElement)value.ChildNodes[num];
					this.key = new KeyInfo();
					this.key.LoadXml(xmlElement3);
				}
				using (IEnumerator enumerator = value.SelectNodes("xd:Object", Signature.dsigNsmgr).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlElement xmlElement4 = (XmlElement)obj;
						DataObject dataObject = new DataObject();
						dataObject.LoadXml(xmlElement4);
						this.AddObject(dataObject);
					}
					goto IL_0180;
				}
				goto IL_0175;
				IL_0180:
				if (this.info == null)
				{
					throw new CryptographicException("SignedInfo");
				}
				if (this.signature == null)
				{
					throw new CryptographicException("SignatureValue");
				}
				return;
			}
			IL_0175:
			throw new CryptographicException("Malformed element: Signature.");
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000109AC File Offset: 0x0000EBAC
		private int NextElementPos(XmlNodeList nl, int pos, string name, string ns, bool required)
		{
			while (pos < nl.Count)
			{
				if (nl[pos].NodeType == XmlNodeType.Element)
				{
					if (!(nl[pos].LocalName != name) && !(nl[pos].NamespaceURI != ns))
					{
						return pos;
					}
					if (required)
					{
						throw new CryptographicException("Malformed element " + name);
					}
					return -2;
				}
				else
				{
					pos++;
				}
			}
			if (required)
			{
				throw new CryptographicException("Malformed element " + name);
			}
			return -1;
		}

		// Token: 0x040001E9 RID: 489
		private static XmlNamespaceManager dsigNsmgr = new XmlNamespaceManager(new NameTable());

		// Token: 0x040001EA RID: 490
		private ArrayList list;

		// Token: 0x040001EB RID: 491
		private SignedInfo info;

		// Token: 0x040001EC RID: 492
		private KeyInfo key;

		// Token: 0x040001ED RID: 493
		private string id;

		// Token: 0x040001EE RID: 494
		private byte[] signature;

		// Token: 0x040001EF RID: 495
		private XmlElement element;
	}
}
