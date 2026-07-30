using System;

namespace System.Threading
{
	// Token: 0x02000454 RID: 1108
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06003507 RID: 13575 RVA: 0x000C424E File Offset: 0x000C244E
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this.m_source = source;
			this.m_index = index;
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000C425E File Offset: 0x000C245E
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003509 RID: 13577 RVA: 0x000C4266 File Offset: 0x000C2466
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x04001C46 RID: 7238
		private SparselyPopulatedArrayFragment<T> m_source;

		// Token: 0x04001C47 RID: 7239
		private int m_index;
	}
}
