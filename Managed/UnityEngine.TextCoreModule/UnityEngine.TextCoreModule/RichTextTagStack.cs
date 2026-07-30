using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000041 RID: 65
	internal struct RichTextTagStack<T>
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00019F5F File Offset: 0x0001815F
		public RichTextTagStack(T[] tagStack)
		{
			this.m_ItemStack = tagStack;
			this.m_Capacity = tagStack.Length;
			this.m_Index = 0;
			this.m_DefaultItem = default(T);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00019F85 File Offset: 0x00018185
		public RichTextTagStack(int capacity)
		{
			this.m_ItemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.m_Index = 0;
			this.m_DefaultItem = default(T);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00019FAE File Offset: 0x000181AE
		public void Clear()
		{
			this.m_Index = 0;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00019FB8 File Offset: 0x000181B8
		public void SetDefault(T item)
		{
			this.m_ItemStack[0] = item;
			this.m_Index = 1;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00019FD0 File Offset: 0x000181D0
		public void Add(T item)
		{
			bool flag = this.m_Index < this.m_ItemStack.Length;
			if (flag)
			{
				this.m_ItemStack[this.m_Index] = item;
				this.m_Index++;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0001A014 File Offset: 0x00018214
		public T Remove()
		{
			this.m_Index--;
			bool flag = this.m_Index <= 0;
			T t;
			if (flag)
			{
				this.m_Index = 1;
				t = this.m_ItemStack[0];
			}
			else
			{
				t = this.m_ItemStack[this.m_Index - 1];
			}
			return t;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0001A070 File Offset: 0x00018270
		public void Push(T item)
		{
			bool flag = this.m_Index == this.m_Capacity;
			if (flag)
			{
				this.m_Capacity *= 2;
				bool flag2 = this.m_Capacity == 0;
				if (flag2)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.m_ItemStack, this.m_Capacity);
			}
			this.m_ItemStack[this.m_Index] = item;
			this.m_Index++;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0001A0E8 File Offset: 0x000182E8
		public T Pop()
		{
			bool flag = this.m_Index == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				this.m_Index--;
				T t2 = this.m_ItemStack[this.m_Index];
				this.m_ItemStack[this.m_Index] = this.m_DefaultItem;
				t = t2;
			}
			return t;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001A14C File Offset: 0x0001834C
		public T Peek()
		{
			bool flag = this.m_Index == 0;
			T t;
			if (flag)
			{
				t = this.m_DefaultItem;
			}
			else
			{
				t = this.m_ItemStack[this.m_Index - 1];
			}
			return t;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0001A188 File Offset: 0x00018388
		public T CurrentItem()
		{
			bool flag = this.m_Index > 0;
			T t;
			if (flag)
			{
				t = this.m_ItemStack[this.m_Index - 1];
			}
			else
			{
				t = this.m_ItemStack[0];
			}
			return t;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001A1CC File Offset: 0x000183CC
		public T PreviousItem()
		{
			bool flag = this.m_Index > 1;
			T t;
			if (flag)
			{
				t = this.m_ItemStack[this.m_Index - 2];
			}
			else
			{
				t = this.m_ItemStack[0];
			}
			return t;
		}

		// Token: 0x0400035E RID: 862
		public T[] m_ItemStack;

		// Token: 0x0400035F RID: 863
		public int m_Index;

		// Token: 0x04000360 RID: 864
		private int m_Capacity;

		// Token: 0x04000361 RID: 865
		private T m_DefaultItem;

		// Token: 0x04000362 RID: 866
		private const int k_DefaultCapacity = 4;
	}
}
