using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000208 RID: 520
	internal class IntValueEvent : ManualResetEventSlim
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002B40F File Offset: 0x0002960F
		internal IntValueEvent()
			: base(false)
		{
			this.Value = 0;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002B41F File Offset: 0x0002961F
		internal void Set(int index)
		{
			this.Value = index;
			base.Set();
		}

		// Token: 0x04000810 RID: 2064
		internal int Value;
	}
}
