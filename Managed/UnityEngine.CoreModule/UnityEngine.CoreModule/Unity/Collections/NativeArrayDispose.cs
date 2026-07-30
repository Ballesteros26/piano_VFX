using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x0200005F RID: 95
	[NativeContainer]
	internal struct NativeArrayDispose
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00003475 File Offset: 0x00001675
		public void Dispose()
		{
			UnsafeUtility.Free(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x0400011B RID: 283
		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		// Token: 0x0400011C RID: 284
		internal Allocator m_AllocatorLabel;
	}
}
