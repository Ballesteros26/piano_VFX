using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006DE RID: 1758
	[Serializable]
	internal class LongList
	{
		// Token: 0x06004A6B RID: 19051 RVA: 0x0010AA2A File Offset: 0x00108C2A
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x0010AA33 File Offset: 0x00108C33
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0010AA58 File Offset: 0x00108C58
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x0010AAA2 File Offset: 0x00108CA2
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0010AAAA File Offset: 0x00108CAA
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x0010AAB4 File Offset: 0x00108CB4
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0010AAFC File Offset: 0x00108CFC
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0010AB0C File Offset: 0x00108D0C
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x0010AB4C File Offset: 0x00108D4C
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("The internal array cannot expand to greater than Int32.MaxValue elements."));
				}
				num = int.MaxValue;
			}
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040026E8 RID: 9960
		private const int InitialSize = 2;

		// Token: 0x040026E9 RID: 9961
		private long[] m_values;

		// Token: 0x040026EA RID: 9962
		private int m_count;

		// Token: 0x040026EB RID: 9963
		private int m_totalItems;

		// Token: 0x040026EC RID: 9964
		private int m_currentItem;
	}
}
