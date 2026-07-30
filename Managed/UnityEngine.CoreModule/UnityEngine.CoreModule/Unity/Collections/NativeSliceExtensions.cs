using System;

namespace Unity.Collections
{
	// Token: 0x02000062 RID: 98
	public static class NativeSliceExtensions
	{
		// Token: 0x06000116 RID: 278 RVA: 0x000034B8 File Offset: 0x000016B8
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray) where T : struct
		{
			return new NativeSlice<T>(thisArray);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000034D0 File Offset: 0x000016D0
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start) where T : struct
		{
			return new NativeSlice<T>(thisArray, start);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000034EC File Offset: 0x000016EC
		public static NativeSlice<T> Slice<T>(this NativeArray<T> thisArray, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisArray, start, length);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003508 File Offset: 0x00001708
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice) where T : struct
		{
			return thisSlice;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000351C File Offset: 0x0000171C
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003538 File Offset: 0x00001738
		public static NativeSlice<T> Slice<T>(this NativeSlice<T> thisSlice, int start, int length) where T : struct
		{
			return new NativeSlice<T>(thisSlice, start, length);
		}
	}
}
