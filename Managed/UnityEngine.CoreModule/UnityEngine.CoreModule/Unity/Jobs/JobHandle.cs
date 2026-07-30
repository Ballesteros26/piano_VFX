using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Jobs
{
	// Token: 0x0200003F RID: 63
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public struct JobHandle
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000275C File Offset: 0x0000095C
		public void Complete()
		{
			bool flag = this.jobGroup == IntPtr.Zero;
			if (!flag)
			{
				JobHandle.ScheduleBatchedJobsAndComplete(ref this);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002788 File Offset: 0x00000988
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle* ptr;
			checked
			{
				ptr = stackalloc JobHandle[unchecked((UIntPtr)2) * (UIntPtr)sizeof(JobHandle)];
				*ptr = job0;
			}
			ptr[1] = job1;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 2);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000027D8 File Offset: 0x000009D8
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle* ptr;
			checked
			{
				ptr = stackalloc JobHandle[unchecked((UIntPtr)3) * (UIntPtr)sizeof(JobHandle)];
				*ptr = job0;
			}
			ptr[1] = job1;
			ptr[2] = job2;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 3);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
			job2 = default(JobHandle);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002844 File Offset: 0x00000A44
		public static void CompleteAll(NativeArray<JobHandle> jobs)
		{
			JobHandle.ScheduleBatchedJobsAndCompleteAll(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000285C File Offset: 0x00000A5C
		public bool IsCompleted
		{
			get
			{
				return JobHandle.ScheduleBatchedJobsAndIsCompleted(ref this);
			}
		}

		// Token: 0x0600008F RID: 143
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		public static extern void ScheduleBatchedJobs();

		// Token: 0x06000090 RID: 144
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern void ScheduleBatchedJobsAndComplete(ref JobHandle job);

		// Token: 0x06000091 RID: 145
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private static extern bool ScheduleBatchedJobsAndIsCompleted(ref JobHandle job);

		// Token: 0x06000092 RID: 146
		[NativeMethod(IsFreeFunction = true)]
		[MethodImpl(4096)]
		private unsafe static extern void ScheduleBatchedJobsAndCompleteAll(void* jobs, int count);

		// Token: 0x06000093 RID: 147 RVA: 0x00002874 File Offset: 0x00000A74
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1)
		{
			return JobHandle.CombineDependenciesInternal2(ref job0, ref job1);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002890 File Offset: 0x00000A90
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1, JobHandle job2)
		{
			return JobHandle.CombineDependenciesInternal3(ref job0, ref job1, ref job2);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000028B0 File Offset: 0x00000AB0
		public static JobHandle CombineDependencies(NativeArray<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000028D4 File Offset: 0x00000AD4
		public static JobHandle CombineDependencies(NativeSlice<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000028F8 File Offset: 0x00000AF8
		[NativeMethod(IsFreeFunction = true)]
		private static JobHandle CombineDependenciesInternal2(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternal2_Injected(ref job0, ref job1, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002910 File Offset: 0x00000B10
		[NativeMethod(IsFreeFunction = true)]
		private static JobHandle CombineDependenciesInternal3(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternal3_Injected(ref job0, ref job1, ref job2, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002928 File Offset: 0x00000B28
		[NativeMethod(IsFreeFunction = true)]
		internal unsafe static JobHandle CombineDependenciesInternalPtr(void* jobs, int count)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternalPtr_Injected(jobs, count, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000293F File Offset: 0x00000B3F
		[NativeMethod(IsFreeFunction = true)]
		public static bool CheckFenceIsDependencyOrDidSyncFence(JobHandle jobHandle, JobHandle dependsOn)
		{
			return JobHandle.CheckFenceIsDependencyOrDidSyncFence_Injected(ref jobHandle, ref dependsOn);
		}

		// Token: 0x0600009B RID: 155
		[MethodImpl(4096)]
		private static extern void CombineDependenciesInternal2_Injected(ref JobHandle job0, ref JobHandle job1, out JobHandle ret);

		// Token: 0x0600009C RID: 156
		[MethodImpl(4096)]
		private static extern void CombineDependenciesInternal3_Injected(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2, out JobHandle ret);

		// Token: 0x0600009D RID: 157
		[MethodImpl(4096)]
		private unsafe static extern void CombineDependenciesInternalPtr_Injected(void* jobs, int count, out JobHandle ret);

		// Token: 0x0600009E RID: 158
		[MethodImpl(4096)]
		private static extern bool CheckFenceIsDependencyOrDidSyncFence_Injected(ref JobHandle jobHandle, ref JobHandle dependsOn);

		// Token: 0x040000CE RID: 206
		internal IntPtr jobGroup;

		// Token: 0x040000CF RID: 207
		internal int version;
	}
}
