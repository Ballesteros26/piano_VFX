using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000042 RID: 66
	internal sealed class SortKey
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00006D3F File Offset: 0x00004F3F
		public SortKey(int numKeys, int originalPosition, XPathNavigator node)
		{
			this.numKeys = numKeys;
			this.keys = new object[numKeys];
			this.originalPosition = originalPosition;
			this.node = node;
		}

		// Token: 0x17000066 RID: 102
		public object this[int index]
		{
			get
			{
				return this.keys[index];
			}
			set
			{
				this.keys[index] = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006D7D File Offset: 0x00004F7D
		public int NumKeys
		{
			get
			{
				return this.numKeys;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006D85 File Offset: 0x00004F85
		public int OriginalPosition
		{
			get
			{
				return this.originalPosition;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006D8D File Offset: 0x00004F8D
		public XPathNavigator Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x040000F9 RID: 249
		private int numKeys;

		// Token: 0x040000FA RID: 250
		private object[] keys;

		// Token: 0x040000FB RID: 251
		private int originalPosition;

		// Token: 0x040000FC RID: 252
		private XPathNavigator node;
	}
}
