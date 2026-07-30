using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000206 RID: 518
	internal sealed class DocumentXPathNodeIterator_Empty : XPathNodeIterator
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x00070A90 File Offset: 0x0006EC90
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNavigator nav)
		{
			this.nav = nav.Clone();
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00070AA4 File Offset: 0x0006ECA4
		internal DocumentXPathNodeIterator_Empty(DocumentXPathNodeIterator_Empty other)
		{
			this.nav = other.nav.Clone();
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00070ABD File Offset: 0x0006ECBD
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_Empty(this);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0000226C File Offset: 0x0000046C
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00070AC5 File Offset: 0x0006ECC5
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0000226C File Offset: 0x0000046C
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0000226C File Offset: 0x0000046C
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04000D40 RID: 3392
		private XPathNavigator nav;
	}
}
