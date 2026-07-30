using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004B RID: 75
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy, nq}")]
	internal class XPathArrayIterator : ResetableIterator
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00007D26 File Offset: 0x00005F26
		public XPathArrayIterator(IList list)
		{
			this.list = list;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007D35 File Offset: 0x00005F35
		public XPathArrayIterator(XPathArrayIterator it)
		{
			this.list = it.list;
			this.index = it.index;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007D55 File Offset: 0x00005F55
		public XPathArrayIterator(XPathNodeIterator nodeIterator)
		{
			this.list = new ArrayList();
			while (nodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = nodeIterator.Current;
				this.list.Add(xpathNavigator.Clone());
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00007D89 File Offset: 0x00005F89
		public IList AsList
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007D91 File Offset: 0x00005F91
		public override XPathNodeIterator Clone()
		{
			return new XPathArrayIterator(this);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007D9C File Offset: 0x00005F9C
		public override XPathNavigator Current
		{
			get
			{
				if (this.index < 1)
				{
					throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
				}
				return (XPathNavigator)this.list[this.index - 1];
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007DFD File Offset: 0x00005FFD
		public override bool MoveNext()
		{
			if (this.index == this.list.Count)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007E23 File Offset: 0x00006023
		public override void Reset()
		{
			this.index = 0;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007E2C File Offset: 0x0000602C
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007E39 File Offset: 0x00006039
		private object debuggerDisplayProxy
		{
			get
			{
				if (this.index >= 1)
				{
					return new XPathNavigator.DebuggerDisplayProxy(this.Current);
				}
				return null;
			}
		}

		// Token: 0x0400010E RID: 270
		protected IList list;

		// Token: 0x0400010F RID: 271
		protected int index;
	}
}
