using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004EC RID: 1260
	internal class BeginEvent : Event
	{
		// Token: 0x0600334A RID: 13130 RVA: 0x00125900 File Offset: 0x00123B00
		public BeginEvent(Compiler compiler)
		{
			NavigatorInput input = compiler.Input;
			this.nodeType = input.NodeType;
			this.namespaceUri = input.NamespaceURI;
			this.name = input.LocalName;
			this.prefix = input.Prefix;
			this.empty = input.IsEmptyTag;
			if (this.nodeType == XPathNodeType.Element)
			{
				this.htmlProps = HtmlElementProps.GetProps(this.name);
				return;
			}
			if (this.nodeType == XPathNodeType.Attribute)
			{
				this.htmlProps = HtmlAttributeProps.GetProps(this.name);
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0012598C File Offset: 0x00123B8C
		public override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.nodeType == XPathNodeType.Attribute && this.namespaceUri.Length == 0)
			{
				return;
			}
			NamespaceInfo namespaceInfo = compiler.FindNamespaceAlias(this.namespaceUri);
			if (namespaceInfo != null)
			{
				this.namespaceUri = namespaceInfo.nameSpace;
				if (namespaceInfo.prefix != null)
				{
					this.prefix = namespaceInfo.prefix;
				}
			}
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x001259E0 File Offset: 0x00123BE0
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.BeginEvent(this.nodeType, this.prefix, this.name, this.namespaceUri, this.empty, this.htmlProps, false);
		}

		// Token: 0x04002126 RID: 8486
		private XPathNodeType nodeType;

		// Token: 0x04002127 RID: 8487
		private string namespaceUri;

		// Token: 0x04002128 RID: 8488
		private string name;

		// Token: 0x04002129 RID: 8489
		private string prefix;

		// Token: 0x0400212A RID: 8490
		private bool empty;

		// Token: 0x0400212B RID: 8491
		private object htmlProps;
	}
}
