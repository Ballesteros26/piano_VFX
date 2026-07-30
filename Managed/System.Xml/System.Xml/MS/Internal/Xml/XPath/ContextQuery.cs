using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000017 RID: 23
	internal class ContextQuery : Query
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000031DF File Offset: 0x000013DF
		public ContextQuery()
		{
			this.count = 0;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000031EE File Offset: 0x000013EE
		protected ContextQuery(ContextQuery other)
			: base(other)
		{
			this.contextNode = other.contextNode;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002979 File Offset: 0x00000B79
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003203 File Offset: 0x00001403
		public override XPathNavigator Current
		{
			get
			{
				return this.contextNode;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000320B File Offset: 0x0000140B
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current;
			this.count = 0;
			return this;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003221 File Offset: 0x00001421
		public override XPathNavigator Advance()
		{
			if (this.count == 0)
			{
				this.count = 1;
				return this.contextNode;
			}
			return null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000206B File Offset: 0x0000026B
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			return current;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000323A File Offset: 0x0000143A
		public override XPathNodeIterator Clone()
		{
			return new ContextQuery(this);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000029F5 File Offset: 0x00000BF5
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003242 File Offset: 0x00001442
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002A0A File Offset: 0x00000C0A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000077 RID: 119
		protected XPathNavigator contextNode;
	}
}
