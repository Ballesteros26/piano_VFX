using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x02000341 RID: 833
	internal abstract class ListProvider<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001931 RID: 6449
		protected abstract T First { get; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001932 RID: 6450
		protected abstract int ElementCount { get; }

		// Token: 0x06001933 RID: 6451
		protected abstract T GetElement(int index);

		// Token: 0x06001934 RID: 6452 RVA: 0x00052EF4 File Offset: 0x000510F4
		public int IndexOf(T item)
		{
			if (this.First == item)
			{
				return 0;
			}
			int i = 1;
			int elementCount = this.ElementCount;
			while (i < elementCount)
			{
				if (this.GetElement(i) == item)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Insert(int index, T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x17000477 RID: 1143
		public T this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.First;
				}
				return this.GetElement(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Add(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00052F53 File Offset: 0x00051153
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00052F64 File Offset: 0x00051164
		public void CopyTo(T[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int elementCount = this.ElementCount;
			if (index + elementCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this.First;
			for (int i = 1; i < elementCount; i++)
			{
				array[index++] = this.GetElement(i);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00052FD1 File Offset: 0x000511D1
		public int Count
		{
			get
			{
				return this.ElementCount;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0000AA13 File Offset: 0x00008C13
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00035338 File Offset: 0x00033538
		[ExcludeFromCodeCoverage]
		public bool Remove(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00052FD9 File Offset: 0x000511D9
		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			int i = 1;
			int j = this.ElementCount;
			while (i < j)
			{
				yield return this.GetElement(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00052FE8 File Offset: 0x000511E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
