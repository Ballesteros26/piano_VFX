using System;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200003F RID: 63
	[JobProducerType(typeof(ProcessAnimationJobStruct<>))]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJob
	{
		// Token: 0x0600029B RID: 667
		void ProcessAnimation(AnimationStream stream);

		// Token: 0x0600029C RID: 668
		void ProcessRootMotion(AnimationStream stream);
	}
}
