using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000067 RID: 103
	internal struct ParticleSystemParallelForBatchJobStruct<T> where T : struct, IJobParticleSystemParallelForBatch
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00006890 File Offset: 0x00004A90
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new ParticleSystemParallelForBatchJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForBatchJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000068E0 File Offset: 0x00004AE0
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			for (;;)
			{
				int num;
				int num2;
				bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
				if (flag)
				{
					break;
				}
				data.Execute(particleSystemJobData, num, num2 - num);
			}
		}

		// Token: 0x04000197 RID: 407
		public static IntPtr jobReflectionData;

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x0600072E RID: 1838
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
