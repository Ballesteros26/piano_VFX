using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000022 RID: 34
	internal class ForwardPositionQuery : CacheOutputQuery
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000035FF File Offset: 0x000017FF
		public ForwardPositionQuery(Query input)
			: base(input)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003608 File Offset: 0x00001808
		protected ForwardPositionQuery(ForwardPositionQuery other)
			: base(other)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000404C File Offset: 0x0000224C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.input.Advance()) != null)
			{
				this.outputBuffer.Add(xpathNavigator.Clone());
			}
			return this;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003649 File Offset: 0x00001849
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			return this.input.MatchNode(context);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004084 File Offset: 0x00002284
		public override XPathNodeIterator Clone()
		{
			return new ForwardPositionQuery(this);
		}
	}
}
