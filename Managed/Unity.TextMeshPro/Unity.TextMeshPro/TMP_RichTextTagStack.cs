using System;

namespace TMPro
{
	// Token: 0x02000035 RID: 53
	public struct TMP_RichTextTagStack<T>
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000F81E File Offset: 0x0000DA1E
		public T current
		{
			get
			{
				if (this.index > 0)
				{
					return this.itemStack[this.index - 1];
				}
				return this.itemStack[0];
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000F849 File Offset: 0x0000DA49
		public TMP_RichTextTagStack(T[] tagStack)
		{
			this.itemStack = tagStack;
			this.m_Capacity = tagStack.Length;
			this.index = 0;
			this.m_DefaultItem = default(T);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000F86E File Offset: 0x0000DA6E
		public TMP_RichTextTagStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_DefaultItem = default(T);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F896 File Offset: 0x0000DA96
		public void Clear()
		{
			this.index = 0;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		public void SetDefault(T item)
		{
			if (this.itemStack == null)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000F8ED File Offset: 0x0000DAED
		public void Add(T item)
		{
			if (this.index < this.itemStack.Length)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F91F File Offset: 0x0000DB1F
		public T Remove()
		{
			this.index--;
			if (this.index <= 0)
			{
				this.index = 1;
				return this.itemStack[0];
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000F960 File Offset: 0x0000DB60
		public void Push(T item)
		{
			if (this.index == this.m_Capacity)
			{
				this.m_Capacity *= 2;
				if (this.m_Capacity == 0)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			this.index++;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public T Pop()
		{
			if (this.index == 0)
			{
				return default(T);
			}
			this.index--;
			T t = this.itemStack[this.index];
			this.itemStack[this.index] = this.m_DefaultItem;
			return t;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FA21 File Offset: 0x0000DC21
		public T Peek()
		{
			if (this.index == 0)
			{
				return this.m_DefaultItem;
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000F81E File Offset: 0x0000DA1E
		public T CurrentItem()
		{
			if (this.index > 0)
			{
				return this.itemStack[this.index - 1];
			}
			return this.itemStack[0];
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public T PreviousItem()
		{
			if (this.index > 1)
			{
				return this.itemStack[this.index - 2];
			}
			return this.itemStack[0];
		}

		// Token: 0x0400019E RID: 414
		public T[] itemStack;

		// Token: 0x0400019F RID: 415
		public int index;

		// Token: 0x040001A0 RID: 416
		private int m_Capacity;

		// Token: 0x040001A1 RID: 417
		private T m_DefaultItem;

		// Token: 0x040001A2 RID: 418
		private const int k_DefaultCapacity = 4;
	}
}
