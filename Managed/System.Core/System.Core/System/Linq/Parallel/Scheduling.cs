using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001F3 RID: 499
	internal static class Scheduling
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x0002A19C File Offset: 0x0002839C
		internal static int GetDefaultDegreeOfParallelism()
		{
			return Scheduling.DefaultDegreeOfParallelism;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002A1A4 File Offset: 0x000283A4
		internal static int GetDefaultChunkSize<T>()
		{
			int num;
			if (default(T) != null || Nullable.GetUnderlyingType(typeof(T)) != null)
			{
				num = 128;
			}
			else
			{
				num = 512 / IntPtr.Size;
			}
			return num;
		}

		// Token: 0x040007CE RID: 1998
		internal const bool DefaultPreserveOrder = false;

		// Token: 0x040007CF RID: 1999
		internal static int DefaultDegreeOfParallelism = Math.Min(Environment.ProcessorCount, 512);

		// Token: 0x040007D0 RID: 2000
		internal const int DEFAULT_BOUNDED_BUFFER_CAPACITY = 512;

		// Token: 0x040007D1 RID: 2001
		internal const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x040007D2 RID: 2002
		internal const int ZOMBIED_PRODUCER_TIMEOUT = -1;

		// Token: 0x040007D3 RID: 2003
		internal const int MAX_SUPPORTED_DOP = 512;
	}
}
