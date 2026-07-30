using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000047 RID: 71
	[NativeHeader("Runtime/Jobs/JobSystem.h")]
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public static class JobsUtility
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000029D4 File Offset: 0x00000BD4
		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* ptr = (int*)(void*)ranges.StartEndIndex;
			beginIndex = ptr[jobIndex * 2];
			endIndex = ptr[jobIndex * 2 + 1];
		}

		// Token: 0x060000A5 RID: 165
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		// Token: 0x060000A6 RID: 166 RVA: 0x00002A08 File Offset: 0x00000C08
		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle jobHandle;
			JobsUtility.Schedule_Injected(ref parameters, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002A20 File Offset: 0x00000C20
		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002A38 File Offset: 0x00000C38
		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002A54 File Offset: 0x00000C54
		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out jobHandle);
			return jobHandle;
		}

		// Token: 0x060000AA RID: 170
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[MethodImpl(4096)]
		public unsafe static extern void PatchBufferMinMaxRanges(IntPtr bufferRangePatchData, void* jobdata, int startIndex, int rangeSize);

		// Token: 0x060000AB RID: 171
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		// Token: 0x060000AC RID: 172 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static IntPtr CreateJobReflectionData(Type type, JobType jobType, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, jobType, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, jobType, managedJobFunction0, null, null);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AE RID: 174
		public static extern bool IsExecutingJob
		{
			[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AF RID: 175
		// (set) Token: 0x060000B0 RID: 176
		public static extern bool JobDebuggerEnabled
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
			[FreeFunction]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		public static extern bool JobCompilerEnabled
		{
			[FreeFunction]
			[MethodImpl(4096)]
			get;
			[FreeFunction]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000B3 RID: 179
		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		private static extern int GetJobQueueWorkerThreadCount();

		// Token: 0x060000B4 RID: 180
		[FreeFunction("JobSystem::ForceSetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		private static extern void SetJobQueueMaximumActiveThreadCount(int count);

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B5 RID: 181
		public static extern int JobWorkerMaximumCount
		{
			[FreeFunction("JobSystem::GetJobQueueMaximumThreadCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060000B6 RID: 182
		[FreeFunction("JobSystem::ResetJobQueueWorkerThreadCount")]
		[MethodImpl(4096)]
		public static extern void ResetJobWorkerCount();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002AAC File Offset: 0x00000CAC
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
			set
			{
				bool flag = value < 0 || value > JobsUtility.JobWorkerMaximumCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("JobWorkerCount", string.Format("Invalid JobWorkerCount {0} must be in the range 0 -> {1}", value, JobsUtility.JobWorkerMaximumCount));
				}
				JobsUtility.SetJobQueueMaximumActiveThreadCount(value);
			}
		}

		// Token: 0x060000B9 RID: 185
		[MethodImpl(4096)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		// Token: 0x060000BA RID: 186
		[MethodImpl(4096)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x060000BB RID: 187
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		// Token: 0x060000BC RID: 188
		[MethodImpl(4096)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		// Token: 0x040000E0 RID: 224
		public const int MaxJobThreadCount = 128;

		// Token: 0x040000E1 RID: 225
		public const int CacheLineSize = 64;

		// Token: 0x02000048 RID: 72
		public struct JobScheduleParameters
		{
			// Token: 0x060000BD RID: 189 RVA: 0x00002B12 File Offset: 0x00000D12
			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}

			// Token: 0x040000E2 RID: 226
			public JobHandle Dependency;

			// Token: 0x040000E3 RID: 227
			public int ScheduleMode;

			// Token: 0x040000E4 RID: 228
			public IntPtr ReflectionData;

			// Token: 0x040000E5 RID: 229
			public IntPtr JobDataPtr;
		}
	}
}
