using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005B RID: 91
	public static class IParticleSystemJobExtensions
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x000062D8 File Offset: 0x000044D8
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystem
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemJobStruct<T>.Initialize());
			JobHandle jobHandle = ParticleSystem.ScheduleManagedJob(ref jobScheduleParameters, ps.GetManagedJobData());
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00006310 File Offset: 0x00004510
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemParallelForJobStruct<T>.Initialize());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, minIndicesPerJobCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000634C File Offset: 0x0000454C
		public static JobHandle ScheduleBatch<T>(this T jobData, ParticleSystem ps, int innerLoopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelForBatch
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = IParticleSystemJobExtensions.CreateScheduleParams<T>(ref jobData, ps, dependsOn, ParticleSystemParallelForBatchJobStruct<T>.Initialize());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, innerLoopBatchCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00006388 File Offset: 0x00004588
		private static JobsUtility.JobScheduleParameters CreateScheduleParams<T>(ref T jobData, ParticleSystem ps, JobHandle dependsOn, IntPtr jobReflectionData) where T : struct
		{
			dependsOn = JobHandle.CombineDependencies(ps.GetManagedJobHandle(), dependsOn);
			return new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), jobReflectionData, dependsOn, ScheduleMode.Batched);
		}
	}
}
