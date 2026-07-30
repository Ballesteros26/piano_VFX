using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200005A RID: 90
	[JobProducerType(typeof(ParticleSystemParallelForBatchJobStruct<>))]
	public interface IJobParticleSystemParallelForBatch
	{
		// Token: 0x06000706 RID: 1798
		void Execute(ParticleSystemJobData jobData, int startIndex, int count);
	}
}
