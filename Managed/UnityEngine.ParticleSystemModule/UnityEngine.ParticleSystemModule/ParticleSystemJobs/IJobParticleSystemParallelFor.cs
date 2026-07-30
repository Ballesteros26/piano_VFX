using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000059 RID: 89
	[JobProducerType(typeof(ParticleSystemParallelForJobStruct<>))]
	public interface IJobParticleSystemParallelFor
	{
		// Token: 0x06000705 RID: 1797
		void Execute(ParticleSystemJobData jobData, int index);
	}
}
