using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000212 RID: 530
	internal class Shared<T>
	{
		// Token: 0x06000D25 RID: 3365 RVA: 0x0002BA2B File Offset: 0x00029C2B
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000830 RID: 2096
		internal T Value;
	}
}
