using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000031 RID: 49
	[Obsolete("For best performance use PlayableAsset and PlayableBehaviour.")]
	[Serializable]
	public class BasicPlayableBehaviour : ScriptableObject, IPlayableAsset, IPlayableBehaviour
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000859F File Offset: 0x0000679F
		public virtual double duration
		{
			get
			{
				return PlayableBinding.DefaultDuration;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008454 File Offset: 0x00006654
		public virtual IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000085A6 File Offset: 0x000067A6
		public virtual Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<BasicPlayableBehaviour>.Create(graph, this, 0);
		}
	}
}
