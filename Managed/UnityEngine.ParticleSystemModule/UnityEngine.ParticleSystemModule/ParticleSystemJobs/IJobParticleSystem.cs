using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000058 RID: 88
	[JobProducerType(typeof(ParticleSystemJobStruct<>))]
	public interface IJobParticleSystem
	{
		// Token: 0x06000704 RID: 1796
		void Execute(ParticleSystemJobData jobData);
	}
}
