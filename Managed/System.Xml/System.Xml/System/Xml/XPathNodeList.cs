using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000213 RID: 531
	internal class XPathNodeList : XmlNodeList
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x000719F4 File Offset: 0x0006FBF4
		public XPathNodeList(XPathNodeIterator nodeIterator)
		{
			this.nodeIterator = nodeIterator;
			this.list = new List<XmlNode>();
			this.done = false;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00071A15 File Offset: 0x0006FC15
		public override int Count
		{
			get
			{
				if (!this.done)
				{
					this.ReadUntil(int.MaxValue);
				}
				return this.list.Count;
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00071A36 File Offset: 0x0006FC36
		private XmlNode GetNode(XPathNavigator n)
		{
			return ((IHasXmlNode)n).GetNode();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00071A44 File Offset: 0x0006FC44
		internal int ReadUntil(int index)
		{
			int num = this.list.Count;
			while (!this.done && num <= index)
			{
				if (!this.nodeIterator.MoveNext())
				{
					this.done = true;
					break;
				}
				XmlNode node = this.GetNode(this.nodeIterator.Current);
				if (node != null)
				{
					this.list.Add(node);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00071AA9 File Offset: 0x0006FCA9
		public override XmlNode Item(int index)
		{
			if (this.list.Count <= index)
			{
				this.ReadUntil(index);
			}
			if (index < 0 || this.list.Count <= index)
			{
				return null;
			}
			return this.list[index];
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00071AE1 File Offset: 0x0006FCE1
		public override IEnumerator GetEnumerator()
		{
			return new XmlNodeListEnumerator(this);
		}

		// Token: 0x04000D78 RID: 3448
		private List<XmlNode> list;

		// Token: 0x04000D79 RID: 3449
		private XPathNodeIterator nodeIterator;

		// Token: 0x04000D7A RID: 3450
		private bool done;

		// Token: 0x04000D7B RID: 3451
		private static readonly object[] nullparams = new object[0];
	}
}
