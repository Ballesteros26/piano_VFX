using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001B RID: 27
	internal sealed class DocumentOrderQuery : CacheOutputQuery
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000035FF File Offset: 0x000017FF
		public DocumentOrderQuery(Query qyParent)
			: base(qyParent)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003608 File Offset: 0x00001808
		private DocumentOrderQuery(DocumentOrderQuery other)
			: base(other)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003614 File Offset: 0x00001814
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				base.Insert(this.outputBuffer, xpathNavigator);
			}
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003649 File Offset: 0x00001849
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003657 File Offset: 0x00001857
		public override XPathNodeIterator Clone()
		{
			return new DocumentOrderQuery(this);
		}
	}
}
