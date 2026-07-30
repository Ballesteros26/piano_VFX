using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200021C RID: 540
	internal struct Wrapper<T>
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x0002C3C6 File Offset: 0x0002A5C6
		internal Wrapper(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000840 RID: 2112
		internal T Value;
	}
}
