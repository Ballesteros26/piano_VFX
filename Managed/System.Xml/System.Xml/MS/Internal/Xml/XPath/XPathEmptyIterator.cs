using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004F RID: 79
	internal sealed class XPathEmptyIterator : ResetableIterator
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00005CC1 File Offset: 0x00003EC1
		private XPathEmptyIterator()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00002068 File Offset: 0x00000268
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000365F File Offset: 0x0000185F
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000226C File Offset: 0x0000046C
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000226C File Offset: 0x0000046C
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00002F50 File Offset: 0x00001150
		public override void Reset()
		{
		}

		// Token: 0x04000118 RID: 280
		public static XPathEmptyIterator Instance = new XPathEmptyIterator();
	}
}
