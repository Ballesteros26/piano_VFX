using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000611 RID: 1553
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlQueryItemSequence : XmlQuerySequence<XPathItem>
	{
		// Token: 0x06003CF2 RID: 15602 RVA: 0x0015288D File Offset: 0x00150A8D
		public static XmlQueryItemSequence CreateOrReuse(XmlQueryItemSequence seq)
		{
			if (seq != null)
			{
				seq.Clear();
				return seq;
			}
			return new XmlQueryItemSequence();
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x0015289F File Offset: 0x00150A9F
		public static XmlQueryItemSequence CreateOrReuse(XmlQueryItemSequence seq, XPathItem item)
		{
			if (seq != null)
			{
				seq.Clear();
				seq.Add(item);
				return seq;
			}
			return new XmlQueryItemSequence(item);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x001528B9 File Offset: 0x00150AB9
		public XmlQueryItemSequence()
		{
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x001528C1 File Offset: 0x00150AC1
		public XmlQueryItemSequence(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x001528CA File Offset: 0x00150ACA
		public XmlQueryItemSequence(XPathItem item)
			: base(1)
		{
			this.AddClone(item);
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x001528DA File Offset: 0x00150ADA
		public void AddClone(XPathItem item)
		{
			if (item.IsNode)
			{
				base.Add(((XPathNavigator)item).Clone());
				return;
			}
			base.Add(item);
		}

		// Token: 0x040027B6 RID: 10166
		public new static readonly XmlQueryItemSequence Empty = new XmlQueryItemSequence();
	}
}
