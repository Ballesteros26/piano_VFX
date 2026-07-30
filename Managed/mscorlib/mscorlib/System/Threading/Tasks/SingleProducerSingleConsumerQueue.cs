using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x020004ED RID: 1261
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060039F4 RID: 14836 RVA: 0x000D1DC0 File Offset: 0x000CFFC0
		internal SingleProducerSingleConsumerQueue()
		{
			this.m_head = (this.m_tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000D1DF0 File Offset: 0x000CFFF0
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this.m_tail;
			T[] array = tail.m_array;
			int last = tail.m_state.m_last;
			int num = (last + 1) & (array.Length - 1);
			if (num != tail.m_state.m_firstCopy)
			{
				array[last] = item;
				tail.m_state.m_last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000D1E54 File Offset: 0x000D0054
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment.m_state.m_firstCopy != segment.m_state.m_first)
			{
				segment.m_state.m_firstCopy = segment.m_state.m_first;
				this.Enqueue(item);
				return;
			}
			int num = this.m_tail.m_array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2.m_array[0] = item;
			segment2.m_state.m_last = 1;
			segment2.m_state.m_lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this.m_tail.m_next, segment2);
				this.m_tail = segment2;
			}
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000D1F1C File Offset: 0x000D011C
		public bool TryDequeue(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000D1F98 File Offset: 0x000D0198
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeue(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
			segment.m_state.m_lastCopy = segment.m_state.m_last;
			return true;
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000D20A8 File Offset: 0x000D02A8
		public bool TryPeek(out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first != head.m_state.m_lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000D20FC File Offset: 0x000D02FC
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryPeek(out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000D21C4 File Offset: 0x000D03C4
		public bool TryDequeueIf(Predicate<T> predicate, out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
			T[] array = head.m_array;
			int first = head.m_state.m_first;
			if (first == head.m_state.m_lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head.m_state.m_first = (first + 1) & (array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000D2258 File Offset: 0x000D0458
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, out T result)
		{
			if (segment.m_state.m_last != segment.m_state.m_lastCopy)
			{
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment.m_next != null && segment.m_state.m_first == segment.m_state.m_last)
			{
				segment = segment.m_next;
				array = segment.m_array;
				this.m_head = segment;
			}
			int first = segment.m_state.m_first;
			if (first == segment.m_state.m_last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment.m_state.m_first = (first + 1) & (segment.m_array.Length - 1);
				segment.m_state.m_lastCopy = segment.m_state.m_last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000D2388 File Offset: 0x000D0588
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000D23A0 File Offset: 0x000D05A0
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this.m_head;
				return head.m_state.m_first == head.m_state.m_lastCopy && head.m_state.m_first == head.m_state.m_last && head.m_next == null;
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000D23F9 File Offset: 0x000D05F9
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this.m_head; segment != null; segment = segment.m_next)
			{
				for (int pt = segment.m_state.m_first; pt != segment.m_state.m_last; pt = (pt + 1) & (segment.m_array.Length - 1))
				{
					yield return segment.m_array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000D2408 File Offset: 0x000D0608
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000D2410 File Offset: 0x000D0610
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this.m_head; segment != null; segment = segment.m_next)
				{
					int num2 = segment.m_array.Length;
					int first;
					int last;
					do
					{
						first = segment.m_state.m_first;
						last = segment.m_state.m_last;
					}
					while (first != segment.m_state.m_first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000D2478 File Offset: 0x000D0678
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04001E55 RID: 7765
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04001E56 RID: 7766
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04001E57 RID: 7767
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_head;

		// Token: 0x04001E58 RID: 7768
		private volatile SingleProducerSingleConsumerQueue<T>.Segment m_tail;

		// Token: 0x020004EE RID: 1262
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x06003A03 RID: 14851 RVA: 0x000D24B8 File Offset: 0x000D06B8
			internal Segment(int size)
			{
				this.m_array = new T[size];
			}

			// Token: 0x04001E59 RID: 7769
			internal SingleProducerSingleConsumerQueue<T>.Segment m_next;

			// Token: 0x04001E5A RID: 7770
			internal readonly T[] m_array;

			// Token: 0x04001E5B RID: 7771
			internal SingleProducerSingleConsumerQueue<T>.SegmentState m_state;
		}

		// Token: 0x020004EF RID: 1263
		private struct SegmentState
		{
			// Token: 0x04001E5C RID: 7772
			internal PaddingFor32 m_pad0;

			// Token: 0x04001E5D RID: 7773
			internal volatile int m_first;

			// Token: 0x04001E5E RID: 7774
			internal int m_lastCopy;

			// Token: 0x04001E5F RID: 7775
			internal PaddingFor32 m_pad1;

			// Token: 0x04001E60 RID: 7776
			internal int m_firstCopy;

			// Token: 0x04001E61 RID: 7777
			internal volatile int m_last;

			// Token: 0x04001E62 RID: 7778
			internal PaddingFor32 m_pad2;
		}

		// Token: 0x020004F0 RID: 1264
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x06003A04 RID: 14852 RVA: 0x000D24CC File Offset: 0x000D06CC
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this.m_queue = queue;
			}

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000D24DC File Offset: 0x000D06DC
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					List<T> list = new List<T>();
					foreach (T t in this.m_queue)
					{
						list.Add(t);
					}
					return list.ToArray();
				}
			}

			// Token: 0x04001E63 RID: 7779
			private readonly SingleProducerSingleConsumerQueue<T> m_queue;
		}
	}
}
