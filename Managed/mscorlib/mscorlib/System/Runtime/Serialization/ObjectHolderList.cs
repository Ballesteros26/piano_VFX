using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006DF RID: 1759
	internal class ObjectHolderList
	{
		// Token: 0x06004A74 RID: 19060 RVA: 0x0010ABA6 File Offset: 0x00108DA6
		internal ObjectHolderList()
			: this(8)
		{
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x0010ABAF File Offset: 0x00108DAF
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x0010ABCC File Offset: 0x00108DCC
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x0010AC08 File Offset: 0x00108E08
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0010AC14 File Offset: 0x00108E14
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0010AC6E File Offset: 0x00108E6E
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x0010AC6E File Offset: 0x00108E6E
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x040026ED RID: 9965
		internal const int DefaultInitialSize = 8;

		// Token: 0x040026EE RID: 9966
		internal ObjectHolder[] m_values;

		// Token: 0x040026EF RID: 9967
		internal int m_count;
	}
}
