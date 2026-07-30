using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200060A RID: 1546
	internal struct XmlNavigatorStack
	{
		// Token: 0x06003C27 RID: 15399 RVA: 0x0014FFC4 File Offset: 0x0014E1C4
		public void Push(XPathNavigator nav)
		{
			if (this.stkNav == null)
			{
				this.stkNav = new XPathNavigator[8];
			}
			else if (this.sp >= this.stkNav.Length)
			{
				Array array = this.stkNav;
				this.stkNav = new XPathNavigator[2 * this.sp];
				Array.Copy(array, this.stkNav, this.sp);
			}
			XPathNavigator[] array2 = this.stkNav;
			int num = this.sp;
			this.sp = num + 1;
			array2[num] = nav;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x0015003C File Offset: 0x0014E23C
		public XPathNavigator Pop()
		{
			XPathNavigator[] array = this.stkNav;
			int num = this.sp - 1;
			this.sp = num;
			return array[num];
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x00150061 File Offset: 0x0014E261
		public XPathNavigator Peek()
		{
			return this.stkNav[this.sp - 1];
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00150072 File Offset: 0x0014E272
		public void Reset()
		{
			this.sp = 0;
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x0015007B File Offset: 0x0014E27B
		public bool IsEmpty
		{
			get
			{
				return this.sp == 0;
			}
		}

		// Token: 0x0400277C RID: 10108
		private XPathNavigator[] stkNav;

		// Token: 0x0400277D RID: 10109
		private int sp;

		// Token: 0x0400277E RID: 10110
		private const int InitialStackSize = 8;
	}
}
