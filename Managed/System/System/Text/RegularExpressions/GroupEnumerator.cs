using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200014B RID: 331
	internal class GroupEnumerator : IEnumerator
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x00031838 File Offset: 0x0002FA38
		internal GroupEnumerator(GroupCollection rgc)
		{
			this._curindex = -1;
			this._rgc = rgc;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00031850 File Offset: 0x0002FA50
		public bool MoveNext()
		{
			int count = this._rgc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0003188B File Offset: 0x0002FA8B
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00031893 File Offset: 0x0002FA93
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rgc.Count)
				{
					throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
				}
				return this._rgc[this._curindex];
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000318D2 File Offset: 0x0002FAD2
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04000EBF RID: 3775
		internal GroupCollection _rgc;

		// Token: 0x04000EC0 RID: 3776
		internal int _curindex;
	}
}
