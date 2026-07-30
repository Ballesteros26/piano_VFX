using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006DD RID: 1757
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x06004A66 RID: 19046 RVA: 0x0010A91B File Offset: 0x00108B1B
		internal FixupHolderList()
			: this(2)
		{
		}

		// Token: 0x06004A67 RID: 19047 RVA: 0x0010A924 File Offset: 0x00108B24
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x06004A68 RID: 19048 RVA: 0x0010A940 File Offset: 0x00108B40
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x0010A994 File Offset: 0x00108B94
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x0010A9D0 File Offset: 0x00108BD0
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
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040026E5 RID: 9957
		internal const int InitialSize = 2;

		// Token: 0x040026E6 RID: 9958
		internal FixupHolder[] m_values;

		// Token: 0x040026E7 RID: 9959
		internal int m_count;
	}
}
