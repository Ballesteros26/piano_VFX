using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000073 RID: 115
	public static class NativeSliceUnsafeUtility
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			bool flag = length < 0;
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid length of '{0}'. It must be greater than 0.", length), "length");
			}
			bool flag2 = stride < 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Invalid stride '{0}'. It must be greater than 0.", stride), "stride");
			}
			return new NativeSlice<T>
			{
				m_Stride = stride,
				m_Buffer = (byte*)dataPointer,
				m_Length = length
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003B20 File Offset: 0x00001D20
		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003B38 File Offset: 0x00001D38
		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return (void*)nativeSlice.m_Buffer;
		}
	}
}
