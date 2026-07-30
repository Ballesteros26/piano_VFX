using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200051B RID: 1307
	internal class EndEvent : Event
	{
		// Token: 0x060034B7 RID: 13495 RVA: 0x0012A102 File Offset: 0x00128302
		internal EndEvent(XPathNodeType nodeType)
		{
			this.nodeType = nodeType;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x0012A111 File Offset: 0x00128311
		public override bool Output(Processor processor, ActionFrame frame)
		{
			return processor.EndEvent(this.nodeType);
		}

		// Token: 0x040021AF RID: 8623
		private XPathNodeType nodeType;
	}
}
