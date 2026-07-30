using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000137 RID: 311
	internal sealed class SharedReference
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x000297C1 File Offset: 0x000279C1
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				object target = this._ref.Target;
				this._locked = 0;
				return target;
			}
			return null;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000297E5 File Offset: 0x000279E5
		internal void Cache(object obj)
		{
			if (Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				this._ref.Target = obj;
				this._locked = 0;
			}
		}

		// Token: 0x04000DCA RID: 3530
		private WeakReference _ref = new WeakReference(null);

		// Token: 0x04000DCB RID: 3531
		private int _locked;
	}
}
