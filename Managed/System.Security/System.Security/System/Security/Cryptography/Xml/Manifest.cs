using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000086 RID: 134
	internal class Manifest
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000103A8 File Offset: 0x0000E5A8
		public Manifest()
		{
			this.references = new ArrayList();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000103BB File Offset: 0x0000E5BB
		public Manifest(XmlElement xel)
			: this()
		{
			this.LoadXml(xel);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000103CA File Offset: 0x0000E5CA
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000103D2 File Offset: 0x0000E5D2
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000103E2 File Offset: 0x0000E5E2
		public ArrayList References
		{
			get
			{
				return this.references;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000103EA File Offset: 0x0000E5EA
		public void AddReference(Reference reference)
		{
			this.references.Add(reference);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000103FC File Offset: 0x0000E5FC
		public XmlElement GetXml()
		{
			if (this.element != null)
			{
				return this.element;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (this.id != null)
			{
				xmlElement.SetAttribute("Id", this.id);
			}
			foreach (object obj in this.references)
			{
				XmlNode xml = ((Reference)obj).GetXml();
				XmlNode xmlNode = xmlDocument.ImportNode(xml, true);
				xmlElement.AppendChild(xmlNode);
			}
			return xmlElement;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000104AC File Offset: 0x0000E6AC
		private string GetAttribute(XmlElement xel, string attribute)
		{
			XmlAttribute xmlAttribute = xel.Attributes[attribute];
			if (xmlAttribute == null)
			{
				return null;
			}
			return xmlAttribute.InnerText;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000104D4 File Offset: 0x0000E6D4
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "Manifest" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException();
			}
			this.id = this.GetAttribute(value, "Id");
			for (int i = 0; i < value.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = value.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == "Reference" && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					Reference reference = new Reference();
					reference.LoadXml((XmlElement)xmlNode);
					this.AddReference(reference);
				}
			}
			this.element = value;
		}

		// Token: 0x040001E6 RID: 486
		private ArrayList references;

		// Token: 0x040001E7 RID: 487
		private string id;

		// Token: 0x040001E8 RID: 488
		private XmlElement element;
	}
}
