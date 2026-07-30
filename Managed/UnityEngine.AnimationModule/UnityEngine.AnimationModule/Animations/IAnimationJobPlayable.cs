using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000040 RID: 64
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJobPlayable : IPlayable
	{
		// Token: 0x0600029D RID: 669
		T GetJobData<T>() where T : struct, IAnimationJob;

		// Token: 0x0600029E RID: 670
		void SetJobData<T>(T jobData) where T : struct, IAnimationJob;
	}
}
