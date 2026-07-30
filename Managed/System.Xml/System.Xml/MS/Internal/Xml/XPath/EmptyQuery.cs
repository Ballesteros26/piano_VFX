using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001C RID: 28
	internal sealed class EmptyQuery : Query
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000365F File Offset: 0x0000185F
		public override XPathNavigator Advance()
		{
			return null;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002068 File Offset: 0x00000268
		public override object Evaluate(XPathNodeIterator context)
		{
			return this;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000226C File Offset: 0x0000046C
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000226C File Offset: 0x0000046C
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002A0A File Offset: 0x00000C0A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Reset()
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000365F File Offset: 0x0000185F
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}
	}
}
