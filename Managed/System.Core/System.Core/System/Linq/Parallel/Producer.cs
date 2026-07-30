using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000122 RID: 290
	internal struct Producer<TKey>
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		internal Producer(TKey maxKey, int producerIndex)
		{
			this.MaxKey = maxKey;
			this.ProducerIndex = producerIndex;
		}

		// Token: 0x0400058E RID: 1422
		internal readonly TKey MaxKey;

		// Token: 0x0400058F RID: 1423
		internal readonly int ProducerIndex;
	}
}
