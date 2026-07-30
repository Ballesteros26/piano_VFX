using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000136 RID: 310
	internal sealed class ExclusiveReference
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x00029724 File Offset: 0x00027924
		internal object Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			object @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002975C File Offset: 0x0002795C
		internal void Release(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = (RegexRunner)obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private RegexRunner _ref;

		// Token: 0x04000DC8 RID: 3528
		private object _obj;

		// Token: 0x04000DC9 RID: 3529
		private int _locked;
	}
}
