using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000033 RID: 51
	[JobProducerType(typeof(IJobExtensions.JobStruct<>))]
	public interface IJob
	{
		// Token: 0x0600006E RID: 110
		void Execute();
	}
}
