using System;

namespace Unity.Collections
{
	// Token: 0x02000061 RID: 97
	internal sealed class NativeArrayDebugView<T> where T : struct
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003499 File Offset: 0x00001699
		public NativeArrayDebugView(NativeArray<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000034AA File Offset: 0x000016AA
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x0400011E RID: 286
		private NativeArray<T> m_Array;
	}
}
