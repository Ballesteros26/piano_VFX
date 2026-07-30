using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006E0 RID: 1760
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x06004A7B RID: 19067 RVA: 0x0010AC76 File Offset: 0x00108E76
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x0010ACA4 File Offset: 0x00108EA4
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x0010AD2B File Offset: 0x00108F2B
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x040026F0 RID: 9968
		private bool m_isFixupEnumerator;

		// Token: 0x040026F1 RID: 9969
		private ObjectHolderList m_list;

		// Token: 0x040026F2 RID: 9970
		private int m_startingVersion;

		// Token: 0x040026F3 RID: 9971
		private int m_currPos;
	}
}
