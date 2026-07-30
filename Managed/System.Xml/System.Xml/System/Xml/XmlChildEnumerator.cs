using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x02000219 RID: 537
	internal sealed class XmlChildEnumerator : IEnumerator
	{
		// Token: 0x0600139A RID: 5018 RVA: 0x00072CD5 File Offset: 0x00070ED5
		internal XmlChildEnumerator(XmlNode container)
		{
			this.container = container;
			this.child = container.FirstChild;
			this.isFirst = true;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00072CF7 File Offset: 0x00070EF7
		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00072D00 File Offset: 0x00070F00
		internal bool MoveNext()
		{
			if (this.isFirst)
			{
				this.child = this.container.FirstChild;
				this.isFirst = false;
			}
			else if (this.child != null)
			{
				this.child = this.child.NextSibling;
			}
			return this.child != null;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00072D51 File Offset: 0x00070F51
		void IEnumerator.Reset()
		{
			this.isFirst = true;
			this.child = this.container.FirstChild;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00072D6B File Offset: 0x00070F6B
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00072D73 File Offset: 0x00070F73
		internal XmlNode Current
		{
			get
			{
				if (this.isFirst || this.child == null)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				return this.child;
			}
		}

		// Token: 0x04000D82 RID: 3458
		internal XmlNode container;

		// Token: 0x04000D83 RID: 3459
		internal XmlNode child;

		// Token: 0x04000D84 RID: 3460
		internal bool isFirst;
	}
}
