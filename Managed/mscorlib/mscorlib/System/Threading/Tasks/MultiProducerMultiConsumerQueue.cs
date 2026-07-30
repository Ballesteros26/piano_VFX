using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x020004EC RID: 1260
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060039EE RID: 14830 RVA: 0x000D1D94 File Offset: 0x000CFF94
		void IProducerConsumerQueue<T>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000D1D9D File Offset: 0x000CFF9D
		bool IProducerConsumerQueue<T>.TryDequeue(out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000D1DA6 File Offset: 0x000CFFA6
		bool IProducerConsumerQueue<T>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000D1DAE File Offset: 0x000CFFAE
		int IProducerConsumerQueue<T>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000D1DAE File Offset: 0x000CFFAE
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}
	}
}
