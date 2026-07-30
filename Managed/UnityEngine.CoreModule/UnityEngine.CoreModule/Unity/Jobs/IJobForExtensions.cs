using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000038 RID: 56
	public static class IJobForExtensions
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000250C File Offset: 0x0000070C
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(false), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002540 File Offset: 0x00000740
		public static JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(true), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002574 File Offset: 0x00000774
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.ForJobStruct<T>.Initialize(false), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x02000039 RID: 57
		internal struct ForJobStruct<T> where T : struct, IJobFor
		{
			// Token: 0x0600007B RID: 123 RVA: 0x000025AC File Offset: 0x000007AC
			public static IntPtr Initialize(bool asParallel)
			{
				bool flag = IJobForExtensions.ForJobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobForExtensions.ForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new IJobForExtensions.ForJobStruct<T>.ExecuteJobFunction(IJobForExtensions.ForJobStruct<T>.Execute), null, null);
				}
				return IJobForExtensions.ForJobStruct<T>.jobReflectionData;
			}

			// Token: 0x0600007C RID: 124 RVA: 0x000025FC File Offset: 0x000007FC
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

			// Token: 0x040000CC RID: 204
			public static IntPtr jobReflectionData;

			// Token: 0x0200003A RID: 58
			// (Invoke) Token: 0x0600007E RID: 126
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
