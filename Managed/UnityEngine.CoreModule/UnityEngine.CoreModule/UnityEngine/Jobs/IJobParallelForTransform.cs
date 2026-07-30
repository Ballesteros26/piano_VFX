using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x0200021A RID: 538
	[JobProducerType(typeof(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<>))]
	public interface IJobParallelForTransform
	{
		// Token: 0x060017F4 RID: 6132
		void Execute(int index, TransformAccess transform);
	}
}
