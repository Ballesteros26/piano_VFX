using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000072 RID: 114
	public static class NativeArrayUnsafeUtility
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00003A20 File Offset: 0x00001C20
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = dataPointer,
				m_Length = length,
				m_AllocatorLabel = allocator
			};
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003A58 File Offset: 0x00001C58
		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003A70 File Offset: 0x00001C70
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003A88 File Offset: 0x00001C88
		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
