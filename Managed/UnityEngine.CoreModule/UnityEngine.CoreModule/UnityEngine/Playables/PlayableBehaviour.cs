using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200039F RID: 927
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		// Token: 0x06002011 RID: 8209 RVA: 0x000166AA File Offset: 0x000148AA
		public PlayableBehaviour()
		{
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00002EC3 File Offset: 0x000010C3
		[Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public virtual void OnBehaviourDelay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0003673C File Offset: 0x0003493C
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
