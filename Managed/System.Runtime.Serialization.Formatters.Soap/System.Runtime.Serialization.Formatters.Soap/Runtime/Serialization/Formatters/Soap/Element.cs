using System;
using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap
{
	// Token: 0x0200000E RID: 14
	internal class Element
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000031D1 File Offset: 0x000013D1
		public Element(string prefix, string localName, string namespaceURI)
		{
			this._prefix = prefix;
			this._localName = localName;
			this._namespaceURI = namespaceURI;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000031EE File Offset: 0x000013EE
		public Element(string localName, string namespaceURI)
			: this(null, localName, namespaceURI)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000031F9 File Offset: 0x000013F9
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003201 File Offset: 0x00001401
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003209 File Offset: 0x00001409
		public string NamespaceURI
		{
			get
			{
				return this._namespaceURI;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003211 File Offset: 0x00001411
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003219 File Offset: 0x00001419
		public MethodInfo ParseMethod
		{
			get
			{
				return this._parseMethod;
			}
			set
			{
				this._parseMethod = value;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003224 File Offset: 0x00001424
		public override bool Equals(object obj)
		{
			Element element = obj as Element;
			return this._localName == XmlConvert.DecodeName(element._localName) && this._namespaceURI == XmlConvert.DecodeName(element._namespaceURI);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000326B File Offset: 0x0000146B
		public override int GetHashCode()
		{
			return string.Format("{0} {1}", XmlConvert.DecodeName(this._localName), XmlConvert.DecodeName(this._namespaceURI)).GetHashCode();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003292 File Offset: 0x00001492
		public override string ToString()
		{
			return string.Format("Element.Prefix = {0}, Element.LocalName = {1}, Element.NamespaceURI = {2}", this.Prefix, this.LocalName, this.NamespaceURI);
		}

		// Token: 0x04000042 RID: 66
		private string _prefix;

		// Token: 0x04000043 RID: 67
		private string _localName;

		// Token: 0x04000044 RID: 68
		private string _namespaceURI;

		// Token: 0x04000045 RID: 69
		private MethodInfo _parseMethod;
	}
}
