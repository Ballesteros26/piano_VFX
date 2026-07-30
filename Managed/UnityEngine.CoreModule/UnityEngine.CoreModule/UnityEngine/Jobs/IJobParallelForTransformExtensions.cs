using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x0200021B RID: 539
	public static class IJobParallelForTransformExtensions
	{
		// Token: 0x060017F5 RID: 6133 RVA: 0x00026C20 File Offset: 0x00024E20
		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x0200021C RID: 540
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			// Token: 0x060017F6 RID: 6134 RVA: 0x00026C58 File Offset: 0x00024E58
			public static IntPtr Initialize()
			{
				bool flag = IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData == IntPtr.Zero;
				if (flag)
				{
					IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
				return IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData;
			}

			// Token: 0x060017F7 RID: 6135 RVA: 0x00026CA8 File Offset: 0x00024EA8
			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IntPtr intPtr;
				UnsafeUtility.CopyPtrToStructure<IntPtr>((void*)jobData2, out intPtr);
				int* ptr = (int*)(void*)TransformAccessArray.GetSortedToUserIndex(intPtr);
				TransformAccess* ptr2 = (TransformAccess*)(void*)TransformAccessArray.GetSortedTransformAccess(intPtr);
				int num;
				int num2;
				JobsUtility.GetJobRange(ref ranges, jobIndex, out num, out num2);
				for (int i = num; i < num2; i++)
				{
					int num3 = i;
					int num4 = ptr[num3];
					jobData.Execute(num4, ptr2[num3]);
				}
			}

			// Token: 0x0400075E RID: 1886
			public static IntPtr jobReflectionData;

			// Token: 0x0200021D RID: 541
			// (Invoke) Token: 0x060017F9 RID: 6137
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
