using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D4 RID: 1492
	internal abstract class RtfNavigator : XPathNavigator
	{
		// Token: 0x06003AF4 RID: 15092
		public abstract void CopyToWriter(XmlWriter writer);

		// Token: 0x06003AF5 RID: 15093
		public abstract XPathNavigator ToNavigator();

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x0000226C File Offset: 0x0000046C
		public override XPathNodeType NodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x00003065 File Offset: 0x00001265
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00003065 File Offset: 0x00001265
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x00003065 File Offset: 0x00001265
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x00003065 File Offset: 0x00001265
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override XmlNameTable NameTable
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToFirstAttribute()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToNextAttribute()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToNext()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToPrevious()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToFirstChild()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToParent()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool MoveToId(string id)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public override bool IsSamePosition(XPathNavigator other)
		{
			throw new NotSupportedException();
		}
	}
}
