using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000398 RID: 920
	public interface IPlayableBehaviour
	{
		// Token: 0x06001FF5 RID: 8181
		[RequiredByNativeCode]
		void OnGraphStart(Playable playable);

		// Token: 0x06001FF6 RID: 8182
		[RequiredByNativeCode]
		void OnGraphStop(Playable playable);

		// Token: 0x06001FF7 RID: 8183
		[RequiredByNativeCode]
		void OnPlayableCreate(Playable playable);

		// Token: 0x06001FF8 RID: 8184
		[RequiredByNativeCode]
		void OnPlayableDestroy(Playable playable);

		// Token: 0x06001FF9 RID: 8185
		[RequiredByNativeCode]
		void OnBehaviourPlay(Playable playable, FrameData info);

		// Token: 0x06001FFA RID: 8186
		[RequiredByNativeCode]
		void OnBehaviourPause(Playable playable, FrameData info);

		// Token: 0x06001FFB RID: 8187
		[RequiredByNativeCode]
		void PrepareFrame(Playable playable, FrameData info);

		// Token: 0x06001FFC RID: 8188
		[RequiredByNativeCode]
		void ProcessFrame(Playable playable, FrameData info, object playerData);
	}
}
