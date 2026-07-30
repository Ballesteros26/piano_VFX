using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000214 RID: 532
	internal class XmlNodeListEnumerator : IEnumerator
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x00071AF6 File Offset: 0x0006FCF6
		public XmlNodeListEnumerator(XPathNodeList list)
		{
			this.list = list;
			this.index = -1;
			this.valid = false;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00071B13 File Offset: 0x0006FD13
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00071B1C File Offset: 0x0006FD1C
		public bool MoveNext()
		{
			this.index++;
			if (this.list.ReadUntil(this.index + 1) - 1 < this.index)
			{
				return false;
			}
			this.valid = this.list[this.index] != null;
			return this.valid;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00071B76 File Offset: 0x0006FD76
		public object Current
		{
			get
			{
				if (this.valid)
				{
					return this.list[this.index];
				}
				return null;
			}
		}

		// Token: 0x04000D7C RID: 3452
		private XPathNodeList list;

		// Token: 0x04000D7D RID: 3453
		private int index;

		// Token: 0x04000D7E RID: 3454
		private bool valid;
	}
}
