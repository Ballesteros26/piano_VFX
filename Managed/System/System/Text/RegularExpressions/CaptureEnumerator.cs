using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	internal class CaptureEnumerator : IEnumerator
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00029F17 File Offset: 0x00028117
		internal CaptureEnumerator(CaptureCollection rcc)
		{
			this._curindex = -1;
			this._rcc = rcc;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00029F30 File Offset: 0x00028130
		public bool MoveNext()
		{
			int count = this._rcc.Count;
			if (this._curindex >= count)
			{
				return false;
			}
			this._curindex++;
			return this._curindex < count;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00029F6B File Offset: 0x0002816B
		public object Current
		{
			get
			{
				return this.Capture;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00029F73 File Offset: 0x00028173
		public Capture Capture
		{
			get
			{
				if (this._curindex < 0 || this._curindex >= this._rcc.Count)
				{
					throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
				}
				return this._rcc[this._curindex];
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00029FB2 File Offset: 0x000281B2
		public void Reset()
		{
			this._curindex = -1;
		}

		// Token: 0x04000DDC RID: 3548
		internal CaptureCollection _rcc;

		// Token: 0x04000DDD RID: 3549
		internal int _curindex;
	}
}
