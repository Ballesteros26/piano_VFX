using System;

namespace System.Xml
{
	// Token: 0x0200023D RID: 573
	internal class XmlUnspecifiedAttribute : XmlAttribute
	{
		// Token: 0x06001656 RID: 5718 RVA: 0x0007BF17 File Offset: 0x0007A117
		protected internal XmlUnspecifiedAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc)
			: base(prefix, localName, namespaceURI, doc)
		{
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0007BF24 File Offset: 0x0007A124
		public override bool Specified
		{
			get
			{
				return this.fSpecified;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0007BF2C File Offset: 0x0007A12C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = (XmlUnspecifiedAttribute)ownerDocument.CreateDefaultAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlUnspecifiedAttribute.CopyChildren(ownerDocument, this, true);
			xmlUnspecifiedAttribute.fSpecified = true;
			return xmlUnspecifiedAttribute;
		}

		// Token: 0x17000478 RID: 1144
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x0007BF6D File Offset: 0x0007A16D
		public override string InnerText
		{
			set
			{
				base.InnerText = value;
				this.fSpecified = true;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0007BF7D File Offset: 0x0007A17D
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode = base.InsertBefore(newChild, refChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0007BF8E File Offset: 0x0007A18E
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode xmlNode = base.InsertAfter(newChild, refChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0007BF9F File Offset: 0x0007A19F
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode xmlNode = base.ReplaceChild(newChild, oldChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0007BFB0 File Offset: 0x0007A1B0
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode xmlNode = base.RemoveChild(oldChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0007BFC0 File Offset: 0x0007A1C0
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode xmlNode = base.AppendChild(newChild);
			this.fSpecified = true;
			return xmlNode;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0007BFD0 File Offset: 0x0007A1D0
		public override void WriteTo(XmlWriter w)
		{
			if (this.fSpecified)
			{
				base.WriteTo(w);
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0007BFE1 File Offset: 0x0007A1E1
		internal void SetSpecified(bool f)
		{
			this.fSpecified = f;
		}

		// Token: 0x04000E2A RID: 3626
		private bool fSpecified;
	}
}
