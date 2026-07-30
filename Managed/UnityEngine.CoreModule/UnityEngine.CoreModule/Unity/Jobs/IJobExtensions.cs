using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000034 RID: 52
	public static class IJobExtensions
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00002448 File Offset: 0x00000648
		public static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002478 File Offset: 0x00000678
		public static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.JobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x02000035 RID: 53
		internal struct JobStruct<T> where T : struct, IJob
		{
			// Token: 0x06000071 RID: 113 RVA: 0x000024AC File Offset: 0x000006AC
			public static IntPtr Initialize()
			{
				bool flag = IJobExtensions.JobStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobExtensions.JobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.Single, new IJobExtensions.JobStruct<T>.ExecuteJobFunction(IJobExtensions.JobStruct<T>.Execute), null, null);
				}
				return IJobExtensions.JobStruct<T>.jobReflectionData;
			}

			// Token: 0x06000072 RID: 114 RVA: 0x000024F9 File Offset: 0x000006F9
			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}

			// Token: 0x040000CB RID: 203
			public static IntPtr jobReflectionData;

			// Token: 0x02000036 RID: 54
			// (Invoke) Token: 0x06000074 RID: 116
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
