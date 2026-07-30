using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003E RID: 62
	internal abstract class ResetableIterator : XPathNodeIterator
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x00006A76 File Offset: 0x00004C76
		public ResetableIterator()
		{
			this.count = -1;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006A85 File Offset: 0x00004C85
		protected ResetableIterator(ResetableIterator other)
		{
			this.count = other.count;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006A99 File Offset: 0x00004C99
		protected void ResetCount()
		{
			this.count = -1;
		}

		// Token: 0x060001A8 RID: 424
		public abstract void Reset();

		// Token: 0x060001A9 RID: 425 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public virtual bool MoveToPosition(int pos)
		{
			this.Reset();
			for (int i = this.CurrentPosition; i < pos; i++)
			{
				if (!this.MoveNext())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001AA RID: 426
		public abstract override int CurrentPosition { get; }
	}
}
