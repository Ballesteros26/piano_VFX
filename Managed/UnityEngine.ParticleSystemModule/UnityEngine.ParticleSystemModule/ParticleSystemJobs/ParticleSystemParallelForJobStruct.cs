using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000065 RID: 101
	internal struct ParticleSystemParallelForJobStruct<T> where T : struct, IJobParticleSystemParallelFor
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x000067CC File Offset: 0x000049CC
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemParallelForJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemParallelForJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new ParticleSystemParallelForJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemParallelForJobStruct<T>.jobReflectionData;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000681C File Offset: 0x00004A1C
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
				for (int i = num; i < num2; i++)
				{
					data.Execute(particleSystemJobData, i);
				}
			}
		}

		// Token: 0x04000196 RID: 406
		public static IntPtr jobReflectionData;

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x06000728 RID: 1832
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
