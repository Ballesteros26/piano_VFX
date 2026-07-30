using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	internal class MatchEnumerator : IEnumerator
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x00033830 File Offset: 0x00031A30
		internal MatchEnumerator(MatchCollection matchcoll)
		{
			this._matchcoll = matchcoll;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00033840 File Offset: 0x00031A40
		public bool MoveNext()
		{
			if (this._done)
			{
				return false;
			}
			this._match = this._matchcoll.GetMatch(this._curindex);
			this._curindex++;
			if (this._match == null)
			{
				this._done = true;
				return false;
			}
			return true;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0003388E File Offset: 0x00031A8E
		public object Current
		{
			get
			{
				if (this._match == null)
				{
					throw new InvalidOperationException(global::SR.GetString("Enumeration has either not started or has already finished."));
				}
				return this._match;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000338AE File Offset: 0x00031AAE
		public void Reset()
		{
			this._curindex = 0;
			this._done = false;
			this._match = null;
		}

		// Token: 0x04000EE0 RID: 3808
		internal MatchCollection _matchcoll;

		// Token: 0x04000EE1 RID: 3809
		internal Match _match;

		// Token: 0x04000EE2 RID: 3810
		internal int _curindex;

		// Token: 0x04000EE3 RID: 3811
		internal bool _done;
	}
}
