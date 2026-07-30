using System;

namespace Unity.Collections
{
	// Token: 0x02000065 RID: 101
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		// Token: 0x0600013E RID: 318 RVA: 0x000039ED File Offset: 0x00001BED
		public NativeSliceDebugView(NativeSlice<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003A00 File Offset: 0x00001C00
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000124 RID: 292
		private NativeSlice<T> m_Array;
	}
}
