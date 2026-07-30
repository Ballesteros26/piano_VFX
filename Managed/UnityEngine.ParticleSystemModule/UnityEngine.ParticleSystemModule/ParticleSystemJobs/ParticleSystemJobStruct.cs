using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000063 RID: 99
	internal struct ParticleSystemJobStruct<T> where T : struct, IJobParticleSystem
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00006740 File Offset: 0x00004940
		public static IntPtr Initialize()
		{
			bool flag = ParticleSystemJobStruct<T>.jobReflectionData == IntPtr.Zero;
			if (flag)
			{
				ParticleSystemJobStruct<T>.jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.Single, new ParticleSystemJobStruct<T>.ExecuteJobFunction(ParticleSystemJobStruct<T>.Execute), null, null);
			}
			return ParticleSystemJobStruct<T>.jobReflectionData;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00006790 File Offset: 0x00004990
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			data.Execute(particleSystemJobData);
		}

		// Token: 0x04000195 RID: 405
		public static IntPtr jobReflectionData;

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000722 RID: 1826
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
