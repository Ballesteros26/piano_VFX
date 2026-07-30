using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000041 RID: 65
	public struct BatchQueryJobStruct<T> where T : struct
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000295C File Offset: 0x00000B5C
		public static IntPtr Initialize()
		{
			bool flag = BatchQueryJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				BatchQueryJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, null, null, null);
			}
			return BatchQueryJobStruct<T>.jobReflectionData;
		}

		// Token: 0x040000D2 RID: 210
		internal static IntPtr jobReflectionData;
	}
}
