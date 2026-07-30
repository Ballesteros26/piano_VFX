using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000526 RID: 1318
	internal class NamespaceEvent : Event
	{
		// Token: 0x06003505 RID: 13573 RVA: 0x0012B2C2 File Offset: 0x001294C2
		public NamespaceEvent(NavigatorInput input)
		{
			this.namespaceUri = input.Value;
			this.name = input.LocalName;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x0012B2E4 File Offset: 0x001294E4
		public override void ReplaceNamespaceAlias(Compiler compiler)
		{
			if (this.namespaceUri.Length != 0)
			{
				NamespaceInfo namespaceInfo = compiler.FindNamespaceAlias(this.namespaceUri);
				if (namespaceInfo != null)
				{
					this.namespaceUri = namespaceInfo.nameSpace;
					if (namespaceInfo.prefix != null)
					{
						this.name = namespaceInfo.prefix;
					}
				}
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0012B32E File Offset: 0x0012952E
		public override bool Output(Processor processor, ActionFrame frame)
		{
			processor.BeginEvent(XPathNodeType.Namespace, null, this.name, this.namespaceUri, false);
			processor.EndEvent(XPathNodeType.Namespace);
			return true;
		}

		// Token: 0x040021D6 RID: 8662
		private string namespaceUri;

		// Token: 0x040021D7 RID: 8663
		private string name;
	}
}
