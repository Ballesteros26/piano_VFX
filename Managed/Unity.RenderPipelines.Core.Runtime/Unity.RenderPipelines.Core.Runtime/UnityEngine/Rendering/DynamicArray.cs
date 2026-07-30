using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200001E RID: 30
	public class DynamicArray<T> where T : new()
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004D52 File Offset: 0x00002F52
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004D5A File Offset: 0x00002F5A
		public int size { get; private set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00004D63 File Offset: 0x00002F63
		public DynamicArray()
		{
			this.m_Array = new T[32];
			this.size = 32;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004D80 File Offset: 0x00002F80
		public DynamicArray(int size)
		{
			this.m_Array = new T[size];
			this.size = size;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004D9B File Offset: 0x00002F9B
		public void Clear()
		{
			this.size = 0;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public int Add(in T value)
		{
			int size = this.size;
			if (size >= this.m_Array.Length)
			{
				T[] array = new T[this.m_Array.Length * 2];
				Array.Copy(this.m_Array, array, this.m_Array.Length);
				this.m_Array = array;
			}
			this.m_Array[size] = value;
			int size2 = this.size;
			this.size = size2 + 1;
			return size;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004E14 File Offset: 0x00003014
		public void Resize(int newSize, bool keepContent = false)
		{
			if (newSize > this.m_Array.Length)
			{
				if (keepContent)
				{
					T[] array = new T[newSize];
					Array.Copy(this.m_Array, array, this.m_Array.Length);
					this.m_Array = array;
				}
				else
				{
					this.m_Array = new T[newSize];
				}
			}
			this.size = newSize;
		}

		// Token: 0x1700000F RID: 15
		public ref T this[int index]
		{
			get
			{
				return ref this.m_Array[index];
			}
		}

		// Token: 0x0400008F RID: 143
		private T[] m_Array;
	}
}
