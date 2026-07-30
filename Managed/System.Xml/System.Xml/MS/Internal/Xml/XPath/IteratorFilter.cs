using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000029 RID: 41
	internal class IteratorFilter : XPathNodeIterator
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000463F File Offset: 0x0000283F
		internal IteratorFilter(XPathNodeIterator innerIterator, string name)
		{
			this.innerIterator = innerIterator;
			this.name = name;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004655 File Offset: 0x00002855
		private IteratorFilter(IteratorFilter it)
		{
			this.innerIterator = it.innerIterator.Clone();
			this.name = it.name;
			this.position = it.position;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004686 File Offset: 0x00002886
		public override XPathNodeIterator Clone()
		{
			return new IteratorFilter(this);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000468E File Offset: 0x0000288E
		public override XPathNavigator Current
		{
			get
			{
				return this.innerIterator.Current;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000469B File Offset: 0x0000289B
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000046A3 File Offset: 0x000028A3
		public override bool MoveNext()
		{
			while (this.innerIterator.MoveNext())
			{
				if (this.innerIterator.Current.LocalName == this.name)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000AE RID: 174
		private XPathNodeIterator innerIterator;

		// Token: 0x040000AF RID: 175
		private string name;

		// Token: 0x040000B0 RID: 176
		private int position;
	}
}
