using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001A2 RID: 418
	internal abstract class QueryResults<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000B41 RID: 2881
		internal abstract void GivePartitionedStream(IPartitionedStreamRecipient<T> recipient);

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00002285 File Offset: 0x00000485
		internal virtual bool IsIndexible
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00003CCF File Offset: 0x00001ECF
		internal virtual T GetElement(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00003CCF File Offset: 0x00001ECF
		internal virtual int ElementsCount
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00003CCF File Offset: 0x00001ECF
		int IList<T>.IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00003CCF File Offset: 0x00001ECF
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00003CCF File Offset: 0x00001ECF
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000163 RID: 355
		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00003CCF File Offset: 0x00001ECF
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00003CCF File Offset: 0x00001ECF
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00003CCF File Offset: 0x00001ECF
		bool ICollection<T>.Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00003CCF File Offset: 0x00001ECF
		void ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002596F File Offset: 0x00023B6F
		public int Count
		{
			get
			{
				return this.ElementsCount;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0000AA13 File Offset: 0x00008C13
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00003CCF File Offset: 0x00001ECF
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00025977 File Offset: 0x00023B77
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			int num;
			for (int index = 0; index < this.Count; index = num + 1)
			{
				yield return this[index];
				num = index;
			}
			yield break;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0000877A File Offset: 0x0000697A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
	}
}
