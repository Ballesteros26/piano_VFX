using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200003B RID: 59
	[JobProducerType(typeof(IJobParallelForExtensions.ParallelForJobStruct<>))]
	public interface IJobParallelFor
	{
		// Token: 0x06000081 RID: 129
		void Execute(int index);
	}
}
