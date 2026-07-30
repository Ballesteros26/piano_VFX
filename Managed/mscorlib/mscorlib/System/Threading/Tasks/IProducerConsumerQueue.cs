using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x020004EB RID: 1259
	internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060039E9 RID: 14825
		void Enqueue(T item);

		// Token: 0x060039EA RID: 14826
		bool TryDequeue(out T result);

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060039EB RID: 14827
		bool IsEmpty { get; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060039EC RID: 14828
		int Count { get; }

		// Token: 0x060039ED RID: 14829
		int GetCountSafe(object syncObj);
	}
}
