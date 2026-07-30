using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000037 RID: 55
	[JobProducerType(typeof(IJobForExtensions.ForJobStruct<>))]
	public interface IJobFor
	{
		// Token: 0x06000077 RID: 119
		void Execute(int index);
	}
}
