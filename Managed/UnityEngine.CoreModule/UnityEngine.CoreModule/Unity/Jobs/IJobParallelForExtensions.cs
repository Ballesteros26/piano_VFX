using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200003C RID: 60
	public static class IJobParallelForExtensions
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002650 File Offset: 0x00000850
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002680 File Offset: 0x00000880
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x0200003D RID: 61
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			// Token: 0x06000084 RID: 132 RVA: 0x000026B8 File Offset: 0x000008B8
			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new IJobParallelForExtensions.ParallelForJobStruct<T>.ExecuteJobFunction(IJobParallelForExtensions.ParallelForJobStruct<T>.Execute), null, null);
				}
				return IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData;
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00002708 File Offset: 0x00000908
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				for (;;)
				{
					int num;
					int num2;
					bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
					if (flag)
					{
						break;
					}
					int num3 = num2;
					for (int i = num; i < num3; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x040000CD RID: 205
			public static IntPtr jobReflectionData;

			// Token: 0x0200003E RID: 62
			// (Invoke) Token: 0x06000087 RID: 135
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
