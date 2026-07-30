using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000102 RID: 258
	internal sealed class SingleLinkedNode<TSource>
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x0001CFB6 File Offset: 0x0001B1B6
		public SingleLinkedNode(TSource item)
		{
			this.Item = item;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001CFC5 File Offset: 0x0001B1C5
		private SingleLinkedNode(SingleLinkedNode<TSource> linked, TSource item)
		{
			this.Linked = linked;
			this.Item = item;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001CFDB File Offset: 0x0001B1DB
		public TSource Item { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001CFE3 File Offset: 0x0001B1E3
		public SingleLinkedNode<TSource> Linked { get; }

		// Token: 0x06000900 RID: 2304 RVA: 0x0001CFEB File Offset: 0x0001B1EB
		public SingleLinkedNode<TSource> Add(TSource item)
		{
			return new SingleLinkedNode<TSource>(this, item);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public int GetCount()
		{
			int num = 0;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D016 File Offset: 0x0001B216
		public IEnumerator<TSource> GetEnumerator(int count)
		{
			return this.ToArray(count).GetEnumerator();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001D024 File Offset: 0x0001B224
		public SingleLinkedNode<TSource> GetNode(int index)
		{
			SingleLinkedNode<TSource> singleLinkedNode = this;
			while (index > 0)
			{
				singleLinkedNode = singleLinkedNode.Linked;
				index--;
			}
			return singleLinkedNode;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001D048 File Offset: 0x0001B248
		private TSource[] ToArray(int count)
		{
			TSource[] array = new TSource[count];
			int num = count;
			for (SingleLinkedNode<TSource> singleLinkedNode = this; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
			{
				num--;
				array[num] = singleLinkedNode.Item;
			}
			return array;
		}
	}
}
